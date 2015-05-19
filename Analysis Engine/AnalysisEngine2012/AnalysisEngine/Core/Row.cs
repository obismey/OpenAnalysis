/*
 * Created by SharpDevelop.
 * User: obeyis
 * Date: 11/05/2015
 * Time: 10:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnalysisEngine.Core
{
	public class Row : IComparable<Row>
	{
		public int Index;
        public Table table;

        public Row(Table table, int i)
        {
            // TODO: Complete member initialization
            this.table = table;
            this.Index = i;
        }
        public override bool Equals(object obj)
        {
            var other = obj as Row;

            return other == null ? false : this.Index == other.Index  ;
        }

        public override int GetHashCode()
        {
            return this.Index.GetHashCode();
        }
        public override string ToString()
        {
            return this.Index.ToString();
        }

        //public static bool operator ==( Row left , Row right)
        //{
        //    return left.Index == right.Index; 
        //}
        //public static bool operator ==(Row left, Row right)
        //{
        //    return left.Index != right.Index;
        //}

        public int CompareTo(Row other)
        {
            return this.Index.CompareTo(other.Index);
        }
    }
}
