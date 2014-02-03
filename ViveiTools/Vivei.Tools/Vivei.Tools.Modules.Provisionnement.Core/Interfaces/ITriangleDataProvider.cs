using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vivei.Tools.Modules.Provisionnement.Model.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITriangleDataProvider : IProcessor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="generator"></param>
        /// <param name="rowlabels"></param>
        /// <param name="columnlabels"></param>
        /// <returns></returns>
        ITriangleData Create(Func<int,int,double> generator, IEnumerable<String> rowlabels, IEnumerable<String> columnlabels);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="generator"></param>
        /// <param name="rowlabels"></param>
        /// <param name="columnlabels"></param>
        /// <returns></returns>
        ITriangleData Create(Func<int, int, double> generator, Func<int, String> rowlabels, Func<int, String> columnlabels);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        ITriangleData Compute(IInternalData data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        ITriangleData Compute(IInternalData data, string filter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="segment"></param>
        /// <returns></returns>
        ITriangleData Compute(IInternalData data, IDataSegment segment);       
    }
}