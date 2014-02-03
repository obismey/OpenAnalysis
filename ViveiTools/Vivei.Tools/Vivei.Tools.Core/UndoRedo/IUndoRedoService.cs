using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vivei.Tools.Core.UndoRedo
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUndoRedoService : IService 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Token"></param>
        void Push(IUndoRedoContext context, IUndoRedoToken Token);

        /// <summary>
        /// 
        /// </summary>
        IUndoRedoContext ActiveContext { get; }
    }    
}
