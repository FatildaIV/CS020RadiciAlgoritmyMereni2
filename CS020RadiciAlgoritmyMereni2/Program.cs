using System;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace CS019RadiciAlgoritmyMereni
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamWriter sw = new StreamWriter("vysledky.csv");
            var start = HighResolutionDateTime.UtcNow;
            //var sw = Stopwatch.StartNew();

            ////while (sw.Elapsed.TotalSeconds < 10)
            ////{
            ////    DateTime nowBasedOnStopwatch = start + sw.Elapsed;
            ////    TimeSpan diff = HighResolutionDateTime.UtcNow - nowBasedOnStopwatch;

            ////    Console.WriteLine("Diff: {0:0.000} ms", diff.TotalMilliseconds);  

            ////    Thread.Sleep(1000);
            ////}
            int a = 7518963, b = -736669222, n = 10;

            start = HighResolutionDateTime.UtcNow;
            for (int i = 0; i < 10000000; i++)
                ProhoditPromenna<int>(ref a, ref b);

            sw.WriteLine("Pocet položek; Bubble Sort; Selection Sort; Insertion Sort");
            while (n <= 50000)
            {
                sw.Write("{0};", n);
                Console.Write("Řazení {0} položek...", n);

                Console.Write("Bubble Sort...");
                int[] cisla = CislaNahodne(n, 1, n);
                start = HighResolutionDateTime.UtcNow;
                BubbleSort<int>(ref cisla);
                sw.Write("{0:0.0000};", (HighResolutionDateTime.UtcNow - start).TotalMilliseconds);

                Console.Write("Selection Sort...");
                cisla = CislaNahodne(n, 1, n);
                start = HighResolutionDateTime.UtcNow;
                SelectionSort<int>(ref cisla);
                sw.Write("{0:0.0000};", (HighResolutionDateTime.UtcNow - start).TotalMilliseconds);
                

                Console.Write("Insertion Sort...");
                cisla = CislaNahodne(n, 1, n);
                start = HighResolutionDateTime.UtcNow;
                InsertionSort<int>(ref cisla);
                sw.Write("{0:0.0000};", (HighResolutionDateTime.UtcNow - start).TotalMilliseconds);
                sw.WriteLine();

                sw.WriteLine();
                Console.WriteLine("hotovo");
                n *= 2;
            }

            //for (int i = 0; i < 10000000; i++)
            //    ProhoditAritmeticky<int>(ref a, ref b);

            //for (int i = 0; i < 10000000; i++)
            //    ProhoditBitove<int>(ref a, ref b);

            //sw.WriteLine("Pocet prohozeni; Prohozeni promenou; Prohozeni aritmeticky; Prohozeni bitove");
            //while (n <= 100000000)
            //{

            //    sw.Write("{0};", n);
            //    Console.Write(".");

            //    start = HighResolutionDateTime.UtcNow;
            //    for (int i = 0; i < n; i++)
            //        ProhoditPromenna<int>(ref a, ref b);
            //    sw.Write("{0:0.0000};", (HighResolutionDateTime.UtcNow - start).TotalMilliseconds);
            //    Console.Write(".");

            //    start = HighResolutionDateTime.UtcNow;
            //    for (int i = 0; i < n; i++)
            //        ProhoditAritmeticky<int>(ref a, ref b);
            //    sw.Write("{0:0.0000};", (HighResolutionDateTime.UtcNow - start).TotalMilliseconds);
            //    Console.Write(".");

            //    start = HighResolutionDateTime.UtcNow;
            //    for (int i = 0; i < n; i++)
            //        ProhoditBitove<int>(ref a, ref b);
            //    sw.Write("{0:0.0000};", (HighResolutionDateTime.UtcNow - start).TotalMilliseconds);
            //    Console.Write(".");

            //    sw.WriteLine();
            //    n *= 10;                    // Zvýšit N 10násobně
            //}


            sw.Close();

            //int[] cisla = CislaNahodne(5, 1, 5); // použití 
            //InsertionSort<int>(ref cisla);
            Console.WriteLine("Měření dokončeno");
        }

        static int[] CislaVzestupne(int pocet)
        {
            int[] cisla = new int[pocet];       // deklarace

            for (int i = 0; i < pocet; i++)     // inicializace
                cisla[i] = i + 1;

            return cisla;                       // vrácení
        }

        static int[] CislaNahodne(int pocet, int minimum, int maximum)
        {
            int[] cisla = new int[pocet];       // deklarace
            var gen = new Random(0);
            for (int i = 0; i < pocet; i++)     // inicializace
                cisla[i] = gen.Next(minimum, maximum);

            return cisla;                       // vrácení
        }

        static void ProhoditPromenna<T>(ref T a, ref T b)
        {
            T temp = a;     // do temp ulozit a
            a = b;          // do a ulozit b
            b = temp;       // do b ulozit temp
        }

        static void ProhoditAritmeticky<T>(ref T a, ref T b)
        {
            a = (dynamic)a - (dynamic)b;  // snížit a o velikost b
            b = (dynamic)b + (dynamic)a;  // zvýšit b o rozdil mezi a a b
            a = (dynamic)b - (dynamic)a;  // dostat do a původní hodnotu b
        }

        static void ProhoditBitove<T>(ref T a, ref T b)
        {
            a = (dynamic)a ^ (dynamic)b;  // do a bitový XOR mezi a a b
            b = (dynamic)b ^ (dynamic)a;  // do b bitový XOR mezi b a a
            a = (dynamic)b ^ (dynamic)a;  // do a bitový XOR mezi b a a
        }
        static void BubbleSort<T>(ref T[] pole)
        {
            for (int i = 0; i < pole.Length; i++)
                for (int j = 0; j < pole.Length - 1; j++)
                    if ((dynamic)pole[j] < (dynamic)pole[j + 1])
                        ProhoditPromenna<T>(ref pole[j], ref pole[j + 1]);
        }
        static void SelectionSort<T>(ref T[] pole)
        {
            for (int i = 0; i < pole.Length - 1; i++)
            {  
                int maxIndex = i;
                for (int j = i + 1; j < pole.Length; j++)
                { 
                    if ((dynamic)pole[j] < (dynamic)pole[maxIndex])
                        maxIndex = j;
                    ProhoditPromenna<T>(ref pole[i], ref pole[maxIndex]);  
                }
            }
        }
               
        public static void InsertionSort<T>(ref T[] pole)
        {
            for (int i = 0; i < pole.Length - 1; i++)
            {
                int j = i + 1;
                T tmp = pole[j];
                while (j > 0 && ((dynamic)tmp < (dynamic)pole[j - 1]))
                {
                    pole[j] = pole[j - 1];
                    j--;
                }
                pole[j] = tmp;
            }
            
        }
    
    }
}
