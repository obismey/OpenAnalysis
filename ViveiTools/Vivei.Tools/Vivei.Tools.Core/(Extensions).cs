using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vivei.Tools.Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface IApplication
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<IService> GetServices();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<IPlugin> GetPlugins();
       
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<IService> GetServices();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Application"></param>
        void Reset(IApplication Application);
        
    }

    /// <summary>
    /// TODO:  Ajouter une méthode Ececute et une méthode d'écoute
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// 
        /// </summary>
        void Reset();
    }

    /// <summary>
    /// 
    /// </summary>    
    [AttributeUsage(AttributeTargets.Assembly)]
    public class EntryPointAttribute : Attribute 
    {
        /// <summary>
        /// 
        /// </summary>
        public Type PluginType { get; set; }
    }
}
