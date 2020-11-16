using System;
using System.IO;

namespace Lang
{
    class Program
    {
        static void Main(string[] args)
        { 
            string nPath = Path.Combine(Environment.CurrentDirectory, "nFile.txt");
            string mPath = Path.Combine(Environment.CurrentDirectory, "mFile.txt");
            string nLine = "";
            string mLine = "";

            //Open M file and read line
            try
            {
                using (StreamReader mRdr = new StreamReader(mPath))
                {
                    mLine = mRdr.ReadLine();
                    Console.WriteLine("M file successfully opened!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The m file could not be read!");
                Console.WriteLine(e);
            }

            //Open N file and read line
            try
            {
                using (StreamReader nRdr = new StreamReader(nPath))
                {
                    nLine = nRdr.ReadLine();
                    Console.WriteLine("N file successfully opened!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The n file could not be read!");
                Console.WriteLine(e);
            }

            //Split into numbers
            string[] mStrs = mLine.Split(' ');
            int[] mNums = new int[mStrs.Length];
            for (int x = 0; x < mStrs.Length - 1; x++)
            {
                mNums[x] = int.Parse(mStrs[x]);
                if (x < 10)
                {
                    Console.Write("{0} ", mNums[x]);
                }
            }

            Console.WriteLine();

            string[] nStrs = nLine.Split(' ');
            int[] nNums = new int[nStrs.Length];
            for (int x = 0; x < nStrs.Length - 1; x++)
            {
                nNums[x] = int.Parse(nStrs[x]);
                if (x < 10)
                {
                    Console.Write("{0} ", nNums[x]);
                }
            }

        }
    }
}
