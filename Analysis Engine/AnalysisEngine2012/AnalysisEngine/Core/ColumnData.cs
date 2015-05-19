/*
 * Created by SharpDevelop.
 * User: obeyis
 * Date: 11/05/2015
 * Time: 10:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnalysisEngine.Core
{
    [Serializable]
	public class ColumnData
	{
		public string Name { get; set ; }
		
		public bool IsIndexed { get;  set ; }
		
		public bool IsUnique { get;  set ; }

        public bool IsNullable { get;  set; }
		
        public bool IsTimeStamp { get;  set ; }

        public TypeCode Type { get;  set; }        
		
        [NonSerialized]
		internal object[] Values;

        [NonSerialized]
		internal int[] Indices;

        public ColumnData()
        {
            this.IsNullable = true;
            this.Type = TypeCode.Object;
        }
		
	}
	
}
