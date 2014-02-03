using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vivei.Tools.Modules.Provisionnement.Model.Interfaces;


namespace Vivei.Tools.Modules.Provisionnement.Model
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AnalysisStage
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Analysis Analysis { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public IProcessor Processor { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDataSegment Segment { get; protected set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public abstract class Analysis
    {
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="InProcessor"></param>
        /// <param name="InArgument"></param>
        /// <param name="OutProcessor"></param>
        /// <param name="OutArgument"></param>
        /// <returns></returns>
        public abstract AnalysisBinding Bind(IProcessor InProcessor, string InArgument, IProcessor OutProcessor, string OutArgument);

        /// <summary>
        /// 
        /// </summary>
        public abstract IEnumerable<AnalysisStage> Stages { get; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDataSegment Segment { get; protected set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AnalysisBinding
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Out"></param>
        /// <param name="In"></param>
        public AnalysisBinding(IProcessor InProcessor, string InArgument, IProcessor OutProcessor, string OutArgument)
        {
            //this.Out= Out;
            //this.In = In;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        //public ProcessorArgument Out { get; private set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public ProcessorArgument In { get; private set; }
    }


}
