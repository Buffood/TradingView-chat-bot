namespace TradingView_Chat_Bot
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listView_chats = new System.Windows.Forms.ListView();
            this.textBox_delay = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_lyrics = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox_Symbol = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button_bitbotSocket = new System.Windows.Forms.Button();
            this.textBox_retbitbot = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.button_reg_generate = new System.Windows.Forms.Button();
            this.label_reg_number = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox_reg_emailDomain = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_reg_emailStart = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDown_reg_num = new System.Windows.Forms.NumericUpDown();
            this.textBox_reg_usernameStart = new System.Windows.Forms.TextBox();
            this.textBox_reg_pass = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label_status = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button_login = new System.Windows.Forms.Button();
            this.textBox_user = new System.Windows.Forms.TextBox();
            this.textBox_pass = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_reg_usernamePool = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_reg_num)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(1, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(674, 471);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tabControl2);
            this.tabPage1.Controls.Add(this.label_status);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.button_login);
            this.tabPage1.Controls.Add(this.textBox_user);
            this.tabPage1.Controls.Add(this.textBox_pass);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(666, 445);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Account1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Location = new System.Drawing.Point(6, 93);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(654, 349);
            this.tabControl2.TabIndex = 10;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listView_chats);
            this.tabPage2.Controls.Add(this.textBox_delay);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.textBox_lyrics);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.textBox_Symbol);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(646, 323);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "Chat";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listView_chats
            // 
            this.listView_chats.Location = new System.Drawing.Point(323, 6);
            this.listView_chats.Name = "listView_chats";
            this.listView_chats.Size = new System.Drawing.Size(317, 300);
            this.listView_chats.TabIndex = 14;
            this.listView_chats.UseCompatibleStateImageBehavior = false;
            // 
            // textBox_delay
            // 
            this.textBox_delay.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_delay.Location = new System.Drawing.Point(111, 215);
            this.textBox_delay.Name = "textBox_delay";
            this.textBox_delay.Size = new System.Drawing.Size(206, 29);
            this.textBox_delay.TabIndex = 13;
            this.textBox_delay.Text = "500";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 224);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "Delay (millis)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 253);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Symbol: BTC/USD";
            // 
            // textBox_lyrics
            // 
            this.textBox_lyrics.Location = new System.Drawing.Point(6, 6);
            this.textBox_lyrics.Multiline = true;
            this.textBox_lyrics.Name = "textBox_lyrics";
            this.textBox_lyrics.Size = new System.Drawing.Size(311, 210);
            this.textBox_lyrics.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Black;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.Location = new System.Drawing.Point(6, 276);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(311, 30);
            this.button1.TabIndex = 8;
            this.button1.Text = "Post Conversation";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox_Symbol
            // 
            this.textBox_Symbol.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Symbol.Location = new System.Drawing.Point(147, 247);
            this.textBox_Symbol.Name = "textBox_Symbol";
            this.textBox_Symbol.Size = new System.Drawing.Size(170, 29);
            this.textBox_Symbol.TabIndex = 9;
            this.textBox_Symbol.Text = "BTCCHF";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button_bitbotSocket);
            this.tabPage3.Controls.Add(this.textBox_retbitbot);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(646, 323);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = "Bitbot";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button_bitbotSocket
            // 
            this.button_bitbotSocket.BackColor = System.Drawing.Color.Black;
            this.button_bitbotSocket.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_bitbotSocket.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button_bitbotSocket.Location = new System.Drawing.Point(446, 248);
            this.button_bitbotSocket.Name = "button_bitbotSocket";
            this.button_bitbotSocket.Size = new System.Drawing.Size(194, 30);
            this.button_bitbotSocket.TabIndex = 9;
            this.button_bitbotSocket.Text = "Start socket";
            this.button_bitbotSocket.UseVisualStyleBackColor = false;
            this.button_bitbotSocket.Click += new System.EventHandler(this.button_bitbotSocket_Click);
            // 
            // textBox_retbitbot
            // 
            this.textBox_retbitbot.Location = new System.Drawing.Point(3, 6);
            this.textBox_retbitbot.Multiline = true;
            this.textBox_retbitbot.Name = "textBox_retbitbot";
            this.textBox_retbitbot.Size = new System.Drawing.Size(634, 210);
            this.textBox_retbitbot.TabIndex = 5;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.checkBox1);
            this.tabPage4.Controls.Add(this.textBox_reg_usernamePool);
            this.tabPage4.Controls.Add(this.button_reg_generate);
            this.tabPage4.Controls.Add(this.label_reg_number);
            this.tabPage4.Controls.Add(this.label11);
            this.tabPage4.Controls.Add(this.textBox_reg_emailDomain);
            this.tabPage4.Controls.Add(this.label9);
            this.tabPage4.Controls.Add(this.textBox_reg_emailStart);
            this.tabPage4.Controls.Add(this.label8);
            this.tabPage4.Controls.Add(this.numericUpDown_reg_num);
            this.tabPage4.Controls.Add(this.textBox_reg_usernameStart);
            this.tabPage4.Controls.Add(this.textBox_reg_pass);
            this.tabPage4.Controls.Add(this.label6);
            this.tabPage4.Controls.Add(this.label7);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(646, 323);
            this.tabPage4.TabIndex = 2;
            this.tabPage4.Text = "Registration bot";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // button_reg_generate
            // 
            this.button_reg_generate.BackColor = System.Drawing.Color.Black;
            this.button_reg_generate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_reg_generate.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button_reg_generate.Location = new System.Drawing.Point(3, 251);
            this.button_reg_generate.Name = "button_reg_generate";
            this.button_reg_generate.Size = new System.Drawing.Size(631, 61);
            this.button_reg_generate.TabIndex = 11;
            this.button_reg_generate.Text = "Generate";
            this.button_reg_generate.UseVisualStyleBackColor = false;
            this.button_reg_generate.Click += new System.EventHandler(this.button_reg_generate_Click);
            // 
            // label_reg_number
            // 
            this.label_reg_number.AutoSize = true;
            this.label_reg_number.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_reg_number.Location = new System.Drawing.Point(605, 228);
            this.label_reg_number.Name = "label_reg_number";
            this.label_reg_number.Size = new System.Drawing.Size(18, 20);
            this.label_reg_number.TabIndex = 14;
            this.label_reg_number.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(532, 228);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 20);
            this.label11.TabIndex = 13;
            this.label11.Text = "Status:";
            // 
            // textBox_reg_emailDomain
            // 
            this.textBox_reg_emailDomain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_reg_emailDomain.Location = new System.Drawing.Point(131, 228);
            this.textBox_reg_emailDomain.Name = "textBox_reg_emailDomain";
            this.textBox_reg_emailDomain.Size = new System.Drawing.Size(334, 22);
            this.textBox_reg_emailDomain.TabIndex = 12;
            this.textBox_reg_emailDomain.Text = "@zmail.com";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(8, 228);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 16);
            this.label9.TabIndex = 11;
            this.label9.Text = "Email domain:";
            // 
            // textBox_reg_emailStart
            // 
            this.textBox_reg_emailStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_reg_emailStart.Location = new System.Drawing.Point(131, 200);
            this.textBox_reg_emailStart.Name = "textBox_reg_emailStart";
            this.textBox_reg_emailStart.Size = new System.Drawing.Size(334, 22);
            this.textBox_reg_emailStart.TabIndex = 10;
            this.textBox_reg_emailStart.Text = "BuyOnPurpleFgt";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(8, 200);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 16);
            this.label8.TabIndex = 9;
            this.label8.Text = "Email start:";
            // 
            // numericUpDown_reg_num
            // 
            this.numericUpDown_reg_num.Location = new System.Drawing.Point(471, 12);
            this.numericUpDown_reg_num.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown_reg_num.Name = "numericUpDown_reg_num";
            this.numericUpDown_reg_num.Size = new System.Drawing.Size(163, 20);
            this.numericUpDown_reg_num.TabIndex = 8;
            // 
            // textBox_reg_usernameStart
            // 
            this.textBox_reg_usernameStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_reg_usernameStart.Location = new System.Drawing.Point(131, 12);
            this.textBox_reg_usernameStart.Name = "textBox_reg_usernameStart";
            this.textBox_reg_usernameStart.Size = new System.Drawing.Size(334, 22);
            this.textBox_reg_usernameStart.TabIndex = 7;
            this.textBox_reg_usernameStart.Text = "BuyOnPurpleFgt";
            // 
            // textBox_reg_pass
            // 
            this.textBox_reg_pass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_reg_pass.Location = new System.Drawing.Point(131, 172);
            this.textBox_reg_pass.Name = "textBox_reg_pass";
            this.textBox_reg_pass.Size = new System.Drawing.Size(333, 22);
            this.textBox_reg_pass.TabIndex = 6;
            this.textBox_reg_pass.Text = "PurpleBarney";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(8, 175);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "Password:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(117, 16);
            this.label7.TabIndex = 4;
            this.label7.Text = "Username start:";
            // 
            // label_status
            // 
            this.label_status.AutoSize = true;
            this.label_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_status.Location = new System.Drawing.Point(294, 54);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(102, 20);
            this.label_status.TabIndex = 7;
            this.label_status.Text = "Not logged in";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(221, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Status:";
            // 
            // button_login
            // 
            this.button_login.BackColor = System.Drawing.Color.Black;
            this.button_login.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_login.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button_login.Location = new System.Drawing.Point(468, 6);
            this.button_login.Name = "button_login";
            this.button_login.Size = new System.Drawing.Size(188, 45);
            this.button_login.TabIndex = 5;
            this.button_login.Text = "Login";
            this.button_login.UseVisualStyleBackColor = false;
            this.button_login.Click += new System.EventHandler(this.button_login_Click);
            // 
            // textBox_user
            // 
            this.textBox_user.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_user.Location = new System.Drawing.Point(93, 3);
            this.textBox_user.Name = "textBox_user";
            this.textBox_user.Size = new System.Drawing.Size(370, 22);
            this.textBox_user.TabIndex = 3;
            // 
            // textBox_pass
            // 
            this.textBox_pass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_pass.Location = new System.Drawing.Point(93, 29);
            this.textBox_pass.Name = "textBox_pass";
            this.textBox_pass.PasswordChar = '*';
            this.textBox_pass.Size = new System.Drawing.Size(369, 22);
            this.textBox_pass.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username:";
            // 
            // textBox_reg_usernamePool
            // 
            this.textBox_reg_usernamePool.Enabled = false;
            this.textBox_reg_usernamePool.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_reg_usernamePool.Location = new System.Drawing.Point(11, 61);
            this.textBox_reg_usernamePool.Multiline = true;
            this.textBox_reg_usernamePool.Name = "textBox_reg_usernamePool";
            this.textBox_reg_usernamePool.Size = new System.Drawing.Size(623, 82);
            this.textBox_reg_usernamePool.TabIndex = 15;
            this.textBox_reg_usernamePool.Text = resources.GetString("textBox_reg_usernamePool.Text");
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(11, 40);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(120, 17);
            this.checkBox1.TabIndex = 17;
            this.checkBox1.Text = "Use username pool:";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 471);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "TV Chat Bot";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_reg_num)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_user;
        private System.Windows.Forms.TextBox textBox_pass;
        private System.Windows.Forms.TextBox textBox_lyrics;
        private System.Windows.Forms.Button button_login;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox_Symbol;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_delay;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_retbitbot;
        private System.Windows.Forms.Button button_bitbotSocket;
        private System.Windows.Forms.ListView listView_chats;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox textBox_reg_usernameStart;
        private System.Windows.Forms.TextBox textBox_reg_pass;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDown_reg_num;
        private System.Windows.Forms.TextBox textBox_reg_emailStart;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_reg_emailDomain;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button_reg_generate;
        private System.Windows.Forms.Label label_reg_number;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox_reg_usernamePool;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

