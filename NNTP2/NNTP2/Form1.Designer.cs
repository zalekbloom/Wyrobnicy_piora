namespace NNTP2
{
    partial class Form1
    {

        private NNTP nntp1;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label lblServerName;
        private System.Windows.Forms.Label lblNewsgroup;
        //  private System.Windows.Forms.Label lblSeperator;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHeaders;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDisconnect;


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
            this.lblNewsgroup = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtHeaders = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4_error_msg = new System.Windows.Forms.Label();
            this.label4_compiled = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.l_msg = new System.Windows.Forms.Label();
            this.b_show_commands = new System.Windows.Forms.Button();
            this.b_header_by_date = new System.Windows.Forms.Button();
            this.l_date_from_to = new System.Windows.Forms.Label();
            this.tB_Newsgoup = new System.Windows.Forms.TextBox();
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
            this.txtServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServer.Location = new System.Drawing.Point(8, 77);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(256, 22);
            this.txtServer.TabIndex = 0;
            this.txtServer.TextChanged += new System.EventHandler(this.txtServer_TextChanged);
            // 
            // lblServerName
            // 
            this.lblServerName.AutoSize = true;
            this.lblServerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServerName.Location = new System.Drawing.Point(8, 53);
            this.lblServerName.Name = "lblServerName";
            this.lblServerName.Size = new System.Drawing.Size(124, 16);
            this.lblServerName.TabIndex = 1;
            this.lblServerName.Text = "News Server Used:";
            // 
            // lblNewsgroup
            // 
            this.lblNewsgroup.AutoSize = true;
            this.lblNewsgroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewsgroup.Location = new System.Drawing.Point(8, 122);
            this.lblNewsgroup.Name = "lblNewsgroup";
            this.lblNewsgroup.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblNewsgroup.Size = new System.Drawing.Size(156, 16);
            this.lblNewsgroup.TabIndex = 3;
            this.lblNewsgroup.Text = "Selected Newsgroup:";
            this.lblNewsgroup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.txtHeaders.Location = new System.Drawing.Point(8, 282);
            this.txtHeaders.Multiline = true;
            this.txtHeaders.Name = "txtHeaders";
            this.txtHeaders.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHeaders.Size = new System.Drawing.Size(981, 182);
            this.txtHeaders.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 231);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(192, 23);
            this.label2.TabIndex = 9;
            this.label2.Text = "Recent Headers";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnDisconnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisconnect.Location = new System.Drawing.Point(448, 498);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(97, 23);
            this.btnDisconnect.TabIndex = 10;
            this.btnDisconnect.Text = "&Disconnect";
            this.btnDisconnect.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(46, 463);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 24);
            this.label3.TabIndex = 11;
            this.label3.Text = "Error Msg:";
            // 
            // label4_error_msg
            // 
            this.label4_error_msg.AutoSize = true;
            this.label4_error_msg.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4_error_msg.ForeColor = System.Drawing.Color.Red;
            this.label4_error_msg.Location = new System.Drawing.Point(158, 463);
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
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(418, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 39);
            this.label5.TabIndex = 14;
            this.label5.Text = "NNTP2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(448, 236);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "label4";
            // 
            // l_msg
            // 
            this.l_msg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.l_msg.AutoSize = true;
            this.l_msg.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_msg.Location = new System.Drawing.Point(12, 196);
            this.l_msg.Name = "l_msg";
            this.l_msg.Size = new System.Drawing.Size(89, 18);
            this.l_msg.TabIndex = 17;
            this.l_msg.Text = "Error Msg:";
            // 
            // b_show_commands
            // 
            this.b_show_commands.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.b_show_commands.Location = new System.Drawing.Point(837, 135);
            this.b_show_commands.Name = "b_show_commands";
            this.b_show_commands.Size = new System.Drawing.Size(152, 39);
            this.b_show_commands.TabIndex = 22;
            this.b_show_commands.Text = "Show Commands";
            this.b_show_commands.UseVisualStyleBackColor = true;
            this.b_show_commands.Click += new System.EventHandler(this.b_show_commands_Click);
            // 
            // b_header_by_date
            // 
            this.b_header_by_date.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.b_header_by_date.Location = new System.Drawing.Point(462, 135);
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
            this.l_date_from_to.Location = new System.Drawing.Point(459, 75);
            this.l_date_from_to.Name = "l_date_from_to";
            this.l_date_from_to.Size = new System.Drawing.Size(89, 18);
            this.l_date_from_to.TabIndex = 25;
            this.l_date_from_to.Text = "Error Msg:";
            // 
            // tB_Newsgoup
            // 
            this.tB_Newsgoup.Enabled = false;
            this.tB_Newsgoup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tB_Newsgoup.Location = new System.Drawing.Point(8, 145);
            this.tB_Newsgoup.Name = "tB_Newsgoup";
            this.tB_Newsgoup.Size = new System.Drawing.Size(256, 22);
            this.tB_Newsgoup.TabIndex = 26;
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
            this.ClientSize = new System.Drawing.Size(1001, 531);
            this.Controls.Add(this.tB_Newsgoup);
            this.Controls.Add(this.l_date_from_to);
            this.Controls.Add(this.b_header_by_date);
            this.Controls.Add(this.b_show_commands);
            this.Controls.Add(this.l_msg);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4_compiled);
            this.Controls.Add(this.label4_error_msg);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.b_exit);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtHeaders);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblNewsgroup);
            this.Controls.Add(this.lblServerName);
            this.Controls.Add(this.txtServer);
            this.Name = "Form1";
            this.Text = "NNTP Application";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button b_exit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4_error_msg;
        private System.Windows.Forms.Label label4_compiled;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label l_msg;
        private System.Windows.Forms.Button b_show_commands;
        private System.Windows.Forms.Button b_header_by_date;
        private System.Windows.Forms.Label l_date_from_to;
        private System.Windows.Forms.TextBox tB_Newsgoup;
    }
}

