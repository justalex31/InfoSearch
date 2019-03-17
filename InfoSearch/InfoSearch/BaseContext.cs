using InfoSearch.AppData;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfoSearch
{
    public class BaseContext
    {
        protected ApplicationDbContext context = new ApplicationDbContext();
    }
}
