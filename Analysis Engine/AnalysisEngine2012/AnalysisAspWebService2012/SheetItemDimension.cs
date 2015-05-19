using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace AnalysisAspWebService
{
    [Serializable]
    public class SheetItemDimension : SheetItemPart
    {

        

        public int Position { get; set; }

        public string Column { get; set; }

        public DimensionLocation Location { get; set; }

        public bool IsSealed { get; set; }
    }

    [Serializable]
    public class DimensionItem
    {
        public int Position { get; set; }

        public int Id { get; set; }

        public object Value { get; set; }

        public bool Selected { get; set; }

        public bool Selectable{ get; set; }

        
    }
}
