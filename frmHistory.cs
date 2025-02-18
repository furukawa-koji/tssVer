using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace tssVer
{
    public partial class frmHistory : Form
    {
        public static string strTitle {get;set; }
        private static string strHistoryFile = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\history.xml";

        

        private frmHistory()
        {
            InitializeComponent();
        }
        public static void Init()
        {
            frmHistory frm=new frmHistory();
            frm.labelName.Text = strTitle;
            frm.dgv.DataSource = LoadHistory();

            frm.ShowDialog();
        }

        private static bool WriteHistory()
        {


            System.Data.DataSet ds = new System.Data.DataSet();
            System.Data.DataTable dt = new System.Data.DataTable();

            try
            {
                if (!System.IO.File.Exists(strHistoryFile))
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(strHistoryFile);
                    sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                    sw.WriteLine("<history>");
                    sw.WriteLine("	<ver></ver>");
                    sw.WriteLine("	<user></user>");
                    sw.WriteLine("	<text></text>");
                    sw.WriteLine("</history>");
                    sw.Close();
                };

                ds.ReadXml(strHistoryFile);
                dt = ds.Tables["history"];


                dt.WriteXml(strHistoryFile);

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }


        private static System.Data.DataTable LoadHistory()
        {

            System.Data.DataSet ds = new System.Data.DataSet();
            System.Data.DataTable dt = new System.Data.DataTable();
            
            try
            {
                if (!System.IO.File.Exists(strHistoryFile)) return null;

                ds.ReadXml(strHistoryFile);
                dt = ds.Tables["history"];
                if (dt.Rows.Count == 0) return null;

                


                return dt;
            }
            catch(Exception ex)
            { return null; 
            }

        }
    }
}
