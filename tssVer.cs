using System;
using System.Windows.Forms;

using System.Data;
using System.Runtime.CompilerServices;

namespace tssVer
{
    public partial class tssVer: UserControl
    {
        private static string strHistoryFile = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\history.xml";

        public tssVer()
        {
            InitializeComponent();

            DispVer();
        }

        private void DispVer()
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            System.Data.DataTable dt = new System.Data.DataTable();
            ds.ReadXml(strHistoryFile);
            dt = ds.Tables["version"];
            
            
            System.Data.DataView dv = dt.AsDataView();
            dv.Sort = "ver desc";
            
            System.Diagnostics.Debug.Print(dv[0].ToString());
            tsslVer.Text = $"Ver.{dv[0]["ver"].ToString()}";
            //tsslVer.Text = $"Ver.{dt.Year}.{dt.Month.ToString("00")}{dt.Day.ToString("00")}.{dt.Hour.ToString("00")}{dt.Minute.ToString("00")}";
        }

        //public void setVersion(System.Reflection.Assembly ass)
        //{
        //    ass = System.Reflection.Assembly.GetExecutingAssembly();
        //    System.Version ver = ass.GetName().Version;
        //    if (ver.Revision.ToString() == string.Empty) return;
        //    int build = ver.Build % 9000;
        //    tsslVer.Text = $"Ver.{ver.Major}.{ver.Minor}.{build}.{ver.Revision.ToString()}";
        //}
        public void setVersion(System.DateTime dt)
        {
            tsslVer.Text = $"Ver.{dt.Year}.{dt.Month.ToString("00")}{dt.Day.ToString("00")}.{dt.Hour.ToString("00")}{dt.Minute.ToString("00")}";
        }

        private void tsslVer_Click(object sender, EventArgs e)
        {
            frmHistory.Init();

        }
    }
}
