using System;
using System.Windows.Forms;

namespace Kropiner
{
    public partial class Ratecs : Form
    {
        public Ratecs()
        {
            InitializeComponent();
        }

        private void Ratecs_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "DataSet1.KROPINERPRO". При необходимости она может быть перемещена или удалена.
            this.KROPINERPROTableAdapter.Fill(this.DataSet1.KROPINERPRO);
            KROPINERPROBindingSource.Sort = "TIME ASC";
            this.reportViewer1.RefreshReport();
        }
    }
}
