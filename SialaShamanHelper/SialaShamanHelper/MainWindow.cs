using System;
using System.Windows.Forms;

namespace ShamanHelper
{
    public partial class MainWindow : Form
    {
        NwnEventsObserver nwnEventsObserver = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            /*
            nwnApi = new NwnApi();

            knownSpells = KnownSpells.Instance;
            knownSpells.Load();

            Hotkey hk = new Hotkey();
            hk.KeyCode = Keys.Back;
            hk.Alt = true;
            hk.Control = true;
            hk.Pressed += delegate
            {
                nwnApi.PrepareProfileSpells();
                NwnEventsObserver nwnEventsObserver = NwnEventsObserver.Instance;
                if (!nwnEventsObserver.Running)
                {
                    nwnEventsObserver.Start();
                }
            };
            hk.Register(this);
            */
            nwnEventsObserver = NwnEventsObserver.Instance;
            if (!nwnEventsObserver.Running)
            {
                nwnEventsObserver.Start();
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (nwnEventsObserver != null)
            {
                NwnEventsObserver.Instance.Stop();
            }
            Application.Exit();
        }
    }
}
