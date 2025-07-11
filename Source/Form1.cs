//  Simple Trainer Left 4 Dead 2 
// @ https://github.com/Yhzan95 
// Credits: Minhook, UC

using System;
using System.Diagnostics;
using System.Windows.Forms;
using Drakula_Csharp;

namespace Left4DeadTrainer
{
    public partial class Form1 : Form
    {
        private bool isVisible = false;
        private HotkeyListener hotkeyListener;
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            hotkeyListener.Dispose();
            base.OnFormClosing(e);
        }
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Opacity = 0;
            this.ShowInTaskbar = false;
            this.Hide();

            hotkeyListener = new HotkeyListener(ToggleMenu);
            DrakulaTheme.ApplyDrakula(this);

            if (Memory.GetGameProcess() == null)
            {
                MessageBox.Show("Left 4 Dead 2 is not launched.");
                Application.Exit();
            }
        }
        private void ToggleMenu()
        {
            isVisible = !isVisible;
            if (isVisible)
            {
                this.Show();
                this.ShowInTaskbar = true;
                this.Opacity = 1;
                this.TopMost = true;
                this.Activate();
            }
            else
            {
                this.Opacity = 0;
                this.ShowInTaskbar = false;
                this.Hide();
            }
        }
        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x84;
            const int HTCLIENT = 1;
            const int HTCAPTION = 2;

            if (m.Msg == WM_NCHITTEST)
            {
                base.WndProc(ref m);
                if ((int)m.Result == HTCLIENT)
                {
                    m.Result = (IntPtr)HTCAPTION;
                    return;
                }
                return;
            }

            if (Toggle.ProcessHotKey(m))
                return;

            base.WndProc(ref m);
        }
        // Checkbox
        private void checkBox1_CheckedChanged(object sender, EventArgs e) => Memory.ToggleMemory("client.dll", 0x7227E8, 2, 1, checkBox1.Checked);
        private void checkBox2_CheckedChanged(object sender, EventArgs e) => Memory.ToggleMemory("engine.dll", 0x605478, 1, 0, checkBox2.Checked);
        private void checkBox3_CheckedChanged(object sender, EventArgs e) => Memory.ToggleMemory("engine.dll", 0x629E20, 2, 1, checkBox3.Checked);
        private void checkBox4_CheckedChanged(object sender, EventArgs e) => Memory.ToggleMemory("engine.dll", 0x603468, 2, 1, checkBox4.Checked);
        private void checkBox5_CheckedChanged(object sender, EventArgs e) => Memory.ToggleMemory("engine.dll", 0x6B67C0, 2, 3, checkBox5.Checked);
        private void checkBox6_CheckedChanged(object sender, EventArgs e) => Memory.ToggleMemory("client.dll", 0x754DB8, 3, 0, checkBox6.Checked);
        private void checkBox7_CheckedChanged(object sender, EventArgs e) => Memory.ToggleMemory("client.dll", 0x721130, 1, 0, checkBox7.Checked);
        private void checkBox8_CheckedChanged(object sender, EventArgs e) => Godmode.Toggle(Memory.GetGameProcess(), checkBox8.Checked);
        private void checkBox9_CheckedChanged(object sender, EventArgs e) => Memory.ToggleMemory("client.dll", 0x7225B8, 1, 0, checkBox9.Checked);

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            int value = checkBox10.Checked ? 2 : 0;
            Memory.TogglePointerMemory("materialsystem.dll", 0xFE920, new int[] { 0x14, 0xF34 }, value);
            Memory.TogglePointerMemory("vstdlib.dll", 0x2C194, new int[] { 0x0, 0x270 }, value);
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e) => Memory.ToggleMemory("materialsystem.dll", 0xFF3B0, 1, 0, checkBox11.Checked);
        private void checkBox12_CheckedChanged(object sender, EventArgs e) => Memory.ToggleMemory("client.dll", 0x7810A8, 1, 0, checkBox12.Checked);
        private void checkBox13_CheckedChanged(object sender, EventArgs e) => Memory.ToggleMemory("engine.dll", 0x603978, 1, 0, checkBox13.Checked);
        private void checkBox14_CheckedChanged(object sender, EventArgs e) => Memory.ToggleMemory("engine.dll", 0x601840, 1, 0, checkBox14.Checked);
        private void checkBox15_CheckedChanged(object sender, EventArgs e) => Memory.ToggleMemory("client.dll", 0x724AB0, 1, 0, checkBox15.Checked);
        private void checkBox16_CheckedChanged(object sender, EventArgs e) => Memory.ToggleMemory("engine.dll", 0x6BF8B8, 1, 0, checkBox16.Checked);
        private void checkBox17_CheckedChanged(object sender, EventArgs e) => Memory.ToggleMemory("engine.dll", 0x603978, 1, 0, checkBox17.Checked);

        private void label1_Click(object sender, EventArgs e)
        {
            isVisible = false;
            this.Opacity = 0;
            this.ShowInTaskbar = false;
            this.Hide();
        }
    }
}
