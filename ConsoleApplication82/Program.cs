using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication82
{
    class Program
    {
        public class MatrixObj
        {
            private Random _random = new Random();

            private static readonly object _locker = new object();
            public void Start(object rowNumber)
            {
                var stripLength = new Random().Next(3, 16);
                var windowHeight = Console.WindowHeight;
                int stripHeadIndex = 0;

                for (int i = 0; i < windowHeight + stripLength; i++)
                {
                    stripHeadIndex++; 

                    //Печатает строки с 0 по stripHeadIndex(не включительно)
                    for (int j = 0; j < stripHeadIndex; j++) //j 
                    {
                        bool headGone = stripHeadIndex >= windowHeight;

                        if (!headGone)
                        {
                            if (IsStripBody(stripLength, stripHeadIndex, j))
                                PrintCyber(_random, (int)rowNumber, j, stripHeadIndex, stripLength);
                            else
                                Console.WriteLine();
                        }
                        else
                        {
                            var realaliveTailIndex = stripHeadIndex - j - stripLength;

                            if (j >= windowHeight)
                                continue;
                            if (realaliveTailIndex >= 0)
                                Console.WriteLine();
                            else
                                PrintCyber(_random, (int)rowNumber, j, stripHeadIndex, stripLength);
                        }
                    }

                    Thread.Sleep(500);
                    Console.Clear();
                }
            }
        }

        private static void PrintCyber(Random rand, int rowNumber, int j, int stripHeadIndex, int stripLength)
        {
            SetColor(j, stripHeadIndex, stripLength);

            Console.CursorLeft = rowNumber;

            Console.WriteLine(rand.Next(9));
        }

        private static void SetColor(int j, int stripHeadIndex, int stripLength)
        {
            if (j == stripHeadIndex - 1)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (j == stripHeadIndex - 2)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (IsStripBody(stripLength, stripHeadIndex, j))
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            }
        }

        private static bool IsStripBody(int stripLength, int stripHeadIndex, int j)
        {
            return stripHeadIndex - j - stripLength <= 0;
        }

        static void Main(string[] args)
        {
            //var matrixObj = new MatrixObj();


            var thread = new Thread(new MatrixObj().Start);
            thread.Start(0);



            Console.ReadKey();

        }
    }
}
