using K2IManageObjects;
using K2Utilities;
using System;
using System.IO;
using System.Management.Automation;

namespace K2EmailDecrypter
{
    class AIPDecryptChore : Chore<IMDocument>
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("AIPDecryptChore");
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

        public override bool Execute()
        {
            try
            {   
                if (PreCondition())
                {
                    PowerShell cmd = CreateCommand();
                    ChoreLog = File.ReadAllText($"{LogStem}.{Extension}");

                    return PostCondition();
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
        }

        protected override bool PostCondition()
        {
            try
            {
                CleanUp();

                return true;
            }
            catch (Exception ex)
            {
                log.Warn(ex);
                return false;
            }
        }

        private void CleanUp()
        {
            // Remove the logfiles
            try
            {
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
