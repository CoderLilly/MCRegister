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
    
    public partial class frmEditProduct : Form
    {
        int myId;
        public frmEditProduct(int Id)
        {
            InitializeComponent();
            myId = Id;
        }

        private void frmEditProduct_Load(object sender, EventArgs e)
        {
            using (mCoyoteDBEntities1 context = new mCoyoteDBEntities1())
            {
                Product product = context.Products.FirstOrDefault(c => c.Id == myId);

                txtProdName.Text = product.prodName;
                txtProdCode.Text = product.prodCode;
                txtProdPrice.Text = product.prodPrice.ToString();
                
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (mCoyoteDBEntities1 context = new mCoyoteDBEntities1())
            {
                Product product = context.Products.FirstOrDefault(c => c.Id == myId);

                product.prodName = txtProdName.Text;
                product.prodCode = txtProdCode.Text;
                product.prodPrice = decimal.Parse(txtProdPrice.Text);

                context.SaveChanges();
                this.Close();

            }
        }
    }
}
