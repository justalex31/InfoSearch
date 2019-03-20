using System;
using System.Collections.Generic;
using System.Text;

namespace InfoSearch.Model
{
    public class Article_Term : BaseModel
    {

        public Guid ArticleId { get; set; }

        public Guid TermId { get; set; }

        public Articles Article { get; set; }

        public Term_List Term { get; set; }
    }
}
