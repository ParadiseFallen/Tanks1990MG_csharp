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
        /// 
        [STAThread]
        static void Main()
        {
            using (var game = new App())
                game.Run();
        }
    }

   
#endif
}