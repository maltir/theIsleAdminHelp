using System;
using System.Windows.Forms;

namespace TheIsleAdminHelp
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
        }

        private void populationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CalcPopForm popForm = new CalcPopForm();
            popForm.Show();
        }
    }
}
