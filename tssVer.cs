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

            if (!System.IO.File.Exists(strHistoryFile))
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(strHistoryFile,false,System.Text.Encoding.UTF8);
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");

                sw.WriteLine("<history>");
                sw.WriteLine("  <version>");
                sw.WriteLine($"	    <ver>{DateTime.Now.ToString("yyyy.MMdd-HHmm")}</ver>");
                sw.WriteLine($"	    <user>{Environment.UserName}</user>");
                sw.WriteLine("	    <text></text>");
                sw.WriteLine("  </version>");
                sw.WriteLine("</history>");

                sw.Close();
            
            }


            ds.ReadXml(strHistoryFile);
            dt = ds.Tables["version"];
            
            
            System.Data.DataView dv = dt.AsDataView();
            dv.Sort = "ver desc";
            
            System.Diagnostics.Debug.Print(dv[0].ToString());
            tsslVer.Text = $"Ver.{dv[0]["ver"].ToString()}";

        }

        private void tsslVer_Click(object sender, EventArgs e)
        {
            frmHistory.Init();

        }
    }
}
