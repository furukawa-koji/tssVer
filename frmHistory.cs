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
        private static System.Data.DataSet ds = new System.Data.DataSet();
        private static System.Data.DataTable dt = new System.Data.DataTable();


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
            if (frm.dgv.DataSource == null) return;
            InitGrid();

            frm.ShowDialog();
        }



        private static void InitGrid()
        {
            frm.dgv.Columns["ver"].ReadOnly = true;
            frm.dgv.Columns["user"].ReadOnly = true;

            frm.dgv.Columns["ver"].Width = 150;
            frm.dgv.Columns["user"].Width = 250;
            frm.dgv.Columns["text"].Width = 500;
        }


        /// <summary>
        /// 履歴更新
        /// </summary>
        /// <returns></returns>
        private static bool WriteHistory()
        {   
            try
            {
                if (!System.IO.File.Exists(strHistoryFile))
                {                    
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(strHistoryFile);
                    sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                    sw.WriteLine("<history>");
                    sw.WriteLine($"	<ver>{DateTime.Now.ToString("yyyy.MMdd-HHmm")}</ver>");
                    sw.WriteLine($"	<user>{Environment.UserName}</user>");
                    sw.WriteLine("	<text></text>");
                    sw.WriteLine("</history>");
                    sw.Close();
                    ds.ReadXml(strHistoryFile);
                    dt = ds.Tables["history"];

                };

                dt.WriteXml(strHistoryFile);

                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"{System.Reflection.MethodBase.GetCurrentMethod().Name} {ex.Message}");
                return false;
            }
        }



        /// <summary>
        /// 履歴ロード
        /// </summary>
        /// <returns></returns>
        private static System.Data.DataTable LoadHistory()
        {
            ds.Clear();
            dt.Clear();
            try
            {
                if (!System.IO.File.Exists(strHistoryFile)) return null;

                ds.ReadXml(strHistoryFile);
                dt = ds.Tables["history"];
                if (dt.Rows.Count == 0)
                {
                    dt.Columns.Add("ver");
                    dt.Columns.Add("name");
                    dt.Columns.Add("text");

                    DataRow r = dt.NewRow();
                    
                    return dt;
                }

                
                dt.AcceptChanges();
                DataView dv = dt.AsDataView();
                dv.Sort = "ver desc";
                dt=dv.ToTable();
                return dt;
            }
            catch(Exception ex)
            { 
                return null; 
            }

        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {

            if(!WriteHistory())MessageBox.Show("更新失敗");
            else MessageBox.Show("更新成功");
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            frm.dgv.Rows[e.RowIndex].Cells["ver"].Value = $"{DateTime.Now.ToString("yy.MMdd.HHmm")}";
            frm.dgv.Rows[e.RowIndex].Cells["user"].Value = $"{Environment.UserName}";
        }
    }
}
