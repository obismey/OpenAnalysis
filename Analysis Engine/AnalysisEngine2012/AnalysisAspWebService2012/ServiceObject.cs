using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace AnalysisAspWebService
{
    [Serializable]
    public class ServiceObject
    {
        public Guid UniqueID { get; set; }

        public ServiceObject()
        {
            this.UniqueID = Guid.NewGuid();
        }
    }
}
