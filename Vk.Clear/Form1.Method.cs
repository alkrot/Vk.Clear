using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Vk.Clear
{
    partial class Form1
    {
        private delegate void LabelText(string text);

        private delegate void BtnEnabled(bool btnEnabled);


        private readonly Work work = new Work();

        public bool ButtonEnabled => tabControl1.Enabled;

        Thread thread;

        /// <summary>
        /// WShow input box
        /// </summary>
        /// <param name="title">Title</param>
        /// <param name="promptText">Message</param>
        /// <param name="value">Value</param>
        /// <returns>Dialog result</returns>
        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button button = new Button();
            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;
            button.Text = @"OK";
            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            button.SetBounds(228, 72, 75, 23);
            button.DialogResult = DialogResult.OK;
            label.AutoSize = true;
            textBox.Anchor |= AnchorStyles.Right;
            button.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[]
            {
                label,
                textBox,
                button
            });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = button;
            DialogResult result = form.ShowDialog();
            value = textBox.Text;
            return result;
        }

        /// <summary>
        /// Show box
        /// </summary>
        /// <param name="title">Title</param>
        /// <param name="promptText">Message</param>
        /// <returns>Dialog result</returns>
        public static DialogResult ShowBox(string title, string promptText)
        {
            Form form = new Form();
            Label label = new Label();
            Button button = new Button();
            Button button2 = new Button();
            form.Text = title;
            label.Text = promptText;
            button.Text = @"Да";
            button2.Text = @"Нет";
            label.SetBounds(9, 20, 372, 13);
            button.SetBounds(100, 72, 75, 23);
            button2.SetBounds(300, 72, 75, 23);
            button.DialogResult = DialogResult.OK;
            button2.DialogResult = DialogResult.Cancel;
            label.AutoSize = true;
            button.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            button2.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[]
            {
                label,
                button,
                button2
            });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = button;
            return form.ShowDialog();
        }

        /// <summary>
        /// Set enabled or disabled
        /// </summary>
        /// <param name="btnEnabled">false or true</param>
        public void SetButtonEnabled(bool btnEnabled)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new BtnEnabled(SetButtonEnabled), btnEnabled);
                return;
            }
            tabControl1.Enabled = btnEnabled;
        }

        /// <summary>
        /// Set status in label
        /// </summary>
        /// <param name="text"></param>
        public void SetTextLabel(string text)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new LabelText(SetTextLabel), text);
                return;
            }
            _label1.Text = text;
        }


        /// <summary>
        /// Out of vk
        /// </summary>
        private void OutVk()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Cookies);
            string[] files = Directory.GetFiles(folderPath);
            string[] array = files;
            for (int i = 0; i < array.Length; i++)
            {
                string path = array[i];
                try
                {
                    File.Delete(path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
