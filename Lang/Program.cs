using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using MPI;

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

            string[] getNumbers(string path)
            {
                string line = "";
                string[] nums;
                try
                {
                    line = File.ReadAllText(path);
                    Console.WriteLine("File successfully opened!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("The file at " + path + "could not be read!");
                    Console.WriteLine(e);
                }
                nums = line.Split(' ');
                return nums;
            }

            Console.Clear();

            Stopwatch watch = new Stopwatch();
            string nPath = Path.Combine(System.Environment.CurrentDirectory, "nFile.txt");
            string mPath = Path.Combine(System.Environment.CurrentDirectory, "mFile.txt");
            string[] mStrs, nStrs;
            int arrLength, x, y;
            int[] mNums, nNums, P, Q, R;
            string resultPath = Path.Combine(System.Environment.CurrentDirectory, "result.txt");

            watch.Start();

            mStrs = getNumbers(mPath);
            nStrs = getNumbers(nPath);

            if (mStrs.Length != nStrs.Length)
            {
                Console.WriteLine("Arrays aren't the same size! ");
                return;
            }

            arrLength = mStrs.Length - 1;

            mNums = new int[arrLength];
            nNums = new int[arrLength];
            P = new int[arrLength];
            Q = new int[arrLength];
            R = new int[arrLength];

            for (y = 0; y < arrLength; y++)
            {
                mNums[y] = int.Parse(mStrs[y]);
                nNums[y] = int.Parse(nStrs[y]);
            }

            printArray(mNums, mNums.Length);
            printArray(nNums, nNums.Length);

            for (y = 0; y < arrLength; y++)
            {
                Thread.Sleep(2);
                P[y] = Math.Abs(mNums[y] - nNums[y]);
                Thread.Sleep(2);
                Q[y] = findDivisor(mNums[y], nNums[y]);
                Thread.Sleep(2);
                R[y] = findDivisor(mNums[y], P[y]);
            }

            printArray(P, arrLength);
            printArray(Q, arrLength);
            printArray(R, arrLength);

            try
            {
                using (StreamWriter resStr = new StreamWriter(resultPath))
                {
                    for (y = 0; y < arrLength; y++)
                    {
                        resStr.Write(P[y].ToString() + " ");
                    }
                    resStr.Write("\n");
                    for (y = 0; y < arrLength; y++)
                    {
                        resStr.Write(Q[y].ToString() + " ");
                    }
                    resStr.Write("\n");
                    for (y = 0; y < arrLength; y++)
                    {
                        resStr.Write(R[y].ToString() + " ");
                    }
                    Console.WriteLine("Wrote to result file!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to write to result file!");
                Console.WriteLine(e);
            }
            watch.Stop();

            Console.WriteLine("Sequential execution time: {0} ms", watch.ElapsedMilliseconds);

            //Parallel version
            watch.Restart();

            mStrs = getNumbers(mPath);
            nStrs = getNumbers(nPath);

            if (mStrs.Length != nStrs.Length)
            {
                Console.WriteLine("Arrays aren't the same size! ");
                return;
            }

            arrLength = mStrs.Length - 1;

            mNums = new int[arrLength];
            nNums = new int[arrLength];
            P = new int[arrLength];
            Q = new int[arrLength];
            R = new int[arrLength];

            Parallel.For(0, mStrs.Length - 1, i =>
            {
                mNums[i] = int.Parse(mStrs[i]);
                nNums[i] = int.Parse(nStrs[i]);
            });

            printArray(mNums, mNums.Length);
            printArray(nNums, nNums.Length);

            Parallel.For(0, arrLength, i =>
            {
                Thread.Sleep(2);
                P[i] = Math.Abs(mNums[i] - nNums[i]);
                Thread.Sleep(2);
                Q[i] = findDivisor(mNums[i], nNums[i]);
                Thread.Sleep(2);
                R[i] = findDivisor(mNums[i], P[i]);
            });

            printArray(P, arrLength);
            printArray(Q, arrLength);
            printArray(R, arrLength);

            try
            {
                using (StreamWriter resStr = new StreamWriter(resultPath))
                {
                    for (y = 0; y < arrLength; y++)
                    {
                        resStr.Write(P[y].ToString() + " ");
                    }
                    resStr.Write("\n");
                    for (y = 0; y < arrLength; y++)
                    {
                        resStr.Write(Q[y].ToString() + " ");
                    }
                    resStr.Write("\n");
                    for (y = 0; y < arrLength; y++)
                    {
                        resStr.Write(R[y].ToString() + " ");
                    }
                    Console.WriteLine("Wrote to result file!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to write to result file!");
                Console.WriteLine(e);
            }
            watch.Stop();

            Console.WriteLine("Parallel execution time: {0} ms", watch.ElapsedMilliseconds);
        }
    }
}
