﻿using System;
using System.Threading;
namespace tcp
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread t = new Thread(delegate ()
            {
                Server myserver = new Server("127.0.0.1", 9000);
            });
            t.Start();

            Console.WriteLine("Server Started...!");
        }
    }
}
