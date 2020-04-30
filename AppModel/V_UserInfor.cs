using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppBoxPro
{
    public class V_UserInfor
    {
        public int ID { get; set; }
        public string userid { get; set; }
        public string ChineseName { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public bool? Enabled { get; set; }
        public string Gender { get; set; }
        public string deptname { get; set; }
        public int? deptid { get; set; }
        public int? RoleID { get; set; }
        public string rolename { get; set; }
        public string jobname { get; set; }
        public int? jobid { get; set; }
    }
}