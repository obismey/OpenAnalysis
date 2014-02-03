using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vivei.Tools.Modules.Provisionnement.Model.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInternalDataColumn
    {
        /// <summary>
        /// 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        int Index { get; }

        /// <summary>
        /// 
        /// </summary>
        ColumnType Type { get; }

        /// <summary>
        /// 
        /// </summary>
        string Usage { get; }

        /// <summary>
        /// 
        /// </summary>
        string UsageTag { get; }

        /// <summary>
        /// 
        /// </summary>
        IInternalData InternalData { get; }
    }  
}
