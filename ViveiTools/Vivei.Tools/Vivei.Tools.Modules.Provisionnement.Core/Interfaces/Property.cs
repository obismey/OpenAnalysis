using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vivei.Tools.Modules.Provisionnement.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Property : Vivei.Tools.Core.UI.UIObject
    {
        /// <summary>
        /// 
        /// </summary>
        public Property()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public Property(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public Property(string name, ColumnType type)
        {
            this.Name = name;
            this.Type = type;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="defaultvalue"></param>
        public Property(string name, ColumnType type, object defaultvalue)
        {
            this.Name = name;
            this.Type = type;
            this.Value = defaultvalue;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public ColumnType Type { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ColumnType
    {
        /// <summary>
        /// 
        /// </summary>
        Unknown,

        /// <summary>
        /// 
        /// </summary>
        Text,

        /// <summary>
        /// 
        /// </summary>
        Number,

        /// <summary>
        /// 
        /// </summary>
        Date
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ArgumentType
    {
        /// <summary>
        /// 
        /// </summary>
        Unknown,

        /// <summary>
        /// 
        /// </summary>
        Text,

        /// <summary>
        /// 
        /// </summary>
        Number,

        /// <summary>
        /// 
        /// </summary>
        Date,

        /// <summary>
        /// 
        /// </summary>
        TextArray,

        /// <summary>
        /// 
        /// </summary>
        NumberArray,

        /// <summary>
        /// 
        /// </summary>
        DateArray,

        /// <summary>
        /// 
        /// </summary>
        Table,

        /// <summary>
        /// 
        /// </summary>
        TriangleData,

        /// <summary>
        /// 
        /// </summary>
        InternalData
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ArgumentDirection
    {
        /// <summary>
        /// 
        /// </summary>
        Unknown,

        /// <summary>
        /// 
        /// </summary>
        InArgument,

        /// <summary>
        /// 
        /// </summary>
        OutArgument,

        /// <summary>
        /// 
        /// </summary>
        InOutArgument
    }
}
