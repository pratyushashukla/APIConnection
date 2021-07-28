using System;
using System.Collections.Generic;

#nullable disable

namespace PostmanApiTrial.Models
{
    public partial class student
    {
        //public student()
        //{
        //    teachers = new HashSet<teacher>();
        //}

        public int s_id { get; set; }
        public string s_name { get; set; }
        public int? s_age { get; set; }

        //public virtual ICollection<teacher> teachers { get; set; }
    }
}
