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
	/// <summary>
	/// Description of Sheet.
	/// </summary>
	public class Sheet
	{
        private Book bookdata;

		public Sheet()
		{
		}

        public Sheet(Book bookdata)
        {
            this.bookdata = bookdata;
        }
	}
	

	

	

	

	public class DimensionFieldItem
	{
		public object Value;
		
		public bool Selected;
	}
	
	public class MeasureField
	{
		public string Name; 
		
		public string Formula;
		
	    public string TargetFormula;
		
		public string Format;
	}
}
