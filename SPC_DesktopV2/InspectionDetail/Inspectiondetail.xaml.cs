using SPC_DesktopV2.InspectionItem;
using SPC_DesktopV2.SPCModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace SPC_DesktopV2.InspectionDetail
{
    /// <summary>
    /// Interaction logic for Inspectiondetail.xaml
    /// </summary>
    public partial class Inspectiondetail : Window
    {
        SPI_SPCSDB_ProdEntities db = new SPI_SPCSDB_ProdEntities();
        public Inspectiondetail(mUser user, string sapcode, string LotNos,string type,string insType)
        {
            InitializeComponent();
            TheType = type;
            SapCode = sapcode;
            LotNo = LotNos;
            currentuser = user;
            
            LoadInspectionitem();


            Attributedata.Visibility = Visibility.Hidden;
            datain.Visibility = Visibility.Hidden;
            Input.Visibility = Visibility.Hidden;
            STARTINSPECT.Visibility = Visibility.Hidden;
            SetNA.Visibility = Visibility.Hidden;

            Input.IsEnabled = false;
            btnSave.IsEnabled = false;
            namecurrentuser.Content = user.FirstName + " " + user.LastName;
        }
        string TheType,SapCode,LotNo;
        mUser currentuser = new mUser();
        int currentIndex = 0;
      
        public class theData
        {
            public string Sequence { get; set; }
            public decimal Data { get; set; }
            public string Judgement { get; set; }
        }

        public class theVisualData
        {
            public string AC { get; set; }
            public string NC { get; set; }
            public string NA { get; set; }
        }

        List<theData> Datalist = new List<theData>();
        List<decimal> Datamultipoint = new List<decimal>();
        int sampling = 1;
        int multipointcount = 1;


        private void SingleMultipoint(DetailsInspect item)
        {
            try
            {
                theData data = new theData();
                if (sampling <= item.SamplingSize)
                {
                    decimal MIN, MAX;
                    if (item.LSL != "")
                    {
                        MIN = Convert.ToDecimal(item.Target) + Convert.ToInt32(item.LSL);
                    }
                    else if (item.MIN != "")
                    {
                        MIN = Convert.ToDecimal(item.MIN);
                    }
                    else
                    {
                        MIN = -9999;
                    }

                    if (item.USL != "")
                    {
                        MAX = Convert.ToDecimal(item.Target) + Convert.ToInt32(item.USL);
                    }
                    else if (item.MAX != "")
                    {
                        MAX = Convert.ToDecimal(item.MAX);
                    }
                    else
                    {
                        MAX = 9999;
                    }
                    data.Sequence = "DATA " + sampling;
                    data.Data = Convert.ToDecimal(Input.Text);
                    decimal DATAInpt = Convert.ToDecimal(Input.Text);
                    if (DATAInpt >= MIN && DATAInpt <= MAX)
                    {
                        data.Judgement = "AC";
                    }
                    else
                    {
                        data.Judgement = "NC";
                    }

                    Datalist.Add(data);
                    DatainsertGrid.ItemsSource = Datalist.ToList();
                    sampling++;
                    Input.Text = "";
                }
            }
            catch(Exception err) { }
           
        }
        private void Multiplemultipoint(DetailsInspect item, List<decimal> multidata)
        {
            try
            {
                switch (item.Formula)
                {
                    case "AVG":
                        Input.Text = returnAVG(multidata).ToString();
                        break;
                    case "MIN":
                        Input.Text = returnMIN(multidata).ToString();
                        break;
                    case "MAX":
                        Input.Text = returnMAX(multidata).ToString();
                        break;
                    case "MAX-MIN":
                        Input.Text = returnMaxMinusMin(multidata).ToString();
                        break;
                    case "MAX+MIN":
                        Input.Text = returnMaxAddMin(multidata).ToString();
                        break;
                }
                SingleMultipoint(item);
                Datamultipoint = new List<decimal>();
            }
            catch(Exception err) { }
        }
        private decimal returnAVG(List<decimal> data)
        {
            Decimal val = 0;
            int counter = 0;
            foreach (decimal value in data)
            {
                val += value;
                counter++;
            }
            val = (val / counter);
            val = Math.Round(val, 3);
            return val;
        }
        private decimal returnMIN(List<decimal> data)
        {
            Decimal val = 0;
            int itemsCount = data.Count;
            Decimal[] decimalArray = new decimal[itemsCount];
            for (int i = 0; i < itemsCount; i++)
            {
                decimalArray[i] = Convert.ToDecimal(data[i]);
            }
            val = decimalArray.Min();
            val = Math.Round(val, 3);
            return val;
        }
        private decimal returnMAX(List<decimal> data)
        {
            Decimal val = 0;
            int itemsCount = data.Count;
            Decimal[] decimalArray = new decimal[itemsCount];

            for (int i = 0; i < itemsCount; i++)
            {
                decimalArray[i] = Convert.ToDecimal(data[i]);
            }
            val = decimalArray.Max();
            val = Math.Round(val, 3);
            return val;
        }
        private Decimal returnMaxMinusMin(List<decimal> data)
        {
            Decimal val = 0;
            decimal max = returnMAX(data);
            decimal min = returnMIN(data);
            val = (max - min);
            val = Math.Round(val, 3);
            return val;
        }
        private Decimal returnMaxAddMin(List<decimal> data)
        {
            Decimal val = 0;
            decimal max = returnMAX(data);
            decimal min = returnMIN(data);
            val = (max + min);
            val = Math.Round(val, 3);
            return val;
        }
        
        private void InputData(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DetailsInspect item = (DetailsInspect)detailsgrid.SelectedItem;
                if(item.Multipoint == 0)
                {
                    SingleMultipoint(item);
                }
                else
                {
                    Multipoint.Content = multipointcount + "/" + item.Multipoint;
                    if (multipointcount == item.Multipoint)
                    {
                        Datamultipoint.Add(Convert.ToDecimal(Input.Text));
                        Multiplemultipoint(item, Datamultipoint);
                        multipointcount = 1;
                    }
                    else
                    {
                        Datamultipoint.Add(Convert.ToDecimal(Input.Text));
                        multipointcount++;
                        Input.Text = "";
                    }
                }
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (DatainsertGrid.HasItems)
            {
                DetailsInspect item = (DetailsInspect)detailsgrid.SelectedItem;
                iInspectionItem inspectionITEM = (from c in db.iInspectionItems where c.ID == item.ID select c).FirstOrDefault();

                //foreach (DetailsInspect item in detailsgrid.SelectedItems)
                //{
                if (item.Datatype != "ATTRIBUTE") { 
                    List<theData> datahere = DatainsertGrid.Items.OfType<theData>().ToList();
                    string OVERALLJudgement = "AC";
                    foreach (theData d in datahere)
                    {
                        if (d.Judgement == "NC")
                        {
                            OVERALLJudgement = "NC";
                        }
                        iInspectionDetail inspectDetail = new iInspectionDetail();

                        inspectDetail.InspectionItemId = item.ID;
                        inspectDetail.CheckItemNo = item.No;
                        inspectDetail.Sequence = sampling;
                        inspectDetail.Data = Convert.ToDecimal(d.Data);
                        inspectDetail.Judgement = d.Judgement;
                        inspectDetail.IsPartial = false;
                        inspectDetail.IsFinished = true;
                        inspectDetail.IsDeleted = false;

                        inspectDetail.RegisterID = currentuser.UserID;
                        inspectDetail.RegisterPG = "PG";
                        inspectDetail.RegisterDate = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm tt"), "yyyy-MM-dd HH:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                        inspectDetail.UpdateID = currentuser.UserID;
                        inspectDetail.UpdatePG = "PG";
                        inspectDetail.UpdateDate = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm tt"), "yyyy-MM-dd HH:mm tt", System.Globalization.CultureInfo.InvariantCulture);

                        db.iInspectionDetails.Add(inspectDetail);
                        db.SaveChanges();
                        //}

                        inspectionITEM.OverallJudgement = OVERALLJudgement;
                        inspectionITEM.IsFinished = true;
                        db.Entry(inspectionITEM).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                else
                {
                    List<theVisualData> thevisualdatahere = DatainsertGrid.Items.OfType<theVisualData>().ToList();
                    SaveVisual("AC", Convert.ToInt32(thevisualdatahere[0].AC),item);
                    SaveVisual("NC", Convert.ToInt32(thevisualdatahere[0].NC), item);
                    SaveVisual("NA", Convert.ToInt32(thevisualdatahere[0].NA), item);

                    if(thevisualdatahere[0].AC == "0" && thevisualdatahere[0].NC == "0")
                    {
                        inspectionITEM.OverallJudgement = "NA";
                    }
                    else if(Convert.ToInt32(thevisualdatahere[0].NC) > 0)
                    {
                        inspectionITEM.OverallJudgement = "NC";
                    }
                    else
                    {
                        inspectionITEM.OverallJudgement = "AC";
                    }

                    inspectionITEM.IsFinished = true;
                    db.Entry(inspectionITEM).State = EntityState.Modified;
                    db.SaveChanges();
                }

                sampling = 1;
                DatainsertGrid.ItemsSource = null;
                detailsgrid.SelectedIndex++;
                Datalist = new List<theData>();
                MethodSelected(sender, null);
               
            }
            else
            {
                MessageBox.Show("Please select check item to inspect");
            }
        }


        private void SaveVisual(string Judgement, int count, DetailsInspect item)
        {

            for (int x = 0; x < count; x++)
            {
                iInspectionDetail inspectDetail = new iInspectionDetail();

                inspectDetail.InspectionItemId = item.ID;
                inspectDetail.CheckItemNo = item.No;
                inspectDetail.Sequence = sampling;
                inspectDetail.Data = 0;
                inspectDetail.Judgement = Judgement;
                inspectDetail.IsPartial = false;
                inspectDetail.IsFinished = true;
                inspectDetail.IsDeleted = false;
                inspectDetail.RegisterID = currentuser.UserID;
                inspectDetail.RegisterPG = "PG";
                inspectDetail.RegisterDate = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm tt"), "yyyy-MM-dd HH:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                inspectDetail.UpdateID = currentuser.UserID;
                inspectDetail.UpdatePG = "PG";
                inspectDetail.UpdateDate = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm tt"), "yyyy-MM-dd HH:mm tt", System.Globalization.CultureInfo.InvariantCulture);

                db.iInspectionDetails.Add(inspectDetail);
                db.SaveChanges();
            }

        }
        private void detailsgrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sampling = 1;
            multipointcount = 1;
            Datamultipoint = new List<decimal>();
            Datalist = new List<theData>();
            try
            {
                insss.Text = "START INSPECT";
                DetailsInspect item = (DetailsInspect)detailsgrid.SelectedItem;
                
                if (Method.SelectedIndex != -1)
                {
                    datain.Visibility = Visibility.Visible;
                    Input.Visibility = Visibility.Visible;
                    
                    if (item.Datatype == "DATA")
                    {
                        STARTINSPECT.Visibility = Visibility.Visible;
                        Attributedata.Visibility = Visibility.Hidden;
                    }
                    else if (item.Datatype == "DATA INPUT")
                    {
                        STARTINSPECT.Visibility = Visibility.Hidden;
                        Attributedata.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        Attributedata.Visibility = Visibility.Visible;
                        datain.Visibility = Visibility.Hidden;
                        Input.Visibility = Visibility.Hidden;
                        STARTINSPECT.Visibility = Visibility.Hidden;
                    }
                }
                SamplingSize.Content = item.SamplingSize;
                if (item.Judgement == null)
                {
                    btnSave.IsEnabled = true;
                    DatainsertGrid.ItemsSource = null;
                }
                else
                {
                    btnSave.IsEnabled = false;
                    List<theData> datalist = (from c in db.iInspectionDetails
                                              where c.InspectionItemId == item.ID
                                              select new theData
                                              {
                                                  Data = c.Data,
                                                  Judgement = c.Judgement
                                              }).ToList();
                    int counter = 1;
                    foreach (theData d in datalist)
                    {
                        d.Sequence = "DATA " + counter;
                        counter++;
                    }
                    DatainsertGrid.ItemsSource = datalist;
                  
                }
            }
            catch(Exception err) {
                btnSave.IsEnabled = true;
                DatainsertGrid.ItemsSource = null;
            }
        }
        private void InspectionScreen_Click(object sender, RoutedEventArgs e)
        {
            Inspectionitem InspectItem = new Inspectionitem(currentuser);
            InspectItem.Show();
            this.Close();
        }
        private void Nextbtn_Click(object sender, RoutedEventArgs e)
        {
            detailsgrid.SelectedIndex++;
            DatainsertGrid.ItemsSource = null;
            Datalist = new List<theData>();
            detailsgrid_SelectionChanged(sender, null);
        }
        private void Prevbtn_Click(object sender, RoutedEventArgs e)
        {
            detailsgrid.SelectedIndex--;
            DatainsertGrid.ItemsSource = null;
            Datalist = new List<theData>();
            detailsgrid_SelectionChanged(sender, null);
        }
        private void SetNA_Click(object sender, RoutedEventArgs e)
        {
            Input.Text = "NA";
            btnSave.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }

        private void SetDataVisual_Click(object sender, RoutedEventArgs e)
        {
            if (ACdata.Text == "" && NCdata.Text == "" && NAdata.Text == "")
            {
                MessageBox.Show("No Data to add");
            }
            else
            {
                int AC = (ACdata.Text != "") ? Convert.ToInt32(ACdata.Text) : 0;
                int NC = (NCdata.Text != "") ? Convert.ToInt32(NCdata.Text) : 0;
                int NA = (NAdata.Text != "") ? Convert.ToInt32(NAdata.Text) : 0;

                if ((AC + NC + NA) > Convert.ToInt32(SamplingSize.Content))
                {
                    MessageBox.Show("Data exceed Sampling Size: " + SamplingSize.Content);
                }
                else if ((AC + NC + NA) != Convert.ToInt32(SamplingSize.Content))
                {
                    MessageBox.Show("Data insufficient. Sampling Size: " + SamplingSize.Content);
                }
                else
                {
                    List<theVisualData> visualdata = new List<theVisualData>();
                    theVisualData da = new theVisualData();
                    da.AC = (ACdata.Text != "") ? ACdata.Text : "0";
                    da.NC = (NCdata.Text != "") ? NCdata.Text : "0";
                    da.NA = (NAdata.Text != "") ? NAdata.Text : "0";
                    visualdata.Add(da);
                    DatainsertGrid.ItemsSource = visualdata;
                }
            }

        }

        private void STARTINSPECT_Click(object sender, RoutedEventArgs e)
        {
            Input.Focus();
            insss.Text = "STARTING...";
        }
        public void LoadInspectionitem()
        {
            if (TheType.Equals("SP"))
            {
                List<DetailsInspect> items = (from c in db.iInspectionItems
                                      join sp in db.cMCSSPs
                                      on c.MCSChildID equals sp.MCSSPID
                                      where c.SAPCode == SapCode
                                      && c.LotNo == LotNo
                                      select new DetailsInspect
                                      {
                                      Method = sp.Method
                                      }).ToList();
                List<string> method = items.Select(c => c.Method).Distinct().ToList();
                Method.Items.Clear();
                Method.Items.Insert(0, "SELECT METHOD");
                foreach (string m in method)
                {
                    Method.Items.Add(m);
                }

                List<DetailsInspect> List = (from c in db.iInspectionItems
                                             join sp in db.cMCSSPs
                                             on c.MCSChildID equals sp.MCSSPID
                                             where c.SAPCode == SapCode
                                             && c.LotNo == LotNo
                                             select new DetailsInspect
                                             {
                                                 ID = c.ID,
                                                 No = sp.No,
                                                 CheckItem = sp.CheckItem,
                                                 LSL = sp.LowerTOL,
                                                 Target = sp.NominalValue,
                                                 USL = sp.UpperTOL,
                                                 MIN = sp.Min,
                                                 MAX = sp.Max,
                                                 Multipoint = sp.MPCNo,
                                                 Formula = sp.MPCCriteria,
                                                 SamplingSize = c.SamplingSize,
                                                 Method = sp.Method,
                                                 Judgement = c.OverallJudgement,
                                                 Datatype = sp.DataType
                                             }).ToList();
                detailsgrid.ItemsSource = List;
                
            }
            else if(TheType.Equals("FG"))
            {
                List<DetailsInspect> items = (from c in db.iInspectionItems
                                              join fg in db.cMCSFGs
                                              on c.MCSChildID equals fg.MCSFGID
                                              where c.SAPCode == SapCode
                                              && c.LotNo == LotNo
                                              select new DetailsInspect
                                              {
                                                  Method = fg.Method
                                              }).ToList();
                List<string> method = items.Select(c => c.Method).Distinct().ToList();
                Method.Items.Clear();
                Method.Items.Insert(0, "SELECT METHOD");
                foreach (string m in method)
                {
                    Method.Items.Add(m);
                }

                List<DetailsInspect> List = (from c in db.iInspectionItems
                                             join fg in db.cMCSFGs
                                             on c.MCSChildID equals fg.MCSFGID
                                             where c.SAPCode == SapCode
                                             && c.LotNo == LotNo
                                             select new DetailsInspect
                                             {
                                                 ID = c.ID,
                                                 No = fg.No,
                                                 CheckItem = fg.CheckItem,
                                                 LSL = fg.CTQMin,
                                                 Target = fg.NominalValue,
                                                 USL = fg.CTQMax,
                                                 MIN = fg.Min,
                                                 MAX = fg.Max,
                                                 Multipoint = fg.MPCNo,
                                                 Formula = fg.MPCCriteria,
                                                 SamplingSize = c.SamplingSize,
                                                 Method = fg.Method,
                                                 Judgement = c.OverallJudgement,
                                                 Datatype = fg.DataType
                                             }).ToList();
                detailsgrid.ItemsSource = List;
            }
            else
            {
                MessageBox.Show("Please recheck the Checksheet");
            }
            
        }
        private void MethodSelected(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                currentIndex = detailsgrid.SelectedIndex;
                SetNA.Visibility = Visibility.Visible;
                Input.IsEnabled = true;
                List<SPModel> splist = new List<SPModel>();
                detailsgrid.Columns.Clear();
                detailsgrid.ItemsSource = null;

                string selected = Method.SelectedValue.ToString();
                if (TheType.Contains("SP"))
                {
                    List<DetailsInspect> List = (from c in db.iInspectionItems
                                                 join sp in db.cMCSSPs
                                                 on c.MCSChildID equals sp.MCSSPID
                                                 where c.SAPCode == SapCode
                                                 && c.LotNo == LotNo
                                                 && sp.Method == selected
                                                 && sp.Is_Deleted == false
                                                 select new DetailsInspect
                                                 {
                                                     ID = c.ID,
                                                     No = sp.No,
                                                     CheckItem = sp.CheckItem,
                                                     LSL = sp.LowerTOL,
                                                     Target = sp.NominalValue,
                                                     USL = sp.UpperTOL,
                                                     MIN = sp.Min,
                                                     MAX = sp.Max,
                                                     Multipoint = sp.MPCNo,
                                                     Formula = sp.MPCCriteria,
                                                     SamplingSize = c.SamplingSize,
                                                     Judgement = c.OverallJudgement,
                                                     Datatype = sp.DataType
                                                 }).ToList();
                    detailsgrid.ItemsSource = List;
                    DetailsInspect item = (DetailsInspect)detailsgrid.SelectedItem;
                    SamplingSize.Content = item.SamplingSize;
                    if (item.Judgement == null)
                    {
                        btnSave.IsEnabled = true;
                        DatainsertGrid.ItemsSource = null;
                    }
                    else
                    {
                        btnSave.IsEnabled = false;

                        List<theData> datalist = (from c in db.iInspectionDetails
                                                  where c.InspectionItemId == item.ID
                                                  select new theData
                                                  {
                                                      Data = c.Data,
                                                      Judgement = c.Judgement
                                                  }).ToList();
                        int counter = 1;
                        foreach (theData d in datalist)
                        {
                            d.Sequence = "DATA " + counter;
                            counter++;
                        }

                        DatainsertGrid.ItemsSource = datalist;

                    }
                }
                else if(TheType.Contains("FG"))
                {
                    List<DetailsInspect> List = (from c in db.iInspectionItems
                                                 join fg in db.cMCSFGs
                                                 on c.MCSChildID equals fg.MCSFGID
                                                 where c.SAPCode == SapCode
                                                 && c.LotNo == LotNo
                                                 && fg.Method == selected
                                                 && fg.Is_Deleted == false
                                                 select new DetailsInspect
                                                 {
                                                     ID = c.ID,
                                                     No = fg.No,
                                                     CheckItem = fg.CheckItem,
                                                     LSL = fg.CTQMin,
                                                     Target = fg.NominalValue,
                                                     USL = fg.CTQMax,
                                                     MIN = fg.Min,
                                                     MAX = fg.Max,
                                                     Multipoint = fg.MPCNo,
                                                     Formula = fg.MPCCriteria,
                                                     SamplingSize = c.SamplingSize,
                                                     Judgement = c.OverallJudgement,
                                                     Datatype = fg.DataType
                                                 }).ToList();


                    detailsgrid.ItemsSource = List;

                    DetailsInspect item = (DetailsInspect)detailsgrid.SelectedItem;
                    SamplingSize.Content = item.SamplingSize;
                    if (item.Judgement == null)
                    {
                        btnSave.IsEnabled = true;
                        DatainsertGrid.ItemsSource = null;
                    }
                    else
                    {
                        btnSave.IsEnabled = false;

                        List<theData> datalist = (from c in db.iInspectionDetails
                                                  where c.InspectionItemId == item.ID
                                                  select new theData
                                                  {
                                                      Data = c.Data,
                                                      Judgement = c.Judgement
                                                  }).ToList();
                        int counter = 1;
                        foreach (theData d in datalist)
                        {
                            d.Sequence = "DATA " + counter;
                            counter++;
                        }
                        DatainsertGrid.ItemsSource = datalist;
                    }
                }

               
            }
            catch(Exception err)
            {
                SamplingSize.Content = "0";
            }
            detailsgrid.SelectedIndex = currentIndex;
        }
        
    }
}
