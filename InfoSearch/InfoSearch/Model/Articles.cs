using System;
using System.Collections.Generic;

namespace InfoSearch.Model
{
    public partial class Articles : BaseModel
    {
        public string Title { get; set; }
        public string Keywords { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public Guid? StudentId { get; set; }

        public virtual Students Student { get; set; }

        public virtual ICollection<Words_MyStem> Words_MyStems { get; set; }

        public virtual ICollection<Words_Porter> Words_Porters { get; set; }

        public Articles()
        {
            Words_MyStems = new HashSet<Words_MyStem>();
            Words_Porters = new HashSet<Words_Porter>();
        }
    }
}
