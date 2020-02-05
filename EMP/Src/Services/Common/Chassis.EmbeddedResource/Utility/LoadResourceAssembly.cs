using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Chassis.EmbeddedResource.Utility
{
    /// <summary>
    /// load resource from current domain assembly and storing into gloable AssemblyResources.
    /// </summary>
    public sealed class LoadResourceAssembly
    {
        /// <summary>
        /// For getting assembly & resource name 
        /// </summary>
        internal Dictionary<Assembly, string[]> AssemblyResources { get; } = new Dictionary<Assembly, string[]>();

        /// <summary>
        /// Load Resource Assembly based on assemblyNames, 
        /// Load External assembly for load resources.
        /// </summary>
        /// <param name="assemblyNames">To pass external assembly path</param>
        internal void LoadResourcedAssembly(string[] assemblyNames = null)
        {
            if (AssemblyResources.Count > 0)
                return;

            //Get assembly name 
            if (assemblyNames != null)
            {
                //load external assembly into current domain & add into assembly name
                foreach (string assembly in assemblyNames)
                {
                    Assembly.Load(assembly);
                }
            }

            LoadResourceFromCurrentDomain();
        }

        /// <summary>
        /// Load resource & assembly into gloable dictionary for finding resources 
        /// We have only load assembly which dynamically is false
        /// </summary>
        /// <param name="assemblyNameList">list of assembly full name</param>
        private void LoadResourceFromCurrentDomain()
        {

            //Get assemblies from current domain             
            var resourceAssembly = AppDomain.CurrentDomain.GetAssemblies().Where(x => (!x.IsDynamic));

            //loop on resourceAssembly to store assembly & Resource Names into dictionary 
            foreach (var assembly in resourceAssembly)
            {
                //Find resource file(.sql) 
                var resources = assembly.GetManifestResourceNames()
                                            .Where(c =>
                                                  c.EndsWith(".sql", StringComparison.OrdinalIgnoreCase)).ToArray();

                if (!AssemblyResources.ContainsKey(assembly) && resources.Length > 0)
                {
                    //Add/override resource  names
                    AssemblyResources[assembly] = resources;
                }
            }
        }
    }
}
