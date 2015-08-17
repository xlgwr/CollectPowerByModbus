namespace MACKey
{
    partial class KeyGen2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyGen2));
            this.label2 = new System.Windows.Forms.Label();
            this.txt1CPU = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt5User = new System.Windows.Forms.TextBox();
            this.txt0Result = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lbl0Msg = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt3SerialModel = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "CPU ID";
            // 
            // txt1CPU
            // 
            this.txt1CPU.Location = new System.Drawing.Point(91, 28);
            this.txt1CPU.Name = "txt1CPU";
            this.txt1CPU.Size = new System.Drawing.Size(325, 21);
            this.txt1CPU.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "自定义字符:";
            // 
            // txt5User
            // 
            this.txt5User.Location = new System.Drawing.Point(91, 105);
            this.txt5User.Name = "txt5User";
            this.txt5User.Size = new System.Drawing.Size(325, 21);
            this.txt5User.TabIndex = 14;
            this.txt5User.Text = "www.szisec.com";
            // 
            // txt0Result
            // 
            this.txt0Result.Location = new System.Drawing.Point(12, 234);
            this.txt0Result.Multiline = true;
            this.txt0Result.Name = "txt0Result";
            this.txt0Result.Size = new System.Drawing.Size(436, 75);
            this.txt0Result.TabIndex = 17;
            this.txt0Result.TextChanged += new System.EventHandler(this.txt0Result_TextChanged);
            this.txt0Result.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt0Result_KeyDown);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(103, 138);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 54);
            this.button1.TabIndex = 16;
            this.button1.Text = "生成Key";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.button1_KeyDown);
            // 
            // lbl0Msg
            // 
            this.lbl0Msg.AutoSize = true;
            this.lbl0Msg.Location = new System.Drawing.Point(19, 211);
            this.lbl0Msg.Name = "lbl0Msg";
            this.lbl0Msg.Size = new System.Drawing.Size(0, 12);
            this.lbl0Msg.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(38, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 21;
            this.label6.Text = "主板ID:";
            // 
            // txt3SerialModel
            // 
            this.txt3SerialModel.Location = new System.Drawing.Point(91, 65);
            this.txt3SerialModel.Name = "txt3SerialModel";
            this.txt3SerialModel.Size = new System.Drawing.Size(325, 21);
            this.txt3SerialModel.TabIndex = 20;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(249, 138);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(119, 54);
            this.button2.TabIndex = 16;
            this.button2.Text = "保存Key";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.button1_KeyDown);
            // 
            // KeyGen2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 321);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt3SerialModel);
            this.Controls.Add(this.lbl0Msg);
            this.Controls.Add(this.txt0Result);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt5User);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt1CPU);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "KeyGen2";
            this.Text = "KeyGen2";
            this.Load += new System.EventHandler(this.KeyGen2_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyGen2_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt1CPU;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt5User;
        private System.Windows.Forms.TextBox txt0Result;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbl0Msg;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt3SerialModel;
        private System.Windows.Forms.Button button2;
    }
}