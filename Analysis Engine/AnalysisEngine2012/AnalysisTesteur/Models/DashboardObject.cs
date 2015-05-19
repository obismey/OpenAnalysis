using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnalysisTesteur.Helpers;

namespace AnalysisTesteur.Models
{
    public class DashboardObject : UIObject
    {
        #region Properties
        private string _Name;
        public virtual  string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                OnPropertyChanged("Name");
            }
        }



        #endregion

    }
}
