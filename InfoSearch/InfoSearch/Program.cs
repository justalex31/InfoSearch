using HtmlAgilityPack;
using InfoSearch.AppData;
using InfoSearch.Helper;
using InfoSearch.Model;
using InfoSearch.Service;
using System;
using System.Linq;

namespace InfoSearch
{
    class Program
    {

        static void Main(string[] args)
        {
            string[] list = ReadListPost.Read();

            ArtService artService = new ArtService();
            artService.CreateStud();
            artService.FillData(list);
        }
    }
}
