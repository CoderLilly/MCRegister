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
    public partial class SetProductKeys : Form
    {
        string department;
        Form1 form1 = new Form1();
        XmlDocument progSettings = new XmlDocument();
        public SetProductKeys()
        {
            InitializeComponent();
            progSettings.Load("settings.xml");

            btnDept1.Text = progSettings.SelectSingleNode("Settings/departmentKeys/dept1").InnerText;
            btnDept2.Text = progSettings.SelectSingleNode("Settings/departmentKeys/dept2").InnerText;
            btnDept3.Text = progSettings.SelectSingleNode("Settings/departmentKeys/dept3").InnerText;
            btnDept4.Text = progSettings.SelectSingleNode("Settings/departmentKeys/dept4").InnerText;
            btnDept5.Text = progSettings.SelectSingleNode("Settings/departmentKeys/dept5").InnerText;
            btnDept6.Text = progSettings.SelectSingleNode("Settings/departmentKeys/dept6").InnerText;
            btnDept7.Text = progSettings.SelectSingleNode("Settings/departmentKeys/dept7").InnerText;
            btnDept8.Text = progSettings.SelectSingleNode("Settings/departmentKeys/dept8").InnerText;
            btnDept9.Text = progSettings.SelectSingleNode("Settings/departmentKeys/dept9").InnerText;
            btnDept10.Text = progSettings.SelectSingleNode("Settings/departmentKeys/dept10").InnerText;
            btnDept11.Text = progSettings.SelectSingleNode("Settings/departmentKeys/dept11").InnerText;
            btnDept12.Text = progSettings.SelectSingleNode("Settings/departmentKeys/dept12").InnerText;
            btnDept13.Text = progSettings.SelectSingleNode("Settings/departmentKeys/dept13").InnerText;
            btnDept14.Text = progSettings.SelectSingleNode("Settings/departmentKeys/dept14").InnerText;
            btnDept15.Text = progSettings.SelectSingleNode("Settings/departmentKeys/dept15").InnerText;
        }

        private void btnDept_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            department = b.Name;

            
            switch (department)
            {
                case "btnDept1":
                    progSettings.SelectSingleNode("Settings/departmentKeys/dept1").InnerText = txtbSetProductKey.Text;
                    btnDept1.Text = txtbSetProductKey.Text;
                    break;
                case "btnDept2":
                    progSettings.SelectSingleNode("Settings/departmentKeys/dept2").InnerText = txtbSetProductKey.Text;
                    btnDept2.Text = txtbSetProductKey.Text;
                    break;
                case "btnDept3":
                    progSettings.SelectSingleNode("Settings/departmentKeys/dept3").InnerText = txtbSetProductKey.Text;
                    btnDept3.Text = txtbSetProductKey.Text;
                    break;
                case "btnDept4":
                    progSettings.SelectSingleNode("Settings/departmentKeys/dept4").InnerText = txtbSetProductKey.Text;
                    btnDept4.Text = txtbSetProductKey.Text;
                    break;
                case "btnDept5":
                    progSettings.SelectSingleNode("Settings/departmentKeys/dept5").InnerText = txtbSetProductKey.Text;
                    btnDept5.Text = txtbSetProductKey.Text;
                    break;
                case "btnDept6":
                    progSettings.SelectSingleNode("Settings/departmentKeys/dept6").InnerText = txtbSetProductKey.Text;
                    btnDept6.Text = txtbSetProductKey.Text;
                    break;
                case "btnDept7":
                    progSettings.SelectSingleNode("Settings/departmentKeys/dept7").InnerText = txtbSetProductKey.Text;
                    btnDept7.Text = txtbSetProductKey.Text;
                    break;
                case "btnDept8":
                    progSettings.SelectSingleNode("Settings/departmentKeys/dept8").InnerText = txtbSetProductKey.Text;
                    btnDept8.Text = txtbSetProductKey.Text;
                    break;
                case "btnDept9":
                    progSettings.SelectSingleNode("Settings/departmentKeys/dept9").InnerText = txtbSetProductKey.Text;
                    btnDept9.Text = txtbSetProductKey.Text;
                    break;
                case "btnDept10":
                    progSettings.SelectSingleNode("Settings/departmentKeys/dept10").InnerText = txtbSetProductKey.Text;
                    btnDept10.Text = txtbSetProductKey.Text;
                    break;
                case "btnDept11":
                    progSettings.SelectSingleNode("Settings/departmentKeys/dept11").InnerText = txtbSetProductKey.Text;
                    btnDept11.Text = txtbSetProductKey.Text;
                    break;
                case "btnDept12":
                    progSettings.SelectSingleNode("Settings/departmentKeys/dept12").InnerText = txtbSetProductKey.Text;
                    btnDept12.Text = txtbSetProductKey.Text;
                    break;
                case "btnDept13":
                    progSettings.SelectSingleNode("Settings/departmentKeys/dept13").InnerText = txtbSetProductKey.Text;
                    btnDept13.Text = txtbSetProductKey.Text;
                    break;
                case "btnDept14":
                    progSettings.SelectSingleNode("Settings/departmentKeys/dept14").InnerText = txtbSetProductKey.Text;
                    btnDept14.Text = txtbSetProductKey.Text;
                    break;
                case "btnDept15":
                    progSettings.SelectSingleNode("Settings/departmentKeys/dept15").InnerText = txtbSetProductKey.Text;
                    btnDept15.Text = txtbSetProductKey.Text;
                    break;

            }

            progSettings.Save("settings.xml");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            form1.loadProductKeys();

            this.Close();
        }
    }
}
