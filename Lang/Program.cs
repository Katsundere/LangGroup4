using System;
using System.IO;
using System.Diagnostics;

namespace Lang
{
    class Program
    {
        static void Main(string[] args)
        {

            void printArray(int[] array, int length)
            {
                if (length > 20)
                {
                    length = 20;
                }
                for (int x = 0; x < length; x++)
                {
                    Console.Write("{0} ", array[x]);
                }
                Console.WriteLine();
            }

            //Returns larger number of a and b
            int max(int a, int b)
            {
                if (a > b)
                {
                    return a;
                }
                return b;
            }

            int findDivisor(int a, int b)
            {
                int divisor;
                int result;
                divisor = max(a, b) - 1;
                while (divisor >= 1)
                {
                    if ((a % divisor == 0 || a % divisor == 1)
                        && (b % divisor == 0 || b % divisor == 1))
                    {
                        return divisor;
                    }
                    divisor--;
                }
                return divisor;
            }


            Stopwatch watch = new Stopwatch();
            watch.Start();
            Console.Clear();

            string nPath = Path.Combine(Environment.CurrentDirectory, "nFile.txt");
            string mPath = Path.Combine(Environment.CurrentDirectory, "mFile.txt");
            string nLine = "";
            string mLine = "";

            //Open M file and read line
            try
            {
                mLine = File.ReadAllText(mPath);
                Console.WriteLine("M file successfully opened!");
            }
            catch (Exception e)
            {
                Console.WriteLine("The M file could not be read!");
                Console.WriteLine(e);
            }

            //Open N file and read line
            try
            {
                nLine = File.ReadAllText(nPath);
                Console.WriteLine("N file successfully opened!");
            }
            catch (Exception e)
            {
                Console.WriteLine("The n file could not be read!");
                Console.WriteLine(e);
            }

            //Split into numbers
            string[] mStrs = mLine.Split(' ');
            int[] mNums = new int[mStrs.Length];
            //Don't do last character because it is an empty space
            for (int x = 0; x < mStrs.Length - 1; x++)
            {
                mNums[x] = int.Parse(mStrs[x]);
            }


            string[] nStrs = nLine.Split(' ');
            int[] nNums = new int[nStrs.Length];
            for (int x = 0; x < nStrs.Length - 1; x++)
            {
                nNums[x] = int.Parse(nStrs[x]);
            }

            if (mNums.Length != nNums.Length)
            {
                Console.WriteLine("Arrays aren't the same size! ");
                return;
            }

            printArray(mNums, mNums.Length);
            printArray(nNums, nNums.Length);

            int arrLength = mNums.Length;
            int y;
            int[] P = new int[arrLength];
            int[] Q = new int[arrLength];
            int[] R = new int[arrLength];
            for(y = 0; y < arrLength; y++)
            {
                P[y] = Math.Abs(mNums[y] - nNums[y]);
            }

            for (y = 0; y < arrLength; y++)
            {
                Q[y] = findDivisor(mNums[y], nNums[y]);
            }

            for (y = 0; y < arrLength; y++)
            {
                R[y] = findDivisor(mNums[y], P[y]);
            }

            printArray(P, arrLength);
            printArray(Q, arrLength);
            printArray(R, arrLength);

            string resultPath = Path.Combine(Environment.CurrentDirectory, "result.txt");
            try
            {
                using(StreamWriter resStr = new StreamWriter(resultPath))
                {
                    for (y = 0; y < arrLength; y++)
                    {
                        resStr.Write(P[y].ToString() + " ");
                    }
                    Console.WriteLine("Wrote to result file!");
                }
            } catch (Exception e)
            {
                Console.WriteLine("Failed to write to result file!");
                Console.WriteLine(e);
            }
            watch.Stop();
            Console.WriteLine("Execution time: {0} ms", watch.ElapsedMilliseconds);
        }
    }
}
