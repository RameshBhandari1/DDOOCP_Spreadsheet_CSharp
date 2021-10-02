using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DDOOCP_Assignment_Ramesh
{
    public partial class Spreadsheet : Form
    {
        String[] cols = {"A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"};
        IDictionary<string, string> value_dictionary= new Dictionary<string, string>();
        IDictionary<string, string> formula = new Dictionary<string, string>();
        List<double> myvalues = new List<double>();
        IDictionary<string, string> calc_value = new Dictionary<string, string>();
        Formula NM = new Formula();
        TextBoxSize gd = new TextBoxSize();
        FlowLayoutPanel panel;

        public Spreadsheet()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                setGroupBoxHeight();
                createTextBox();
                AddButton();
               
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void setGroupBoxHeight()
        {
            try
            {
                groupBox2.Height = this.Height - 176;
                groupBox1.Height = this.Height - groupBox2.Height;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AddButton()
        {
            try
            {
                FlowLayoutPanel button_panel = new FlowLayoutPanel();
                Button b = new Button();
                b.Size = new Size(120, 50);
                b.Text = "Show_Bar_Chart";
                b.ForeColor = Color.White;
                button_panel.Controls.Add(b);
                button_panel.Padding = new Padding(50, 50, 0, 0);
                b.BackColor = Color.DarkGreen;
                b.Font = new Font(b.Font, FontStyle.Bold);
                groupBox1.Controls.Add(button_panel);
                b.Click += B_Click;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void B_Click(object sender, EventArgs e)
        {
            try
            {
                new Bar_Chart(value_dictionary).ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void createTextBox()
        {
            try
            {
                gd.Width = 51;
                gd.Height = 51;
                panel = new FlowLayoutPanel();
                panel.Size = new Size(groupBox2.Width, groupBox2.Height);
                panel.Location = new Point(10, 0);
                panel.Padding = new Padding(0);
                panel.WrapContents = true;
                for (int i = 0; i < 26; i++)
                {
                    Label lbl = new Label();
                    lbl.Size = new Size(52, 16);
                    lbl.Text = cols[i];
                    lbl.TextAlign = ContentAlignment.MiddleRight;
                    panel.Controls.Add(lbl);
                    lbl.Font = new Font(lbl.Font, FontStyle.Bold);
                }
                for (int i = 1; i <= 26; i++)
                {
                    for (int j = 0; j < 26; j++)
                    {
                        if (j == 0)
                        {
                            Label lbl = new Label();
                            lbl.Size = new Size(25, 20);
                            lbl.Text = i.ToString();
                            lbl.TextAlign = ContentAlignment.MiddleCenter;
                            panel.Controls.Add(lbl);
                            lbl.Font = new Font(lbl.Font, FontStyle.Bold);
                        }
                        TextBox tb = new TextBox();
                        tb.Size = new Size(gd.Width, gd.Height);
                        tb.Name = cols[j] + i.ToString();
                        if (j == 25)
                        {
                            panel.SetFlowBreak(tb, true);
                        }
                        tb.Height = groupBox2.Height;
                        panel.Controls.Add(tb);
                        tb.Leave += Tb_Leave;
                        tb.TextChanged += Tb_TextChanged;
                        tb.Enter += Tb_Enter;
                    }
                }
                groupBox2.Controls.Add(panel);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Tb_Enter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            TextBox tbx = sender as TextBox;
            txtindex.Text = tbx.Name;
        }

        private void Tb_TextChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            TextBox tbx = sender as TextBox;
            txtshow.Text = tbx.Text;
        }

        private void Tb_Leave(object sender, EventArgs e)
        {
            try
            {
                string firstnum, secondnum;
                double total = 0;
                TextBox tb = sender as TextBox;
                string tb_contents = tb.Text.Trim();
                if (tb_contents != "")
                {
                    if (tb_contents.Contains("+"))
                    {
                        firstnum = tb_contents.Substring(0, tb_contents.IndexOf('+'));
                        secondnum = tb_contents.Substring(tb_contents.IndexOf('+') + 1, (tb_contents.Length - tb_contents.IndexOf('+')) - 1);
                        total = double.Parse(firstnum) + double.Parse(secondnum);
                        tb.Text = total.ToString();
                    }
                    if (tb_contents.Contains("-"))
                    {
                        firstnum = tb_contents.Substring(0, tb_contents.IndexOf('-'));
                        secondnum = tb_contents.Substring(tb_contents.IndexOf('-') + 1, (tb_contents.Length - tb_contents.IndexOf('-')) - 1);
                        total = double.Parse(firstnum) - double.Parse(secondnum);
                        tb.Text = total.ToString();
                    }
                    if (tb_contents.Contains("/"))
                    {
                        firstnum = tb_contents.Substring(0, tb_contents.IndexOf('/'));
                        secondnum = tb_contents.Substring(tb_contents.IndexOf('/') + 1, (tb_contents.Length - tb_contents.IndexOf('/')) - 1);
                        total = double.Parse(firstnum) / double.Parse(secondnum);
                        tb.Text = total.ToString();
                    }
                    if (tb.Text != "" && !tb.Text.Contains("="))
                    {
                        if (value_dictionary.ContainsKey(tb.Name))
                        {
                            value_dictionary.Remove(tb.Name);
                        }
                        value_dictionary.Add(tb.Name, tb.Text);
                    }
                    else
                    {
                        formula.Add(tb.Name, tb.Text);
                    }
                    foreach (KeyValuePair<string, string> item in formula)
                    {
                        string contents = item.Value;                       
                        if (contents.Contains("SUM") || contents.Contains("sum"))
                        {
                            string start = contents.Substring(contents.IndexOf((char)32) + 1, (contents.IndexOf(':') - (contents.IndexOf((char)32)+1))).Trim();
                            string end = contents.Substring(contents.IndexOf(':') + 1, (contents.Length - contents.IndexOf(':')) - 1).Trim();
                            if (start.Substring(0, 1) == end.Substring(0, 1))
                            {
                                string ch = start.Substring(0, 1);
                                int start_index = int.Parse(start.Substring(1, start.Length-1));
                                int end_index = int.Parse(end.Substring(1, end.Length-1));

                                for (int i = start_index; i <= end_index; i++)
                                {
                                    string tb_name = ch + i.ToString();
                                    string val = value_dictionary[tb_name];
                                    if (calc_value.ContainsKey(tb_name))
                                    {
                                        calc_value.Remove(tb_name);
                                    }
                                    calc_value.Add(tb_name, val);
                                }
                                double sum = NM.Sum(calc_value);
                                calc_value.Clear();
                                foreach (Control x in panel.Controls)
                                {
                                    if (x.Name == item.Key)
                                    {
                                        x.Text = sum.ToString();
                                    }
                                }
                            }
                            else
                            {
                                string start_ch = start.Substring(0, 1);
                                string end_ch = end.Substring(0, 1);
                                int row = int.Parse(start.Substring(1, start.Length - 1));
                                int start_ch_index = Array.IndexOf(cols, start_ch);
                                int end_ch_index = Array.IndexOf(cols, end_ch);

                                for (int i = start_ch_index; i <= end_ch_index; i++)
                                {
                                    string tb_name = cols[i] + row.ToString();
                                    string val = value_dictionary[tb_name];

                                    if (calc_value.ContainsKey(tb_name))
                                    {
                                        calc_value.Remove(tb_name);
                                    }
                                    calc_value.Add(tb_name, val);
                                }
                                double sum = NM.Sum(calc_value);
                                calc_value.Clear();
                                foreach (Control x in panel.Controls)
                                {
                                    if (x.Name == item.Key)
                                    {
                                        x.Text = sum.ToString();
                                    }
                                }
                            }
                        }
                        if (contents.Contains("AVG") || contents.Contains("avg"))
                        {
                            string start = contents.Substring(contents.IndexOf((char)32) + 1,(contents.IndexOf(':') - (contents.IndexOf((char)32) + 1))).Trim();
                            string end = contents.Substring(contents.IndexOf(':') + 1, (contents.Length - contents.IndexOf(':')) - 1).Trim();
                            if (start.Substring(0, 1) == end.Substring(0, 1))
                            {
                                string ch = start.Substring(0, 1);
                                int start_index = int.Parse(start.Substring(1, start.Length - 1));
                                int end_index = int.Parse(end.Substring(1, end.Length - 1));

                                for (int i = start_index; i <= end_index; i++)
                                {
                                    string tb_name = ch + i.ToString();
                                    string val = value_dictionary[tb_name];
                                    if (calc_value.ContainsKey(tb_name))
                                    {
                                        calc_value.Remove(tb_name);
                                    }
                                    calc_value.Add(tb_name, val);
                                }
                                double Avg = NM.Average(calc_value);
                                calc_value.Clear();
                                foreach (Control x in panel.Controls)
                                {
                                    if (x.Name == item.Key)
                                    {
                                        x.Text = Avg.ToString();
                                    }
                                }
                            }
                            else
                            {
                                string start_ch = start.Substring(0, 1);
                                string end_ch = end.Substring(0, 1);
                                int row = int.Parse(start.Substring(1, start.Length - 1));
                                int start_ch_index = Array.IndexOf(cols, start_ch);
                                int end_ch_index = Array.IndexOf(cols, end_ch);

                                for (int i = start_ch_index; i <= end_ch_index; i++)
                                {
                                    string tb_name = cols[i] + row.ToString();
                                    string val = value_dictionary[tb_name];

                                    if (calc_value.ContainsKey(tb_name))
                                    {
                                        calc_value.Remove(tb_name);
                                    }
                                    calc_value.Add(tb_name, val);
                                }
                                double Avg = NM.Average(calc_value);
                                calc_value.Clear();

                                foreach (Control x in panel.Controls)
                                {
                                    if (x.Name == item.Key)
                                    {
                                        x.Text = Avg.ToString();
                                    }
                                }
                            }
                        }
                        if (contents.Contains("*"))
                        {
                            string start = contents.Substring(contents.IndexOf((char)32) + 1,(contents.IndexOf('*') - (contents.IndexOf((char)32) + 1))).Trim();
                            string end = contents.Substring(contents.IndexOf('*') + 1, (contents.Length - contents.IndexOf('*')) - 1).Trim();
                            if (start.Substring(0, 1) == end.Substring(0, 1))
                            {
                                string ch = start.Substring(0, 1);
                                int start_index = int.Parse(start.Substring(1, start.Length - 1));
                                int end_index = int.Parse(end.Substring(1, end.Length - 1));

                                for (int i = start_index; i <= end_index; i++)
                                {
                                    string tb_name = ch + i.ToString();
                                    string val = value_dictionary[tb_name];

                                    if (calc_value.ContainsKey(tb_name))
                                    {
                                        calc_value.Remove(tb_name);
                                    }
                                    calc_value.Add(tb_name, val);

                                }
                                double mul = NM.multiply(calc_value);
                                calc_value.Clear();
                                foreach (Control x in panel.Controls)
                                {
                                    if (x.Name == item.Key)
                                    {
                                        x.Text = mul.ToString();
                                    }
                                }
                            }
                            else
                            {
                                string start_ch = start.Substring(0, 1);
                                string end_ch = end.Substring(0, 1);
                                int row = int.Parse(start.Substring(1, start.Length - 1));
                                int start_ch_index = Array.IndexOf(cols, start_ch);
                                int end_ch_index = Array.IndexOf(cols, end_ch);

                                for (int i = start_ch_index; i <= end_ch_index; i++)
                                {
                                    string tb_name = cols[i] + row.ToString();
                                    string val = value_dictionary[tb_name];

                                    if (calc_value.ContainsKey(tb_name))
                                    {
                                        calc_value.Remove(tb_name);
                                    }
                                    calc_value.Add(tb_name, val);
                                }
                                double mul = NM.multiply(calc_value);
                                calc_value.Clear();

                                foreach (Control x in panel.Controls)
                                {
                                    if (x.Name == item.Key)
                                    {
                                        x.Text = mul.ToString();
                                    }
                                }
                            }
                        }
                        if (contents.Contains("MEAN") || contents.Contains("mean"))
                        {

                            string start = contents.Substring(contents.IndexOf((char)32) + 1,(contents.IndexOf(':') - (contents.IndexOf((char)32) + 1))).Trim();
                            string end = contents.Substring(contents.IndexOf(':') + 1, (contents.Length - contents.IndexOf(':')) - 1).Trim();
                            if (start.Substring(0, 1) == end.Substring(0, 1))
                            {
                                string ch = start.Substring(0, 1);
                                int start_index = int.Parse(start.Substring(1, start.Length - 1));
                                int end_index = int.Parse(end.Substring(1, end.Length - 1));

                                for (int i = start_index; i <= end_index; i++)
                                {
                                    string tb_name = ch + i.ToString();
                                    string val = value_dictionary[tb_name];
                                    myvalues.Add(double.Parse(val));

                                }
                                double mean = NM.mean(myvalues);
                                myvalues.Clear();
                                foreach (Control x in panel.Controls)
                                {
                                    if (x.Name == item.Key)
                                    {
                                        x.Text = mean.ToString();
                                    }
                                }
                            }
                            else
                            {
                                string start_ch = start.Substring(0, 1);
                                string end_ch = end.Substring(0, 1);
                                int row = int.Parse(start.Substring(1, start.Length - 1));
                                int start_ch_index = Array.IndexOf(cols, start_ch);
                                int end_ch_index = Array.IndexOf(cols, end_ch);

                                for (int i = start_ch_index; i <= end_ch_index; i++)
                                {
                                    string tb_name = cols[i] + row.ToString();
                                    string val = value_dictionary[tb_name];
                                    myvalues.Add(double.Parse(val));
                                }
                                double mean = NM.mean(myvalues);
                                myvalues.Clear();

                                foreach (Control x in panel.Controls)
                                {
                                    if (x.Name == item.Key)
                                    {
                                        x.Text = mean.ToString();
                                    }
                                }
                            }
                        }
                        if (contents.Contains("MEDIAN") || contents.Contains("median"))
                        {
                            string start = contents.Substring(contents.IndexOf((char)32) + 1,(contents.IndexOf(':') - (contents.IndexOf((char)32) + 1))).Trim();
                            string end = contents.Substring(contents.IndexOf(':') + 1, (contents.Length - contents.IndexOf(':')) - 1).Trim();
                            if (start.Substring(0, 1) == end.Substring(0, 1))
                            {
                                string ch = start.Substring(0, 1);
                                int start_index = int.Parse(start.Substring(1, start.Length - 1));
                                int end_index = int.Parse(end.Substring(1, end.Length - 1));

                                for (int i = start_index; i <= end_index; i++)
                                {
                                    string tb_name = ch + i.ToString();
                                    string val = value_dictionary[tb_name];

                                    if (calc_value.ContainsKey(tb_name))
                                    {
                                        calc_value.Remove(tb_name);
                                    }
                                    myvalues.Add(double.Parse(val));

                                }
                                double median = NM.median(myvalues);
                                myvalues.Clear();
                                foreach (Control x in panel.Controls)
                                {
                                    if (x.Name == item.Key)
                                    {
                                        x.Text = median.ToString();
                                    }
                                }
                            }
                            else
                            {
                                string start_ch = start.Substring(0, 1);
                                string end_ch = end.Substring(0, 1);
                                int row = int.Parse(start.Substring(1, start.Length - 1));
                                int start_ch_index = Array.IndexOf(cols, start_ch);
                                int end_ch_index = Array.IndexOf(cols, end_ch);

                                for (int i = start_ch_index; i <= end_ch_index; i++)
                                {
                                    string tb_name = cols[i] + row.ToString();
                                    string val = value_dictionary[tb_name];

                                    if (calc_value.ContainsKey(tb_name))
                                    {
                                        calc_value.Remove(tb_name);
                                    }
                                    myvalues.Add(double.Parse(val));
                                }
                                double median = NM.median(myvalues);
                                myvalues.Clear();

                                foreach (Control x in panel.Controls)
                                {
                                    if (x.Name == item.Key)
                                    {
                                        x.Text = median.ToString();
                                    }
                                }
                            }
                        }
                        if (contents.Contains("MODE") || contents.Contains("mode"))
                        {

                            string start = contents.Substring(contents.IndexOf((char)32) + 1,(contents.IndexOf(':') - (contents.IndexOf((char)32) + 1))).Trim();
                            string end = contents.Substring(contents.IndexOf(':') + 1, (contents.Length - contents.IndexOf(':')) - 1).Trim();
                            if (start.Substring(0, 1) == end.Substring(0, 1))
                            {
                                string ch = start.Substring(0, 1);
                                int start_index = int.Parse(start.Substring(1, start.Length - 1));
                                int end_index = int.Parse(end.Substring(1, end.Length - 1));

                                for (int i = start_index; i <= end_index; i++)
                                {
                                    string tb_name = ch + i.ToString();
                                    string val = value_dictionary[tb_name];                                    
                                    myvalues.Add(double.Parse(val));

                                }
                                double mode = NM.mode(myvalues);
                                myvalues.Clear();
                                foreach (Control x in panel.Controls)
                                {
                                    if (x.Name == item.Key)
                                    {
                                        x.Text = mode.ToString();
                                    }
                                }
                            }
                            else
                            {
                                string start_ch = start.Substring(0, 1);
                                string end_ch = end.Substring(0, 1);
                                int row = int.Parse(start.Substring(1, start.Length - 1));
                                int start_ch_index = Array.IndexOf(cols, start_ch);
                                int end_ch_index = Array.IndexOf(cols, end_ch);

                                for (int i = start_ch_index; i <= end_ch_index; i++)
                                {
                                    string tb_name = cols[i] + row.ToString();
                                    string val = value_dictionary[tb_name];
                                    myvalues.Add(double.Parse(val));
                                }
                                double mode = NM.mode(myvalues);
                                myvalues.Clear();

                                foreach (Control x in panel.Controls)
                                {
                                    if (x.Name == item.Key)
                                    {
                                        x.Text = mode.ToString();
                                    }
                                }
                            }                        
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
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

