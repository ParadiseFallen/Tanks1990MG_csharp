using System;
using System.Collections.Generic;
using Tanks1990MG_csharp.Application;

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
        [STAThread]
        static void Main()
        {
            List<int> X = new List<int>() { 3, 5 };

            List<int> A = new List<int>() { 1 };

            A.AddRange(X);

            A.ForEach(Console.WriteLine);
            Console.WriteLine();
            X.ForEach(i=>A.Remove(i));
            A.ForEach(Console.WriteLine);

            Console.ReadKey();

            using (var game = new App())
                game.Run();
        }

        
    }

   
#endif
}
