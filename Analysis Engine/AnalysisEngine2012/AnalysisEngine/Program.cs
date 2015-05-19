/*
 * Created by SharpDevelop.
 * User: obeyis
 * Date: 11/05/2015
 * Time: 10:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace AnalysisEngine
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var data=  LoadCsv(@"C:\Users\obeyis\conso_2014t4.csv");	
		}
		
		public static SortedList<string, object[]> LoadCsv(string filepath)
		{
			string[] lines = System.IO.File.ReadAllLines(filepath);
			
			string[] headers = lines[0].Split(',');
			
			object[][] data = new object[headers.Length][];
			
			SortedList<string, object[]> result = new SortedList<string, object[]>();
			
			for (int i = 0; i < headers.Length ; i++) {				
				data[i] = new object[lines.Length-1] ;
				result.Add(headers[i], data[i]);								
			}
			
			for (int i = 1; i < lines.Length ; i++) {
				string[] row = lines[i].Split(',');
				
				for (int j = 0; j < headers.Length; j++) {
					data[j][i-1] = GetValue(row[j]);
				}
			}
			
			return result; 
		}
        public static SortedList<string, object[]> LoadCsv(string[] lines)
        {
            string[] headers = lines[0].Split(',');

            object[][] data = new object[headers.Length][];

            SortedList<string, object[]> result = new SortedList<string, object[]>();

            for (int i = 0; i < headers.Length; i++)
            {
                data[i] = new object[lines.Length - 1];
                result.Add(headers[i], data[i]);
            }

            for (int i = 1; i < lines.Length; i++)
            {
                string[] row = lines[i].Split(',');

                for (int j = 0; j < headers.Length; j++)
                {
                    data[j][i - 1] = GetValue(row[j]);
                }
            }

            return result;
        }
		public static object GetValue(string source)
		{
			if(string.IsNullOrEmpty(source) ) return null;
			
			if(string.IsNullOrWhiteSpace(source) ) return null;
			
			double result = double.NaN;
			
			if (double.TryParse(
				source.Trim(),
				System.Globalization.NumberStyles.Any, 
				System.Globalization.CultureInfo.InvariantCulture, 
				out result  )) {
				
				return result;
			}
			
			return source.Trim();
		}
	}
}