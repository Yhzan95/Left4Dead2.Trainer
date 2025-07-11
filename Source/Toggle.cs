using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Left4DeadTrainer
{
    public static class Toggle
    {
        //WinProc Hook and hotkeys
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        private const int HOTKEY_ID = 9000;
        private const int WM_HOTKEY = 0x0312;
        private const int MOD_NONE = 0x0000;
        private const int MOD_NOREPEAT = 0x4000;
        private const int VK_F1 = 0x70;
        private static IntPtr handle;
        private static Action toggleCallback;
        public static void Initialize(IntPtr windowHandle, Action callback)
        {
            handle = windowHandle;
            toggleCallback = callback;
            if (!RegisterHotKey(handle,
                HOTKEY_ID,
                MOD_NONE | MOD_NOREPEAT, VK_F1))
            {
                MessageBox.Show("Unable to save F1 as shortcut.");
            }}
        public static void Dispose()
        {
            UnregisterHotKey(handle, HOTKEY_ID);}
        public static bool ProcessHotKey(Message m)
        {
            if (m.Msg == WM_HOTKEY &&
                m.WParam.ToInt32() == HOTKEY_ID)
            {
                toggleCallback?.Invoke();
                return true;
            }
            return false;
        }}}
