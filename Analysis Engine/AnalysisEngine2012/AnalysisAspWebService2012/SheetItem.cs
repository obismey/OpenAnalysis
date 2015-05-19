using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace AnalysisAspWebService
{
    [Serializable]
    public class SheetItem : ServiceObject
    {
        [NonSerialized]
        internal string SheetFullName;

        [NonSerialized]
        internal string BookFullName;

        [NonSerialized]
        internal Dictionary<Guid, SheetItemDimension> Dimensions = new Dictionary<Guid, SheetItemDimension>();

        [NonSerialized]
        internal Dictionary<Guid, SheetItemMeasure> Measures = new Dictionary<Guid, SheetItemMeasure>();

        public string ItemType { get; set; }
    }
}
