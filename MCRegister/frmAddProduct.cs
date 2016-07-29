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
    public partial class frmAddProduct : Form
    {
        public frmAddProduct()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (mCoyoteDBEntities1 context = new mCoyoteDBEntities1())
                {
                    Product product = new Product()
                    {
                        prodName = txtProdName.Text,
                        prodCode = txtProdCode.Text,
                        prodPrice = decimal.Parse(txtProdPrice.Text)
                        
                    };

                    context.Products.Add(product);
                    context.SaveChanges();
                    this.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("You must enter a value for each item");
            }
        }
    }
}
