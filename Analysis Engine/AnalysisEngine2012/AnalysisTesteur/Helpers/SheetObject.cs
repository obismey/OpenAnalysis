using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnalysisTesteur.Analysis
{
    public partial class  ServiceObject
    {
        public ServiceObject()
        {
            this.UniqueID = Guid.NewGuid();
        }
    }
}
