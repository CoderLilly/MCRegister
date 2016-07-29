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
using System.IO;

namespace MCRegister
{
    public partial class Form1 : Form
    {
        string myTyped;
        double runningTotal = 0;
        double taxRate = 0;
        double tax;
        string department = "Item";
        double discountRate = 0;
        string discountPercentage;
        double finalTotal = 0;
        double discount = 0;
        int transId;
        decimal grossTotal = 0;
        decimal taxTotal = 0;
        decimal discTotal = 0;
        decimal netTotal = 0;
        private int checkPrint;

        List<SaleItems> myItems = new List<SaleItems>();
        XmlDocument progSettings = new XmlDocument();

        public Form1()
        {
            InitializeComponent();

            loadRTFbox();
            progSettings.Load("settings.xml");
            taxRate = double.Parse(progSettings.SelectSingleNode("Settings/taxRate").InnerText);
            loadProductKeys();

            // initializes printing event handlers
            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);

        }

        public void loadProductKeys()
        {
            // This funtion loads the department key lables from settings.xml
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

        // this event captures the text property of the register keys and displays in the text box
        private void button0_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;  //pulls text property values
            textBoxTotal.Text = textBoxTotal.Text + b.Text; //appends b to previous textbox text
            myTyped = myTyped + b.Text;

            textBoxTotal.Text = (double.Parse(myTyped) / 100).ToString("C");    //converts text to double and displays as cash value
        }

        // This event deletes last digit in myTyped
        private void buttonBack_Click(object sender, EventArgs e)
        {
            myTyped = myTyped.Substring(0, myTyped.Length - 1);

            if (myTyped.Length == 0)
            {
                myTyped = "0";
            }

            textBoxTotal.Text = (double.Parse(myTyped) / 100).ToString("C");
        }

        // This events resets all variables and properties for next transaction
        private void buttonClear_Click(object sender, EventArgs e)
        {
            myItems.Clear();
            rtfReceipt.Clear();
            textBoxTotal.Text = "$0.00";
            myTyped = "0";
            runningTotal = 0;
            tax = 0;
            loadRTFbox();
            discountRate = .0;
            btnDisc10.Enabled = true;
            btnDisc15.Enabled = true;
            btnDisc20.Enabled = true;
            btnDisc25.Enabled = true;
        }

        private void buttonCashTend_Click(object sender, EventArgs e)
        {
            double cashTendered = 0;

            if (myTyped != "")
            {
                cashTendered = double.Parse(myTyped) / 100;
            
                if (cashTendered >= ((runningTotal * discountRate)))
                {
                    rtfReceipt.AppendText("-----------\n" + "Cash tendered:\t \t" + cashTendered.ToString("C") + "\n");
                    if (taxRate != 0.00)
                    {
                        rtfReceipt.AppendText("Change:\t \t\t" + (cashTendered - ((runningTotal + tax) - (runningTotal * discountRate))).ToString("C") + "\n");
                    }
                    else
                    {
                        rtfReceipt.AppendText("Change:\t \t\t" + (cashTendered - (runningTotal * discountRate)).ToString("C") + "\n");
                    }
                }
                else
                {
                    MessageBox.Show("Cash Tendered must be more than total.");
                    cashTendered = 0;
                }
                using (mCoyoteDBEntities1 context = new mCoyoteDBEntities1())
                {
                    Transaction transaction = new Transaction()
                    {
                        Id = transId,
                        transDate = DateTime.Now,
                        transGrossTotal = decimal.Parse(runningTotal.ToString()),
                        transDiscount = decimal.Parse(discount.ToString()),
                        transTax = decimal.Parse(tax.ToString()),
                        transNetTotal = decimal.Parse(finalTotal.ToString())
                    };

                    context.Transactions.Add(transaction);
                    context.SaveChanges();
                    transId = 0;
                    runningTotal = 0;
                    tax = 0;
                    myItems.Clear();
                }
            }

        }
        
        private void mnuTax_Click(object sender, EventArgs e)
        {
            SetTaxRate setTax = new SetTaxRate();
            setTax.ShowDialog();
            progSettings.Load("settings.xml");
            taxRate = double.Parse(progSettings.SelectSingleNode("Settings/taxRate").InnerText);


        }

        private void loadRTFbox()
        {
            rtfReceipt.AppendText("\t");
            Clipboard.SetImage(MCRegister.Properties.Resources.MadamCoyoteLogo);
            rtfReceipt.Paste();
            rtfReceipt.AppendText("\n\n");
            rtfReceipt.AppendText("\t     Madam Coyote\n    www.etsy.com/shop/MadamCoyote\n");
            rtfReceipt.AppendText("\t " + DateTime.Now + "\n\n");

            
        }

        private void btnDepartment_click(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            department = b.Text;

            loadReceipt();

            

        }

        //private int checkPrint;

        private void mnuPageSetup_Click(object sender, System.EventArgs e)
        {
            pageSetupDialog1.ShowDialog();
        }

        private void mnuPrintPreview_Click(object sender, System.EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
        }

        private void mnuPrint_Click(object sender, System.EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            checkPrint = 0;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Print the content of RichTextBox. Store the last character printed.
            checkPrint = rtfReceipt.Print(checkPrint, rtfReceipt.TextLength, e);

            // Check for more pages
            if (checkPrint < rtfReceipt.TextLength)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'customersDataSet.customers' table. You can move, or remove it, as needed.
            // this.customersTableAdapter.Fill(this.customersDataSet.customers);

        }
        
        private void setDepartmentKeysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetProductKeys setProductKeys = new SetProductKeys();
            setProductKeys.ShowDialog();
            loadProductKeys();
        }

        private void btnVoidItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < myItems.Count; i++)
            {
                if (i == myItems.Count - 1)
                {
                    myItems.RemoveAt(i);
                }

                btnDept15.PerformClick();
            }
        }
        
        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabCustomers)
            {
                loadCustomerDatagrid();

            }
            else if (tabControl1.SelectedTab == tabProducts)
            {
                loadProductDatagrid();
            }
            else if ( tabControl1.SelectedTab == tabTransactions)
            {
                
                loadTransactionDatagrid();
            }
            
        }

        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddCustomer addCustomer = new frmAddCustomer();
            addCustomer.ShowDialog();
            loadCustomerDatagrid();
        }

        public void loadCustomerDatagrid()
        {
            if (tabControl1.SelectedTab == tabCustomers)
            {
                try
                {

                    using (mCoyoteDBEntities1 context = new mCoyoteDBEntities1())
                    {
                        var cust = from Customer in context.Customers select Customer;
                        dtgCustomers.DataSource = cust.ToList();


                        dtgCustomers.Columns[0].HeaderText = "CustID";
                        dtgCustomers.Columns[1].HeaderText = "First Name";
                        dtgCustomers.Columns[2].HeaderText = "Last Name";
                        dtgCustomers.Columns[3].HeaderText = "Address";
                        dtgCustomers.Columns[4].HeaderText = "Email";
                        dtgCustomers.Columns[5].HeaderText = "Phone";


                    }
                }
                catch (Exception g)
                {
                    MessageBox.Show(g.Message);
                }
            }


        }

        public void loadProductDatagrid()
        {
            if (tabControl1.SelectedTab == tabProducts)
            {
                try
                {

                    using (mCoyoteDBEntities1 context = new mCoyoteDBEntities1())
                    {
                        var prod = from Product in context.Products select Product;
                        dtgProducts.DataSource = prod.ToList();

                        dtgProducts.Columns[0].Visible = false;
                        dtgProducts.Columns[0].HeaderText = "ProdID";
                        dtgProducts.Columns[1].HeaderText = "Product Name";
                        dtgProducts.Columns[2].HeaderText = "Product Code";
                        dtgProducts.Columns[3].HeaderText = "Product Price";


                    }
                }
                catch (Exception g)
                {
                    MessageBox.Show(g.Message);
                }
            }
        }

        public void loadTransactionDatagrid()
        {
            grossTotal = 0;
            taxTotal = 0;
            discTotal = 0;
            netTotal = 0;
            //dtpFrom.Format = DateTimePickerFormat.Custom;
            //dtpFrom.CustomFormat = "M/dd/yyyy";
            if (tabControl1.SelectedTab == tabTransactions)
            {
                try
                {

                    using (mCoyoteDBEntities1 context = new mCoyoteDBEntities1())
                    {
                        var trans = from Transaction in context.Transactions select Transaction;
                        
                        dtgTransactions.DataSource = trans.ToList();

                        dtgTransactions.Columns[0].HeaderText = "Transaction#";
                        dtgTransactions.Columns[1].HeaderText = "Date";
                        dtgTransactions.Columns[2].HeaderText = "Gross Total";
                        dtgTransactions.Columns[3].HeaderText = "Tax Applied";
                        dtgTransactions.Columns[4].HeaderText = "Discount Applied";
                        dtgTransactions.Columns[5].HeaderText = "Net Total";

                        
                        List<Transaction> transAction = trans.ToList();
                        for (int i = 0; i < trans.Count(); i++)
                        {
                            grossTotal = grossTotal + transAction[i].transGrossTotal;
                            taxTotal = taxTotal + transAction[i].transTax;
                            discTotal = discTotal + transAction[i].transDiscount;
                            netTotal = netTotal + transAction[i].transNetTotal;
                        }

                        txtGrossTotal.Text = grossTotal.ToString();
                        txtTaxTotal.Text = taxTotal.ToString();
                        txtDiscountTotal.Text = discTotal.ToString();
                        txtNetTotal.Text = netTotal.ToString();

                    }
                }
                catch (Exception g)
                {
                    MessageBox.Show(g.Message);
                }
            }
        }

        private void loadReceipt()
        {


            if (myTyped != "")
            {
                myItems.Add(new SaleItems { name = department, price = double.Parse(myTyped) / 100 });
            }
            rtfReceipt.Clear();
            loadRTFbox();

            
            using (mCoyoteDBEntities1 context = new mCoyoteDBEntities1())
            {
                int lastId = context.Transactions.Max(p => p.Id);
                transId = lastId + 1;
                
            }

            rtfReceipt.AppendText("\n\tTransaction# " + transId + "\n\n");

            runningTotal = 0;

            for (int i = 0; i < myItems.Count; i++)
            {

                rtfReceipt.AppendText(myItems[i].name + "\n");
                rtfReceipt.AppendText("->\t\t\t" + myItems[i].price.ToString("C") + "\n");
                runningTotal = runningTotal + myItems[i].price;
            }

            
            
            if (discountRate != 0)
            {
                discount = runningTotal * discountRate;
                double totalMinusDiscount = runningTotal - discount;
                rtfReceipt.AppendText("Discount:\n" + discountPercentage + " ->\t\t\t" + discount.ToString("C") + "\n");
                rtfReceipt.AppendText("-----------" + "\n" + "Subtotal:\t\t\t" + totalMinusDiscount.ToString("C") + "\n");

                tax = totalMinusDiscount * (taxRate / 100);
                rtfReceipt.AppendText("Tax:\t\t\t" + tax.ToString("C") + "\n");
                finalTotal = totalMinusDiscount + tax;
                rtfReceipt.AppendText("Total:\t\t\t" + finalTotal.ToString("C") + "\n");
            }
            else
            {
                tax = runningTotal * (taxRate / 100);
                rtfReceipt.AppendText("-----------" + "\n" + "Subtotal:\t\t\t" + runningTotal.ToString("C") + "\n");
                rtfReceipt.AppendText("Tax:\t\t\t" + tax.ToString("C") + "\n");
                finalTotal = (runningTotal + (runningTotal * (taxRate / 100)));
                rtfReceipt.AppendText("Total:\t\t\t" + finalTotal.ToString("C") + "\n");

            }

            
            textBoxTotal.Text = "$0.00";
            myTyped = "";
        }

        // Adds product to myItems
        private void dtgProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string prodCode = dtgProducts.Rows[dtgProducts.SelectedRows[0].Index].Cells[2].Value.ToString();
            double prodPrice = double.Parse(dtgProducts.Rows[dtgProducts.SelectedRows[0].Index].Cells[3].Value.ToString());

            myItems.Add(new SaleItems { name = prodCode, price = prodPrice });
            myTyped = "";
            loadReceipt();

            
        }

        // Add product to database
        private void addNewProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddProduct addProduct = new frmAddProduct();
            addProduct.ShowDialog();
            loadProductDatagrid();

            
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure you want to delete this customer?",
                "Delete Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                using (mCoyoteDBEntities1 context = new mCoyoteDBEntities1())
                {
                    Customer customer = new Customer();

                    foreach (DataGridViewRow row in this.dtgCustomers.SelectedRows)
                    {
                        Customer cust = row.DataBoundItem as Customer;
                        if (cust != null)
                        {
                            Customer cust_to_delete = context.Customers.Single(c => c.Id == cust.Id);
                            context.Customers.Remove(cust_to_delete);
                        }
                    }

                    context.SaveChanges();
                    loadCustomerDatagrid();
                }
               
            }
            
        }

        private void btnSearchCustomer_Click(object sender, EventArgs e)
        {
            if (txtSearchCustomer.Text != "")
            {
                using (mCoyoteDBEntities1 context = new mCoyoteDBEntities1())
                {
                    Customer customer = new Customer();
                    string userSelected = txtSearchCustomer.Text;
                    
                    if (cbCustSearchOptions.Text == "First Name")
                    {
                        var data = from b in context.Customers where b.custFirstName.Contains(userSelected) select b;
                        dtgCustomers.DataSource = data.ToList();
                    }
                    else if (cbCustSearchOptions.Text == "Last Name")
                    {
                        var data = from b in context.Customers where b.custLastName.Contains(userSelected) select b;
                        dtgCustomers.DataSource = data.ToList();
                    }
                    else if (cbCustSearchOptions.Text == "Address")
                    {
                        var data = from b in context.Customers where b.custAddress.Contains(userSelected) select b;
                        dtgCustomers.DataSource = data.ToList();
                    }
                    else if (cbCustSearchOptions.Text == "Email")
                    {
                        var data = from b in context.Customers where b.custEmail.Contains(userSelected) select b;
                        dtgCustomers.DataSource = data.ToList();
                    }
                    else if (cbCustSearchOptions.Text == "Phone")
                    {
                        var data = from b in context.Customers where b.custPhone.Contains(userSelected) select b;
                        dtgCustomers.DataSource = data.ToList();
                    }
                    else
                    {
                        MessageBox.Show("Please select a search criteria");
                    }
                    
                }
            }

        }

        private void btnRefreshCustomer_Click(object sender, EventArgs e)
        {
            loadCustomerDatagrid();
        }

        private void btnRefreshProducts_Click(object sender, EventArgs e)
        {
            loadProductDatagrid();
        }

        private void btnSearchProducts_Click(object sender, EventArgs e)
        {
            if (txtSearchProducts.Text != "")
            {
                using (mCoyoteDBEntities1 context = new mCoyoteDBEntities1())
                {
                    Product product = new Product();
                    string userSelected = txtSearchProducts.Text;
                    var data = from b in context.Products where b.prodName.Contains(userSelected) select b;
                    dtgProducts.DataSource = data.ToList();
                }
            }
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure you want to delete this product?",
                "Delete product", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                using (mCoyoteDBEntities1 context = new mCoyoteDBEntities1())
                {
                    Product product = new Product();

                    foreach (DataGridViewRow row in this.dtgProducts.SelectedRows)
                    {
                        Product prod = row.DataBoundItem as Product;
                        if (prod != null)
                        {
                            Product prod_to_delete = context.Products.Single(c => c.Id == prod.Id);
                            context.Products.Remove(prod_to_delete);
                        }
                    }

                    context.SaveChanges();
                    loadProductDatagrid();
                }
            }
        }

        private void btnEditCutomer_Click(object sender, EventArgs e)
        {

            using (mCoyoteDBEntities1 context = new mCoyoteDBEntities1())
            {
                Customer customer = new Customer();

                foreach (DataGridViewRow row in this.dtgCustomers.SelectedRows)
                {
                    Customer cust = row.DataBoundItem as Customer;
                    if (cust != null)
                    {
                        
                        frmEditCustomer editCust = new frmEditCustomer(cust.Id);
                        editCust.ShowDialog();
                    }
                }

                
                loadCustomerDatagrid();
            }
        }

        private void btnEditProducts_Click(object sender, EventArgs e)
        {
            using (mCoyoteDBEntities1 context = new mCoyoteDBEntities1())
            {
                Product product = new Product();

                foreach (DataGridViewRow row in this.dtgProducts.SelectedRows)
                {
                    Product prod = row.DataBoundItem as Product;
                    if (prod != null)
                    {

                        frmEditProduct editProd = new frmEditProduct(prod.Id);
                        
                        editProd.ShowDialog();
                    }
                }

                loadProductDatagrid();
            }
        }

        private void cbCustSearchOptions_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void discountButton_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            discountPercentage = b.Text;
            if (b.Text == "10%")
            {
                discountRate = .10;
                btnDisc10.Enabled = false;
                btnDisc15.Enabled = false;
                btnDisc20.Enabled = false;
                btnDisc25.Enabled = false;
            }
            else if (b.Text == "15%")
            {
                discountRate = .15;
                btnDisc10.Enabled = false;
                btnDisc15.Enabled = false;
                btnDisc20.Enabled = false;
                btnDisc25.Enabled = false;
            }
            else if (b.Text == "20%")
            {
                discountRate = .20;
                btnDisc10.Enabled = false;
                btnDisc15.Enabled = false;
                btnDisc20.Enabled = false;
                btnDisc25.Enabled = false;
            }
            else if (b.Text == "25%")
            {
                discountRate = .25;
                btnDisc10.Enabled = false;
                btnDisc15.Enabled = false;
                btnDisc20.Enabled = false;
                btnDisc25.Enabled = false;
            }
        }

        private void btnSearchTransactions_Click(object sender, EventArgs e)
        {
            grossTotal = 0;
            taxTotal = 0;
            discTotal = 0;
            netTotal = 0;

            using (mCoyoteDBEntities1 context = new mCoyoteDBEntities1())
            {
                var trans = context.Transactions.Where(entry => entry.transDate >= dtpFrom.Value.Date && entry.transDate <= dtpTo.Value.Date).ToList();
                dtgTransactions.DataSource = trans.ToList();
                List<Transaction> transAction = trans.ToList();
                for (int i = 0; i < trans.Count(); i++)
                {
                    grossTotal = grossTotal + transAction[i].transGrossTotal;
                    taxTotal = taxTotal + transAction[i].transTax;
                    discTotal = discTotal + transAction[i].transDiscount;
                    netTotal = netTotal + transAction[i].transNetTotal;
                }

                txtGrossTotal.Text = grossTotal.ToString();
                txtTaxTotal.Text = taxTotal.ToString();
                txtDiscountTotal.Text = discTotal.ToString();
                txtNetTotal.Text = netTotal.ToString();
            }
        }

        private void tabTransactions_Enter(object sender, EventArgs e)
        {
            saveToolStripButton.Enabled = true;
            saveToolStripMenuItem.Enabled = true;
        }

        private void tabTransactions_Leave(object sender, EventArgs e)
        {
            saveToolStripButton.Enabled = false;
            saveToolStripMenuItem.Enabled = false;
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            string path = saveFileDialog1.FileName;

            string rows = DataGridViewPrinter.ToCsv(dtgTransactions);
            string calculation = ",Totals:,"
                + txtGrossTotal.Text
                + "," + txtTaxTotal.Text
                + "," + txtDiscountTotal.Text
                + "," + txtNetTotal.Text;
            //var rows = dtgTransactions.Rows;

            //StringBuilder csvContent = new StringBuilder();

            //MessageBox.Show(rows);
            //csvContent.AppendLine("Transaction#,Date,Gross Total,Tax,Discount,Net Total");

            //csvContent.AppendLine(rows[0].ToString());

            if (path != "")
            {
                File.WriteAllText(path, rows);
                File.AppendAllText(path, calculation);
            }
        }
    }

}
