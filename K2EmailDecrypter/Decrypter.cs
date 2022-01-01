using K2IManageObjects;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace K2EmailDecrypter
{
    public class Decrypter
    {
        private BlockingCollection<IMDBObject> queue;

        private CancellationTokenSource receivingCts = new CancellationTokenSource();

        public Decrypter()
        {
            queue = new BlockingCollection<IMDBObject>();
        }

        public void Start()
        {
            try
            {
                while (!receivingCts.IsCancellationRequested)
                {
                    // Block until document is added to the queue or cancellation
                    IMDBObject item = queue.Take(receivingCts.Token);

                    Decrypt(item);
                }
            }
            catch (OperationCanceledException)
            {

            }
        }
        public void Decrypt(IMDBObject item)
        {
            queue.Add(item);
        }

        // Stop the processing thread
        public void CancelReceivingThread()
        {
            receivingCts.Cancel();
        }
    }
}
