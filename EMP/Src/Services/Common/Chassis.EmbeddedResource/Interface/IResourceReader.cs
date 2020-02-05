using System.Collections.Generic;

namespace Chassis.EmbeddedResource.Interface
{
    /// <summary>
    /// It is Use for Read Embedded resources 
    /// </summary>
    public interface IResourceReader
    {
        /// <summary>
        ///  To read resource from assembly.
        /// </summary>
        /// <param name="resourceName">Use to pass resource path</param>
        /// <returns>It read embedded resource</returns>
        IEnumerable<string> ReadResource(string fileExtension = ".sql", params string[] resourceNames);
    }
}
