using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Microsoft.WindowsAzure.Storage;

namespace AnalysisAspWebService
{
    /// <summary>
    /// Description résumée de Service1
    /// </summary>
    [WebService(Namespace = "http://obismey.org/analysis")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    // [System.Web.Script.Services.ScriptService]
    public class AnalysisService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return this.Server.MapPath("~/App_Data"); ;
        }

        [WebMethod]
        public string ReloadData(string Name)
        {
            var azuremode = true;

            if (azuremode)
            {
                try
                {
                    if (this.Application.Get(Name.ToLower()) != null)
                    {
                        return Name + " is Loaded";
                    }

                    var storageaccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=analysisenginestorage;AccountKey=IPwZHBnr4hvTtB4zqEgs6rE5RMatHnjj1UivGjqXzQnbzoCuwOaEgAqSfdwK9JwUe0AcahKL6Dz3+lMqiB8Ekg==");
                    var shareref = storageaccount.CreateCloudBlobClient().GetContainerReference("defaulttest");
                    var blob = shareref.GetBlobReferenceFromServer(Name +".csv");

                    if (!blob.Exists())   return "Le fichier n'existe pas";

                    var rowlist = new List<string>();

                    using (var stream = blob.OpenRead())
                    {
                        using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
                        {
                            while (!reader.EndOfStream)
                            {
                                rowlist.Add(reader.ReadLine());
                            }
                        }
                    }

                    var table2 = new AnalysisEngine.Core.TableData(
                         AnalysisEngine.Program.LoadCsv(rowlist.ToArray()));

                    this.Application.Add(Name.ToLower(), table2);

                    return Name + " is Loaded";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }             

                return "";
            }
            var f = this.Server.MapPath("~/App_Data");

            if (this.Application.Get(Name.ToLower()) != null)
            {
                return Name + " is Loaded";
            }

            if (!System.IO.File.Exists(System.IO.Path.Combine(f, Name + ".csv")))
                return "Le fichier n'existe pas";

            f = System.IO.Path.Combine(f, Name + ".csv");

            var table = new AnalysisEngine.Core.TableData(
                AnalysisEngine.Program.LoadCsv(f));

            this.Application.Add(Name.ToLower(), table);

            return Name + " is Loaded";
        }

        [WebMethod]
        public AnalysisEngine.Core.ColumnData[] GetColumnList(string TableName)
        {
            var data = this.Application.Get(TableName.ToLower()) as AnalysisEngine.Core.TableData;

            if (data == null) return null;

            var result = from col in data.Columns select col.Value;

            return result.ToArray();
        }


        private void ValidateCall()
        {

        }

        [WebMethod]
        public bool CreateBook(string TableName, string Name)
        {
            ValidateCall();

            var tabledata = this.Application.Get(TableName.ToLower()) as AnalysisEngine.Core.TableData;

            if (tabledata == null) return false;

            var fullname = string.Format("Table[{0}].Book[{1}]", TableName, Name).ToLower();

            var data = this.Application.Get(fullname) as AnalysisEngine.Core.Book;

            if (data == null)
            {
                var cache = new AnalysisEngine.Core.Cache(tabledata);

                data = new AnalysisEngine.Core.Book(cache);

                this.Application.Add(fullname, data);

                lock (((System.Collections.ICollection)bookcontainer).SyncRoot)
                {
                    HashSet<string> data2 = null;

                    bookcontainer.TryGetValue(TableName, out data2);

                    if (data2 == null) { data2 = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase); bookcontainer.Add(TableName, data2); }

                    data2.Add(Name);
                }
            }

            return true;
        }

        [WebMethod]
        public bool DeleteBook(string TableName, string Name)
        {
            ValidateCall();

            return true;
        }

        [WebMethod]
        public bool OpenBook(string TableName, string Name)
        {
            ValidateCall();

            return true;
        }

        [WebMethod]
        public bool CloseBook(string TableName, string Name)
        {
            ValidateCall();

            return true;
        }

        private static Dictionary<string, HashSet<string>> bookcontainer = new Dictionary<string, HashSet<string>>(StringComparer.InvariantCultureIgnoreCase);

        [WebMethod]
        public string[] GetBooks(string TableName)
        {
            ValidateCall();

            this.ReloadData(TableName);

            lock (((System.Collections.ICollection)bookcontainer).SyncRoot)
            {
                HashSet<string> data = null;

                bookcontainer.TryGetValue(TableName, out data);

                if (data == null) { data = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase); bookcontainer.Add(TableName, data); }

                return data.ToArray();
            }
        }

        [WebMethod]
        public bool CreateSheet(string TableName, string BookName, string Name)
        {
            ValidateCall();

            var bookdata = this.Application.Get(string.Format("Table[{0}].Book[{1}]", TableName, BookName).ToLower()) as AnalysisEngine.Core.Book;

            if (bookdata == null) return false;

            var fullname = string.Format("Table[{0}].Book[{1}].Sheet[{2}]", TableName, BookName, Name).ToLower();

            var data = this.Application.Get(fullname) as AnalysisEngine.Core.Sheet;

            if (data == null)
            {
                data = new AnalysisEngine.Core.Sheet(bookdata);

                this.Application.Add(fullname, data);
            }

            return true;
        }

        [WebMethod]
        public bool DeleteSheet(string TableName, string BookName, string Name)
        {
            ValidateCall();

            return true;
        }

        [WebMethod]
        public string[] GetSheets(string TableName, string BookName)
        {
            ValidateCall();

            return null;
        }

        [WebMethod]
        public object GetGlobalProperty(string TableName, string BookName, string SheetName, string PropertyPath)
        {
            ValidateCall();

            return null;
        }

        [WebMethod]
        public bool SetGlobalProperty(string TableName, string BookName, string SheetName, string PropertyPath, object value)
        {
            ValidateCall();

            return true;
        }

        [WebMethod]
        public SheetItem CreateSheetItem(string TableName, string BookName, string SheetName)
        {
            ValidateCall();

            var shfullname = string.Format("Table[{0}].Book[{1}].Sheet[{2}]", TableName, BookName, SheetName).ToLower();
            var bkfullname = string.Format("Table[{0}].Book[{1}]", TableName, BookName).ToLower();

            var data = this.Application.Get(shfullname) as AnalysisEngine.Core.Sheet;

            if (data == null) return null;

            var shitem = new SheetItem();
            shitem.SheetFullName = shfullname;
            shitem.BookFullName = bkfullname;
            this.Application.Add(shitem.UniqueID.ToString(), shitem);

            return shitem;
        }

        [WebMethod]
        public bool DeleteSheetItem(SheetItem item)
        {
            ValidateCall();

            return true;
        }

        [WebMethod]
        public SheetItem[] GetSheetItems(string TableName, string BookName, string SheetName)
        {
            ValidateCall();

            return null;
        }

        [WebMethod]
        public object[] GetStyleProperties(SheetItem item, string[] paths)
        {
            ValidateCall();

            return null;
        }

        [WebMethod]
        public string GetSheetItemType(SheetItem item)
        {
            ValidateCall();

            return null;
        }
        [WebMethod]
        public bool SetSheetItemType(SheetItem item, string type)
        {
            ValidateCall();

            return true;
        }

        [WebMethod]
        public bool UpdateDimensionList(SheetItem item, UpdateAction action, SheetItemDimension dimension)
        {
            ValidateCall();

            var data = this.Application.Get(item.UniqueID.ToString()) as SheetItem;

            if (data == null) return false;

            if (action == UpdateAction.Insert)
            {
                lock (data.Dimensions)
                {
                    if (data.Dimensions.ContainsKey(dimension.UniqueID)
                        || string.IsNullOrEmpty(dimension.Column)) return false;

                    dimension.SheetItem = item;
                    data.Dimensions.Add(dimension.UniqueID, dimension);

                    return true;
                }
            }
            if (action == UpdateAction.Remove)
            {
                lock (data.Dimensions)
                {
                    if (!data.Dimensions.ContainsKey(dimension.UniqueID)) return false;

                    data.Dimensions.Remove(dimension.UniqueID);

                    return true;
                }
            }
            return false;
        }
        [WebMethod]
        public bool UpdateMeasureList(SheetItem item, UpdateAction action, SheetItemMeasure measure)
        {
            ValidateCall();

            var data = this.Application.Get(item.UniqueID.ToString()) as SheetItem;

            if (data == null) return false;

            if (action == UpdateAction.Insert)
            {
                lock (data.Measures)
                {
                    if (data.Dimensions.ContainsKey(measure.UniqueID)
                        || string.IsNullOrEmpty(measure.ValueColumn)) return false;

                    measure.SheetItem = item;
                    data.Measures.Add(measure.UniqueID, measure);

                    return true;
                }
            }
            if (action == UpdateAction.Remove)
            {
                lock (data.Measures)
                {
                    if (!data.Measures.ContainsKey(measure.UniqueID)) return false;

                    data.Measures.Remove(measure.UniqueID);

                    return true;
                }
            }
            return false;
        }
        [WebMethod]
        public bool UpdateDimension(SheetItemDimension dimension, string[] properties)
        {
            ValidateCall();

            return true;
        }
        [WebMethod]
        public bool UpdateMeasure(SheetItemMeasure measure, string[] properties)
        {
            ValidateCall();

            return true;
        }
        [WebMethod]
        public DimensionItem[] GetDimensionItems(SheetItemDimension dimension)
        {
            ValidateCall();

            var sheetitemdata = this.Application.Get(dimension.SheetItem.UniqueID.ToString()) as SheetItem;

            var bookdata = this.Application.Get(sheetitemdata.BookFullName) as AnalysisEngine.Core.Book;

            var colinstance = bookdata.Table.GetColums(dimension.Column);

            var groupdata = bookdata.Cache.GetGroupData(colinstance);
            var comp = new AnalysisEngine.Core.ColumnComparer(colinstance);

            var result = from elt in groupdata.Lookup.OrderBy((gelt) => gelt.Key, comp)
                         select new DimensionItem()
                         {
                             Id = elt.Key.Index,
                             Value = colinstance[0].GetValue(elt.Key)
                         };

            return result.ToArray();
        }

        [WebMethod]
        public SheetItemRowSet GetSheetItemRowSet(SheetItem initem)
        {
            ValidateCall();
            
            var item = this.Application.Get(initem.UniqueID.ToString()) as SheetItem;

            if (item.Dimensions.Count == 0) return null;

            var bookdata = this.Application.Get(item.BookFullName) as AnalysisEngine.Core.Book;

            var colinstance = bookdata.Table.GetColums(item.Dimensions.Select((col) => col.Value.Column).ToArray());

            var groupdata = bookdata.Cache.GetGroupData(colinstance);
            var colgroupdata = colinstance.Select((col) => bookdata.Cache.GetGroupData(col)).ToArray();

            var result = new SheetItemRowSet();
            result.RowList = new SheetItemRow[groupdata.Lookup.Count];
            result.DimensionIdList = item.Dimensions.Select((col) => col.Key).ToArray();
            result.MeasureIdList = item.Measures.Select((col) => col.Key).ToArray();

            var comp = new AnalysisEngine.Core.ColumnComparer(colinstance);
            
            var counter = 0;
            foreach (var g in groupdata.Lookup.OrderBy((gelt)=>gelt.Key, comp))
            {
                var row = new SheetItemRow();
                row.Position = counter;
                row.Id = g.Key.Index;
                row.DimensionItems = new DimensionItem[item.Dimensions.Count];

                for (int i = 0; i < colinstance.Length; i++)
                {
                    row.DimensionItems[i] = new DimensionItem()
                    {
                        Id = colgroupdata[i].InverseLookup[g.Key].Index,
                        Value = colinstance[i].GetValue(g.Key)
                    };
                }

                row.MeasureValues = new object[item.Measures.Count];

                if (item.Measures.Count > 0)
                {
                    var tempcounter = 0;
                    foreach (var m in item.Measures )
                    {
                        row.MeasureValues[tempcounter] = Aggregate(g, m.Value, bookdata);
                        tempcounter += 1;
                    }
                }

                result.RowList[counter] = row;
                counter += 1;
            }

            return result;
        }
        private double? Aggregate(IGrouping<AnalysisEngine.Core.Row, AnalysisEngine.Core.Row> g, SheetItemMeasure m, AnalysisEngine.Core.Book b)
        {
            var col = b.Table.GetColums(m.ValueColumn)[0];

            if (m.ValueAggregate == MeasureAggregate.CountRows)
            {
                return (double)g.Count();
            }
            if (m.ValueAggregate == MeasureAggregate.CountNull)
            {
                return (double)(from r in g where col.GetValue(r) == null select r).Count();
            }
            if (m.ValueAggregate == MeasureAggregate.Count)
            {
                return (double)(from r in g where col.GetValue(r) != null select r).Count();
            }

            var counter = 0.0;
            var acc1 = 0.0;
            var acc2 = 0.0;

            foreach (var r in g)
            {
                var v = col.GetValue(r);

                //if (v == null || !(v is double)) return null;

                if (v is double && (v != null))
                {
                    counter += 1.0;
                    var vdbl = (double)v;

                    switch (m.ValueAggregate)
                    {

                        case MeasureAggregate.Min:
                            acc1 = counter == 1.0 ? vdbl : Math.Min(acc1, vdbl);
                            break;
                        case MeasureAggregate.Max:
                            acc1 = counter == 1.0 ? vdbl : Math.Max(acc1, vdbl);
                            break;
                        case MeasureAggregate.Range:
                            acc1 = counter == 1.0 ? vdbl : Math.Min(acc1, vdbl);
                            acc2 = counter == 1.0 ? vdbl : Math.Max(acc2, vdbl);
                            break;
                        case MeasureAggregate.First:
                            if (counter == 1.0) acc1 = vdbl;
                            break;
                        case MeasureAggregate.Last:
                            acc1 = vdbl;
                            break;
                        case MeasureAggregate.Avg:
                            acc1 = counter == 1.0 ? vdbl : acc1 + vdbl;
                            break;
                        case MeasureAggregate.Sum:
                            acc1 = counter == 1.0 ? vdbl : acc1 + vdbl;
                            break;
                        case MeasureAggregate.Std:
                            acc1 = counter == 1.0 ? vdbl : acc1 + vdbl;
                            acc2 = counter == 1.0 ? vdbl * vdbl : acc2 + vdbl * vdbl;
                            break;
                        case MeasureAggregate.Var:
                            acc1 = counter == 1.0 ? vdbl : acc1 + vdbl;
                            acc2 = counter == 1.0 ? vdbl * vdbl : acc2 + vdbl * vdbl;
                            break;
                        case MeasureAggregate.CV:
                            acc1 = counter == 1.0 ? vdbl : acc1 + vdbl;
                            acc2 = counter == 1.0 ? vdbl * vdbl : acc2 + vdbl * vdbl;
                            break;
                        default:
                            break;
                    }
                }
            }

            if (counter == 0.0) return null;

            switch (m.ValueAggregate )
            {
              
                case MeasureAggregate.Min:
                    return acc1;
                case MeasureAggregate.Max:
                    return acc1;
                case MeasureAggregate.Range:
                    break;
                case MeasureAggregate.First:
                    return acc1;
                case MeasureAggregate.Last:
                    return acc1;
                case MeasureAggregate.Avg:
                    return acc1/counter ;
                case MeasureAggregate.Sum:
                    return acc1;
                case MeasureAggregate.Std:
                    return Math.Sqrt( (acc1 / counter) * (acc1 / counter) - acc2 / counter);
                case MeasureAggregate.Var:
                    return (acc1 / counter) * (acc1 / counter) - acc2/counter;
                case MeasureAggregate.CV:
                   return Math.Sqrt((acc1 / counter) * (acc1 / counter) - acc2 / counter) / (acc1 / counter);
                default:
                    break;
            }
            return null;
        }

        [WebMethod]
        public DimensionItem[] GetSelectedDimensionItems(SheetItemDimension dimension)
        {
            ValidateCall();

            return null;
        }
        [WebMethod]
        public DimensionItem[] GetPossibleDimensionItems(SheetItemDimension dimension)
        {
            ValidateCall();

            return null;
        }
        [WebMethod]
        public DimensionItem[] GetExcludedDimensionItems(SheetItemDimension dimension)
        {
            ValidateCall();

            return null;
        }
        [WebMethod]
        public bool UpdateSelection(SheetItemDimension dimension, int[] itemIdList)
        {
            ValidateCall();

            return true;
        }



        [WebMethod]
        public bool SetStyleProperties(SheetItem item, string[] paths, object[] values)
        {
            ValidateCall();

            return true;
        }
    }

    [Serializable]
    public class SheetItemRow
    {
        public int Position { get; set; }

        public int Id { get; set; }

        public DimensionItem[] DimensionItems { get; set; }

        public object[] MeasureValues { get; set; }
    }

    [Serializable]
    public class SheetItemRowSet
    {
        public Guid[] DimensionIdList { get; set; }

        public Guid[] MeasureIdList { get; set; }

        public SheetItemRow[] RowList { get; set; }
    }

    public enum UpdateAction
    {
        Insert,
        Move,
        Remove
    }
    public enum DimensionLocation
    {
        Row,
        Serie,
        Column
    }
    public enum MeasureAggregate
    {
        Count,
        CountRows,
        CountNull,
        Min,
        Max,
        Range,
        First,
        Last,
        Avg,
        Sum,
        Std,
        Var,
        CV,
    }
}