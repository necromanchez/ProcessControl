using SPC_DesktopV2.InspectionDetail;
using SPC_DesktopV2.SPCModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SPC_DesktopV2.InspectionItem
{
    /// <summary>
    /// Interaction logic for Inspectionitem.xaml
    /// </summary>
    public partial class Inspectionitem : Window
    {
        SPI_SPCSDB_ProdEntities db = new SPI_SPCSDB_ProdEntities();
        mUser currentuser = new mUser();
        public Inspectionitem(mUser user)
        {
            currentuser = user;
            InitializeComponent();
            loadStages();
            newInspection.IsChecked = true;
            GetUnfinishedLots();
            namecurrentuser.Content = user.FirstName + " " + user.LastName;
        }
        string type = "";
        string theLots = "";
        private void loadStages()
        {
            this.Stage.Items.Insert(0, "SELECT STAGE");
            this.Stage.SelectedIndex = 0;
            List<mCode> stageNameList = new List<mCode>();
            
            stageNameList = (from c in db.mCodes where c.CodeType.ToUpper().Contains("STAGE") && c.IsDeleted == false select c).ToList();

            foreach (object o in stageNameList)
            {
                mCode stage = (mCode)o;
                this.Stage.Items.Add(stage.CodeName);
            }  
        }
        
        private void loadDataRelatedFromChosenSAPCode(object sender, RoutedEventArgs e)
        {
            try
            {
                string sapCode = Regex.Replace(Sapcode.Text, @"\s+", "");
                string type = gettype(sapCode);
                if (type.Equals("SP"))
                {
                    List<SPModel> items = loadSAPCodeDataSP(sapCode);

                    thegrid.ItemsSource = items;
                    //GET PartCode
                    string mcsid = items[0].MCSID;
                    cMC Master = (from c in db.cMCS where c.MCSID == mcsid select c).FirstOrDefault();
                    DrawingCode.Content = Master.DrawingCode;
                    PartNo.Content = Master.Model;
                    Process.Content = items[0].Process;
                    Product.Content = (from c in db.mProducts where c.ProductCode == Master.ProductCode select c.ProductName).FirstOrDefault();
                }
                else
                {
                    List<FGModel> items = loadSAPCodeDataFG(sapCode);
                    thegrid.ItemsSource = items;

                    //GET PartCode
                    string mcsid = items[0].MCSID;
                    cMC Master = (from c in db.cMCS where c.MCSID == mcsid select c).FirstOrDefault();
                    DrawingCode.Content = Master.DrawingCode;
                    PartNo.Content = Master.Model;
                    Process.Content = items[0].Process;
                    Product.Content = (from c in db.mProducts where c.ProductCode == Master.ProductCode select c.ProductName).FirstOrDefault();
                }
            }
            catch(Exception err)
            {

            }
        }

        private void loadDataRelated(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                loadDataRelatedFromChosenSAPCode(sender, new RoutedEventArgs());
            }
        }

        private string gettype(string sapcode)
        {
            type = (from mcs in db.cMCS
                           join sp in db.cMCSSPs on mcs.MCSID equals sp.MCSID
                           //join fg in db.cMCSFGs on mcs.MCSID equals fg.MCSID
                           where sp.SAPCode == sapcode
                           && sp.Is_Deleted == false
                           select mcs.Type).FirstOrDefault();
            if(type == null)
            {
                type = (from mcs in db.cMCS
                               //join sp in db.cMCSSPs on mcs.MCSID equals sp.MCSID
                               join fg in db.cMCSFGs on mcs.MCSID equals fg.MCSID
                               where fg.SAPCode == sapcode
                               && fg.Is_Deleted == false
                               select mcs.Type).FirstOrDefault();
            }
            return type;
        }

        private List<SPModel> loadSAPCodeDataSP(string sapCode)
        {
            
            List<SPModel> splist = new List<SPModel>();
            if (newInspection.IsChecked == true)
            {
                    if (Stage.Text == "FIRST ARTICLE")
                    {
                        splist = (from sp in db.cMCSSPs
                                    join mcs in db.cMCS on sp.MCSID equals mcs.MCSID
                                    where sp.Is_Deleted == false && mcs.IsDeleted == false
                                    && sp.InspectionStatus == "FA"
                                    && sp.SAPCode == sapCode
                                    orderby sp.No
                                    select new SPModel
                                    {
                                        MCSID = sp.MCSID
                                        ,MCSSPID = sp.MCSSPID
                                        ,Process = sp.Process
                                        ,Method = sp.Method
                                        ,No = sp.No
                                        ,CheckItem = sp.CheckItem
                                        ,NominalValue = sp.NominalValue
                                        ,LowerTOL = sp.LowerTOL
                                        ,UpperTOL = sp.LowerTOL
                                        ,Min = sp.Min
                                        ,Max = sp.Max
                                        ,DataType = sp.DataType
                                        ,Calculation = sp.Calculation
                                        ,MPCNo = sp.MPCNo
                                        ,MPCCriteria = sp.MPCCriteria,
                                        Remarks = sp.Remark
                                    }).ToList();
                    }
                    else
                    {
                        splist = (from sp in db.cMCSSPs
                                    join mcs in db.cMCS on sp.MCSID equals mcs.MCSID
                                    where sp.Is_Deleted == false && mcs.IsDeleted == false
                                    && sp.InspectionStatus.Trim() == "NORMAL"
                                    && sp.SAPCode == sapCode
                                    orderby sp.No
                                       select new SPModel
                                       {
                                           MCSID = sp.MCSID
                                          ,MCSSPID = sp.MCSSPID
                                          ,Process = sp.Process
                                          ,Method = sp.Method
                                          ,No = sp.No
                                          ,CheckItem = sp.CheckItem
                                          ,NominalValue = sp.NominalValue
                                          ,LowerTOL = sp.LowerTOL
                                          ,UpperTOL = sp.LowerTOL
                                          ,Min = sp.Min
                                          ,Max = sp.Max
                                          ,DataType = sp.DataType
                                          ,Calculation = sp.Calculation
                                          ,MPCNo = sp.MPCNo
                                          ,MPCCriteria = sp.MPCCriteria,
                                           Remarks = sp.Remark
                                       }).ToList();
                }
                
            }
            else
            {
                if (Stage.Text == "FIRST ARTICLE")
                {
                    splist = (from sp in db.cMCSSPs
                              join mcs in db.cMCS on sp.MCSID equals mcs.MCSID
                              join ins in db.iInspectionItems on sp.MCSSPID equals ins.MCSChildID
                              where sp.Is_Deleted == false && mcs.IsDeleted == false
                              && sp.InspectionStatus == "FA"
                              && sp.SAPCode == sapCode
                              && ins.LotNo == Lotnocontainer.Text
                              orderby sp.No
                              select new SPModel
                              {
                                  MCSID = sp.MCSID
                                  ,MCSSPID = sp.MCSSPID
                                  ,Process = sp.Process
                                  ,Method = sp.Method
                                  ,No = sp.No
                                  ,CheckItem = sp.CheckItem
                                  ,NominalValue = sp.NominalValue
                                  ,LowerTOL = sp.LowerTOL
                                  ,UpperTOL = sp.LowerTOL
                                  ,Min = sp.Min
                                  ,Max = sp.Max
                                  ,DataType = sp.DataType
                                  ,Calculation = sp.Calculation
                                  ,MPCNo = sp.MPCNo
                                  ,MPCCriteria = sp.MPCCriteria
                                  ,Remarks = sp.Remark

                              }).ToList();
                }
                else
                {
                    splist = (from sp in db.cMCSSPs
                              join mcs in db.cMCS on sp.MCSID equals mcs.MCSID
                              join ins in db.iInspectionItems on sp.MCSSPID equals ins.MCSChildID
                              where sp.Is_Deleted == false && mcs.IsDeleted == false
                              && sp.InspectionStatus.Trim() == "NORMAL"
                              && sp.SAPCode == sapCode
                              && ins.LotNo == Lotnocontainer.Text
                              orderby sp.No
                              select new SPModel
                              {
                                  MCSID = sp.MCSID
                                 ,MCSSPID = sp.MCSSPID
                                 ,Process = sp.Process
                                 ,Method = sp.Method
                                 ,No = sp.No
                                 ,CheckItem = sp.CheckItem
                                 ,NominalValue = sp.NominalValue
                                 ,LowerTOL = sp.LowerTOL
                                 ,UpperTOL = sp.LowerTOL
                                 ,Min = sp.Min
                                 ,Max = sp.Max
                                 ,DataType = sp.DataType
                                 ,Calculation = sp.Calculation
                                 ,MPCNo = sp.MPCNo
                                 ,MPCCriteria = sp.MPCCriteria
                                 ,Remarks = sp.Remark
                              }).ToList();
                }
            }
            return splist;
        }

        private List<FGModel> loadSAPCodeDataFG(string sapCode)
        {
            List<FGModel> fglist = new List<FGModel>();
            if (newInspection.IsChecked == true)
            {

                if (Stage.Text == "FIRST ARTICLE")
                {
                    fglist = (from fg in db.cMCSFGs
                              join mcs in db.cMCS on fg.MCSID equals mcs.MCSID
                              where fg.Is_Deleted == false && mcs.IsDeleted == false
                              && fg.InspectionStatus == "FA"
                              && fg.SAPCode == sapCode
                              orderby fg.No
                              select new FGModel
                              {
                                  MCSID = fg.MCSID,
                                  MCSFGID = fg.MCSFGID,
                                  Process = fg.Process,
                                  Method = fg.Method,
                                  No = fg.No,
                                  CheckItem = fg.CheckItem,
                                  NominalValue = fg.NominalValue,
                                  CTQMin = fg.CTQMin,
                                  CTQMax = fg.CTQMax,
                                  Min = fg.Min,
                                  Max = fg.Max,
                                  DataType = fg.DataType,
                                  Calculation = fg.Calculation,
                                  MPCNo = fg.MPCNo,
                                  MPCCriteria = fg.MPCCriteria,
                                  Remarks = fg.Remark
                              }).ToList();
                }
                else
                {
                    fglist = (from fg in db.cMCSFGs
                              join mcs in db.cMCS on fg.MCSID equals mcs.MCSID
                              where fg.Is_Deleted == false && mcs.IsDeleted == false
                              && fg.InspectionStatus == "NORMAL"
                              && fg.SAPCode == sapCode
                              orderby fg.No
                              select new FGModel
                              {
                                  MCSID = fg.MCSID,
                                  MCSFGID = fg.MCSFGID,
                                  Process = fg.Process,
                                  Method = fg.Method,
                                  No = fg.No,
                                  CheckItem = fg.CheckItem,
                                  NominalValue = fg.NominalValue,
                                  CTQMin = fg.CTQMin,
                                  CTQMax = fg.CTQMax,
                                  Min = fg.Min,
                                  Max = fg.Max,
                                  DataType = fg.DataType,
                                  Calculation = fg.Calculation,
                                  MPCNo = fg.MPCNo,
                                  MPCCriteria = fg.MPCCriteria,
                                  Remarks = fg.Remark
                              }).ToList();

                }

            }
            else
            {
                if (Stage.Text == "FIRST ARTICLE")
                {
                    fglist = (from fg in db.cMCSFGs
                              join mcs in db.cMCS on fg.MCSID equals mcs.MCSID
                              join ins in db.iInspectionItems on fg.MCSFGID equals ins.MCSChildID
                              where fg.Is_Deleted == false && mcs.IsDeleted == false
                              && fg.InspectionStatus == "FA"
                              && fg.SAPCode == sapCode
                              && ins.LotNo == Lotnocontainer.Text
                              orderby fg.No
                              select new FGModel
                              {
                                  MCSID = fg.MCSID,
                                  MCSFGID = fg.MCSFGID,
                                  Process = fg.Process,
                                  Method = fg.Method,
                                  No = fg.No,
                                  CheckItem = fg.CheckItem,
                                  NominalValue = fg.NominalValue,
                                  CTQMin = fg.CTQMin,
                                  CTQMax = fg.CTQMax,
                                  Min = fg.Min,
                                  Max = fg.Max,
                                  DataType = fg.DataType,
                                  Calculation = fg.Calculation,
                                  MPCNo = fg.MPCNo,
                                  MPCCriteria = fg.MPCCriteria,
                                  Remarks = fg.Remark
                              }).ToList();
                }
                else
                {
                    fglist = (from fg in db.cMCSFGs
                              join mcs in db.cMCS on fg.MCSID equals mcs.MCSID
                              join ins in db.iInspectionItems on fg.MCSFGID equals ins.MCSChildID
                              where fg.Is_Deleted == false && mcs.IsDeleted == false
                              && fg.InspectionStatus.Trim() == "NORMAL"
                              && fg.SAPCode == sapCode
                              && ins.LotNo == Lotnocontainer.Text
                              orderby fg.No
                              select new FGModel
                              {
                                  MCSID = fg.MCSID,
                                  MCSFGID = fg.MCSFGID,
                                  Process = fg.Process,
                                  Method = fg.Method,
                                  No = fg.No,
                                  CheckItem = fg.CheckItem,
                                  NominalValue = fg.NominalValue,
                                  CTQMin = fg.CTQMin,
                                  CTQMax = fg.CTQMax,
                                  Min = fg.Min,
                                  Max = fg.Max,
                                  DataType = fg.DataType,
                                  Calculation = fg.Calculation,
                                  MPCNo = fg.MPCNo,
                                  MPCCriteria = fg.MPCCriteria,
                                  Remarks = fg.Remark
                              }).ToList();
                }
            }
            return fglist;
        }
        
        private int LoadSamplingSize(string method)
        {
            string txtQuantity = Quantity.Text;
            txtQuantity = Regex.Replace(txtQuantity, @"\s+", "");
            int QTY = Convert.ToInt32(txtQuantity);
            if (Stage.Text == "PRODUCTION INSPECTION" && (Process.Content.ToString() == "CASTING" || Process.Content.ToString() == "HT"))
            {
                return 8;
            }
            mSampling sampling = (from c in db.mSamplings where QTY >= c.LotSizeLower && QTY <= c.LotSizeUpper select c).FirstOrDefault();
            if (method.Contains("VISUAL"))
            {
                return sampling.Visual;
            }
            else
            {
                return sampling.Dimension;
            }
        }

        private void AddLotnumber(object sender, KeyEventArgs e)
        {
            string verify = @"\d{6}-\d{3}\w";
            string verifyIncoming = @"\d{11}";
            string verifyProduction = @"\w\w\d{2}-\d{6}-\d{3}\w";
            string verifyProduction2 = @"\w\w\d{2}-\d{6}-\d{3}\w-\d";
            string verifyProduction3 = @"\w-\d{6}-\d{3}\w-\d{2}";
            bool Pass = true;
            if (e.Key == Key.Enter)
            {
                try
                {
                    if (Stage.Text.Trim() == "INCOMING")
                    {
                        if (!Regex.IsMatch(Lotno.Text, verifyIncoming))
                        {
                            Pass = false;
                        }
                        if (Regex.IsMatch(Lotno.Text, verifyProduction) || Regex.IsMatch(Lotno.Text, verifyProduction2) || Regex.IsMatch(Lotno.Text, verifyProduction3))
                        {
                            MessageBox.Show("This Lot number is for PRODUCTION INSPECTION STAGE Only");
                            Pass = false;
                        }
                    }
                    else if (Stage.Text.Trim() == "PRODUCTION INSPECTION")
                    {
                        if (Regex.IsMatch(Lotno.Text, verifyProduction) || Regex.IsMatch(Lotno.Text, verifyProduction2) || Regex.IsMatch(Lotno.Text, verifyProduction3))
                        {
                            Pass = true;
                        }

                        if (Regex.IsMatch(Lotno.Text, verifyIncoming))
                        {
                            MessageBox.Show("This Lot number is for INCOMING STAGE Only");
                            Pass = false;
                        }

                    }
                    else
                    {
                        if (Regex.IsMatch(Lotno.Text, verifyIncoming) && Stage.Text.Trim() != "INCOMING")
                        {
                            MessageBox.Show("This Lot number is for INCOMING STAGE Only");
                            Pass = false;
                        }
                        if ((Regex.IsMatch(Lotno.Text, verifyProduction) || Regex.IsMatch(Lotno.Text, verifyProduction2) || Regex.IsMatch(Lotno.Text, verifyProduction3)) && Stage.Text.Trim() != "PRODUCTION INSPECTION")
                        {
                            MessageBox.Show("This Lot number is for PRODUCTION INSPECTION STAGE Only");
                            Pass = false;
                        }

                        if ((Lotno.Text[10] == 'N' || Lotno.Text[10] == 'D') && Lotno.Text.Count() == 11)
                        {
                            Pass = true;
                        }
                        else
                        {
                            Pass = false;
                        }
                    }
                    if (Pass == true)
                    {
                        theLots += Lotno.Text + "\n";
                        Lotnocontainer.Text = theLots;
                        Lotno.Text = "";
                    }
                    else
                    {
                        if (Regex.IsMatch(Lotno.Text, verifyIncoming) && Stage.Text.Trim() != "INCOMING")
                        {
                            MessageBox.Show("This Lot number is for INCOMING STAGE Only");
                            Pass = false;
                        }
                        else if((Regex.IsMatch(Lotno.Text, verifyProduction) || Regex.IsMatch(Lotno.Text, verifyProduction2) || Regex.IsMatch(Lotno.Text, verifyProduction3)) && Stage.Text.Trim() != "PRODUCTION INSPECTION")
                        {
                            MessageBox.Show("This Lot number is for PRODUCTION INSPECTION STAGE Only");
                            Pass = false;
                        }
                        else
                        {
                            MessageBox.Show("Invalid Lot Number");
                        }
                       
                    }
                }
                catch(Exception err)
                {
                    MessageBox.Show("Invalid Lot Number");
                }
            }
        }
        
        private void clearLots_Click(object sender, RoutedEventArgs e)
        {
            Lotnocontainer.Text = "";
            theLots = "";
        }

        //public bool Validation()
        //{
        //    bool result = true;

        //    if(this.Stage.SelectedIndex == 0)
        //    {
        //        result = false;
        //    }

        //}
        
        private void StartInspection_Click(object sender, RoutedEventArgs e)
        {
            string sapCode = Regex.Replace(Sapcode.Text, @"\s+", "");
            string insType = "";
            iInspectionItem exist = (from c in db.iInspectionItems where c.LotNo == Lotnocontainer.Text && c.SAPCode == sapCode select c).FirstOrDefault();
            bool proceed = true;

            if (newInspection.IsChecked == true)
            {
                insType = "NEW";
                string type = gettype(sapCode);
                if (exist == null)
                {
                    if (type.Equals("SP"))
                    {
                        List<SPModel> items = loadSAPCodeDataSP(sapCode);
                        foreach (SPModel i in items)
                        {
                            DateTime datehere = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm tt"), "yyyy-MM-dd HH:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                            iInspectionItem checkitem = new iInspectionItem();
                            checkitem.MCSID = i.MCSID;
                            checkitem.MCSChildID = i.MCSSPID;
                            checkitem.CheckItem = i.CheckItem;
                            checkitem.Stage = Stage.Text.Trim();
                            checkitem.SAPCode = sapCode;
                            checkitem.Qty = Convert.ToInt32(Quantity.Text);
                            checkitem.LotNo = Lotnocontainer.Text;
                            checkitem.SamplingSize = LoadSamplingSize(i.Method);
                            checkitem.SACStart = Convert.ToDateTime("1/1/1900 12:00:00 AM");
                            checkitem.SACEnd = Convert.ToDateTime("1/1/1900 12:00:00 AM");
                            checkitem.RegisterDate = datehere;
                            checkitem.RegisterID = currentuser.UserID;
                            checkitem.UpdateDate = datehere;
                            checkitem.UpdateID = currentuser.UserID;
                            checkitem.RegisterPG = "PG";
                            checkitem.UpdatePG = "ADMIN";
                            db.iInspectionItems.Add(checkitem);
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        List<FGModel> items = loadSAPCodeDataFG(sapCode);
                        foreach (FGModel i in items)
                        {
                            DateTime datehere = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm tt"), "yyyy-MM-dd HH:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                            iInspectionItem checkitem = new iInspectionItem();
                            checkitem.MCSID = i.MCSID;
                            checkitem.MCSChildID = i.MCSFGID;
                            checkitem.CheckItem = i.CheckItem;
                            checkitem.Stage = Stage.Text.Trim();
                            checkitem.SAPCode = sapCode;
                            checkitem.Qty = Convert.ToInt32(Quantity.Text);
                            checkitem.LotNo = Lotnocontainer.Text;
                            checkitem.SamplingSize = LoadSamplingSize(i.Method);
                            checkitem.SACStart = Convert.ToDateTime("1/1/1900 12:00:00 AM");
                            checkitem.SACEnd = Convert.ToDateTime("1/1/1900 12:00:00 AM");
                            checkitem.RegisterDate = datehere;
                            checkitem.RegisterID = currentuser.UserID;
                            checkitem.UpdateDate = datehere;
                            checkitem.UpdateID = currentuser.UserID;
                            checkitem.RegisterPG = "PG";
                            checkitem.UpdatePG = "ADMIN";
                            db.iInspectionItems.Add(checkitem);
                            db.SaveChanges();
                        }
                    }
                    MessageBox.Show("Inspection Successfully Created");
                }
                else
                {
                    MessageBox.Show("Inspection Already Exist");
                    proceed = false;
                }
            }
            else
            {
                MessageBox.Show("Continue Inspection");
            }


            if (proceed)
            {
                Inspectiondetail detail = new Inspectiondetail(currentuser, sapCode, Lotnocontainer.Text, type, insType);
                detail.Show();
                this.Close();
            }
        }

        private void LogOffScreen_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void HomeScreen_Click(object sender, RoutedEventArgs e)
        {
            Home home = new Home(currentuser);
            home.Show();
            this.Close();
        }

        private void GetUnfinishedLots()
        {
            List<UnFinished> UF_List = new List<UnFinished>();
            UF_List = (from c in db.iInspectionItems
                       where c.RegisterID == currentuser.UserID
                       group c by new
                       {
                           c.SAPCode,
                           c.LotNo,
                       } into gcs
                       select new UnFinished()
                       {
                           Sapcode = gcs.Key.SAPCode,
                           LotNo = gcs.Key.LotNo
                       }).ToList();

            unfinished.ItemsSource = UF_List;
        }

        private void unfinished_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UnFinished item = (UnFinished)unfinished.SelectedItem;
            continueInspection.IsChecked = true;

            iInspectionItem Inspect = (from c in db.iInspectionItems
                                       where c.SAPCode == item.Sapcode
                                       && c.LotNo == item.LotNo
                                       select c).FirstOrDefault();

            Stage.Text = Inspect.Stage;
            Sapcode.Text = Inspect.SAPCode;
            Quantity.Text = Convert.ToString(Inspect.Qty);
            Lotnocontainer.Text = Inspect.LotNo;
            loadDataRelatedFromChosenSAPCode(sender, null);

            DisabledAll();
        }

        private void newInspection_Checked(object sender, RoutedEventArgs e)
        {
            Stage.SelectedIndex = 0;
            Sapcode.Text = "";
            Quantity.Text = "";
            Lotno.Text = "";
            Lotnocontainer.Text = "";
            DrawingCode.Content = "";
            PartNo.Content = "";
            Process.Content = "";
            Product.Content = "";
            thegrid.ItemsSource = null;
            EnabledAll();
        }

        private void EnabledAll()
        {
            Stage.IsEnabled = true;
            Sapcode.IsEnabled = true;
            Quantity.IsEnabled = true;
            Lotno.IsEnabled = true;
        }

        private void DisabledAll()
        {
            Stage.IsEnabled = false;
            Sapcode.IsEnabled = false;
            Quantity.IsEnabled = false;
            Lotno.IsEnabled = false;
        }
    }
}
