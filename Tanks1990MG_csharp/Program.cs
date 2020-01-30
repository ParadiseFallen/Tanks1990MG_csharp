using System;
using System.Collections.Generic;
using Tanks1990MG_csharp.Application;
using Tanks1990MG_csharp.Application.GameEntityes.Test;

namespace Tanks1990MG_csharp
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {

            //var a = 5;
            //ref int b = ref a;
            //b++;
            //Console.WriteLine(a);
            //Console.ReadLine();

            //List<int> A = new List<int>();

            //List<RefInt> B = new List<int>();
            //A.Add(0);
            //B.Add(A[0]);
            //B[0]++;
            //Console.WriteLine(A[0]);
            //Console.WriteLine(B[0]);
            //Console.ReadLine();

            using (var game = new App())
                game.Run();
        }
    }

   
#endif
}