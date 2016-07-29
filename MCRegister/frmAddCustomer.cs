using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCRegister
{
    public partial class frmAddCustomer : Form
    {
        public frmAddCustomer()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (mCoyoteDBEntities1 context = new mCoyoteDBEntities1())
                {
                    Customer customer = new Customer()
                    {
                        custFirstName = txtFirstName.Text,
                        custLastName = txtLastName.Text,                       
                        custAddress = txtAddress.Text,
                        custEmail = txtEmail.Text,
                        custPhone = txtPhone.Text
                    };

                    context.Customers.Add(customer);
                    context.SaveChanges();
                    this.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("You must enter a value for each item");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
