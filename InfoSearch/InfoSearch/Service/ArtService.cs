using HtmlAgilityPack;
using InfoSearch.AppData;
using InfoSearch.Helper;
using InfoSearch.Model;
using System;
using System.Linq;

namespace InfoSearch.Service
{
    public class ArtService
    {
        ApplicationDbContext context = new ApplicationDbContext();
        HtmlWeb web = new HtmlWeb();
        string url = "https://habr.com/ru/post/";

        public void FillData(string[] listPost)
        {
            if (!context.Articles.Any()) { 

            var stud_id = context.Students.FirstOrDefault();

                foreach (var ob in listPost)
                {
                    HtmlDocument doc = web.Load(url + ob + "/");

                    var article = new Articles()
                    {
                        Id = Guid.NewGuid(),
                        Title = doc.DocumentNode.SelectSingleNode("//title").InnerHtml,
                        Url = url + ob + "/",
                        StudentId = stud_id.Id
                    };

                    string content = "";
                    foreach (HtmlNode row in doc.DocumentNode.SelectNodes("//div[@class='post__text post__text-html js-mediator-article']"))
                            content = content + row.InnerText;

                    content.Replace("\n", " ");
                    content.Replace("\r", " ");
                    content.Replace("\t", " ");

                    article.Content = content.Trim().Substring(0, 255);

                    var inht = doc.DocumentNode.SelectSingleNode("//meta[@name=\"keywords\"]").Attributes["content"].Value;

                    inht.Replace(",", ";");

                    var ite = inht.Length < 255 ? inht.Substring(0, inht.Length) : inht.Substring(0, 255);

                    article.Keywords = ite;

                    context.Articles.Add(article);

                    context.SaveChanges();
                }
            }
        }

        public void CreateStud()
        {
            if (!context.Students.Any())
            {
                var stud = new Students()
                {
                    Id = Guid.NewGuid(),
                    Name = "Alexey",
                    Surname = "Kleshevnikov",
                    Mygroup = "11-501"
                };

                context.Students.Add(stud);
                context.SaveChanges();
            }
        }
    }
}
