using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Left4DeadTrainer
{
    // WinProc hotkey listener
    public class HotkeyListener : NativeWindow, IDisposable
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        private const int HOTKEY_ID = 9000;
        private const int WM_HOTKEY = 0x0312;
        private const int VK_F1 = 0x70;
        private Action onHotkey;
        public HotkeyListener(Action callback)
        {
            onHotkey = callback;
            CreateHandle(new CreateParams());
            RegisterHotKey(this.Handle, HOTKEY_ID, 0, VK_F1);}
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == HOTKEY_ID)
            {
                onHotkey?.Invoke();}
            base.WndProc(ref m);}
        public void Dispose()
        {
            UnregisterHotKey(this.Handle, HOTKEY_ID);
            this.DestroyHandle();
        }}}
