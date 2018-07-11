using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DB
{
    public class Category
    {
        public int c_id { get; set; }
        public string c_name { get; set; }
        public string operate_by { get; set; }
        public string img_path { get; set; }
        public bool is_enable { get; set; }
        public DateTime create_at { get; set; }
        public DateTime operate_at { get; set; }
    }
}
