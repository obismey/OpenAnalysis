/*
 * Created by SharpDevelop.
 * User: obeyis
 * Date: 11/05/2015
 * Time: 11:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace AnalysisEngine.Core
{
	public class SheetObject
	{
		public List<string> Groups;
		
		public SortedList<int, DimensionField> Rows;
		
		public SortedList<int, DimensionField> Columns;
		
		public SortedList<string, MeasureField> Measures;
	}
}
