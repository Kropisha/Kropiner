using System;
using System.Windows.Forms;

namespace Kropiner
{
    public partial class Rate : Form
    {
        public Rate()
        {
            InitializeComponent();
        }

        private void Rate_Load(object sender, EventArgs e)
        {
            KROPINERPROTableAdapter.Fill(DataSet1.KROPINERPRO);
            KROPINERPROBindingSource.Sort = "TIME ASC";
            reportViewer1.RefreshReport();
        }
    }
}
