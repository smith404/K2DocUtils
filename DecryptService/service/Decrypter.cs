using DecryptService.model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DecryptService.service
{
    public class Decrypter
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly BlockingCollection<FileResponse> queue;

        private readonly CancellationTokenSource receivingCts = new CancellationTokenSource();

        public Decrypter()
        {
            log.Debug("Starting decryter object");
            queue = new BlockingCollection<FileResponse>();

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
                    FileResponse item = queue.Take(receivingCts.Token);

                    log.Info($"Removing {item.FilePath} from queue");
                    Process(item);
                }
            }
            catch (OperationCanceledException)
            {

            }
        }
        public void Decrypt(FileResponse item)
        {
            log.Info($"Adding {item.FilePath} to queue");
            queue.Add(item);
        }

        public void Process(FileResponse item)
        {
            using (log4net.NDC.Push(item.FilePath))
            {
                log.Info($"Starting to process {item.FilePath}");

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
