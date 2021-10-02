using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DDOOCP_Assignment_Ramesh
{
    public partial class Bar_Chart : Form
    {
        IDictionary<string, string> Dic_Values = new Dictionary<string, string>();
        public Bar_Chart(IDictionary<string, string> val)
        {
            InitializeComponent();
            this.Dic_Values = val;
        }
        private void Bar_Chart_Load(object sender, EventArgs e)
        {
            try
            {
                chart1.Series["Values"].Points.DataBindXY(Dic_Values.Keys, Dic_Values.Values);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Bar_Chart_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Do you really want to close the program?", "Exit", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                Application.ExitThread();
            }
            else if (dialog == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
