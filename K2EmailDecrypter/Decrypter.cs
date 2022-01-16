using K2IManageObjects;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace K2EmailDecrypter
{
    public class Decrypter
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly BlockingCollection<IMDocument> queue;

        private readonly CancellationTokenSource receivingCts = new CancellationTokenSource();

        public Decrypter()
        {
            log.Debug("Starting decryter object");
            queue = new BlockingCollection<IMDocument>();

            log.Debug("Creating decryption thread");
            Thread thread = new Thread(Start);
            log.Debug("Starting decryption thread");
            thread.Start();

            log.Debug("Decryter object created");
        }

        public void Start()
        {
            try
            {
                while (!receivingCts.IsCancellationRequested)
                {
                    // Block until document is added to the queue or cancellation
                    IMDocument item = queue.Take(receivingCts.Token);

                    log.Info($"Removing {item.Id} from queue");
                    Process(item);
                }
            }
            catch (OperationCanceledException)
            {

            }
        }
        public void Decrypt(IMDocument item)
        {
            log.Info($"Adding {item.Id} to queue");
            queue.Add(item);
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

        // Stop the processing thread
        public void CancelReceivingThread()
        {
            receivingCts.Cancel();
        }
    }
}
