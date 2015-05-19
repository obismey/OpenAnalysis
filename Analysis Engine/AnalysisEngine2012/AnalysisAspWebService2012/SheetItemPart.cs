using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace AnalysisAspWebService
{
    [Serializable]
    public class SheetItemPart : ServiceObject
    {
        public SheetItem SheetItem { get; set; }
    }
}
