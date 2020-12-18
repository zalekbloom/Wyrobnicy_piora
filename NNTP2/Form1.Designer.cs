namespace NNTP2
{
    partial class Form1
    {

        private NNTP nntp1;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label lblServerName;
        //  private System.Windows.Forms.Label lblSeperator;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHeaders;
        private System.Windows.Forms.Label label2;


        /// <summary>
        /// Required designer variable.
        /// </summary>
        /// 

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
            this.components = new System.ComponentModel.Container();
            this.b_exit = new System.Windows.Forms.Button();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.lblServerName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtHeaders = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4_error_msg = new System.Windows.Forms.Label();
            this.label4_compiled = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.l_msg = new System.Windows.Forms.Label();
            this.b_show_commands = new System.Windows.Forms.Button();
            this.b_header_by_date = new System.Windows.Forms.Button();
            this.l_date_from_to = new System.Windows.Forms.Label();
            this.mC_from = new System.Windows.Forms.MonthCalendar();
            this.mC_to = new System.Windows.Forms.MonthCalendar();
            this.lB_newsgroups = new System.Windows.Forms.ListBox();
            this.nntp1 = new NNTP2.NNTP(this.components);
            this.SuspendLayout();
            // 
            // b_exit
            // 
            this.b_exit.Location = new System.Drawing.Point(862, 12);
            this.b_exit.Name = "b_exit";
            this.b_exit.Size = new System.Drawing.Size(100, 39);
            this.b_exit.TabIndex = 0;
            this.b_exit.Text = "Exist";
            this.b_exit.UseVisualStyleBackColor = true;
            this.b_exit.Click += new System.EventHandler(this.b_exit_Click);
            // 
            // txtServer
            // 
            this.txtServer.Enabled = false;
            this.txtServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServer.Location = new System.Drawing.Point(179, 50);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(270, 26);
            this.txtServer.TabIndex = 0;
            this.txtServer.TextChanged += new System.EventHandler(this.txtServer_TextChanged);
            // 
            // lblServerName
            // 
            this.lblServerName.AutoSize = true;
            this.lblServerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServerName.Location = new System.Drawing.Point(8, 53);
            this.lblServerName.Name = "lblServerName";
            this.lblServerName.Size = new System.Drawing.Size(161, 20);
            this.lblServerName.TabIndex = 1;
            this.lblServerName.Text = "News Server Used:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 15;
            // 
            // txtHeaders
            // 
            this.txtHeaders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHeaders.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHeaders.Location = new System.Drawing.Point(8, 372);
            this.txtHeaders.Multiline = true;
            this.txtHeaders.Name = "txtHeaders";
            this.txtHeaders.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHeaders.Size = new System.Drawing.Size(981, 251);
            this.txtHeaders.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 343);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Results:";
            // 
            // label4_error_msg
            // 
            this.label4_error_msg.AutoSize = true;
            this.label4_error_msg.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4_error_msg.ForeColor = System.Drawing.Color.Red;
            this.label4_error_msg.Location = new System.Drawing.Point(12, 626);
            this.label4_error_msg.Name = "label4_error_msg";
            this.label4_error_msg.Size = new System.Drawing.Size(89, 18);
            this.label4_error_msg.TabIndex = 12;
            this.label4_error_msg.Text = "Error Msg:";
            // 
            // label4_compiled
            // 
            this.label4_compiled.AutoSize = true;
            this.label4_compiled.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4_compiled.Location = new System.Drawing.Point(22, 9);
            this.label4_compiled.Name = "label4_compiled";
            this.label4_compiled.Size = new System.Drawing.Size(89, 18);
            this.label4_compiled.TabIndex = 13;
            this.label4_compiled.Text = "Error Msg:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(367, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(464, 31);
            this.label5.TabIndex = 14;
            this.label5.Text = "Counts posts on soc.culture.polish";
            // 
            // l_msg
            // 
            this.l_msg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.l_msg.AutoSize = true;
            this.l_msg.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_msg.ForeColor = System.Drawing.Color.Red;
            this.l_msg.Location = new System.Drawing.Point(12, 663);
            this.l_msg.Name = "l_msg";
            this.l_msg.Size = new System.Drawing.Size(89, 18);
            this.l_msg.TabIndex = 17;
            this.l_msg.Text = "Error Msg:";
            // 
            // b_show_commands
            // 
            this.b_show_commands.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.b_show_commands.Location = new System.Drawing.Point(787, 295);
            this.b_show_commands.Name = "b_show_commands";
            this.b_show_commands.Size = new System.Drawing.Size(152, 54);
            this.b_show_commands.TabIndex = 22;
            this.b_show_commands.Text = "Show Server Commands";
            this.b_show_commands.UseVisualStyleBackColor = true;
            this.b_show_commands.Click += new System.EventHandler(this.b_show_commands_Click);
            // 
            // b_header_by_date
            // 
            this.b_header_by_date.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.b_header_by_date.Location = new System.Drawing.Point(444, 295);
            this.b_header_by_date.Name = "b_header_by_date";
            this.b_header_by_date.Size = new System.Drawing.Size(152, 54);
            this.b_header_by_date.TabIndex = 24;
            this.b_header_by_date.Text = "Retrieve headers by date";
            this.b_header_by_date.UseVisualStyleBackColor = true;
            this.b_header_by_date.Click += new System.EventHandler(this.b_header_by_date_Click);
            // 
            // l_date_from_to
            // 
            this.l_date_from_to.AutoSize = true;
            this.l_date_from_to.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_date_from_to.ForeColor = System.Drawing.Color.Blue;
            this.l_date_from_to.Location = new System.Drawing.Point(496, 262);
            this.l_date_from_to.Name = "l_date_from_to";
            this.l_date_from_to.Size = new System.Drawing.Size(100, 18);
            this.l_date_from_to.TabIndex = 25;
            this.l_date_from_to.Text = "date from/to";
            // 
            // mC_from
            // 
            this.mC_from.Location = new System.Drawing.Point(405, 92);
            this.mC_from.Name = "mC_from";
            this.mC_from.TabIndex = 27;
            this.mC_from.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.mC_from_DateChanged);
            // 
            // mC_to
            // 
            this.mC_to.Location = new System.Drawing.Point(735, 92);
            this.mC_to.Name = "mC_to";
            this.mC_to.TabIndex = 28;
            this.mC_to.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.mC_to_DateChanged);
            // 
            // lB_newsgroups
            // 
            this.lB_newsgroups.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lB_newsgroups.FormattingEnabled = true;
            this.lB_newsgroups.ItemHeight = 20;
            this.lB_newsgroups.Items.AddRange(new object[] {
            "newsfeed.aioe.org",
            "news.bbs.nz"});
            this.lB_newsgroups.Location = new System.Drawing.Point(15, 97);
            this.lB_newsgroups.Name = "lB_newsgroups";
            this.lB_newsgroups.ScrollAlwaysVisible = true;
            this.lB_newsgroups.Size = new System.Drawing.Size(283, 64);
            this.lB_newsgroups.TabIndex = 29;
            this.lB_newsgroups.SelectedIndexChanged += new System.EventHandler(this.lB_newsgroups_SelectedIndexChanged);
            // 
            // nntp1
            // 
            this.nntp1.EnableEvents = true;
            this.nntp1.ServerName = "";
            this.nntp1.Timeout = 30000;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 690);
            this.Controls.Add(this.lB_newsgroups);
            this.Controls.Add(this.mC_to);
            this.Controls.Add(this.mC_from);
            this.Controls.Add(this.l_date_from_to);
            this.Controls.Add(this.b_header_by_date);
            this.Controls.Add(this.b_show_commands);
            this.Controls.Add(this.l_msg);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4_compiled);
            this.Controls.Add(this.label4_error_msg);
            this.Controls.Add(this.b_exit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtHeaders);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblServerName);
            this.Controls.Add(this.txtServer);
            this.Name = "Form1";
            this.Text = "NNTP Application";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button b_exit;
        private System.Windows.Forms.Label label4_error_msg;
        private System.Windows.Forms.Label label4_compiled;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label l_msg;
        private System.Windows.Forms.Button b_show_commands;
        private System.Windows.Forms.Button b_header_by_date;
        private System.Windows.Forms.Label l_date_from_to;
        private System.Windows.Forms.MonthCalendar mC_from;
        private System.Windows.Forms.MonthCalendar mC_to;
        private System.Windows.Forms.ListBox lB_newsgroups;
    }
}

