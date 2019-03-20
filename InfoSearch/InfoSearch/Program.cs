﻿using HtmlAgilityPack;
using InfoSearch.AppData;
using InfoSearch.Helper;
using InfoSearch.Model;
using InfoSearch.Service;
using MyStemWrapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InfoSearch
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Start program");
            ArtService artService = new ArtService();

            //Task #1
            //string[] list = ReadListPost.Read();

            //artService.CreateStud();
            //artService.FillData(list);

            //Task #2 (работает долго много слов)
            //LemService lem = new LemService();
            //lem.Mystem(artService);
            //lem.Porter(artService);

            //Task #3
            SearchService search = new SearchService();
            search.IndexIndex();

            Console.WriteLine("End program");
            Console.ReadKey();
        }
    }
}
