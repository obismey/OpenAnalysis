using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vivei.Tools.Modules.Provisionnement.Model.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITriangleDataProcessor : IProcessor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="triangle"></param>
        /// <returns></returns>
        ITriangleData Transform(ITriangleData triangle);
    }
}