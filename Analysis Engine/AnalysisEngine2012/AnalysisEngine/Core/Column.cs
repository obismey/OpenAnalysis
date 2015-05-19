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
    public class Column
    {
        public string Formula;

        public ColumnData ColumnData;

        public Table Table;

        public List<ColumnData> SourceColumnData;

        public List<Column> DependentColumns;

        public List<Column> PrecedentColumns;

        public Column(Table table, ColumnData corecolumn)
        {
            this.Formula = corecolumn.Name;

            this.ColumnData = corecolumn;

            this.Table = table;
        }

        public object GetValue(Row row)
        {
            return this.ColumnData.Values[row.Index];
        }
    }

    public class ColumnEqualityComparer : IEqualityComparer<Row>
    {
        internal Column[] columns;

        public ColumnEqualityComparer(Column[] columns)
        {
            this.columns = columns;
        }

        public bool Equals(Row ri, Row rj)
        {
            foreach (var fld in this.columns)
            {
                object vi = fld.ColumnData.Values[ri.Index];
                object vj = fld.ColumnData.Values[rj.Index];

                if (!object.Equals(vi, vj)) return false;
            }

            return true;
        }

        public int GetHashCode(Row ri)
        {
            int result = 0;
            bool first = true;
            foreach (var fld in this.columns)
            {
                var v = fld.ColumnData.Values[ri.Index];
                if (first)
                {
                    result = v==null ? 0 : v.GetHashCode();
                    first = false;
                }
                else
                {
                    result = CombineHashCodes(v == null ? 0 : v.GetHashCode(), result);
                }
            }
            return result;
        }

        public static int CombineHashCodes(int h1, int h2)
        {
            return unchecked(((h1 << 5) + h1) ^ h2);
        }

    }

    public class ColumnComparer : IComparer<Row>
    {

        internal Column[] columns;

        public ColumnComparer(Column[] columns)
        {
            this.columns = columns;
        }
     
        public int Compare(Row ri, Row rj)
        {
            foreach (var fld in this.columns)
            {
                var ric = fld.ColumnData.Values[ri.Index] as IComparable;
                var rjc = fld.ColumnData.Values[rj.Index] as IComparable;

                if ((ric == null) && (rjc != null)) return  -1;
                if ((ric != null) && (rjc == null)) return 1;
                if ((ric is  string) && !(rjc is string)) return -1;
                if (!(ric is string) && (rjc is string)) return 1;
                if ((ric != null) && (rjc != null))
                {
                    var comp = ric.CompareTo(rjc);
                    if (comp != 0) return comp ;
                }
            }

            return 0;
        }
    }
}
