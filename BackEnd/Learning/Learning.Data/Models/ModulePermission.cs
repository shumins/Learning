﻿using System;
using System.Collections.Generic;

namespace Learning.Data.Models
{
    public partial class ModulePermission
    {
        public int Id { get; set; }
        public bool? IsDeleted { get; set; }
        public int ModuleId { get; set; }
        public int PermissionId { get; set; }
        public int? CreateId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyTime { get; set; }

        public Module Module { get; set; }
        public Permission Permission { get; set; }
    }
}
