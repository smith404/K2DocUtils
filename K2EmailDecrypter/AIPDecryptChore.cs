using K2IManageObjects;
using K2IMInterface;
using K2Utilities;
using Microsoft.Win32;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace K2EmailDecrypter
{
    class AIPDecryptChore : Chore<IMDocument>
    {
        private static readonly string tempPath;
        private static readonly string logFile;

        private readonly string targetFile;

        private const string SucessString = "of 1 of 1 of 1 ";
        private const string LogStem = "decryptlog";
        private const string Extension = "txt";
        private const string Cmdlet = "Unprotect-RMSFile";

        static AIPDecryptChore()
        {
            // Get a temporary directory path
            tempPath = $"{Path.GetTempPath()}{Path.DirectorySeparatorChar}API";
            logFile = $"{AIPDecryptChore.tempPath}{Path.DirectorySeparatorChar}{AIPDecryptChore.LogStem}.{AIPDecryptChore.Extension}";

            // Create the directory if it doesn't already exist
            _ = Directory.CreateDirectory(tempPath);
        }

        public AIPDecryptChore(IMDocument item) : base(item)
        {
            targetFile = $"{AIPDecryptChore.tempPath}{Path.DirectorySeparatorChar}{item.Name}.{item.Extension}";
        }

        public void Test(string lastLine)
        {
            string countTemplate = @"of \d+";

            try
            {
                int lastOccurance = 0;
                string result = "";
                while (lastLine.IndexOf("of ", lastOccurance) != -1)
                {
                    lastOccurance = lastLine.IndexOf("of ", lastOccurance);
                    string match = Regex.Match(lastLine.Substring(lastOccurance), countTemplate).Value;
                    result = $"{result}{match} ";
                    lastOccurance += 3;
                }
                Console.WriteLine("---------------");
                Console.WriteLine($"result: [{result}]");
                Console.WriteLine("===============");
                CleanUp();
            }
            catch (Exception ex)
            {
                log.Warn(ex);
            }
        }

        protected override bool PreCondition()
        {
            try
            {
                // Create the file
                workItem.PersistFile(targetFile, false);

                return true;
            }
            catch (Exception ex)
            {
                log.Warn(ex);
                return false;
            }
        }

        public override bool Process()
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                log.Warn(ex);
                return false;
            }
        }

        protected override bool PostCondition()
        {
            string countTemplate = @"of d+";

            try
            {
                string[] lines = Regex.Split(choreLog, "\r\n|\r|\n");
                string lastLine = lines[lines.Length - 1];

                int lastOccurance = -1;
                string result = "";
                while (lastLine.IndexOf("of ", lastOccurance) != -1)
                {
                    lastOccurance = lastLine.IndexOf("of ", lastOccurance);
                    string match = Regex.Match(lastLine.Substring(lastOccurance), countTemplate).Value;
                    result = $"{result}{match} ";
                    lastOccurance += 3;
                }
                Console.WriteLine($"Result: [{result}]");

                // Check for positive result
                if (result.Equals(SucessString))
                {
                    Key actionKey = Utilities.Instance.ReadUserKey(Registry.CurrentUser, "Action");
                    if (actionKey != null)
                    {
                        // Set item values based on any action key
                        Utilities.Instance.SetObjectProperties(workItem, actionKey);
                    }

                    // Reload the decrpyted file
                    workItem.ReadFile(targetFile, false);

                    // Create new version of document
                    workItem.NewVersion(IMConnection.Instance);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                log.Warn(ex);
                return false;
            }
            finally
            {
                CleanUp();
            }
        }

        protected override void CleanUp()
        {
            // Remove the logfiles
            try
            {
                log.Debug($"Removing the target file: {targetFile}");
                File.Delete(targetFile);
                log.Debug($"Removing the log file: {AIPDecryptChore.tempPath}{Path.DirectorySeparatorChar}{LogStem}.{Extension}");
                File.Delete($"{AIPDecryptChore.tempPath}{Path.DirectorySeparatorChar}{LogStem}.{Extension}");
                log.Debug($"Removing the debug-log file: {AIPDecryptChore.tempPath}{Path.DirectorySeparatorChar}{LogStem}-debug.{Extension}");
                File.Delete($"{AIPDecryptChore.tempPath}{Path.DirectorySeparatorChar}{LogStem}-debug.{Extension}");
                log.Debug($"Removing the failure-log file: {AIPDecryptChore.tempPath}{Path.DirectorySeparatorChar}{LogStem}-failure.{Extension}");
                File.Delete($"{AIPDecryptChore.tempPath}{Path.DirectorySeparatorChar}{LogStem}-failure.{Extension}");
            }
            catch (Exception ex)
            {
                log.Warn(ex);
            }

        }
    }
}
