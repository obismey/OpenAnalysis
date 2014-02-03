using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vivei.Tools.Modules.Provisionnement.Model.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProcessor : IDisposable 
    {        
        /// <summary>
        /// 
        /// </summary>
        void Execute();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="noview"></param>
        /// <param name="stage"></param>
        void Initialize(bool noview, AnalysisStage stage);

        /// <summary>
        /// 
        /// </summary>
        object View { get; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ProcessorArgumentAttribute : Attribute
    {
        public ArgumentType Type { get; set; }
        public ArgumentDirection Direction { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ProcessorParameterAttribute : Attribute
    {
        public ColumnType Type { get; set; }        
    }
}
