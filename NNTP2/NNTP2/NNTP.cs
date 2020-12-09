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
using System.IO;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Globalization;

namespace NNTP2
{   
    public class NNTP : System.ComponentModel.Component
    {
        /// <summary>
        /// Constructor for the NNTP class
        /// </summary>
        /// 

        public List<string>  posts;
        public List<string> help_commands;
        public List<string> last_100_headers;
        public string error_msg = "";
        public List<string> all_posts;
        public List<Email> results_unsorted;
        public List<Email> results_sorted;
         

        public NNTP()
        {
            InitializeComponent();
            posts = new List<string>();

          
        }

        /// <summary>
        /// Constructor for the NNTP component
        /// </summary>
        /// <param name="container">NNTP as Component</param>
        public NNTP(System.ComponentModel.IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            posts = new List<string>();
        }

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }
        #endregion

        #region Fields
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        /// <summary>
        /// TCP Connection to the Newsgroup Server
        /// </summary>
        private TcpClient tcpServer;
        /// <summary>
        /// Name of the news server
        /// </summary>
        private string serverName = string.Empty;
        /// <summary>
        /// Boolean variable indicating whether the connection is active
        /// </summary>
        private bool isConnected = false;
        /// <summary>
        /// Number of Millisecond for server timeout, default = 30 sec
        /// </summary>
        private int timeout = 30000;
        /// <summary>
        /// Boolean Variable indicating whether posting to the news server is allowed
        /// </summary>
        private bool isPostingAllowed = false;
        /// <summary>
        /// Current selected newsgroup
        /// </summary>
        protected Newsgroup newsgroup;
        /// <summary>
        /// Boolean Variable indicating whether events are fired
        /// </summary>
        private bool enableEvents = true;
        #endregion

        #region Properties
        /// <summary>
        /// Whether the NNTP Connection is connected
        /// </summary>
        [Description("Whether Connection is Established; Set automatically by Connect()"), Category("NNTP")]
        public bool IsConnected
        {
            get
            {
                return this.isConnected;
            }
        }

        /// <summary>
        /// Gets the name of the server
        /// </summary>
        [Description("Name of the Server"), Category("NNTP")]
        public string ServerName
        {
            get
            {
                return this.serverName;
            }
            set
            {
                this.serverName = value;
            }
        }

        /// <summary>
        /// Gets / Sets the connection timeout for the server
        /// </summary>
        [Description("Connection Timeout for the Server"), Category("NNTP")]
        public int Timeout
        {
            get
            {
                return this.timeout;
            }
            set
            {
                this.timeout = value;
            }
        }

        /// <summary>
        /// Gets a bool whether posting is allowed on this server
        /// </summary>
        [Description("Whether posting is allowed on the server.  Set automatically by Connect()")]
        [Category("NNTP")]
        public bool IsPostingAllowed
        {
            get
            {
                return this.isPostingAllowed;
            }
        }

        /// <summary>
        /// Currently selected newsgroup on the news server
        /// </summary>
        [Category("NNTP")]
        [Description("Selected Newsgroup")]
        public Newsgroup CurrentNewsgroup
        {
            get
            {
                return this.newsgroup;
            }
        }

        /// <summary>
        /// Indicating whether events are fired
        /// </summary>
        [Category("NNTP"), Description("Indicating whether events are enabled")]
        public bool EnableEvents
        {
            get
            {
                return this.enableEvents;
            }
            set
            {
                this.enableEvents = value;
            }
        }

        #endregion


        /// <summary>
        /// Attempt to connect to the News Server whose address is specified 
        /// at the object's ServerName property.
        /// </summary>
        /// <returns>Boolean value indicating whether the connection is successful</returns>
        public bool Connect()
        {
            if (this.serverName == "")
            {
                if (this.enableEvents && this.OnConnectFailure != null)
                    this.OnConnectFailure(this, "Server Name Missing");
                return false;
            }

            return this.Connect(this.ServerName, 119);
        }

        /// <summary>
        /// Attempt to connect to the news Server
        /// </summary>
        /// <param name="address">Address of the Server</param>
        /// <returns>Boolean value indicating whether the connection is successful</returns>
        public bool Connect(string address)
        {
            bool connect = this.Connect(address, 119);
            return connect;
        }

        /// <summary>
        /// Attempt to connect to the news server
        /// </summary>
        /// <param name="address">Address of the Server</param>
        /// <param name="port">Port to use for the connection</param>
        /// <exception cref="System.Exception">Thrown when error occurs during the
        /// initialization of the tcpclient object</exception>
        /// <returns>True or false denoting whether the connection is successful</returns>
        public bool Connect(string address, int port)
        {
            // Check whether we are connected to other servers.
            // If so, disconnect from the old server first
            if (this.isConnected)
            {
                if (this.enableEvents && this.OnConnectFailure != null)
                {
                    this.OnConnectFailure(this, "Already Connected to " + this.serverName);
                }
                return false;
            }

            if (this.enableEvents && this.OnConnectStart != null)
            {
                this.OnConnectStart(this, address);
            }

            this.serverName = address;

            // Create the underlying TCP Connection to the News Server
            try
            {
                this.tcpServer = new TcpClient(address, port);
                this.tcpServer.ReceiveTimeout = this.Timeout;

                // Setup the Network Stream and its reader
                NetworkStream ns = this.tcpServer.GetStream();
                StreamReader r = new StreamReader(ns);

                // Read the welcome message
                string welcome = r.ReadLine();

                // Decode the welcome message from the Server
                switch (welcome.Substring(0, 3))
                {
                    case "200":
                        this.isConnected = true;
                        this.isPostingAllowed = true;
                        break;
                    case "201":
                        this.isConnected = true;
                        this.isPostingAllowed = false;
                        break;
                    case "400":
                    case "502":
                        this.isPostingAllowed = this.isConnected = false;
                        break;
                    default:
                        throw new Exception("Unexpected Server Response while Connecting: " + welcome);
                }

                if (this.isConnected)
                {
                    if (this.enableEvents && this.OnConnectSucceed != null)
                        this.OnConnectSucceed(this, welcome);
                    return true;
                }
                else
                {
                    if (this.enableEvents && this.OnConnectFailure != null)
                        this.OnConnectFailure(this, welcome);
                    return false;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error : " + ex.Message,
                    "Connection Error!",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
                this.isConnected = this.isPostingAllowed = false;
                return false;
            }
        }

        /// <summary>
        /// Disconnect from the current news server
        /// </summary>
        /// <exception cref="System.Exception">Thrown when network stream operation fails or 
        /// tcpclient close operation fails</exception>
        public void Disconnect()
        {
            if (!this.isConnected)
            {
                return;
            }

            try
            {
                NetworkStream ns = this.tcpServer.GetStream();
                StreamReader sr = new StreamReader(ns);
                StreamWriter sw = new StreamWriter(ns);
                sw.AutoFlush = true;
                sw.WriteLine("QUIT");
                sr.ReadLine();
                this.isConnected = false;
                this.isPostingAllowed = false;
                this.serverName = string.Empty;
                this.newsgroup = null;
                sw.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error : " + ex.Message,
                    "Error During Signing Off Server!",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }

            try
            {
                this.tcpServer.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error : " + ex.Message,
                    "Error Encountered During Disconnect!",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }

            this.tcpServer = null;
            if (this.enableEvents && this.OnDisconnect != null)
                this.OnDisconnect(this, "Disconnected from + " + this.serverName);
        }

        /// <summary>
        /// Represents a method that will handle events associated with the 
        /// Connection operations to the server
        /// </summary>
        public delegate void OnConnectHandler(object sender, string message);
        /// <summary>
        /// Occurs when a connection is established to the news server
        /// </summary>
        [Category("Connect"), Description("Occurs when Connection is established")]
        public event OnConnectHandler OnConnectSucceed;
        /// <summary>
        /// Occurs when the attempt connection to the news server failed
        /// </summary>
        [Category("Connect"), Description("Occurs when Connection attempt failed")]
        public event OnConnectHandler OnConnectFailure;
        /// <summary>
        /// Occurs when an attempt to connect to the news server has started
        /// </summary>
        [Category("Connect"), Description("Occurs when attempting to connect")]
        public event OnConnectHandler OnConnectStart;
        /// <summary>
        /// Occurs when the connection is disconnected
        /// </summary>
        [Category("Connect"), Description("Occurs when disconnect")]
        public event OnConnectHandler OnDisconnect;

        /// <summary>
        /// List the Current Active newsgroup on the news server
        /// </summary>
        /// <returns>The number of newsgroup active</returns>
        /// <exception cref="System.Exception">Thrown when we have an invalid response the server</exception>
        public string[] ListGroup()
        {
            if (!this.isConnected)
            {
                System.Windows.Forms.MessageBox.Show(
                    "Not connected to any server!", "Error: Not Connected!",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }

            NetworkStream ns = this.tcpServer.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;

            if (this.enableEvents && this.OnListNewsgroupStart != null)
                this.OnListNewsgroupStart(this, this.serverName);

            sw.WriteLine("LIST ACTIVE");
            string strResponse = "";
            try
            {
                strResponse = sr.ReadLine();
            }
            catch (Exception ex)
            {
                return null;
            }

            if (strResponse == null)
            {
                throw new Exception("LIST ACTIVE : NULL Response received from Server");
            }

            if (strResponse.StartsWith("215"))
            {
                bool done = false;
                string response;

                System.Collections.ArrayList newsgroups = new System.Collections.ArrayList();

                do
                {
                    response = sr.ReadLine();

                    if ((response.Length > 4) && (response.Substring(0, 18) != "soc.culture.polish"))
                    {
                        continue;
                    }
                    if (response == null)
                    {
                        throw new Exception("LIST ACTIVE : NULL Response received from Server");
                    }

                    if (response == ".")
                        done = true;
                    else
                    {
                        string[] GroupNumbers = response.Split(' ');
                        newsgroups.Add(GroupNumbers[0]);
                        if (this.enableEvents && this.OnListNewsgroupOne != null)
                            this.OnListNewsgroupOne(this, GroupNumbers[0]);
                    }
                }
                while (!done);

                if (this.enableEvents && this.OnListNewsgroupDone != null)
                    this.OnListNewsgroupDone(this, this.serverName);

                return (string[])newsgroups.ToArray(Type.GetType("System.String", true));

            }
            else
            {
                throw new Exception("LIST ACTIVE : Unexpected response from server. Received : " + strResponse);
            }
        }

        /// <summary>
        /// Represents method that will handle events associating with the List 
        /// Newsgroup operations
        /// </summary>
        public delegate void OnListNewsgroupHandler(object sender, string newsgroupName);
        /// <summary>
        /// Occurs when a newsgroup name on the connected server is retrieved
        /// </summary>
        [Category("List"), Description("Occurs every time ONE newsgroup name is retrieved")]
        public event OnListNewsgroupHandler OnListNewsgroupOne;
        /// <summary>
        /// Occurs when the List Newsgroup operation has started
        /// </summary>
        [Category("List"), Description("Occurs before the LIST operation")]
        public event OnListNewsgroupHandler OnListNewsgroupStart;
        /// <summary>
        /// Occurs when the List Newsgroup operation has completed
        /// </summary>
        [Category("List"), Description("Occurs after the LIST operation")]
        public event OnListNewsgroupHandler OnListNewsgroupDone;


        /// <summary>
        /// Connects to a specific newsgroup on the news server
        /// </summary>
        /// <param name="group">Name of the newsgroup</param>
        /// <returns> Estimated number of post in the newsgroup.  -1 is returned if there's 
        /// no such group </returns>
        /// <exception cref="System.Exception">Thrown when an invalid response is received
        /// from the server</exception>
        public int SelectGroup(string group)
        {
            NetworkStream ns = this.tcpServer.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;

         //   sw.WriteLine("GROUP " + group);

            sw.WriteLine("GROUP " + group);

            string strResponse = sr.ReadLine();
            if (strResponse == null)
            {
                throw new Exception("GROUP : NULL Response received from server");
            }

            if (strResponse.StartsWith("211"))
            {
                string[] MessageNumbers;
                MessageNumbers = strResponse.Split(' ');
                int estMsgCount = Convert.ToInt32(MessageNumbers[1]);
                int low = Convert.ToInt32(MessageNumbers[2]);
                int high = Convert.ToInt32(MessageNumbers[3]);
                this.newsgroup = new Newsgroup(group, estMsgCount, low, high);
                if (this.enableEvents && this.OnSelectGroupSucceeded != null)
                    this.OnSelectGroupSucceeded(this, String.Join(" ", MessageNumbers));
                return estMsgCount;
            }
            else if (strResponse.StartsWith("411"))
            {
                this.newsgroup = null;
                if (this.enableEvents && this.OnSelectGroupFailed != null)
                    this.OnSelectGroupFailed(this, strResponse);
                return -1;
            }
            else
            {
                throw new Exception("GROUP : Unexpected Response from server;  Received: " + strResponse);
            }
        }

        /// <summary>
        /// Reconnect to the previously connected newsgroup
        /// </summary>
        /// <returns>Boolean variable indicating whether the connection is successful</returns>
        public bool ReconnectNewsgroup()
        {
            this.isConnected = false;

            if (!this.Connect(this.serverName, 119))
                return false;

            if (this.SelectGroup(this.newsgroup.GroupName) != -1)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Represents a method that will handle events associated with the Select Group operations
        /// </summary>
        public delegate void OnSelectGroupHandler(object Sender, string message);
        /// <summary>
        /// Occurs when the GROUP command during the SelectGroup operation succeeded
        /// </summary>
        [Category("Select Group"), Description("Occurs when the GROUP command succeeded")]
        public event OnSelectGroupHandler OnSelectGroupSucceeded;
        /// <summary>
        /// Occurs when the GROUP command during the SelectGroup operation failed
        /// </summary>
        [Category("Select Group"), Description("Occurs when the GROUP command failed")]
        public event OnSelectGroupHandler OnSelectGroupFailed;


        /// <summary>
        /// Download all the headers from the selected newsgroup
        /// </summary>
        public int DownloadHeaders()
        {
            return this.DownloadHeaders(this.newsgroup.Low, this.newsgroup.High);
        }

        /// <summary>
        /// Download all headers between the range [fromMsg, toMsg]
        /// </summary>
        /// <param name="fromMsg">Lower bound of the MessageID</param>
        /// <param name="toMsg">Upper bound of the MessageID</param>
        public int DownloadHeaders(int fromMsg, int toMsg)
        {
            if (this.newsgroup == null || !this.isConnected)
            {
                if (this.OnDownloadHeaderFailed != null)
                    this.OnDownloadHeaderFailed(this, new EventArgs());
                return -1;
            }
            else
            {
                string XOVERCommand = "XOVER " + fromMsg + "-" + toMsg;
                return this.DownloadHeaders(XOVERCommand);
            }
        }

        /// <summary>
        /// Execute the XOVER Command to retrieve the headers from the current newsgroup
        /// </summary>
        /// <param name="XOVERCommand">XOVER command string</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Thrown when an invalid response is received
        /// from the server</exception>
        private int DownloadHeaders(string XOVERCommand)
        {
            // Setup the network stream
            NetworkStream ns = this.tcpServer.GetStream();
            StreamReader sr = new StreamReader(ns, System.Text.Encoding.Default);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;


            //  sw.WriteLine(XOVERCommand);             // Execute the XOVER command
            sw.WriteLine("newnews soc.culture.polis* 201101 000000 "); 

           //  sw.WriteLine("HELP ");

            string response = sr.ReadLine();        // Retrieve Response

            int headerCount = 0;
            if (response == null)
                return headerCount;

            switch (response.Substring(0, 3))
            {
                 
                case "230":
                    string a = sr.ReadLine();

                    // Setup the network stream
                    // ns = this.tcpServer.GetStream();
                    // sr = new StreamReader(ns, System.Text.Encoding.Default);
                    // sw = new StreamWriter(ns);
                    // sw.AutoFlush = true;

                    string xx = "ARTICLE " + a.Trim() + "  "  ; // ('<', '>');
                    sw.WriteLine(xx); 

                    string b = sr.ReadLine();
                   break;
                     

                case "224":                         // Valid command, article list to follow
                    bool done = false;
                    string article;
                    do
                    {
                        int i = 0;
                        article = sr.ReadLine();    // Read the Header of the article
                        if (article == null)        // NULL response from server
                            return headerCount;

                        if (article == ".")     // Check for finish
                            done = true;
                        else
                        {
                            article.Replace("\t\t", "\t<empty>\t");     // take care of empty fields
                            string[] fields = article.Split('\t');  // break up the server response

                            ArticleHeader h = new ArticleHeader();      // fill in header
                            h.ArticleID = fields[0];
                            h.Subject = Utils.DecodeHeaderString(fields[1]);
                            h.From = Utils.DecodeHeaderString(fields[2]);
                           // h.DateString = fields[3];

                            h.DateString = (DateTime.Parse(fields[3].Substring(0,24))).ToString() ;
                             

                            if (i < 10)
                            {
                                i++;
                                this.posts.Add(h.Subject + " || " + h.From + " || " + h.DateString + Environment.NewLine);
                            }



                            //       this.f txtHeaders.Text =
                            //            this.txtHeaders.Text + Environment.NewLine + header.ToString();
                            headerCount++;

                            if (this.enableEvents && this.OnDownLoadHeaderOne != null)
                                this.OnDownLoadHeaderOne(this, h);
                        }
                    } while (!done);

                    if (this.OnDownloadHeaderSucceeded != null && this.enableEvents)
                        this.OnDownloadHeaderSucceeded(this, new EventArgs());
                    headerCount = int.MaxValue;
                    break;

                case "412":     // 412 No newsgroup selected
                case "420":     // 420 Current article number is invalid
                case "423":     // 423 No articles in that range
                    if (this.OnDownloadHeaderFailed != null && this.enableEvents)
                        this.OnDownloadHeaderFailed(this, new EventArgs());
                    headerCount = -1;
                    break;
                default:
                    throw new Exception("XOVER : Unexpected response from server. Received: " + response);
            }

            return headerCount;
        }

        /// <summary>
        /// Represents Methods that will handle events associated with Header download events
        /// </summary>
        public delegate void OnDownloadHeaderDelegate(object Sender, ArticleHeader header);
        /// <summary>
        /// Occurs when one header is retrieved
        /// </summary>
        /// 

        [Category("Header"), Description("Occurs EVERY TIME a header is retrieved")]
        public event OnDownloadHeaderDelegate OnDownLoadHeaderOne;
        /// <summary>
        /// Occurs when the header retrieval process has failed
        /// </summary>
        /// 

        [Category("Header"), Description("XOVER Command Failed")]
        public event EventHandler OnDownloadHeaderFailed;
        /// <summary>
        /// Occurs when the header retrieval process has succeeded
        /// </summary>
        [Category("Header"), Description("XOVER Command Succeeded")]
        public event EventHandler OnDownloadHeaderSucceeded;


        public List<string> DownloadHelpCommands()
        {
            help_commands = new List<string>();

            // Setup the network stream
            NetworkStream ns = this.tcpServer.GetStream();
            StreamReader sr = new StreamReader(ns, System.Text.Encoding.Default);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; 
             
            sw.WriteLine("Help"); 
            

            int i = 0;

            Boolean done = false;

            do
                {
                    
                    string article = sr.ReadLine();    // Read the Header of the article

                    if (  (article == null) && (i == 0) )
                    {
                        return null;
                    }

                    i++;

                    if (article == null)        // NULL response from server
                    {
                        return help_commands;
                    }

                    if (article == ".")     // Check for finish
                    {
                        done = true;
                    }
                    else
                    {
                        help_commands.Add(article);
                    }

                } while (!done);

            return help_commands; 
         
        }

        public List<string> Retrieve_100_Headers(string group)
        {
            NetworkStream ns = this.tcpServer.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; 

            sw.WriteLine("GROUP " + group);

            int estMsgCount = 0;
            int low = 0;
            int high = 0;

            string strResponse = sr.ReadLine();
            if (strResponse == null)
            {
                error_msg = "GROUP : NULL Response received from server";
                return null;
            }
            else if (strResponse.StartsWith("211"))
            {
                string[] MessageNumbers;
                MessageNumbers = strResponse.Split(' ');
                estMsgCount = Convert.ToInt32(MessageNumbers[1]);
                low = Convert.ToInt32(MessageNumbers[2]);
                high = Convert.ToInt32(MessageNumbers[3]); 
            }
            else if (strResponse.StartsWith("411"))
            {
                error_msg = "Msg 411, group is null";
                 
                return null;
            }
            else  
            {
                error_msg = "Msg:" + strResponse;

                return null;
            }


            last_100_headers = new List<string>();

            string XOVERCommand = "XOVER " + (high - 100).ToString() + "-" + high.ToString() ;

            sw.WriteLine(XOVERCommand);             // Execute the XOVER command  

            string response = sr.ReadLine();        // Retrieve Response

            int i = 0;
            if (response == null)
            {
                return null;
            }

            switch (response.Substring(0, 3))
            {

                case "224":

                    bool done = false;
                    string article;
                    do
                    {
                        
                        article = sr.ReadLine();    // Read the Header of the article
                        if (  (article == null) && (i == 0)   )        // NULL response from server
                        {
                            return null;
                        }


                        if (article == ".")     // Check for finish
                            done = true;
                        else
                        {
                            i++;

                            article.Replace("\t\t", "\t<empty>\t");     // take care of empty fields
                            string[] fields = article.Split('\t');  // break up the server response
                            int len = fields.Length;

                            string DateString = (DateTime.Parse(fields[3].Substring(0, 24))).ToString();

                            last_100_headers.Add(fields[0] + " || " + fields[1] + " || " + fields[2] + " || " + DateString + " || " + fields[4]);


                            if (i > 100)
                            {
                                i++;
                                done = true;
                            }
                        }
                             
                    } while (!done);

                    string a = sr.ReadLine(); 

                    break;

                default:
                    return null; 
            }

            return last_100_headers ;
        }


        public List<string> Retrieve_By_dates(string group)
        {
            List<string> posts = new List<string>();

            NetworkStream ns = this.tcpServer.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;

            string parm1 = "newnews soc.culture.polis* " + Globals.date_from.ToString("yyyyMMdd") + " 000000 ";
            sw.WriteLine(parm1); 

            string Response = sr.ReadLine();
            if (Response == null)
            {
                error_msg = "GROUP : NULL Response received from server";
                return null;
            } 

            bool done = false;
            string article;

            int i = 0;
            switch (Response.Substring(0, 3))
            {
                case "230":
                    do
                    {

                        article = sr.ReadLine();    // Read the Header of the article
                        if ((article == null) && (i == 0))        // NULL response from server
                        {
                            error_msg = "null after 230 response";
                            return null;
                        }


                        if (article == ".")     // Check for finish
                            done = true;
                        else
                        { 
                            
                            parm1 = "ARTICLE " + article + " ";
                            sw.WriteLine(parm1);

                            article = sr.ReadLine();

                            article.Replace("\t\t", "\t<empty>\t");     // take care of empty fields
                            string[] fields = article.Split('\t');  // break up the server response
                            int len = fields.Length;

                            DateTime DateString = (DateTime.Parse(fields[3].Substring(0, 24)));

                            if (DateString > Globals.date_to)
                            {
                                continue;
                            }

                            posts.Add(fields[0] + " || " + fields[1] + " || " + fields[2] + " || " + DateString + " || " + fields[4]);

                            int j = 0;
                        }

                       

                        i++;

                    } while (!done); 
                     

                    break;

                default:
                    return null;
            }

            return posts;
        }

        public List<Email> Retrieve_10000_Headers(string group)
        {
            NetworkStream ns = this.tcpServer.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;

            sw.WriteLine("GROUP " + group);

            int estMsgCount = 0;
            int low = 0;
            int high = 0;

            string strResponse = sr.ReadLine();
            if (strResponse == null)
            {
                error_msg = "GROUP : NULL Response received from server";
                return null;
            }
            else if (strResponse.StartsWith("211"))
            {
                string[] MessageNumbers;
                MessageNumbers = strResponse.Split(' ');
                estMsgCount = Convert.ToInt32(MessageNumbers[1]);
                low = Convert.ToInt32(MessageNumbers[2]);
                high = Convert.ToInt32(MessageNumbers[3]);
            }
            else if (strResponse.StartsWith("411"))
            {
                error_msg = "Msg 411, group is null";

                return null;
            }
            else
            {
                error_msg = "Msg:" + strResponse;

                return null;
            }


            last_100_headers = new List<string>();

            Email post;

            all_posts = new List<string>();


            string XOVERCommand = "XOVER " + (high - 10000).ToString() + "-" + high.ToString();

            sw.WriteLine(XOVERCommand);             // Execute the XOVER command  

            string response = sr.ReadLine();        // Retrieve Response

            int i = 0;
            if (response == null)
            {
                return null;
            }

            switch (response.Substring(0, 3))
            {

                case "224":

                    bool done = false;
                    string article;
                     
                    do
                    {

                        article = sr.ReadLine();    // Read the Header of the article
                        if ((article == null) && (i == 0))        // NULL response from server
                        {
                            return null;
                        }


                        if (article == ".")     // Check for finish
                            done = true;
                        else
                        {
                            i++;

                            article.Replace("\t\t", "\t<empty>\t");     // take care of empty fields
                            string[] fields = article.Split('\t');  // break up the server response
                            int len = fields.Length;

                            string DateString = (DateTime.Parse(fields[3].Substring(0, 24))).ToString();

                            DateTime dt = (DateTime.Parse(fields[3].Substring(0, 24))) ;

                            if  (dt < Globals.date_from)
                            {
                                continue;
                            }


                            if (dt > Globals.date_to)
                            {
                                continue;
                            }
                            // fields[0] - msg id, fields[1] - subject, fields[2] - email, fields[3] - date

                            last_100_headers.Add(fields[0] + " || " + fields[1] + " || " + fields[2] + " || " + DateString + " || " + fields[4]);

                            all_posts.Add(fields[2]);
                        }

                    } while (!done);

                     var noduplicates = all_posts.Distinct().ToList();

                     var GroupByMS = all_posts.GroupBy(s => s);

                    results_unsorted = new List<Email>();

                    foreach (var x in GroupByMS)
                    {
                        string email = x.Key.ToString();
                        int nbr_of_posts = x.Count();
                        Email e = new Email(email, nbr_of_posts);
                        results_unsorted.Add(e);

                    }

                    var sorted = results_unsorted.OrderBy(k => k.nbr_posts).Reverse().ToList();

                    results_sorted = (List<Email>)sorted;

                    break;

                default:
                    return null;
            }

            return results_sorted;
        }






        /// <summary>
        /// Containing Details about one newsgroup
        /// </summary>
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

        /// <summary>
        /// Utility Functions
        /// </summary>
        public class Utils
        {
            /// <summary>
            /// Private Constructors to prevent this class from being instantiated
            /// </summary>
            private Utils()
            { }

            /// <summary>
            /// Parse the strings in the newsgroup article's header; Encoding-aware
            /// </summary>
            /// <param name="s">The header string to be decoded</param>
            /// <returns>The decoded string in the system default encoding</returns>
            public static string DecodeHeaderString(string s)
            {
                Match header = Regex.Match(s, @".?=\?(.+)\?(.+)\?(.+)\?=");
                if (header.Success)
                {
                    string charset = header.Groups[1].Value;
                    string user_encoding = header.Groups[2].Value;
                    string text = header.Groups[3].Value;
                    byte[] bytArray;

                    switch (user_encoding)
                    {
                        case "B":
                            bytArray = Convert.FromBase64String(text);
                            break;
                        case "Q":

                            bytArray = Utils.GetByteArray(text);
                            break;
                        default:
                            bytArray = Encoding.Default.GetBytes(text);
                            break;
                    }

                    Encoding encoding = System.Text.Encoding.Default;
                    text = encoding.GetString(bytArray);

                    int index = s.IndexOf("=?");
                    string str = s.Substring(0, index);

                    if (index != 0)
                        text = string.Join("", new string[] { s.Substring(0, index), text });

                    int end;
                    if (s.EndsWith("?=") == false)
                    {
                        end = s.LastIndexOf("?=");
                        string strEnd = s.Substring(end + 2, s.Length - end - 2);
                        text = string.Join("", new string[] { text, strEnd });
                    }

                    return text;
                }
                else
                    return s;
            }

            /// <summary>
            /// Convert a Quoted Printable string into Byte Array
            /// </summary>
            /// <param name="s">string encoded in QP format</param>
            /// <returns>A byte array containing each character in 
            /// the string parameter in byte format</returns>
            public static byte[] GetByteArray(string s)
            {
                byte[] buffer = new byte[s.Length];

                int bufferPosition = 0;
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] == '=')
                    {
                        if (s[i + 1] == '\r' && s[i + 2] == '\n')
                            bufferPosition--;
                        else
                            buffer[bufferPosition] = System.Convert.ToByte(s.Substring(i + 1, 2), 16);
                        i += 2;
                    }
                    else if (s[i] == '_')
                        buffer[bufferPosition] = 32;
                    else
                        buffer[bufferPosition] = (byte)s[i];
                    bufferPosition++;
                }
                byte[] newArray = new byte[bufferPosition];
                Array.Copy(buffer, newArray, bufferPosition);
                return newArray;
            }
        }

        /// <summary>
        /// Collection of Article Headers
        /// </summary>
        public class ArticleHeaderCollection : System.Collections.Specialized.NameObjectCollectionBase
        {
            /// <summary>
            /// Gets / Sets the value of the first entry with the specified key
            /// </summary>
            public ArticleHeader this[string name]
            {
                get
                {
                    return (ArticleHeader)this.BaseGet(name);
                }
                set
                {
                    this.BaseSet(name, value);
                }
            }

            /// <summary>
            /// Gets / Sets the value of the entry at the specified index.  If asIndex is true, 
            /// index is used as a zero-based index to the collection.  If false, index 
            /// represents the message id and is used as a string key.
            /// </summary>
            public ArticleHeader this[int index, bool asIndex]
            {
                get
                {
                    if (asIndex)
                        return (ArticleHeader)this.BaseGet(index);
                    else
                        return (ArticleHeader)this.BaseGet(index.ToString());
                }
                set
                {
                    if (asIndex)
                        this.BaseSet(index, value);
                    else
                        this.BaseSet(index.ToString(), value);
                }
            }

            /// <summary>
            /// Adds an entry with the specified key and value
            /// </summary>
            /// <param name="messageID">The String key of the entry to add</param>
            /// <param name="header">The ArticleHeader value of the entry to add</param>
            public void Add(int messageID, ArticleHeader header)
            {
                this.BaseAdd(messageID.ToString(), header);
            }


            /// <summary>
            /// Returns an array of ArticleHeader that contains all the values in the collection
            /// </summary>
            /// <returns>Newsgroup_Application.ArticleHeader[]</returns>
            public ArticleHeader[] GetArticleList()
            {
                return (ArticleHeader[])this.BaseGetAllValues(typeof(ArticleHeader));
            }

            /// <summary>
            /// Removes all entries from the collection
            /// </summary>
            public void Clear()
            {
                this.BaseClear();
            }

            /// <summary>
            /// Remove one single entry from the collection
            /// </summary>
            /// <param name="index">Index of the entry</param>
            /// <param name="asInt">Whether the index is to be used as a zero-based index
            /// or a string key</param>
            public void RemoveAt(int index, bool asInt)
            {
                if (asInt)
                    this.BaseRemoveAt(index);
                else
                    this.BaseRemove(index.ToString());
            }

        }

    }
}

