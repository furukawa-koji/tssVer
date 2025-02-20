using System;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace tssVer
{
    public partial class frmHistory : Form
    {
        public static string strTitle {get;set; }
        private static string strHistoryFile = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\history.xml";
        public static frmHistory frm;


        private frmHistory()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 起動時
        /// </summary>
        public static void Init()
        {
            frm=new frmHistory();
            frm.labelName.Text = strTitle;
            frm.dgv.DataSource = LoadHistory();

            frm.ShowDialog();
        }


        /// <summary>
        /// 履歴更新
        /// </summary>
        /// <returns></returns>
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

                foreach(DataRow r in dt.Rows)
                {
                    if (r.RowState == DataRowState.Modified)
                    {

                    }
                }



                dt.WriteXml(strHistoryFile);

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }



        /// <summary>
        /// 履歴ロード
        /// </summary>
        /// <returns></returns>
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
