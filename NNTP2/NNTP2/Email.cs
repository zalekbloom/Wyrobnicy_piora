using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNTP2
{
    public class Email
    {
        public string email_address { get; set; }
        public int nbr_posts { get; set; }

        public Email(string email_addr, int cnt)
        {
            email_address = email_addr;
            nbr_posts = cnt;
        }

         
    }
}
