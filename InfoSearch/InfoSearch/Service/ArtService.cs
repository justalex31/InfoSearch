using HtmlAgilityPack;
using InfoSearch.AppData;
using InfoSearch.Helper;
using InfoSearch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace InfoSearch.Service
{
    public class ArtService : BaseContext
    {
        HtmlWeb web = new HtmlWeb();
        readonly string url = "https://habr.com/ru/post/";

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

                    article.Content = content.Trim();

                    var inht = doc.DocumentNode.SelectSingleNode("//meta[@name=\"keywords\"]").Attributes["content"].Value;

                    inht.Replace(",", ";");

                    var ite = inht.Length < 255 ? inht.Substring(0, inht.Length) : inht.Substring(0, 255);

                    article.Keywords = ite;

                    context.Articles.Add(article);

                    context.SaveChanges();
                }
            }
        }

        public Dictionary<Guid, List<string>> GetListWords()
        {
            var list = new Dictionary<Guid, List<string>>();

            var stopWords = ReadListPost.ReadStopWords().ToList();

            if (!context.Articles.Any())
                throw new Exception("Database is empty");

            var articles = context.Articles;

            foreach (var article in articles)
            {
                if (article.Content != string.Empty)
                {
                    list.Add(article.Id, new List<string>());

                    foreach (var nv in article.Content.Split(" "))
                    {
                        var t = Regex.Replace(nv.ToLower(), @"[^\w\s]", "");
                        if (CheckString(t, list[article.Id], stopWords))
                            list[article.Id].Add(t);
                    }
                }
            }

            return list;
        }

        private bool CheckString(string t, List<string> list, List<string> stopwords)
        {
            return t != string.Empty && !list.Contains(t) && !stopwords.Contains(t);
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
