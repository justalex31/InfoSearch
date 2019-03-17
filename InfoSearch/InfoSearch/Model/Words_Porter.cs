using System;
using System.Collections.Generic;
using System.Text;

namespace InfoSearch.Model
{
    public class Words_Porter : BaseModel
    {
        public string Term { get; set; }
        public Guid? ArticleId { get; set; }

        public virtual Articles Article { get; set; }
    }
}
