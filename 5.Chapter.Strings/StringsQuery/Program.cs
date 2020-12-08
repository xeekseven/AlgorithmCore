﻿using System;

namespace StringsQuery
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] a = "she sells seashells by the sea shore the shells she sells are surely seashells".Split(' ');
            var trieST = new TrieST<int>(256);
            for (int i = 0; i < a.Length; i++)
            {
                trieST.Put(a[i], i);
            }

            Console.WriteLine("Contains:" + trieST.Contains("slls"));
            Console.WriteLine("KeysThatMach:" + string.Join("|", trieST.KeysThatMach("she...")));

            Console.WriteLine("GetValue:" + trieST.GetValue("sea"));

            Console.WriteLine(KMP.Search("she sells seashells by the sea shore the shells she sells are surely seashells".Replace(" ", ""), "are"));

            BoyerMooreQuery boyerMoore = new BoyerMooreQuery("are");
            Console.WriteLine(boyerMoore.Search("she sells seashells by the sea shore the shells she sells are surely seashells".Replace(" ", "")));

            RabinKarp rk = new RabinKarp("are");
            Console.WriteLine(rk.Search("she sells seashells by the sea shore the shells she sells are surely seashells".Replace(" ", "")));
        }
    }
}
