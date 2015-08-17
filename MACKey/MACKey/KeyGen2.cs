using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MACKey
{
    public partial class KeyGen2 : Form
    {
        public KeyGen2()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void KeyGen2_Load(object sender, EventArgs e)
        {
            genkey();
        }
        void genkey()
        {
            txt1CPU.Text = getInfoToMd5.getCPU();
            txt3SerialModel.Text = getInfoToMd5.getSerialNumber();

            var tomd5key = txt1CPU.Text.Trim() + txt3SerialModel.Text.Trim() + txt5User.Text.Trim();
            txt0Result.Text = getInfoToMd5.MD5Encrypt(tomd5key);
            txt0Result.Focus();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            genkey();
            this.Enabled = true;
        }

        private void txt0Result_TextChanged(object sender, EventArgs e)
        {
            lbl0Msg.Text = txt0Result.Text.Length.ToString();
        }

        private void KeyGen2_KeyDown(object sender, KeyEventArgs e)
        {
            morefrom(e);
        }

        private static void morefrom(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5 || e.KeyCode == Keys.F6 || e.KeyCode == Keys.F7 || e.KeyCode == Keys.F8)
            {
                KeyGen frmkey1 = new KeyGen();
                frmkey1.ShowDialog();
            }
        }

        private void txt0Result_KeyDown(object sender, KeyEventArgs e)
        {
            morefrom(e);
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            morefrom(e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            try
            {
                var filename = AppDomain.CurrentDomain.BaseDirectory + "\\FtdAdapter.Core.dll";
                if (File.Exists(filename))
                {
                    File.Delete(filename);

                }

                using (var tmpfile = File.CreateText(filename))
                {
                    tmpfile.AutoFlush = true;
                    tmpfile.Write(txt0Result.Text.Trim());
                }

                lbl0Msg.Text = "保存成功." + DateTime.Now.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                button2.Enabled = true;
            }
        }
    }
}
