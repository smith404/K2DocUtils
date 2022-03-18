using K2IManageObjects;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace K2EmailDecrypter
{
    public class Decrypter
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Decrypter()
        {
            log.Debug("Decryter object created");
        }

        public void Decrypt(String request)
        {
            log.Debug(request);
        }

        public void Process(IMDocument item)
        {
            using (log4net.NDC.Push(item.Id))
            {
                log.Info($"Starting to process {item.Id}");

                // Load the document

                // Create the decryption job
                AIPDecryptChore c = new AIPDecryptChore(item);

                Console.WriteLine("+++++++++++++++++");
                c.Test("Completed UnProtection after '0:00:07.3948731', successfully completed processing of 1 of 1 items, failed processing 0 of 1, DateTime : 2022-01-03T08:25:14.8028592+01:00");
                Console.WriteLine("/////////////////");
            }
        }
    }
}
