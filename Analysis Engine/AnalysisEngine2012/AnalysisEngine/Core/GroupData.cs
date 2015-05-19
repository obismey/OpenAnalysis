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
	public class GroupData
	{
		public Lookup<Row, Row> Lookup;
		public Dictionary<Row, Row> InverseLookup;
		
		public GroupData Parent;
		public List<Column> Columns ;
	}

    public class Key
    {
        Row Row;
        object[] Values;
        Column[] Columns;
    }
}
