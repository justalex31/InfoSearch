using System;
using System.Collections.Generic;
using System.Text;

namespace InfoSearch.Model
{
    public class Term_List : BaseModel
    {
        public string Term_Text { get; set; }

        public virtual ICollection<Article_Term> Article_Terms { get; set; }

        public Term_List() => Article_Terms = new HashSet<Article_Term>();
    }
}
