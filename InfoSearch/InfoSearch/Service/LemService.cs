using Microsoft.EntityFrameworkCore;
using MyStemWrapper;
using PorterStemmer;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfoSearch.Service
{
    public class LemService : BaseContext
    {
        public void Mystem(ArtService artService)
        {
            Console.WriteLine("Configuration (mystem)");

            var stem = new MyStem
            {
                PathToMyStem = @"C:\Users\Алексей\source\repos\InfoSearch\InfoSearch\Shared\mystem.exe"
            };

            Console.WriteLine("Configurated (mystem)");
            Console.WriteLine("Start mystem");

            var listWords = artService.GetListWords();

            foreach (var t in listWords)
            {
                foreach (var n in t.Value)
                {
                    var rec = stem.Analysis(n);
                    var ind_st = rec.IndexOf("{");
                    var ind_en = rec.IndexOf("}");

                    if (rec != string.Empty && rec != null && rec.Length >= ind_en)
                    {
                        var sub_str = rec.Substring(ind_st + 1, ind_en - ind_st - 1).Replace("?", "").Split("|");

                        if (sub_str[0] != string.Empty)
                        {
                            context.Words_MyStems.Add(new Model.Words_MyStem
                            {
                                Id = Guid.NewGuid(),
                                Term = sub_str[0],
                                ArticleId = t.Key
                            });
                            context.SaveChanges();
                        }
                    }
                }
            }

            Console.WriteLine("End mystem");
        }

        public void Porter(ArtService artService)
        {
            Console.WriteLine("Start porter");
            var listWords = artService.GetListWords();

            foreach (var t in listWords)
            {
                foreach (var n in t.Value)
                {
                    if (n != null && n != string.Empty)
                    {
                        var rec = "";
                        try
                        {
                            rec = n.GetStem();

                            context.Words_Porters.Add(new Model.Words_Porter
                            {
                                Id = Guid.NewGuid(),
                                Term = rec,
                                ArticleId = t.Key
                            });

                            context.SaveChanges();

                        }
                        //костыль: библиотека авторская из NuGet, поэтому не всегда удается определить и бросает ошибку 
                        catch (Exception ex) { }
                    }
                }
            }

            Console.WriteLine("End porter");
        }
    }
}
