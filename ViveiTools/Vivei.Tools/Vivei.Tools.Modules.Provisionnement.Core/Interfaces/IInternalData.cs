
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vivei.Tools.Modules.Provisionnement.Model.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInternalData
    {
        /// <summary>
        /// 
        /// </summary>
        IEnumerable<IInternalDataColumn> Columns { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<IInternalDataRow> Select();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        IEnumerable<IInternalDataRow> Select(string filter, string sort);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segement"></param>
        /// <returns></returns>
        IEnumerable<IInternalDataRow> Select(IDataSegment segement);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface ISqlInternalData : IInternalData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        System.Data.Common.DbDataReader ExecuteQuery(string query);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="table"></param>
        /// <param name="temporary"></param>
        void ExecuteQuery(string query, string table, bool temporary);
    }
}

   
