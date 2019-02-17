using System;
using System.Collections.Generic;

namespace InfoSearch.Model
{
    public partial class Students
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mygroup { get; set; }

        public virtual ICollection<Articles> Articles { get; set; }

        public Students()
        {
            Articles = new HashSet<Articles>();
        }
    }
}
