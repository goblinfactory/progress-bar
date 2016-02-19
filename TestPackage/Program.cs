﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goblinfactory.ProgressBar;

namespace TestPackage
{
    class Program
    {
        static void Main(string[] args)
        {
            var pb = new ProgressBar(50);
            pb.Refresh(0, "connecting to server to download 50 files sychronously.");
            Console.ReadLine();

            pb.Refresh(25, "downloading file number 25");
            Console.ReadLine();
            pb.Refresh(50, "finished.");
        }
    }
}