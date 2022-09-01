using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CultureInfoTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string strConvertType = string.Empty;
            try
            {
                if (txtVariant.Text != "")
                {
                    strConvertType = txtVariant.Text;
                }
                else
                {
                    if (comboBox1.SelectedIndex != -1)
                    {
                        strConvertType = comboBox1.SelectedValue.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Please Select Culture Info!");
                    }
                }
                if (txtTextToConvert.Text != "")
                {
                    CultureInfo c = new CultureInfo(strConvertType);

                    double a = Convert.ToDouble(txtTextToConvert.Text, c);

                    txtResultView.Text = "String formatted for the " + c.Name + " culture: " + Environment.NewLine + a.ToString("C", c);
                }
                else
                {
                    MessageBox.Show("Please enter Numbers to convert!");
                    txtTextToConvert.Focus();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("ERROR: "+ex.Message);
            }
            
            
        }

        public string ToCurrencyFormat(string str)
        {
            string result = "";
            try
            {
                result = Convert.ToDouble(str, System.Globalization.CultureInfo.InvariantCulture).ToString("C");
            }
            catch { }
            return result;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            try
            {
                var items = new List<Item>();
                items.Add(new Item("---SELECT Culture---", ""));
                CultureInfo[] cinfo = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures);
                    foreach (CultureInfo a in cinfo)
                    {
                        if (!string.IsNullOrEmpty(a.Name))
                        {
                            items.Add(new Item(a.DisplayName, a.Name));
                        }
                    }
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "Value";
                comboBox1.DataSource = items;
                //getTableData(strSQL);
            }
            catch (Exception ee)
            {
                MessageBox.Show(this, "Something Went Wrong!\n REASON: " + ee.Message, "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            txtTextToConvert.Focus();


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
                txtVariant.Text = comboBox1.SelectedValue.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtTextToConvert.Text = "";
            txtResultView.Text = "";
            txtVariant.Text = "";
            comboBox1.SelectedIndex = 0;
        }
    }

    public class Item
    {
        public string Name { get; private set; }
        public string Value { get; private set; }
        public Item(string _name, string _value)
        {
            Name = _name; Value = _value;
        }
    }
}
