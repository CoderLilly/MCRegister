using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MCRegister
{
    public partial class SetTaxRate : Form
    {


        public SetTaxRate()
        {
            InitializeComponent();
        }

        private void btnTaxSave_Click(object sender, EventArgs e)
        {
            XmlDocument progSettings = new XmlDocument();
            progSettings.Load("settings.xml");
            progSettings.SelectSingleNode("Settings/taxRate").InnerText = txbTaxRate.Text;
            progSettings.Save("settings.xml");
            this.Close();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            txbTaxRate.Text = txbTaxRate.Text + b.Text;
            
        }
    }
}
