using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Vivei.Tools.Core.UI
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUIService : IService 
    {
        /// <summary>
        /// 
        /// </summary>
        MenuItemCollection Menu { get; }

        /// <summary>
        /// 
        /// </summary>
        NavigationGroupCollection NavigationGroup { get; }        
    }
}