using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace MACKey
{
    public partial class KeyGen : Form
    {
        public KeyGen()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            initTxtmd5();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            initAddress();
            initComb();
            txt1CPU.Text = getInfoToMd5.getCPU();
            txt2HardDevice.Text = getInfoToMd5.getDiskDriveid();
            txt3SerialModel.Text = getInfoToMd5.getSerialNumber();
            initTxtmd5();
        }
        void initComb()
        {
            if (getInfoToMd5.nicList.Count > 1)
            {
                for (int i = 1; i <= getInfoToMd5.nicList.Count; i++)
                {
                    comboBox1.Items.Add(i);
                }
            }
        }
        void initTxtmd5()
        {
            var tomd5 = txt0MacAddress.Text.Trim();
            if (chk0cpu.Checked)
            {
                tomd5 += txt1CPU.Text.Trim();
            }
            if (chk0DeviceID.Checked)
            {
                tomd5 += txt2HardDevice.Text.Trim();
            }
            if (chk3Model.Checked)
            {
                tomd5 += txt3SerialModel.Text.Trim();
            }
            if (chk5user.Checked)
            {
                tomd5 += txt5User.Text.Trim();
            }
            txt0Result.Text = getInfoToMd5.MD5Encrypt(tomd5);
        }
        void initAddress()
        {
            var getaddindex = 0;
            if (!string.IsNullOrEmpty(comboBox1.Text))
            {
                getaddindex = Convert.ToInt16(comboBox1.Text) - 1;
            }
            if (getaddindex < 0)
            {
                getaddindex = 0;
            }
            if (getInfoToMd5.nicList.Count <= 0)
            {
                txt0MacAddress.Text = getInfoToMd5.getMacAddress(getaddindex);
            }
            else
            {
                txt0MacAddress.Text = getInfoToMd5.nicList[getaddindex];

            }

            initTxtmd5();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            initAddress();
        }

        private void chk0cpu_CheckedChanged(object sender, EventArgs e)
        {
            initTxtmd5();
        }

        private void chk0DeviceID_CheckedChanged(object sender, EventArgs e)
        {
            initTxtmd5();
        }

        private void chk3Model_CheckedChanged(object sender, EventArgs e)
        {
            initTxtmd5();
        }

        private void chk5user_CheckedChanged(object sender, EventArgs e)
        {
            initTxtmd5();
        }

        private void txt0Result_TextChanged(object sender, EventArgs e)
        {
            lbl0Msg.Text = txt0Result.Text.Length.ToString();
        }
    }
}
