using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Wyrobnicy_piora
{
    public partial class Form1 : Form
    {
        int _year;
        int _month;


        public Form1()
        {
            InitializeComponent();

            string filePath = System.Reflection.Assembly.GetCallingAssembly().Location;
            DateTime c3 = File.GetLastWriteTime(filePath);
            label4_error_msg.Text = "";

            label4_compiled.Text = "Built: Wyrobnicy_piora, " + c3.ToString();

            lB_newsgroups.SelectedIndex = 0;

            txtServer.Text = lB_newsgroups.SelectedItem.ToString().Trim();

            Globals.newsgroup = "soc.culture.polish";

            DateTime today = DateTime.Today;

            int today_month = DateTime.Today.Month;
            int today_day = DateTime.Today.Day;
            int today_year = DateTime.Today.Year;

            int prev_day = 1;
            int prev_month = DateTime.Today.Month;
            int prev_year = DateTime.Today.Year;

            if (today_month == 1)
            {
                prev_month = 12;
                prev_year = today_year - 1;
            }
            else
            {
                prev_month = today_month - 1;
            }

            Globals.date_from = new DateTime(prev_year, prev_month, 1);

            int days_in_month = DateTime.DaysInMonth(Globals.date_from.Year, Globals.date_from.Month);

            Globals.date_to = new DateTime(prev_year, prev_month, days_in_month, 23, 59, 59);  // time: 23:59:59 PM

            l_date_from_to.Text = "Counting posts from: " + Globals.date_from.ToString("yyyy-MM-dd") + " to: " +
                                  Globals.date_to.ToString("yyyy-MM-dd");

            dTP_from.MaxDate = new DateTime(today_year, today_month, today_day, 23, 59, 59);  // time: 23:59:59 PM
            dTP_from.Value = new DateTime(prev_year, prev_month, 1, 0, 0, 1);               // time: 23:59:59 PM
            dTP_from.MinDate = new DateTime(prev_year, prev_month, 1, 0, 0, 1);               // time: 23:59:59 PM

            dTP_to.MaxDate = new DateTime(today_year, today_month, today_day, 23, 59, 59);  // time: 23:59:59 PM
            dTP_to.Value = new DateTime(today_year, today_month, today_day, 23, 59, 59);  // time: 23:59:59 PM
            dTP_to.MinDate = new DateTime(prev_year, prev_month, 1, 0, 0, 1);               // time: 23:59:59 PM  
        }

        private void b_exit_Click(object sender, EventArgs e)
        {
            this.nntp1.Disconnect();
            Close();
        }





        private void nntp1_OnDownLoadHeaderOne(object Sender, ArticleHeader header)
        {
            this.txtHeaders.Text =
                this.txtHeaders.Text + Environment.NewLine + header.ToString();
        }

        private void btnRetrieve_Click(object sender, System.EventArgs e)
        {
            this.nntp1.SelectGroup(Globals.newsgroup);
            this.txtHeaders.Clear();
            int result = this.nntp1.DownloadHeaders(this.nntp1.CurrentNewsgroup.High - 4, this.nntp1.CurrentNewsgroup.High);
            if (result == int.MaxValue)
            {
                // MessageBox.Show("Headers Downloaded Successfully", "Done");

                l_msg.Text = "Headers Downloaded Successfully";

                txtHeaders.Text = String.Join(" ", this.nntp1.posts);
            }
            else
            {
                MessageBox.Show("Download Header Operation Failed", "Error");
            }
        }



        public class Newsgroup
        {
            /// <summary>
            /// Default Constructors with no arguments cannot be called by the user
            /// </summary>
            private Newsgroup()
            { }
            /// <summary>
            /// Conversion Constructor for the class
            /// </summary>
            /// <param name="groupName">Name of the Newsgroup</param>
            public Newsgroup(string groupName)
            {
                this.groupName = groupName;
                this.numberPosts = 0;
                this.lowWatermark = 0;
                this.highWatermark = 0;
            }

            /// <summary>
            /// Constructor for the Newsgroup class
            /// </summary>
            /// <param name="groupName">Name of the Newsgroup</param>
            /// <param name="numberPosts">Estimated number of posts</param>
            public Newsgroup(string groupName, int numberPosts)
            {
                this.groupName = groupName;
                this.numberPosts = numberPosts;
                this.lowWatermark = 0;
                this.highWatermark = 0;
            }

            /// <summary>
            /// General Constructor for the Newsgroup class
            /// </summary>
            /// <param name="groupName">Name of the Newsgroup</param>
            /// <param name="numberPosts">Estimated number of posts</param>
            /// <param name="low">low watermark</param>
            /// <param name="high">high watermark</param>
            public Newsgroup(string groupName, int numberPosts, int low, int high)
            {
                this.groupName = groupName;
                this.numberPosts = numberPosts;
                this.lowWatermark = low;
                this.highWatermark = high;
            }


            #region Private Variables
            /// <summary>
            /// Name of the Newsgroup
            /// </summary>
            private string groupName;
            /// <summary>
            /// Estimated Number of posts in the newsgroup
            /// </summary>
            private int numberPosts;
            /// <summary>
            /// Reported low watermark - article numbers of the first article
            /// </summary>
            private int lowWatermark;
            /// <summary>
            /// Reported high watermark - article numbers of the last article
            /// </summary>
            private int highWatermark;
            #endregion

            #region Public Properties
            /// <summary>
            /// Gets / Sets the Newsgroup Name
            /// </summary>
            public string GroupName
            {
                get
                {
                    return this.groupName;
                }
                set
                {
                    this.groupName = value;
                }
            }

            /// <summary>
            /// Gets / Sets the Estimated Number of Posts
            /// </summary>
            public int NumberPosts
            {
                get
                {
                    return this.numberPosts;
                }
                set
                {
                    this.numberPosts = value;
                }
            }

            /// <summary>
            /// Gets / Sets the low watermark for the newsgroup
            /// </summary>
            public int Low
            {
                get
                {
                    return this.lowWatermark;
                }
                set
                {
                    this.lowWatermark = value;
                }
            }

            /// <summary>
            /// Gets / Sets the high watermark of the newsgroup
            /// </summary>
            public int High
            {
                get
                {
                    return this.highWatermark;
                }
                set
                {
                    this.highWatermark = value;
                }
            }

            #endregion

        }

        private void txtServer_TextChanged(object sender, EventArgs e)
        {

        }


        private void b_show_commands_Click(object sender, EventArgs e)
        {
            if (!this.nntp1.Connect(this.txtServer.Text))
            {
                l_msg.Text = "cannot connect to a server";

            }

            this.txtHeaders.Clear();

            List<string> a = nntp1.DownloadHelpCommands();

            txtHeaders.Text = String.Join(Environment.NewLine, a);

            this.nntp1.Disconnect();
        }


        private void b_header_by_date_Click(object sender, EventArgs e)
        {
            Globals.error_msg = null;
                
            if (!this.nntp1.Connect(this.txtServer.Text))
            {
                l_msg.Text = "cannot connect to a server: " + txtServer.Text.Trim();
                txtHeaders.Text = "";
                return;

            }

            this.txtHeaders.Clear();


             List<Email> a = nntp1.Retrieve_10000_Headers(Globals.newsgroup);

            
            txtHeaders.Text = "count from the server: " + txtServer.Text.Trim() + 
                              " from: " + Globals.date_from.ToString("yyyy-MM-dd") + 
                              " to: " +  Globals.date_to.ToString("yyyy-MM-dd") +
                               Environment.NewLine + Environment.NewLine;

            if (a != null)
            {
                foreach (Email em in a)
                {
                    txtHeaders.Text = txtHeaders.Text + em.email_address + "    " + em.nbr_posts.ToString() +
                         Environment.NewLine;
                }
            }
            else
            {
                l_msg.Text = nntp1.error_msg;
            }

            this.nntp1.Disconnect();
        }  

         

        private void lB_newsgroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtServer.Text = lB_newsgroups.SelectedItem.ToString().Trim();
        }

        private void dTP_from_ValueChanged(object sender, EventArgs e)
        {
            Globals.date_from = (DateTime)dTP_from.Value;

            l_msg.Text = ""; 
            l_date_from_to.Text = "Counting posts from: " + Globals.date_from.ToString("yyyy-MM-dd") + " to: " +
                                  Globals.date_to.ToString("yyyy-MM-dd");

            if (Globals.date_to <= Globals.date_from)
            {
                l_msg.Text = "****Error: Date From must be > date to*****";
            }
        }

        private void dTP_to_ValueChanged(object sender, EventArgs e)
        {
            Globals.date_to = (DateTime)dTP_to.Value;

            l_msg.Text = "";
            
            l_date_from_to.Text = "Counting posts from: " + Globals.date_from.ToString("yyyy-MM-dd") + " to: " +
                                  Globals.date_to.ToString("yyyy-MM-dd");

            if (Globals.date_to <= Globals.date_from)
            {
                l_msg.Text = "****Error: Date From must be > date to*****";
            }
        }

        private void b_test_server_Click(object sender, EventArgs e)
        {
            l_msg.Text = "";

            Globals.error_msg = null;

            if (!this.nntp1.Connect(tB_server_to_test.Text.Trim() ))
            {
                l_msg.Text = "cannot connect to a server: " + tB_server_to_test.Text.Trim();
                txtHeaders.Text = "";
                return;

            }

            this.txtHeaders.Clear(); 
            
            txtHeaders.Text = "count from the server: " + tB_server_to_test.Text.Trim() +
                              " from: " + Globals.date_from.ToString("yyyy-MM-dd") +
                              " to: " + Globals.date_to.ToString("yyyy-MM-dd") +
                               Environment.NewLine + Environment.NewLine;


            List<Email> a = nntp1.Retrieve_10000_Headers(Globals.newsgroup);
             

            if (a != null)
            {
                foreach (Email em in a)
                {
                    txtHeaders.Text = txtHeaders.Text + em.email_address + "    " + em.nbr_posts.ToString() +
                         Environment.NewLine;
                }
            }
            else
            {
                l_msg.Text = nntp1.error_msg;
            }

            this.nntp1.Disconnect();
        }
    } 
}



