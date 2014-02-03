using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vivei.Tools.Core.UndoRedo
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUndoRedoContext
    {
        /// <summary>
        /// 
        /// </summary>
        bool Active { get; set; }
    }
}
