using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vivei.Tools.Modules.Provisionnement.Model.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataSegment : IComparable<IDataSegment>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDataSegment GetParent();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<IDataSegment> GetChildren();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        IEnumerable<IDataSegment> GetChildren(IInternalDataColumn column);

        /// <summary>
        /// 
        /// </summary>
        string Text { get; }

        /// <summary>
        /// 
        /// </summary>
        int Priority { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IDataSegmentProvider Provider { get; }
    }

    /// <summary>
    /// Permet de creer des segments de donnees
    /// </summary>
    public interface IDataSegmentProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        string GetFilter(IDataSegment segment);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="segments"></param>
        /// <returns></returns>
        IDataSegment Merge(IEnumerable<IDataSegment> segments);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        IEnumerable<IDataSegment> GetSegments(IInternalDataColumn column);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columns"></param>
        void Initialize(IEnumerable<IInternalDataColumn> columns);
    }
}
