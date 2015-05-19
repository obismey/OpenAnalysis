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

namespace AnalysisEngine.Core
{
	/// <summary>
	/// Description of Table.
	/// </summary>
	public class TableData
	{
		public SortedList<string, ColumnData> Columns = new SortedList<string, ColumnData>(StringComparer.InvariantCultureIgnoreCase);

        public int RowCount;

		public void Load(SortedList<string, object[]> data)
		{
			foreach (var elt in data) {
				var col = new ColumnData();
				col.Name = elt.Key.ToLower();
				col.Values = elt.Value;
				Columns.Add(col.Name, col);
                this.RowCount = col.Values.Length;
			}
            
           
		}
		
		public TableData(SortedList<string, object[]> data)
		{
			this.Load(data);
		}
	}
	
	public class SimpleSheetObject
	{
		public string DimensionColumn;
		
		public string MeasureColumn;
		
		public bool SortDescending ;
		
		public bool SortByMeasure ;		
		
	}
}
