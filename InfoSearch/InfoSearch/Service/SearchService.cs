using InfoSearch.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace InfoSearch.Service
{
    public class SearchService : BaseContext
    {
        public static Dictionary<string, List<Guid>> invertedIndex;

        private void AddToIndex(List<string> words, Guid articleId)
        {
            foreach (var word in words)
            {
                if (!invertedIndex.ContainsKey(word))
                {
                    invertedIndex.Add(word, new List<Guid> { articleId });
                }
                else
                {
                    invertedIndex[word].Add(articleId);
                }
            }

            foreach (var pair in invertedIndex)
            {
                var id = Guid.NewGuid();
                context.Term_Lists.Add(new Term_List
                {
                    Id = id,
                    Term_Text = pair.Key
                });

                context.SaveChanges();

                var list = new List<Article_Term>();

                foreach (var t in pair.Value)
                {
                    list.Add(
                        new Article_Term
                        {
                            Id = Guid.NewGuid(),
                            TermId = id,
                            ArticleId = t
                        });                    
                }

                context.SaveChanges();
            }
        }
        
        public void IndexIndex()
        {
            if (!context.Articles.Any() && !context.Article_Terms.Any()) {

                invertedIndex = new Dictionary<string, List<Guid>>();

                var articles = context.Articles.ToList();

                foreach (var a in articles)
                {
                    var content = Regex.Replace(a.Content.ToLower(), @"[^\w\s]", "").Split(" ").Distinct().ToList<string>();
                    AddToIndex(content, a.Id);
                }
            } else {

                var term = context.Term_Lists
                            .Include(x => x.Article_Terms);

                invertedIndex = new Dictionary<string, List<Guid>>();

                foreach (var pair in term)
                {
                    if (!invertedIndex.ContainsKey(pair.Term_Text))
                        invertedIndex.Add(pair.Term_Text, pair.Article_Terms.Select(x => x.Id).ToList());
                    else
                    {
                        invertedIndex[pair.Term_Text].AddRange(pair.Article_Terms.Select(x => x.Id).ToList());
                        invertedIndex[pair.Term_Text].Distinct();
                    }
                }
            }

            Console.WriteLine("Enter your query:");

            string str = Console.ReadLine();
            foreach (var k in invertedIndex.And(str).Keys)
            {
                Console.WriteLine(k);
            }
        }
    }

    public static class Extensions
    {

        public static Dictionary<string, int> And(this Dictionary<string, List<Guid>> index, string str)
        {
            Dictionary<string, int> array_word = new Dictionary<string, int>();

            foreach (var rt in str.Split(" "))
            {
                if (index.Keys.Contains(rt))
                {
                    array_word.Add(rt, index[rt].Count());
                }
            }

            return array_word.OrderBy(key => key.Key).ToDictionary(k => k.Key, y => y.Value);
        }
    }
}
