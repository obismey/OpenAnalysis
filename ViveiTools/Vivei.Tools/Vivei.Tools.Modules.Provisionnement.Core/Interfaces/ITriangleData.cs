using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vivei.Tools.Modules.Provisionnement.Model.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITriangleData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        string GetRowLabel(int index);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        string GetColumnLabel(int index);

        /// <summary>
        /// 
        /// </summary>
        int RowCount { get; }

        /// <summary>
        /// 
        /// </summary>
        int ColumnCount { get; }

        /// <summary>
        /// 
        /// </summary>
        int DiagonalColumn { get; }

        /// <summary>
        /// 
        /// </summary>
        int DiagonalRow { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowindex"></param>
        /// <param name="columnindex"></param>
        /// <returns></returns>
        double GetValue(int rowindex, int columnindex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="rowindex"></param>
        /// <param name="columnindex"></param>
        /// <returns></returns>
        Property GetProperty(string name, int rowindex, int columnindex);

        /// <summary>
        /// 
        /// </summary>
        TriangleFormat Format { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ITriangleDataProvider GetProvider();
    } 

    public enum TriangleFormat
    {
        Unknown,
        Top,
        Down,
        Full
    }
}