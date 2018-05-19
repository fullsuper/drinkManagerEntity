using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrinkManager.Report
{
    public partial class frmBill : Form
    {
        private int bill;
        public frmBill(int bid)
        {
            InitializeComponent();
            InitializeComponent();
            bill = bid;
        }

        private void frmBill_Load(object sender, EventArgs e)
        {
            this.dataTable1TableAdapter.Fill(this.dataSet1.DataTable1, bill);

            this.reportViewer1.RefreshReport();
        }
    }
}
