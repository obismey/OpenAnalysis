using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Vivei.Tools.Core.Directories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDirectoryService : IService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Plugin"></param>
        /// <returns></returns>
        DirectoryInfo GetPluginDirectory(IPlugin Plugin);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        DirectoryInfo GetApplicationDirectory();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LogicalName"></param>
        /// <returns></returns>
        DirectoryInfo[] GetLogicalDirectory(string LogicalName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LogicalName"></param>
        /// <param name="Directory"></param>
        void SetLogicalName(string LogicalName, string Directory); 
    }
}
