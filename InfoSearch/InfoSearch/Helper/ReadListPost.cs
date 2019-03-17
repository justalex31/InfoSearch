using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InfoSearch.Helper
{
    public class ReadListPost
    {
        static readonly string uri = @"C:\Users\Алексей\source\repos\InfoSearch\InfoSearch\Posts.txt";
        static readonly string stop_words = @"C:\Users\Алексей\source\repos\InfoSearch\InfoSearch\Shared\stopwords.txt";

        public static string[] Read()
        {
            var fileInfo = new FileInfo(uri);

            if (fileInfo.Exists)
            {
                List<string> list = new List<string>();
                using (StreamReader sr = new StreamReader(uri, Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                        list.Add(line);
                }

                return list.ToArray();
            }

            throw new Exception("File not found");
            
        }

        public static string[] ReadStopWords()
        {
            var fileInfo = new FileInfo(stop_words);

            if (fileInfo.Exists)
            {
                var list = new List<string>();
                using (StreamReader sr = new StreamReader(uri, Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                        list.Add(line.ToLower().Trim());
                }

                return list.ToArray();
            }

            throw new Exception("File not found");
        }
    }
}
