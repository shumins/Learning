using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learning.Data.Models
{
    public partial class User
    {
        public User()
        {
            OperateLog = new HashSet<OperateLog>();
            UserRole = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string LoginName { get; set; }
        public string LoginPwd { get; set; }
        public string RealName { get; set; }
        public int Status { get; set; }
        public string Remark { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime LastErrTime { get; set; }
        public int ErrorCount { get; set; }
        public int? Sex { get; set; }
        public int? Age { get; set; }
        public DateTime? Birth { get; set; }
        public string Addr { get; set; }
        public bool? TdIsDelete { get; set; }
        public string HeadImg { get; set; }
        public string Email { get; set; }

        public ICollection<OperateLog> OperateLog { get; set; }
        public ICollection<UserRole> UserRole { get; set; }

        [NotMapped]
        public int RID { get; set; }
        [NotMapped]
        public string RoleName { get; set; }
    }
}
