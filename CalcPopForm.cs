using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using TheIsleAdminHelp.classe;

namespace TheIsleAdminHelp
{
    public partial class CalcPopForm : Form
    {
        public CalcPopForm()
        {
            InitializeComponent();
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            CalcPop calcPopulation = new CalcPop();
            Stat stat = calcPopulation.Calcul(progressBar1);
            HerbiTot.Text = stat.Herbi.ToString();
            CarniTot.Text = stat.Carni.ToString();

            foreach (var group in Controls.OfType­<GroupBox>())
            {
                foreach (var label in group.Controls.OfType<Label>())
                {
                    if (label.Name.Contains("lbl"))
                    {
                        foreach (PropertyInfo property in stat.GetType().GetProperties())
                        {
                            if (label.Name == "lbl" + property.Name)
                            {
                                label.Text = property.GetValue(stat).ToString();
                            }
                        }
                    }
                }
            }

            progressBar1.Value = 0;
        }
    }
}
