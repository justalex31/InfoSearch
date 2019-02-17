using System;

namespace InfoSearch.Model
{
    public partial class Articles
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Keywords { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public Guid? StudentId { get; set; }

        public virtual Students Student { get; set; }
    }
}
