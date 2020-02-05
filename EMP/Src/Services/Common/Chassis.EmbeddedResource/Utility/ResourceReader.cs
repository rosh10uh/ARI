using Chassis.EmbeddedResource.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Chassis.EmbeddedResource.Utility
{
    /// <summary>
    /// It is Use for Read Embedded resources 
    /// </summary>
    public class ResourceReader : IResourceReader
    {
        /// <summary>
        /// Use to get service of LoadResourceAssembly
        /// </summary>
        private readonly LoadResourceAssembly _loadResourceAssembly;
        /// <summary>
        /// Initializes a new instance of the ResourceReader class.
        /// </summary>
        /// <param name="loadResourceAssembly">Use to get service of LoadResourceAssembly</param>
        public ResourceReader(LoadResourceAssembly loadResourceAssembly)
        {
            _loadResourceAssembly = loadResourceAssembly;
            loadResourceAssembly.LoadResourcedAssembly();
        }

        /// <summary>
        ///  To read resource from assembly.
        /// </summary>
        /// <param name="resourceName">Use to pass resource path</param>
        /// <returns>It read embedded resource</returns>
        public IEnumerable<string> ReadResource(string fileExtension = ".sql", params string[] resourceNames)
        {
            //Find resource name   
            foreach (var resourceName in resourceNames)
            {
                var _resourceDetail = GetAssemblyData(string.Format("{0}{1}", resourceName, fileExtension));

                if (_resourceDetail != null)
                {
                    //Get resource stream from assembly 
                    using (Stream stream = _resourceDetail.Item1.GetManifestResourceStream(_resourceDetail.Item2))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            yield return reader.ReadToEnd();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Use to make tuple from assembly
        /// </summary>
        /// <param name="resourceName">Use to pass resource path</param>
        /// <returns>It returns key/value pair of assembly</returns>
        private Tuple<Assembly, string> GetAssemblyData(string resourceName)
        {
            Tuple<Assembly, string> assemblyList = null;
            var assemblies = _loadResourceAssembly.AssemblyResources;
            for (int assemblyIndex = 0; assemblyIndex < assemblies.Count; assemblyIndex++)
            {
                var assembly = assemblies.ElementAt(assemblyIndex);
                for (int queryPathIndex = 0; queryPathIndex < assembly.Value.Count(); queryPathIndex++)
                {
                    if (assembly.Value[queryPathIndex].Contains(resourceName))
                    {
                        assemblyList = new Tuple<Assembly, string>(assembly.Key, assembly.Value[queryPathIndex]);
                        break;
                    }
                }
            }
            return assemblyList;
        }
    }
}
