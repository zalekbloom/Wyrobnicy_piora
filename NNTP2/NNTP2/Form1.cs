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

namespace NNTP2
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

            label4_compiled.Text = "Built: NNTP2, " + c3.ToString();

            txtServer.Text = "newsfeed.aioe.org";
            tB_Newsgoup.Text = "soc.culture.polish";

            DateTime today = DateTime.Today;

            int today_month = DateTime.Today.Month;
            int today_year = DateTime.Today.Year;

            if  (today_month == 1)
            {
                today_month = 12;
                today_year = today_year - 1;
            }
            else
            {
                today_month = today_month - 1;
            }

            Globals.date_from = new DateTime(today_year, today_month, 1);
            Globals.date_to = new DateTime(today_year, today_month, DateTime.DaysInMonth(today_year, today_month), 23, 59, 59);

            l_date_from_to.Text = "Counting posts from: " + Globals.date_from.ToString("yyyy-MM-dd") + " to: " +
                                  Globals.date_to.ToString("yyyy-MM-dd");


            if (!this.nntp1.Connect(this.txtServer.Text)  )
            {  
                l_msg.Text = "cannot connect to a server";
            
            } 

        }

        private void b_exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        

         
        
        private void nntp1_OnDownLoadHeaderOne(object Sender, ArticleHeader header)
        {
            this.txtHeaders.Text =
                this.txtHeaders.Text + Environment.NewLine + header.ToString();
        }

        private void btnRetrieve_Click(object sender, System.EventArgs e)
        {
            this.nntp1.SelectGroup(this.tB_Newsgoup.Text);
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

        private void button1_Click(object sender, System.EventArgs e)
        {
            this.nntp1.Disconnect();
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
            this.txtHeaders.Clear();

            List<string> a = nntp1.DownloadHelpCommands();

            txtHeaders.Text = String.Join(Environment.NewLine, a) ;       
        }

       
        private void b_header_by_date_Click(object sender, EventArgs e)
        {
            this.txtHeaders.Clear();

            //  List<string> a = nntp1.Retrieve_By_dates(cboNewsgroup.Text);
            List<Email> a = nntp1.Retrieve_10000_Headers(tB_Newsgoup.Text);

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
        }
    }
}

 
