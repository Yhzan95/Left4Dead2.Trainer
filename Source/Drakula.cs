// Drakula Csharp theme based By Yhzan
// www.github.com/yhzan95/
// Credits/Thanks to:
// www.draculatheme.com/contribute

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Drakula_Csharp
{
    public static class DrakulaTheme
    {
        // Drakula Color Palette
        // @https://draculatheme.com/contribute

        private const string BackgroundColor = "#282A36";
        private const string CurrentLineColor = "#44475A";
        private const string ForegroundColor = "#F8F8F2";
        private const string CommentColor = "#6272A4";
        private const string CyanColor = "#8BE9FD";
        private const string PinkColor = "#FF79C6";
        private const string GreenColor = "#50FA7B";
        private const string OrangeColor = "#FFB86C";
        private const string PurpleColor = "#BD93F9";
        private const string RedColor = "#FF5555";
        private const string YellowColor = "#F1FA8C";

        // Apply theme to a form
        public static void ApplyDrakula(Form form)
        {
            form.BackColor = ColorTranslator.FromHtml(BackgroundColor);
            ApplyDrakulaControls(form.Controls);
        }

        //Apply Drakula theme to datagrid
        public static void ApplyDrakulaDataGridView(DataGridView dataGridView)
        {
            dataGridView.BackgroundColor = ColorTranslator.FromHtml(BackgroundColor);
            dataGridView.ForeColor = ColorTranslator.FromHtml(ForegroundColor);
            dataGridView.GridColor = ColorTranslator.FromHtml(CurrentLineColor);
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml(CommentColor);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = ColorTranslator.FromHtml(ForegroundColor);
            dataGridView.DefaultCellStyle.BackColor = ColorTranslator.FromHtml(CurrentLineColor);
            dataGridView.DefaultCellStyle.ForeColor = ColorTranslator.FromHtml(ForegroundColor);
            dataGridView.EnableHeadersVisualStyles = false;
        }


        // Apply Drakula theme to TabControl
        private static void ApplyDrakulaTabControl(TabControl tabControl)
        {
            tabControl.BackColor = ColorTranslator.FromHtml(BackgroundColor);
            tabControl.ForeColor = ColorTranslator.FromHtml(ForegroundColor);
            foreach (TabPage tabPage in tabControl.TabPages)
            {
                tabPage.BackColor = ColorTranslator.FromHtml(CurrentLineColor);
                tabPage.ForeColor = ColorTranslator.FromHtml(ForegroundColor);
            }
        }

        // Apply Drakula theme to all controls 
        private static void ApplyDrakulaControls(Control.ControlCollection controls){
            foreach (Control control in controls)
            {
                switch (control)
                {
                    case LinkLabel linkLabel:
                        linkLabel.LinkColor = ColorTranslator.FromHtml(PurpleColor);
                        linkLabel.ActiveLinkColor = ColorTranslator.FromHtml(PinkColor);
                        break;
                    case Label label:
                        label.ForeColor = ColorTranslator.FromHtml(CyanColor);
                        break;
                    case Button button:
                        button.BackColor = ColorTranslator.FromHtml(PinkColor);
                        button.ForeColor = ColorTranslator.FromHtml(ForegroundColor);
                        button.FlatStyle = FlatStyle.Flat;
                        if (button.Tag?.ToString() == "Warning")
                            button.BackColor = ColorTranslator.FromHtml(OrangeColor);
                        else if (button.Tag?.ToString() == "Danger")
                            button.BackColor = ColorTranslator.FromHtml(RedColor);
                        break;
                    case TextBox textBox:
                        textBox.BackColor = ColorTranslator.FromHtml(CurrentLineColor);
                        textBox.ForeColor = ColorTranslator.FromHtml(ForegroundColor);
                        break;
                    case ComboBox comboBox:
                        comboBox.BackColor = ColorTranslator.FromHtml(CurrentLineColor);
                        comboBox.ForeColor = ColorTranslator.FromHtml(ForegroundColor);
                        comboBox.FlatStyle = FlatStyle.Flat;
                        break;
                    case ListBox listBox:
                        listBox.BackColor = ColorTranslator.FromHtml(CurrentLineColor);
                        listBox.ForeColor = ColorTranslator.FromHtml(ForegroundColor);
                        listBox.BorderStyle = BorderStyle.FixedSingle;
                        break;
                    case ListView listView:
                        listView.BackColor = ColorTranslator.FromHtml(CurrentLineColor);
                        listView.ForeColor = ColorTranslator.FromHtml(ForegroundColor);
                        listView.BorderStyle = BorderStyle.FixedSingle;
                        listView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
                        break;
                    case CheckBox checkBox:
                        checkBox.ForeColor = ColorTranslator.FromHtml(GreenColor);
                        checkBox.FlatStyle = FlatStyle.Flat;
                        break;
                    case RadioButton radioButton:
                        radioButton.ForeColor = ColorTranslator.FromHtml(YellowColor);
                        radioButton.FlatStyle = FlatStyle.Flat;
                        break;
                    case Panel panel:
                        panel.BackColor = ColorTranslator.FromHtml(BackgroundColor);
                        ApplyDrakulaControls(panel.Controls);
                        break;
                    case GroupBox groupBox:
                        groupBox.BackColor = ColorTranslator.FromHtml(BackgroundColor);
                        groupBox.ForeColor = ColorTranslator.FromHtml(CyanColor);
                        ApplyDrakulaControls(groupBox.Controls);
                        break;
                    case DataGridView dataGridView:
                        ApplyDrakulaDataGridView(dataGridView);
                        break;
                    case TabControl tabControl:
                        ApplyDrakulaTabControl(tabControl);
                        break;
                    case TrackBar trackBar:
                        trackBar.BackColor = ColorTranslator.FromHtml(BackgroundColor);
                        trackBar.ForeColor = ColorTranslator.FromHtml(CyanColor);
                        trackBar.TickStyle = TickStyle.Both;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}