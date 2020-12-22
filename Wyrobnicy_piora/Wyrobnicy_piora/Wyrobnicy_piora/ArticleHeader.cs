using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wyrobnicy_piora
{
    public class ArticleHeader
    {

        /// <summary>
        /// The message ID of the newsgroup message
        /// </summary>
        protected string articleID;
        /// <summary>
        /// Sender of the message
        /// </summary>
        protected string from;
        /// <summary>
        /// Subject of the message
        /// </summary>
        protected string subject;
        /// <summary>
        /// Date the message was sent.  Time is recorded using 
        /// the server time.  Hence, there might be differences
        /// if the user and the server are in different time zones
        /// </summary>
        protected string dateString;

        public string ArticleID
        {
            get
            { return this.articleID; }
            set
            { this.articleID = value; }
        }

        public string From
        {
            get
            { return this.from; }
            set
            { this.from = value; }
        }

        public string Subject
        {
            get
            { return this.subject; }
            set
            { this.subject = value; }
        }

        public string DateString
        {
            get
            { return this.dateString; }
            set
            { this.dateString = value; }
        }

        /// <summary>
        /// Generate a string representation of the header article
        /// </summary>
        /// <returns>string</returns>
        public new string ToString()
        {
            return string.Format("{0} : {1} by {2} on {3}",
                this.articleID, this.subject, this.from, this.dateString);
        }
    }
}
