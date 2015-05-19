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
	public class Table
	{
		public Book Book;
		
		public Row[] Rows;
        public SortedList<string, Column> Columns = new SortedList<string, Column>(StringComparer.InvariantCultureIgnoreCase);

        public Column[] GetColums(params string[] selectedcolumns)
        {
            return (from str in selectedcolumns select this.Columns[str]).ToArray(); 
        }

		public Table(Book book)
		{
			this.Book = book;
            this.Rows = new Row[book.Cache.TableData.RowCount];
            lock (this.Rows.SyncRoot)
            {
                for (int i = 0; i < book.Cache.TableData.RowCount; i++)
                {
                    Rows[i] = new Row(this, i);
                }
            }

            lock (this.Columns)
            {
                lock (this.Book.Cache.TableData.Columns)
                {
                    foreach (var item in this.Book.Cache.TableData.Columns )
                    {
                        this.Columns.Add(item.Key, new Column(this, item.Value));
                    }
                }
            }

		}
	}
	
	public class AggregateTable
	{
		public GroupData GroupData;
		
		public SortedList<string, Dictionary<Row,object>>  AggregateData;
	}
	
	public class AggregateFunction
	{
		
		
	}
	
	
	public class Parameter
	{
		public object Value;
		
		public string Name;
	}
}
