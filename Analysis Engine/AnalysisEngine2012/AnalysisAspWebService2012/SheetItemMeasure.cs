using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace AnalysisAspWebService
{
    [Serializable]
    public class SheetItemMeasure : SheetItemPart
    {
        public int Position { get; set; }

        public string ValueColumn { get; set; }

        public MeasureAggregate ValueAggregate { get; set; }

        public string TargetColumn { get; set; }

        public MeasureAggregate TargetAggregate { get; set; }

        public bool IsSealed { get; set; }


    }
}
