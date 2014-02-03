using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vivei.Tools.Core.Data
{
    public interface IDataService : IService
    {
        void CreateLocalDatabase(string name, string folder);


    }
}
