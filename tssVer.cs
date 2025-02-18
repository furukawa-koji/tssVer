using System;
using System.Windows.Forms;

namespace tssVer
{
    public partial class tssVer: UserControl
    {
        public tssVer()
        {
            InitializeComponent();            
        }

        public void setVersion(System.Reflection.Assembly ass)
        {
            ass = System.Reflection.Assembly.GetExecutingAssembly();
            System.Version ver = ass.GetName().Version;
            if (ver.Revision.ToString() == string.Empty) return;
            int build = ver.Build % 9000;
            tsslVer.Text = $"Ver.{ver.Major}.{ver.Minor}.{build}.{ver.Revision.ToString()}";
        }
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
