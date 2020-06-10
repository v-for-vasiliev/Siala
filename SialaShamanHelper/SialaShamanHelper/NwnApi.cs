using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using WindowsInput;
using System.Threading;

namespace ShamanHelper
{
    class NwnApi
    {
        enum ShowWindowCommands
        {
            /// <summary>
            /// Hides the window and activates another window.
            /// </summary>
            Hide = 0,
            /// <summary>
            /// Activates and displays a window. If the window is minimized or 
            /// maximized, the system restores it to its original size and position.
            /// An application should specify this flag when displaying the window 
            /// for the first time.
            /// </summary>
            Normal = 1,
            /// <summary>
            /// Activates the window and displays it as a minimized window.
            /// </summary>
            ShowMinimized = 2,
            /// <summary>
            /// Maximizes the specified window.
            /// </summary>
            Maximize = 3, // is this the right value?
                          /// <summary>
                          /// Activates the window and displays it as a maximized window.
                          /// </summary>       
            ShowMaximized = 3,
            /// <summary>
            /// Displays a window in its most recent size and position. This value 
            /// is similar to <see cref="Win32.ShowWindowCommand.Normal"/>, except 
            /// the window is not activated.
            /// </summary>
            ShowNoActivate = 4,
            /// <summary>
            /// Activates the window and displays it in its current size and position. 
            /// </summary>
            Show = 5,
            /// <summary>
            /// Minimizes the specified window and activates the next top-level 
            /// window in the Z order.
            /// </summary>
            Minimize = 6,
            /// <summary>
            /// Displays the window as a minimized window. This value is similar to
            /// <see cref="Win32.ShowWindowCommand.ShowMinimized"/>, except the 
            /// window is not activated.
            /// </summary>
            ShowMinNoActive = 7,
            /// <summary>
            /// Displays the window in its current size and position. This value is 
            /// similar to <see cref="Win32.ShowWindowCommand.Show"/>, except the 
            /// window is not activated.
            /// </summary>
            ShowNA = 8,
            /// <summary>
            /// Activates and displays the window. If the window is minimized or 
            /// maximized, the system restores it to its original size and position. 
            /// An application should specify this flag when restoring a minimized window.
            /// </summary>
            Restore = 9,
            /// <summary>
            /// Sets the show state based on the SW_* value specified in the 
            /// STARTUPINFO structure passed to the CreateProcess function by the 
            /// program that started the application.
            /// </summary>
            ShowDefault = 10,
            /// <summary>
            ///  <b>Windows 2000/XP:</b> Minimizes a window, even if the thread 
            /// that owns the window is not responding. This flag should only be 
            /// used when minimizing windows from a different thread.
            /// </summary>
            ForceMinimize = 11
        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, ShowWindowCommands nCmdShow);

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CallingConvention = CallingConvention.Winapi)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

        [DllImport("user32.dll")]
        static extern IntPtr AttachThreadInput(IntPtr idAttach, IntPtr idAttachTo, bool fAttach);

        [DllImport("Kernel32.dll")]
        public static extern int GetCurrentThreadId();

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        // Delegate to filter Neverwinter Nights windows
        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        private const string NWN_WINDOW_CAPTION_FRAGMENT = "Neverwinter";

        private const int GWL_STYLE = (-16);
        private const UInt32 WS_VISIBLE = 0x10000000;

        public const Int32 WM_CHAR = 0x0102;
        public const Int32 WM_PASTE = 0x0302;

        private List<IntPtr> nwnWindows = new List<IntPtr>(0);

        private string GetWindowText(IntPtr Handle)
        {
            int size = GetWindowTextLength(Handle);
            if (size > 0)
            {
                var builder = new StringBuilder(size + 1);
                GetWindowText(Handle, builder, builder.Capacity);
                return builder.ToString();
            }
            return String.Empty;
        }

        private bool IsVisibleWindow(IntPtr Handle)
        {
            return (GetWindowLong(Handle, GWL_STYLE) & WS_VISIBLE) != 0;
        }

        private bool EnumNwnWindow(IntPtr Handle, IntPtr lParam)
        {
            if (GetWindowText(Handle).Contains(NWN_WINDOW_CAPTION_FRAGMENT) && IsVisibleWindow(Handle))
            {
                nwnWindows.Add(Handle);
            }
            return true;
        }

        public bool FindNwnWindows()
        {
            EnumWindowsProc nwnProc = new EnumWindowsProc(EnumNwnWindow);
            EnumWindows(nwnProc, IntPtr.Zero);
            return (nwnWindows.Count > 0);
        }

        public List<IntPtr> NwnWindows
        {
            get { return nwnWindows; }
        }

        public void WhisperToChat(string message)
        {
            
            FindNwnWindows();
            if (nwnWindows.Count > 0)
            {
                IntPtr nwnWindowHandle = nwnWindows[0];
                IntPtr nwnThreadId = GetWindowThreadProcessId(nwnWindowHandle, IntPtr.Zero);

                AttachThreadInput(new IntPtr(GetCurrentThreadId()), nwnThreadId, true);

                ShowWindow(nwnWindowHandle, ShowWindowCommands.Restore);
                SetForegroundWindow(nwnWindowHandle);

                AttachThreadInput(new IntPtr(GetCurrentThreadId()), nwnThreadId, false);
                Thread.Sleep(150);

                InputSimulator.SimulateKeyPress(VirtualKeyCode.RETURN);
                Thread.Sleep(150);

                // Workaround
                // Application.OpenForms[0].Invoke(new Action(() => Clipboard.SetText("/w " + message, TextDataFormat.Text)));
                copyTextToClipboard("/w " + message);

                InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V);
                InputSimulator.SimulateKeyPress(VirtualKeyCode.RETURN);
                Thread.Sleep(150);
            }
        }

        /// <summary>
        /// Since clipboard can be accessen only from UI thread this method must switch it to STAThread.
        /// </summary>
        /// <param name="text">Clipboard text</param>
        private void copyTextToClipboard(string text)
        {
            Thread thread = new Thread(() => Clipboard.SetText(text, TextDataFormat.Text));
            thread.SetApartmentState(ApartmentState.STA); // Set the thread to STA
            thread.Start();
            thread.Join();
        }
    }
}
