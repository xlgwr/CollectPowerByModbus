using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            genkey();
        }

        private void txt0Result_TextChanged(object sender, EventArgs e)
        {
            lbl0Msg.Text = txt0Result.Text.Length.ToString();
        }
    }
}
