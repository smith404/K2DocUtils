using K2IManageObjects;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace K2EmailDecrypter
{
    public class Decrypter
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("Decrypter");

        private BlockingCollection<IMDBObject> queue;

        private CancellationTokenSource receivingCts = new CancellationTokenSource();

        public Decrypter()
        {
            log.Debug("Starting decryter object");
            queue = new BlockingCollection<IMDBObject>();

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
                    IMDBObject item = queue.Take(receivingCts.Token);

                    log.Info(string.Format("Removing {0} from queue", item.Id));
                    Process(item);
                }
            }
            catch (OperationCanceledException)
            {

            }
        }
        public void Decrypt(IMDBObject item)
        {
            log.Info(string.Format("Adding {0} to queue", item.Id));
            queue.Add(item);
        }

        public void Process(IMDBObject item)
        {
            log.Info(string.Format("Starting to process {0}", item.Id));
        }

        // Stop the processing thread
        public void CancelReceivingThread()
        {
            receivingCts.Cancel();
        }
    }
}
