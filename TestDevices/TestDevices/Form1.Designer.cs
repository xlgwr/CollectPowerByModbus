namespace TestDevices
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
            this.txt1Rece = new System.Windows.Forms.TextBox();
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt1Rece
            // 
            this.txt1Rece.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt1Rece.Location = new System.Drawing.Point(3, 17);
            this.txt1Rece.Multiline = true;
            this.txt1Rece.Name = "txt1Rece";
            this.txt1Rece.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt1Rece.Size = new System.Drawing.Size(683, 175);
            this.txt1Rece.TabIndex = 31;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 27;
            this.label4.Text = "停止位(S)：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 147);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 12);
            this.label6.TabIndex = 26;
            this.label6.Text = "校验位(P)：";
            // 
            // cbox4StopBits
            // 
            this.cbox4StopBits.FormattingEnabled = true;
            this.cbox4StopBits.Location = new System.Drawing.Point(89, 170);
            this.cbox4StopBits.Name = "cbox4StopBits";
            this.cbox4StopBits.Size = new System.Drawing.Size(147, 20);
            this.cbox4StopBits.TabIndex = 5;
            // 
            // cbox3Parity
            // 
            this.cbox3Parity.FormattingEnabled = true;
            this.cbox3Parity.Location = new System.Drawing.Point(89, 144);
            this.cbox3Parity.Name = "cbox3Parity";
            this.cbox3Parity.Size = new System.Drawing.Size(147, 20);
            this.cbox3Parity.TabIndex = 4;
            // 
            // cbox2DataBits
            // 
            this.cbox2DataBits.FormattingEnabled = true;
            this.cbox2DataBits.Location = new System.Drawing.Point(89, 118);
            this.cbox2DataBits.Name = "cbox2DataBits";
            this.cbox2DataBits.Size = new System.Drawing.Size(147, 20);
            this.cbox2DataBits.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 22;
            this.label3.Text = "数据位(D)：";
            // 
            // cbox1BaudRate
            // 
            this.cbox1BaudRate.FormattingEnabled = true;
            this.cbox1BaudRate.Location = new System.Drawing.Point(89, 92);
            this.cbox1BaudRate.Name = "cbox1BaudRate";
            this.cbox1BaudRate.Size = new System.Drawing.Size(147, 20);
            this.cbox1BaudRate.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 20;
            this.label2.Text = "波特率(B)：";
            // 
            // cbox0PortName
            // 
            this.cbox0PortName.FormattingEnabled = true;
            this.cbox0PortName.Location = new System.Drawing.Point(89, 66);
            this.cbox0PortName.Name = "cbox0PortName";
            this.cbox0PortName.Size = new System.Drawing.Size(147, 20);
            this.cbox0PortName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "端口号(COM)：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt1Rece);
            this.groupBox1.Location = new System.Drawing.Point(12, 284);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(689, 195);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "收到数据：";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // groupBox2
            // 
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
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(254, 213);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设置：";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton1.Location = new System.Drawing.Point(33, 29);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(49, 16);
            this.radioButton1.TabIndex = 35;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "电表";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton2.Location = new System.Drawing.Point(141, 29);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(62, 16);
            this.radioButton2.TabIndex = 35;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "报警灯";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 491);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txt1Rece;
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
    }
}

