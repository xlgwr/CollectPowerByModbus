namespace MACKey
{
    partial class KeyGen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyGen));
            this.button1 = new System.Windows.Forms.Button();
            this.txt0MacAddress = new System.Windows.Forms.TextBox();
            this.txt0Result = new System.Windows.Forms.TextBox();
            this.txt1CPU = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt2HardDevice = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt5User = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt3SerialModel = new System.Windows.Forms.TextBox();
            this.chk0cpu = new System.Windows.Forms.CheckBox();
            this.chk0DeviceID = new System.Windows.Forms.CheckBox();
            this.chk3Model = new System.Windows.Forms.CheckBox();
            this.chk5user = new System.Windows.Forms.CheckBox();
            this.lbl0Msg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(185, 254);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(143, 54);
            this.button1.TabIndex = 0;
            this.button1.Text = "生成Key";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txt0MacAddress
            // 
            this.txt0MacAddress.Location = new System.Drawing.Point(103, 49);
            this.txt0MacAddress.Name = "txt0MacAddress";
            this.txt0MacAddress.Size = new System.Drawing.Size(225, 21);
            this.txt0MacAddress.TabIndex = 1;
            // 
            // txt0Result
            // 
            this.txt0Result.Location = new System.Drawing.Point(12, 325);
            this.txt0Result.Multiline = true;
            this.txt0Result.Name = "txt0Result";
            this.txt0Result.Size = new System.Drawing.Size(492, 75);
            this.txt0Result.TabIndex = 3;
            this.txt0Result.TextChanged += new System.EventHandler(this.txt0Result_TextChanged);
            // 
            // txt1CPU
            // 
            this.txt1CPU.Location = new System.Drawing.Point(103, 85);
            this.txt1CPU.Name = "txt1CPU";
            this.txt1CPU.Size = new System.Drawing.Size(325, 21);
            this.txt1CPU.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "MAC Address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "CPU ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "硬盘ID:";
            // 
            // txt2HardDevice
            // 
            this.txt2HardDevice.Location = new System.Drawing.Point(103, 122);
            this.txt2HardDevice.Name = "txt2HardDevice";
            this.txt2HardDevice.Size = new System.Drawing.Size(325, 21);
            this.txt2HardDevice.TabIndex = 6;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(381, 50);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(57, 20);
            this.comboBox1.TabIndex = 8;
            this.comboBox1.Text = "1";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(334, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "网卡:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 199);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "自定义字符:";
            // 
            // txt5User
            // 
            this.txt5User.Location = new System.Drawing.Point(103, 196);
            this.txt5User.Name = "txt5User";
            this.txt5User.Size = new System.Drawing.Size(325, 21);
            this.txt5User.TabIndex = 12;
            this.txt5User.Text = "www.szisec.com";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(50, 162);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "主板ID:";
            // 
            // txt3SerialModel
            // 
            this.txt3SerialModel.Location = new System.Drawing.Point(103, 159);
            this.txt3SerialModel.Name = "txt3SerialModel";
            this.txt3SerialModel.Size = new System.Drawing.Size(325, 21);
            this.txt3SerialModel.TabIndex = 10;
            // 
            // chk0cpu
            // 
            this.chk0cpu.AutoSize = true;
            this.chk0cpu.Checked = true;
            this.chk0cpu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk0cpu.Location = new System.Drawing.Point(434, 87);
            this.chk0cpu.Name = "chk0cpu";
            this.chk0cpu.Size = new System.Drawing.Size(15, 14);
            this.chk0cpu.TabIndex = 14;
            this.chk0cpu.UseVisualStyleBackColor = true;
            this.chk0cpu.CheckedChanged += new System.EventHandler(this.chk0cpu_CheckedChanged);
            // 
            // chk0DeviceID
            // 
            this.chk0DeviceID.AutoSize = true;
            this.chk0DeviceID.Location = new System.Drawing.Point(434, 125);
            this.chk0DeviceID.Name = "chk0DeviceID";
            this.chk0DeviceID.Size = new System.Drawing.Size(15, 14);
            this.chk0DeviceID.TabIndex = 15;
            this.chk0DeviceID.UseVisualStyleBackColor = true;
            this.chk0DeviceID.CheckedChanged += new System.EventHandler(this.chk0DeviceID_CheckedChanged);
            // 
            // chk3Model
            // 
            this.chk3Model.AutoSize = true;
            this.chk3Model.Location = new System.Drawing.Point(434, 162);
            this.chk3Model.Name = "chk3Model";
            this.chk3Model.Size = new System.Drawing.Size(15, 14);
            this.chk3Model.TabIndex = 16;
            this.chk3Model.UseVisualStyleBackColor = true;
            this.chk3Model.CheckedChanged += new System.EventHandler(this.chk3Model_CheckedChanged);
            // 
            // chk5user
            // 
            this.chk5user.AutoSize = true;
            this.chk5user.Checked = true;
            this.chk5user.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk5user.Location = new System.Drawing.Point(434, 199);
            this.chk5user.Name = "chk5user";
            this.chk5user.Size = new System.Drawing.Size(15, 14);
            this.chk5user.TabIndex = 17;
            this.chk5user.UseVisualStyleBackColor = true;
            this.chk5user.CheckedChanged += new System.EventHandler(this.chk5user_CheckedChanged);
            // 
            // lbl0Msg
            // 
            this.lbl0Msg.AutoSize = true;
            this.lbl0Msg.Location = new System.Drawing.Point(12, 310);
            this.lbl0Msg.Name = "lbl0Msg";
            this.lbl0Msg.Size = new System.Drawing.Size(0, 12);
            this.lbl0Msg.TabIndex = 18;
            // 
            // KeyGen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 440);
            this.Controls.Add(this.lbl0Msg);
            this.Controls.Add(this.chk5user);
            this.Controls.Add(this.chk3Model);
            this.Controls.Add(this.chk0DeviceID);
            this.Controls.Add(this.chk0cpu);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt5User);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt3SerialModel);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt2HardDevice);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt1CPU);
            this.Controls.Add(this.txt0Result);
            this.Controls.Add(this.txt0MacAddress);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "KeyGen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KeyGen--默认MAC+CPU+自定义";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txt0MacAddress;
        private System.Windows.Forms.TextBox txt0Result;
        private System.Windows.Forms.TextBox txt1CPU;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt2HardDevice;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt5User;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt3SerialModel;
        private System.Windows.Forms.CheckBox chk0cpu;
        private System.Windows.Forms.CheckBox chk0DeviceID;
        private System.Windows.Forms.CheckBox chk3Model;
        private System.Windows.Forms.CheckBox chk5user;
        private System.Windows.Forms.Label lbl0Msg;
    }
}

