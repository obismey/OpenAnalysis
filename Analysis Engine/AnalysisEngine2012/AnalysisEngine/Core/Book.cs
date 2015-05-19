/*
 * Created by SharpDevelop.
 * User: obeyis
 * Date: 11/05/2015
 * Time: 11:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace AnalysisEngine.Core
{
	/// <summary>
	/// Description of Book.
	/// </summary>
	public class Book
	{
		public Cache Cache;
		
		public Table Table;
		
		public Book(Cache cache)
		{
			this.Cache = cache;
            this.Table = new Table(this);
			
		}
	}
}
