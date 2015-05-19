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
using System.Collections;

namespace AnalysisEngine.Core
{
	/// <summary>
	/// Description of Cache.
	/// </summary>
	public class Cache
	{
		public TableData TableData;

        public Dictionary<string, GroupData> GroupDataCache = new Dictionary<string, GroupData>(StringComparer.InvariantCultureIgnoreCase);  

		public Cache(TableData tableData)
		{
			this.TableData = tableData;
		}

        public static string GetColumnHash(params Column[] columns)
        {
            return string.Join(" % ", from str in columns orderby str.Formula.ToLower() select "[" + str.Formula.ToLower() + "]");
        }

        public GroupData GetGroupData(params Column[] columns)
        {
            GroupData result = null ;
            string colhash= GetColumnHash(columns);

            lock ( ((ICollection)this.GroupDataCache).SyncRoot )
            {
                this.GroupDataCache.TryGetValue(colhash, out result);

                if (result == null)
                {
                    var table = columns[0].Table;

                    var eqcomp = new ColumnEqualityComparer(columns);

                    var lookup = table.Rows.ToLookup((r) => r, eqcomp);
                    var invlookup = new Dictionary<Row, Row>();
                    foreach (var item in lookup)
                    {
                        foreach (var item2 in item)
                        {
                            invlookup.Add(item2, item.Key);
                        }
                    }
                    result = new GroupData();
                    result.Lookup =(Lookup<Row,Row>) lookup;
                    result.InverseLookup = invlookup;
                    result.Columns = columns.ToList();
                    result.Parent = null;

                    GroupDataCache.Add(colhash, result);
                }
            }

            return result ;
        }
	}


}
