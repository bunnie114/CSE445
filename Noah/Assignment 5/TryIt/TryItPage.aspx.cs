using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Net.Http;
using TryIt.InventoryManagementService;

namespace TryIt
{
    public partial class TryItPage : System.Web.UI.Page
    {
        // Create the inventory management proxy
        private InventoryManagementService.InventoryManagementSoapClient prxy = new InventoryManagementService.InventoryManagementSoapClient();

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void GetInventoryBtn_Click(object sender, EventArgs e)
        {
            // Call the method through the proxy
            InventoryManagementService.Inventory inventory = prxy.getInventory();

            // Show the response on the page
            // Need to serialize response in order to print it out
            GetInventoryResult.Text = JsonConvert.SerializeObject(inventory, Formatting.Indented);
        }

        protected void GetStoreBtn_Click(object sender, EventArgs e)
        {
            // Get user input
            string storeId = GetStoreInput.Text;

            // Call the method through the proxy
            InventoryManagementService.Store store = prxy.getStore(storeId);

            // Show the response on the page
            // Need to serialize response in order to print it out
            GetStoreResult.Text = store == null ? $"Store {storeId} does not exist." :
                JsonConvert.SerializeObject(store, Formatting.Indented);
        }

        protected void GetProductBtn_Click(object sender, EventArgs e)
        {
            // Get user input
            string storeId = GetProductInput1.Text;
            string productName = GetProductInput2.Text;

            // Call the method through the proxy
            InventoryManagementService.Product product = prxy.getProduct(storeId, productName);

            // Show the response on the page
            // Need to serialize response in order to print it out
            GetProductResult.Text = product == null ? $"Product {productName} does not exist in store {storeId}." :
                JsonConvert.SerializeObject(product, Formatting.Indented);
        }

        protected void LowStockBtn_Click(object sender, EventArgs e)
        {
            // Get user input
            string storeId = LowStockInput1.Text;
            int threshold;
            // Make sure they entered a number
            if (!int.TryParse(LowStockInput2.Text, out threshold))
            {
                LowStockResult.Text = "Input must be an integer.";
                return;
            }

            // Call the method through the proxy
            InventoryManagementService.Product[] products = prxy.lowStock(storeId, threshold);

            // Show the response on the page
            // Need to serialize response in order to print it out
            LowStockResult.Text = products == null ? $"No products in store {storeId} are below {threshold}." :
                JsonConvert.SerializeObject(products, Formatting.Indented);
        }

        protected void AddStoreBtn_Click(object sender, EventArgs e)
        {
            // Get user input
            string storeId = AddStoreInput.Text;

            // Call the method through the proxy and display result
            AddStoreResult.Text = prxy.addStore(storeId);
        }

        protected void AddProductBtn_Click(object sender, EventArgs e)
        {
            // Get user input
            string storeId = AddProductInput1.Text;
            string productId = AddProductInput2.Text;
            string productName = AddProductInput3.Text;
            int quantity;
            // Make sure they entered a number
            if (!int.TryParse(AddProductInput4.Text, out quantity))
            {
                AddProductResult.Text = "Input must be an integer.";
                return;
            }

            // Call the method through the proxy and display result
            AddProductResult.Text = prxy.addProduct(storeId, productId, productName, quantity);
        }

        protected void RestockProductBtn_Click(object sender, EventArgs e)
        {
            // Get user input
            string storeId = RestockProductInput1.Text;
            string productName = RestockProductInput2.Text;
            int amount;
            // Make sure they entered a number
            if (!int.TryParse(RestockProductInput3.Text, out amount))
            {
                RestockProductResult.Text = "Input must be an integer.";
                return;
            }

            // Call the method through the proxy and display result
            RestockProductResult.Text = prxy.restockProduct(storeId, productName, amount);
        }

        protected void SellProductBtn_Click(object sender, EventArgs e)
        {
            // Get user input
            string storeId = SellProductInput1.Text;
            string productName = SellProductInput2.Text;
            int amount;
            // Make sure they entered a number
            if (!int.TryParse(SellProductInput3.Text, out amount))
            {
                SellProductResult.Text = "Input must be an integer.";
                return;
            }

            // Call the method through the proxy and display result
            SellProductResult.Text = prxy.sellProduct(storeId, productName, amount);
        }

        protected void BalanceProductBtn_Click(object sender, EventArgs e)
        {
            // Get user input
            string productName = BalanceProductInput.Text;

            // Call the method through the proxy and display result
            BalanceProductResult.Text = prxy.balanceProduct(productName);
        }

        protected void RemoveStoreBtn_Click(object sender, EventArgs e)
        {
            // Get user input
            string storeId = RemoveStoreInput.Text;

            // Call the method through the proxy and display result
            RemoveStoreResult.Text = prxy.removeStore(storeId);
        }

        protected void RemoveProductBtn_Click(object sender, EventArgs e)
        {
            // Get user input
            string storeId = RemoveProductInput1.Text;
            string productName = RemoveProductInput2.Text;

            // Call the method through the proxy and display result
            RemoveProductResult.Text = prxy.removeProduct(storeId, productName);
        }

        protected void ResetInventoryBtn_Click(object sender, EventArgs e)
        {
            // Call the method through the proxy
            prxy.resetInventory();
            ResetInventoryResult.Text = "Inventory successfully reset.";
        }
    }
}