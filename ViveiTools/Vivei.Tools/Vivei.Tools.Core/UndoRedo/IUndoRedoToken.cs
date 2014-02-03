using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vivei.Tools.Core.UndoRedo
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUndoRedoToken
    {
        /// <summary>
        /// 
        /// </summary>
        void Undo();

        /// <summary>
        /// 
        /// </summary>
        void Redo();

        /// <summary>
        /// 
        /// </summary>
        string Message { get; }
    }
}