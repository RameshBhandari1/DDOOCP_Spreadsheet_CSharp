using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DDOOCP_Assignment_Ramesh
{
    public class Formula 
    {        
        public double Sum(IDictionary<string, string> values)
        {
             double total = 0;
                foreach (KeyValuePair<string, string> item in values)
                {
                try
                {
                    double value = double.Parse(item.Value);
                    total += value; //(total = total + value)
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                }          
             return total;
        }
        public double Average(IDictionary<string, string> values)
        {
            double total = 0;
            foreach (KeyValuePair<string, string> item in values)
            {
                try
                {
                    double value = double.Parse(item.Value);
                    total += value; //(total = total + value)
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return total / values.Count; ;
        }
        public double multiply(IDictionary<string, string> values)
        {
            double total = 1;
            foreach (KeyValuePair<string, string> item in values)
            {
                try
                {
                    double value = double.Parse(item.Value);
                    total *= value; //(total = total * value)
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return total;
        }
        public double mean(List<double> values)
        {
            try
            {
                if (values.Count == 0)
                {
                    return 0.0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            double ans = 0.0;
            for (int i=0;i<values.Count; ++i)
            {
                try
                {
                    ans += values[i];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return ans/(double)values.Count;
        }

        public double median(List<double> values)
        {
            double ans = 0;
            if (values.Count % 2 == 0)
            {
                try
                {
                    double MiddleElement1 = values[(values.Count / 2) - 1];
                    double MiddleElement2 = values[(values.Count / 2)];
                    ans = (MiddleElement1 + MiddleElement2) / 2;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    ans = values[(values.Count / 2)];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return ans;
        }
        public double mode(List<double> values)
        {
            
            Dictionary<int, int> counts = new Dictionary<int, int>();
            foreach (int x in values)
            {
                try
                {
                    if (counts.ContainsKey(x))
                    {
                        counts[x] = counts[x] + 1;
                    }
                    else
                    {
                        counts[x] = 1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            int ans = int.MinValue;
            int max = int.MinValue;
            foreach(int key in counts.Keys)
            {
                try
                {
                    if (counts[key] > max)
                    {
                        max = counts[key];
                        ans = key;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return ans;
        }
    }
}









































































