using System;

namespace Chassis.Dapper.Interfaces
{
    /// <summary>
    /// Interface to Compile template/content using Razor Engine
    /// </summary>
    internal interface IRazor
    {
        /// <summary>
        /// Compile template/content using Razor Engine
        /// </summary>
        /// <typeparam name="T">Razor model</typeparam>
        /// <param name="content">Template</param>
        /// <param name="templateCacheName">Template to cache</param>
        /// <param name="model">Razor model object</param>
        /// <returns>value of compiled template</returns>
        string CompileTemplate<T>(string content, string templateCacheName, T model);

        /// <summary>
        /// To check template is cache or not
        /// </summary>
        /// <param name="templateCacheName">template name</param>
        /// <param name="modelType">object type to help getting cache value</param>
        /// <returns>if template is exists in cache then return true other wise false</returns>
        bool IsTemplateCached(string templateCacheName, Type modelType);

        /// <summary>
        /// Run template using template name/ model
        /// </summary>
        /// <typeparam name="T">Model type pass</typeparam>
        /// <param name="templateCacheName">Temoplate name</param>
        /// <param name="model">razor model object</param>
        /// <returns>value of compiled template</returns>
        string Run<T>(string templateCacheName, T model);

        /// <summary>
        /// Run and Compile template 
        /// </summary>
        /// <typeparam name="T">Model type pass</typeparam>
        /// <param name="content">Template content</param>
        /// <param name="templateCacheName">Temoplate name</param>
        /// <param name="model">razor model object</param>
        /// <returns>value of compiled template</returns>
        string RunCompile<T>(string content, string templateCacheName, T model);
    }
}
