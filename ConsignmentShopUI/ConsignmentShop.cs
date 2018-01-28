using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsignmentShopLibrary;

namespace ConsignmentShopUI
{
    public partial class ConsignmentShop : Form
    {
        private Store store = new Store();
        private List<Item> shoppingCartData = new List<Item>();

        BindingSource itemsBinding = new BindingSource();
        BindingSource cartBinding = new BindingSource();
        BindingSource vendorsBinding = new BindingSource();

        private decimal storeProfit = 0;

        public ConsignmentShop()
        {
            InitializeComponent();
            SetupData();
            ItemBind();
            CartBind();
            VendorBind();
        }

        private void SetupData()
        {
            store.Vendors.Add(new Vendor
            {
                FirstName = "Bill",
                LastName = "Smith"                    
            });

            store.Vendors.Add(new Vendor
            {
                FirstName = "Sue", 
                LastName = "Jones"
            });

            store.Vendors.Add(new Vendor
            {
                FirstName = "Kevin",
                LastName = "Chirayath"
            });

            store.Items.Add(new Item {

                Title = "Moby Dick",
                Description = "Book about a whale",
                Price = 4.50M, 
                Owner = store.Vendors[0],
                PaymentDistributed = true
                
            });

            store.Items.Add(new Item
            {

                Title = "Chronicles of White Fox",
                Description = "A boy troubled with the task of saving the world!",
                Price = 7.25M,
                Owner = store.Vendors[2],
                PaymentDistributed = true

            });

            store.Items.Add(new Item
            {

                Title = "A Tale of Two Cities",
                Description = "A book about the revolution",
                Price = 4.32M,
                Owner = store.Vendors[1],
                PaymentDistributed = true

            });

            store.Items.Add(new Item
            {

                Title = "Harry Potter and the Prisoner of Azkaban",
                Description = "4th installation to the Harry Potter series!",
                Price = 4.25M,
                Owner = store.Vendors[2],
                PaymentDistributed = true

            });

            store.Name = "Mike's Cereal Shack";
        }

        private void ItemBind()
        {
            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList(); 
            itemsListBox.DataSource = itemsBinding;

            itemsListBox.DisplayMember = "Display";
            itemsListBox.ValueMember = "Display";
        }

        private void CartBind()
        {
            cartBinding.DataSource = shoppingCartData;
            shoppingCartListbox.DataSource = cartBinding;

            shoppingCartListbox.DisplayMember = "Display";
            shoppingCartListbox.ValueMember = "Display";
        }

        private void VendorBind()
        {
            vendorsBinding.DataSource = store.Vendors;
            vendorsListbox.DataSource = vendorsBinding;

            vendorsListbox.DisplayMember = "Display";
            vendorsListbox.ValueMember = "Display";
        }

        private void addToCart_Click(object sender, EventArgs e)
        {
            Item selectedItem = (Item)itemsListBox.SelectedItem;

            shoppingCartData.Add(selectedItem);
            cartBinding.ResetBindings(false);
        }
        private void makePurchase_Click(object sender, EventArgs e)
        {
            foreach (Item item in shoppingCartData)
            {
                item.Sold = true;
                item.Owner.PaymentDue += (decimal)item.Owner.Commission * item.Price;
                storeProfit += (1 - (decimal)item.Owner.Commission) * item.Price;
            }

            shoppingCartData.Clear();

            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();

            storeProfitValue.Text = string.Format("${0}", storeProfit);

            cartBinding.ResetBindings(false);
            itemsBinding.ResetBindings(false);
            vendorsBinding.ResetBindings(false);
        }

        private void ConsignmentShop_Load(object sender, EventArgs e)
        {

        }        

        private void headerText_Click(object sender, EventArgs e)
        {

        }

        private void storeItems_Click(object sender, EventArgs e)
        {

        }

        private void itemsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void vendorsListbox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void storeProfitValue_Click(object sender, EventArgs e)
        {

        }
    }
}
