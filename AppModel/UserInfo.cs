using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBoxPro
{
    public class UserInfo
    {
        [Key]
        public int userID { get; set; }

        public int userNO { get; set; }//
        public string userName { get; set; }
        public string jobName { get; set; }
        public string partID { get; set; }
        public string useridno { get; set; }
    }
}