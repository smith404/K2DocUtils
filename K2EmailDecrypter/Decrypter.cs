using K2IManageObjects;
using System;
using System.Collections.Concurrent;
using System.Text;
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

        public byte[] Decrypt(String request)
        {
            log.Debug(request);

            if (request.StartsWith("GET"))
            {
                // Read connection data
                int pFrom = request.IndexOf("GET") + 5;
                int pTo = request.IndexOf("HTTP/");

                string result = request.Substring(pFrom, pTo - pFrom);


                String resHeader = "HTTP/1.1 200 Email Decrypt\nServer: localhost\nContent-Type: application/json\n\n";
                String resBody = "{ \"filepath\" : \"" + result + "\" } ";

                String resStr = resHeader + resBody;

                return Encoding.ASCII.GetBytes(resStr);
            }
            else
            {
                return Unsupported();
            }

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

        private byte[] Unsupported()
        {
            String resHeader = "HTTP/1.1 405 Method Not Allowed\nServer: localhost\nContent-Type: text/html; charset=UTF-8\n\n";
            String resBody = "";

            String resStr = resHeader + resBody;

            return Encoding.ASCII.GetBytes(resStr);

        }
    }
}
