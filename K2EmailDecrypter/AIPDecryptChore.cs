using K2IManageObjects;
using K2Utilities;
using Microsoft.Win32;
using System;
using System.IO;
using System.Management.Automation;
using System.Text.RegularExpressions;

namespace K2EmailDecrypter
{
    class AIPDecryptChore : Chore<IMDocument>
    {
        private static readonly string tempPath;
        private static readonly string logFile;

        private readonly string targetFile;

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
                int lastOccurance = -1;
                while (lastLine.IndexOf("of ", lastOccurance + 1) != -1)
                {
                    lastOccurance = lastLine.IndexOf("of ", lastOccurance + 1);
                    string match = Regex.Match(lastLine.Substring(lastOccurance), countTemplate).Value;

                    Console.WriteLine("M: " + match);
                }
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
                File.WriteAllBytes(targetFile, workItem.DocumentContent);

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
                // Create the command
                PowerShell cmd = CreateCommand();

                // Make the synchronous call
                //cmd.Invoke();

                // If we got here read the logfile
                choreLog = File.ReadAllText($"{LogStem}.{Extension}");

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
                while (lastLine.IndexOf("of ") != -1)
                {
                    lastOccurance = lastLine.IndexOf("of ");
                    string match = Regex.Match(lastLine.Substring(lastOccurance), countTemplate).Value;

                    Console.WriteLine("M: " + match);
                }

                Key actionKey = Utilities.Instance.ReadUserKey(Registry.CurrentUser, "Action");
                if (actionKey != null)
                {
                    // Set item values based on any action key
                    Utilities.Instance.SetObjectProperties(workItem, actionKey);
                }

                CleanUp();

                return true;
            }
            catch (Exception ex)
            {
                log.Warn(ex);
                return false;
            }
        }

        protected override void CleanUp()
        {
            // Remove the logfiles
            try
            {
                File.Delete(targetFile);
                File.Delete($"{LogStem}.{Extension}");
                File.Delete($"{LogStem}-debug.{Extension}");
                File.Delete($"{LogStem}-failure.{Extension}");
            }
            catch (Exception ex)
            {
                log.Warn(ex);
            }

        }

        private PowerShell CreateCommand()
        {
            return PowerShell.Create()
                .AddCommand(Cmdlet)
                .AddParameter("File", targetFile)
                .AddParameter("InPlace")
                .AddParameter("LogFile", logFile);
        }
    }
}
