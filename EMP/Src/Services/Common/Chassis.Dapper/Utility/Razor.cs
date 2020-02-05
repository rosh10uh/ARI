using Chassis.Dapper.Interfaces;
using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Net;

namespace Chassis.Dapper.Utility
{
    /// <summary>
    /// Compile template/content using Razor Engine
    /// </summary>
    internal class Razor : IRazor
    {
        /// <summary>
        /// Compile template/content using Razor Engine
        /// </summary>
        /// <typeparam name="T">Razor model</typeparam>
        /// <param name="content">Template</param>
        /// <param name="templateCacheName">Template to cache</param>
        /// <param name="model">Razor model object</param>
        /// <returns>value of compiled template</returns>
        public virtual string CompileTemplate<T>(string content, string templateCacheName, T model)
        {
            if (IsTemplateCached(templateCacheName, typeof(T)))
            {
                return Run(templateCacheName, model);
            }
            else
            {
                return RunCompile(content, templateCacheName, model);
            }
        }

        /// <summary>
        /// To check template is cache or not
        /// </summary>
        /// <param name="templateCacheName">template name</param>
        /// <param name="modelType">object type to help getting cache value</param>
        /// <returns>if template is exists in cache then return true other wise false</returns>
        public virtual bool IsTemplateCached(string templateCacheName, Type modelType)
        {
            if (modelType == null)
            {
                return false;
            }
            return Engine.Razor.IsTemplateCached(templateCacheName, modelType);
        }

        /// <summary>
        /// Run template using template name/ model
        /// </summary>
        /// <typeparam name="T">Model type pass</typeparam>
        /// <param name="templateCacheName">Temoplate name</param>
        /// <param name="model">razor model object</param>
        /// <returns>value of compiled template</returns>
        public virtual string Run<T>(string templateCacheName, T model)
        {
            string content = Engine.Razor.Run(templateCacheName, typeof(T), model);
            return WebUtility.HtmlDecode(content);
        }

        /// <summary>
        /// Run and Compile template 
        /// </summary>
        /// <typeparam name="T">Model type pass</typeparam>
        /// <param name="content">Template content</param>
        /// <param name="templateCacheName">Temoplate name</param>
        /// <param name="model">razor model object</param>
        /// <returns>value of compiled template</returns>
        public virtual string RunCompile<T>(string content, string templateCacheName, T model)
        {
            if (model == null)
                return content;

            content = Engine.Razor.RunCompile(content, templateCacheName, typeof(T), model);

            //Decode 
            return WebUtility.HtmlDecode(content);
        }
    }
}
