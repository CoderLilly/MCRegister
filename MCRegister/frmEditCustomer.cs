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
    public partial class frmEditCustomer : Form
    {
        int myId;
        public frmEditCustomer(int Id)
        {
            InitializeComponent();

            myId = Id;
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (mCoyoteDBEntities1 context = new mCoyoteDBEntities1())
            {
                Customer customer = context.Customers.FirstOrDefault(c => c.Id == myId);

                customer.custFirstName = txtFirstName.Text;
                customer.custLastName = txtLastName.Text;
                customer.custAddress = txtAddress.Text;
                customer.custEmail = txtEmail.Text;
                customer.custPhone = txtPhone.Text;

                context.SaveChanges();
                this.Close();
                     
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEditCustomer_Load(object sender, EventArgs e)
        {
            using (mCoyoteDBEntities1 context = new mCoyoteDBEntities1())
            {
                Customer customer = context.Customers.FirstOrDefault(c => c.Id == myId);
                
                txtFirstName.Text = customer.custFirstName;
                txtLastName.Text = customer.custLastName;
                txtAddress.Text = customer.custAddress;
                txtEmail.Text = customer.custEmail;
                txtPhone.Text = customer.custPhone;    
            }
        }
    }
}
