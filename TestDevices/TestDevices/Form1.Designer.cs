﻿namespace TestDevices
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbox4StopBits = new System.Windows.Forms.ComboBox();
            this.cbox3Parity = new System.Windows.Forms.ComboBox();
            this.cbox2DataBits = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbox1BaudRate = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbox0PortName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt1Rece = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comb0Devices = new System.Windows.Forms.ComboBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.btn2Power = new System.Windows.Forms.Button();
            this.btn1Alert = new System.Windows.Forms.Button();
            this.cbox7ID = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txt6timeOff = new System.Windows.Forms.MaskedTextBox();
            this.btn2NewTest = new System.Windows.Forms.Button();
            this.lbl0Msg = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 27;
            this.label4.Text = "停止位(S)：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 168);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 12);
            this.label6.TabIndex = 26;
            this.label6.Text = "校验位(P)：";
            // 
            // cbox4StopBits
            // 
            this.cbox4StopBits.FormattingEnabled = true;
            this.cbox4StopBits.Location = new System.Drawing.Point(89, 136);
            this.cbox4StopBits.Name = "cbox4StopBits";
            this.cbox4StopBits.Size = new System.Drawing.Size(147, 20);
            this.cbox4StopBits.TabIndex = 5;
            // 
            // cbox3Parity
            // 
            this.cbox3Parity.FormattingEnabled = true;
            this.cbox3Parity.Location = new System.Drawing.Point(89, 165);
            this.cbox3Parity.Name = "cbox3Parity";
            this.cbox3Parity.Size = new System.Drawing.Size(147, 20);
            this.cbox3Parity.TabIndex = 4;
            // 
            // cbox2DataBits
            // 
            this.cbox2DataBits.FormattingEnabled = true;
            this.cbox2DataBits.Location = new System.Drawing.Point(89, 110);
            this.cbox2DataBits.Name = "cbox2DataBits";
            this.cbox2DataBits.Size = new System.Drawing.Size(147, 20);
            this.cbox2DataBits.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 22;
            this.label3.Text = "数据位(D)：";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // cbox1BaudRate
            // 
            this.cbox1BaudRate.FormattingEnabled = true;
            this.cbox1BaudRate.Location = new System.Drawing.Point(89, 84);
            this.cbox1BaudRate.Name = "cbox1BaudRate";
            this.cbox1BaudRate.Size = new System.Drawing.Size(147, 20);
            this.cbox1BaudRate.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 20;
            this.label2.Text = "波特率(B)：";
            // 
            // cbox0PortName
            // 
            this.cbox0PortName.FormattingEnabled = true;
            this.cbox0PortName.Location = new System.Drawing.Point(89, 58);
            this.cbox0PortName.Name = "cbox0PortName";
            this.cbox0PortName.Size = new System.Drawing.Size(147, 20);
            this.cbox0PortName.TabIndex = 1;
            this.cbox0PortName.Text = "COM4";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "端口号(COM)：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt1Rece);
            this.groupBox1.Location = new System.Drawing.Point(12, 263);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(548, 216);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Message：";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // txt1Rece
            // 
            this.txt1Rece.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt1Rece.Location = new System.Drawing.Point(3, 17);
            this.txt1Rece.Name = "txt1Rece";
            this.txt1Rece.Size = new System.Drawing.Size(542, 196);
            this.txt1Rece.TabIndex = 0;
            this.txt1Rece.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.comb0Devices);
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Controls.Add(this.cbox0PortName);
            this.groupBox2.Controls.Add(this.cbox1BaudRate);
            this.groupBox2.Controls.Add(this.cbox4StopBits);
            this.groupBox2.Controls.Add(this.cbox3Parity);
            this.groupBox2.Controls.Add(this.cbox2DataBits);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(254, 222);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设置：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(31, 198);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 37;
            this.label8.Text = "电表：";
            // 
            // comb0Devices
            // 
            this.comb0Devices.FormattingEnabled = true;
            this.comb0Devices.Items.AddRange(new object[] {
            "device-默认",
            "device-CL7339N",
            "device-DTSF1352",
            "device-SPM33",
            "device-PD168",
            "device-PD168-main",
            "device-松下表1",
            "device-松下表2",
            "deviceAll-温州YN194E-2SY",
            "deviceAll-Old温州YN194E-2SY"});
            this.comb0Devices.Location = new System.Drawing.Point(89, 193);
            this.comb0Devices.Name = "comb0Devices";
            this.comb0Devices.Size = new System.Drawing.Size(147, 20);
            this.comb0Devices.TabIndex = 36;
            this.comb0Devices.Text = "device-松下表1";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton2.Location = new System.Drawing.Point(141, 21);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(62, 16);
            this.radioButton2.TabIndex = 35;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "报警灯";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton1.Location = new System.Drawing.Point(33, 21);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(49, 16);
            this.radioButton1.TabIndex = 35;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "电表";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // btn2Power
            // 
            this.btn2Power.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn2Power.Location = new System.Drawing.Point(280, 113);
            this.btn2Power.Name = "btn2Power";
            this.btn2Power.Size = new System.Drawing.Size(131, 52);
            this.btn2Power.TabIndex = 0;
            this.btn2Power.Text = "电表测试";
            this.btn2Power.UseVisualStyleBackColor = true;
            this.btn2Power.Click += new System.EventHandler(this.btn2Power_Click);
            // 
            // btn1Alert
            // 
            this.btn1Alert.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn1Alert.Location = new System.Drawing.Point(280, 41);
            this.btn1Alert.Name = "btn1Alert";
            this.btn1Alert.Size = new System.Drawing.Size(131, 52);
            this.btn1Alert.TabIndex = 0;
            this.btn1Alert.Text = "报警灯测试";
            this.btn1Alert.UseVisualStyleBackColor = true;
            this.btn1Alert.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbox7ID
            // 
            this.cbox7ID.FormattingEnabled = true;
            this.cbox7ID.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cbox7ID.Location = new System.Drawing.Point(366, 182);
            this.cbox7ID.Name = "cbox7ID";
            this.cbox7ID.Size = new System.Drawing.Size(67, 20);
            this.cbox7ID.TabIndex = 35;
            this.cbox7ID.Text = "1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(313, 185);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 12);
            this.label5.TabIndex = 36;
            this.label5.Text = "电表ID:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(301, 213);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 12);
            this.label7.TabIndex = 38;
            this.label7.Text = "电表超时:";
            // 
            // txt6timeOff
            // 
            this.txt6timeOff.Location = new System.Drawing.Point(366, 208);
            this.txt6timeOff.Mask = "999";
            this.txt6timeOff.Name = "txt6timeOff";
            this.txt6timeOff.Size = new System.Drawing.Size(69, 21);
            this.txt6timeOff.TabIndex = 39;
            this.txt6timeOff.Text = "120";
            // 
            // btn2NewTest
            // 
            this.btn2NewTest.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn2NewTest.Location = new System.Drawing.Point(417, 113);
            this.btn2NewTest.Name = "btn2NewTest";
            this.btn2NewTest.Size = new System.Drawing.Size(131, 52);
            this.btn2NewTest.TabIndex = 0;
            this.btn2NewTest.Text = "电表测试New_main";
            this.btn2NewTest.UseVisualStyleBackColor = true;
            this.btn2NewTest.Click += new System.EventHandler(this.btn2NewTest_Click);
            // 
            // lbl0Msg
            // 
            this.lbl0Msg.AutoSize = true;
            this.lbl0Msg.ForeColor = System.Drawing.Color.Red;
            this.lbl0Msg.Location = new System.Drawing.Point(13, 234);
            this.lbl0Msg.Name = "lbl0Msg";
            this.lbl0Msg.Size = new System.Drawing.Size(47, 12);
            this.lbl0Msg.TabIndex = 40;
            this.lbl0Msg.Text = "lbl0Msg";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 491);
            this.Controls.Add(this.lbl0Msg);
            this.Controls.Add(this.txt6timeOff);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbox7ID);
            this.Controls.Add(this.btn1Alert);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn2NewTest);
            this.Controls.Add(this.btn2Power);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "TestDevices";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbox4StopBits;
        private System.Windows.Forms.ComboBox cbox3Parity;
        private System.Windows.Forms.ComboBox cbox2DataBits;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbox1BaudRate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbox0PortName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Button btn2Power;
        private System.Windows.Forms.Button btn1Alert;
        private System.Windows.Forms.ComboBox cbox7ID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox txt1Rece;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MaskedTextBox txt6timeOff;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comb0Devices;
        private System.Windows.Forms.Button btn2NewTest;
        private System.Windows.Forms.Label lbl0Msg;
    }
}

