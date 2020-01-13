using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks1990MG_csharp.Application.Providers.Interfaces
{
    /// <summary>
    /// Universal interface for all providers
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IProvider<T>
    {
        /// <summary>
        /// Filter of data
        /// </summary>
        Predicate<string> Filter { get; set; }
        /// <summary>
        /// Working link (file,server,etc)
        /// </summary>
        string Link { get; set; }
        /// <summary>
        /// Get data
        /// </summary>
        /// <returns>T data</returns>
        T Get();
        /// <summary>
        /// Place data
        /// </summary>
        /// <param name="data"> data</param>
        void Place(T data);
    }
}
