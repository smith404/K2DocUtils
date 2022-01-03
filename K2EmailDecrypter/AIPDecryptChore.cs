using K2IManageObjects;
using K2Utilities;
using System;
using System.IO;

namespace K2EmailDecrypter
{
    class AIPDecryptChore : Chore<IMDBObject>
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("AIPDecryptChore");

        private const string logStem = "decryptlog";
        private const string extension = "txt";

        private static readonly string tempPath;
        static AIPDecryptChore()
        {
            // Get a temporary directory path
            tempPath = $"{Path.GetTempPath()}{Path.DirectorySeparatorChar}API";

            // Create the directory if it doesn't already exist
            _ = Directory.CreateDirectory(tempPath);
        }

        public AIPDecryptChore(IMDBObject item) : base(item)
        {
        }

        public override bool Execute()
        {
            CleanUp();

            return true;
        }

        public override string Log()
        {
            throw new NotImplementedException();
        }

        private void CleanUp()
        {
            // Remove the logfiles
            try
            {
                File.Delete($"{logStem}.{extension}");
                File.Delete($"{logStem}-debug.{extension}");
                File.Delete($"{logStem}-failure.{extension}");
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

        }
    }
}
