using System;
using System.Media;
using System.Windows.Forms;

namespace ShamanHelper
{
    public partial class MainWindow : Form
    {
        private static MainWindow mainWindow = null;
        private NwnEventsObserver nwnEventsObserver = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            mainWindow = this;
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
            this.notifyIcon1.Visible = false;
            mainWindow = null;
            Application.Exit();
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        public static void DisplayNotification(string message, int duration)
        {
            if (mainWindow != null)
            {
                using (var soundPlayer = new SoundPlayer(SialaShamanHelper.Properties.Resources.notification))
                {
                    soundPlayer.Play();
                }
                mainWindow.notifyIcon1.BalloonTipTitle = "Siala Shaman Helper";
                mainWindow.notifyIcon1.BalloonTipText = message;
                mainWindow.notifyIcon1.ShowBalloonTip(duration);
            }
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }
    }
}
