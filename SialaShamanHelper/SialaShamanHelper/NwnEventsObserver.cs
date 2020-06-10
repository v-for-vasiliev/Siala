using System;
using System.IO;
using System.Text;
using System.Threading;

namespace ShamanHelper
{
    class NwnEventsObserver
    {
        private Thread workThread;
        private NwnEventsProcessor eventsProcessor;
        private bool isRunning;

        private static NwnEventsObserver instance;

        private NwnEventsObserver()
        {
            isRunning = false;
            eventsProcessor = NwnEventsProcessor.Instance;
        }

        public static NwnEventsObserver Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NwnEventsObserver();
                }
                return instance;
            }
        }

        private void Observe()
        {
            //string logPath = @"c:\Users\Кирилл\Games\NWN\Siala_1\logs\nwclientLog1.txt";
            string logPath = Path.Combine(Environment.CurrentDirectory, @"..\logs\", "nwclientLog1.txt");
            using (var fs = new FileStream(logPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                fs.Seek(0, SeekOrigin.End);
                using (var sr = new StreamReader(fs, Encoding.GetEncoding("windows-1251")))
                {
                    string nwnEvent = null;
                    while (true)
                    {
                        nwnEvent = sr.ReadLine();
                        if (!String.IsNullOrWhiteSpace(nwnEvent))
                        {
                            if (eventsProcessor.ProcessEvent(nwnEvent)) { }
                            else
                            {
                                Console.WriteLine("UNKNOWN_EVENT: " + nwnEvent);
                            }
                        }
                        else
                        {
                            // Sleep for a while if no new events detected
                            Thread.Sleep(500);
                        }
                    }
                }
            }
        }

        public void Start()
        {
            isRunning = true;
            workThread = new Thread(new ThreadStart(Observe));
            workThread.Start();
            while (!workThread.IsAlive) ; // Wait until thread start
            Console.WriteLine("Spell observer launched");
        }

        public void Stop()
        {
            workThread.Abort();
            workThread.Join();
            isRunning = false;
            Console.WriteLine("Spell observer stopped");
        }

        public bool Running
        {
            get { return isRunning; }
            set { isRunning = value; }
        }
    }
}
