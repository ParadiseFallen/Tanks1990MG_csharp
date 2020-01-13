using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Tanks1990MG_csharp.Application.InputMG.Solutions;
using Tanks1990MG_csharp.Application.Providers.Interfaces;

namespace Tanks1990MG_csharp.Application.Providers.Solutions
{
    class SampleKeyFileProvider : IProvider<List<KeyMetadata>>
    {
        public Predicate<string> Filter { get; set; } = (string a) => { return true; };
        /// <summary>
        /// Concrete file.ly
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// Get list of key samples from file.ly
        /// </summary>
        /// <returns>List<KeyMetadata></returns>
        public List<KeyMetadata> Get()
        {
            List<KeyMetadata> Samples = new List<KeyMetadata>();
            FileStream fs = new FileStream($"{ Link }", FileMode.OpenOrCreate);
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                //deserialize
                Samples = binaryFormatter.Deserialize(fs) as List<KeyMetadata>;
                fs.Close();
            }
            catch (Exception)
            {

            }
            finally
            {
                fs.Close();
            }
            return Samples;
        }
        /// <summary>
        /// Save to file.ly
        /// </summary>
        /// <param name="data">List of key samples</param>
        public void Place(List<KeyMetadata> data)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fs = new FileStream($"{Link}", FileMode.OpenOrCreate);
            binaryFormatter.Serialize(fs, data);
            fs.Close();
        }
    }
}
