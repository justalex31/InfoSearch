using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InfoSearch.Helper
{
    public class ReadListPost
    {
        static string uri = @"C:\Users\Алексей\source\repos\InfoSearch\InfoSearch\Posts.txt";

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
            else throw new Exception("Файл не найден");
            
        }
    }
}
