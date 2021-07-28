using System;
using System.Collections.Generic;

#nullable disable

namespace PostmanApiTrial.Models
{
    public partial class teacher
    {
        public int t_id { get; set; }
        public string t_name { get; set; }
        public int? t_age { get; set; }
        public int? s_id { get; set; }
        public virtual student s_idNavigation { get; set; }
    }
}
