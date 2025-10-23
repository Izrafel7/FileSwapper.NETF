using System;
using System.IO;
using System.Linq;

namespace FileSwapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var filesToSwap = Directory.GetFiles(".\\").Where(filename => filename != ".\\FileSwapper.exe");

            if (filesToSwap.Count() > 0)
            {
                SwapManager swap = new SwapManager();

                if(swap.init())
                {
                    if(!swap.swapFiles(filesToSwap))
                    {
                        Console.WriteLine("Files swap failed");
                    }
                }
                else
                {
                    Console.WriteLine("No permission");
                }
            }
        }
    }
}
