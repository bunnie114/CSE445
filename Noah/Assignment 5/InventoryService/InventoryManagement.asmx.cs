using System;
using System.Collections.Generic;
using System.Web.Services;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using System.Web;
using System.Xml;

namespace InventoryService
{
    /// <summary>
    /// Summary description for InventoryManagement
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class InventoryManagement : System.Web.Services.WebService
    {
        // ******** HELPER METHODS ********

        /// <summary>
        /// Private method to update the inventory stores in the XML.
        /// </summary>
        /// <param name="inventory">The inventory to store in the xml</param>
        private void updateInventory(Inventory inventory)
        {
            // Get the current path to the xml file
            string path = Server.MapPath("~/inventory.xml");

            // Create an XmlSerializer to write the inventory object into the xml
            XmlSerializer serializer = new XmlSerializer(typeof(Inventory));

            // Write the inventory object to the xml file
            using (StreamWriter writer = new StreamWriter(path))
            {
                serializer.Serialize(writer, inventory);
            }
        }

        /// <summary>
        /// Private helper method to get the inventory object from xml.
        /// </summary>
        /// <returns>The XML in Inventory object form</returns>
        private Inventory getInventoryHelper()
        {
            // Get the current path to the xml file
            string path = Server.MapPath("~/inventory.xml");

            // If file does not yet exist, return an empty Inventory object
            if (!File.Exists(path))
            {
                return new Inventory();
            }

            // Create an XmlSerializer to read the xml into an Inventory object
            XmlSerializer serializer = new XmlSerializer(typeof(Inventory));

            // Read the xml file and deserialize it into an Inventory object
            using (StreamReader reader = new StreamReader(path))
            {
                Inventory inventory = (Inventory)serializer.Deserialize(reader);
                return inventory;
            }
        }

        /// <summary>
        /// Private helper method to get the store object from inventory.
        /// </summary>
        /// <remarks>Assumes that the inventory to search exists</remarks>
        /// <param name="inventory">The inventory to search</param>
        /// <param name="storeId">The id of the store to find</param>
        /// <returns>The Store object</returns>
        private Store getStoreHelper(Inventory inventory, string storeId)
        {
            // Search the inventory for the specified store and return result
            return inventory.Stores.Find(s => s.StoreId == storeId);
        }

        /// <summary>
        /// Private helper method to get the product object from store.
        /// </summary>
        /// <remarks>Assumes that the store to search exists</remarks>
        /// <param name="store">The store to search</param>
        /// <param name="productName">The name of the product to find</param>
        /// <returns>The Product object</returns>
        private Product getProductHelper(Store store, string productName)
        {
            // Search the store for the specified product and return result
            return store.Stock.Find(p => p.Name.ToLower() == productName.ToLower());
        }

        /// <summary>
        /// Private helper method to all unique product names.
        /// </summary>
        /// <param name="inventory">Inventory to get all unique product names for</param>
        /// <returns>A list of all unique product names</returns>
        private List<string> getAllProducts(Inventory inventory)
        {
            List<string> products = new List<string>();

            // If inventory is empty, return null
            if (inventory.Stores.Count == 0)
            {
                return products;
            }

            // Loop through each store and their products
            foreach (Store store in inventory.Stores)
            {
                foreach (Product product in store.Stock)
                {
                    // If the product name isn't in the list, add it to list
                    if (!products.Contains(product.Name.ToLower()))
                    {
                        products.Add(product.Name.ToLower());
                    }
                }
            }

            return products;
        }

        /// <summary>
        /// Private helper method to add or remove quantity of product from store.
        /// </summary>
        /// <remarks>Specified store and product must exist</remarks>
        /// <param name="inventory">Inventory to search/update</param>
        /// <param name="storeId">Id of store to update</param>
        /// <param name="productName">The name of the product to update</param>
        /// <param name="amount">Amount to update the quantity by</param>
        /// <param name="op">Specified whether to add or remove quantity</param>
        /// <returns>A string indicating success or failure</returns>
        private string sellRestockHelper(Inventory inventory, string storeId, string productName, int amount, string op)
        {
            // Make sure new amount is not negative
            if (amount <= 0)
                return "New amount must be greater than zero.";

            // To update product quantity and update xml
            // Specified store and product must exist
            Store store = getStoreHelper(inventory, storeId);
            if (store == null)
            {
                return $"No store with id {storeId} exists.";
            }

            // If product doesn't exist, return message
            Product product = getProductHelper(store, productName);
            if (product == null)
            {
                return $"No product with name {productName} exists.";
            }

            // If requirements met, update product and xml
            // If specified operation is "restock", add quantity
            if (op == "restock")
            {
                product.Quantity += amount;
                updateInventory(inventory);
                return $"Restocked {amount} units of product {productName} in store {storeId}.";
                // Else, operation is "sell", so remove quantity
            }
            else
            {
                if (product.Quantity < amount)
                    return "Cannot sell more product than what is in stock.";
                product.Quantity -= amount;
                updateInventory(inventory);
                return $"Sold {amount} units of product {productName} at store {storeId}.";
            }
        }

        /// <summary>
        /// Private helper method to reallocate a product's quantity to another store.
        /// </summary>
        /// <remarks>It allocates it to the first store with the product and if no store has the product, do nothing</remarks>
        /// <param name="inventory">Inventory to search/update</param>
        /// <param name="storeId">Id of store to exclude</param>
        /// <param name="productName">The name of the product to reallocate</param>
        /// <return>Id of store reallocted to</return>
        private string reallocateProduct(Inventory inventory, string storeId, string productName)
        {
            // Get the quantity of the product to reallocate
            int amount = getStoreHelper(inventory, storeId).Stock.Find(p => p.Name.ToLower() == productName.ToLower()).Quantity;

            // Loop through each store in inventory
            foreach (Store store in inventory.Stores)
            {
                // If the store contains the product, transfer product's quantity to store
                // Exlude the store that the product is from
                if (store.StoreId != storeId)
                {
                    Product product = getProductHelper(store, productName);

                    if (product != null)
                    {
                        product.Quantity += amount;
                        return store.StoreId;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Private helper method to balance a specific product within all stores that have the product.
        /// </summary>
        /// <remarks>If the total quantity doesn't divide perfectly across all stores, assign excess to first stores</remarks>
        /// <param name="inventory">Inventory to search/update</param>
        /// <param name="productName">The name of the product to balance</param>
        /// <return>List of how many stores were balanced and, on average, how much stock at each</return>
        private List<int> balanceHelper(Inventory inventory, string productName)
        {
            int total = 0;
            List<Store> stores = new List<Store>();

            // Loop through each store in inventory
            foreach (Store store in inventory.Stores)
            {
                // Find the total of the product across all stores
                Product product = getProductHelper(store, productName);
                if (product != null)
                {
                    total += product.Quantity;
                    stores.Add(store);
                }
            }

            // Calculate how much to assign to each store and how much leftover
            int avg = stores.Count == 0 ? 0 : total / stores.Count;
            int remainder = stores.Count == 0 ? 0 : total % stores.Count;
            bool extra = remainder > 0;

            // Update the quantity of each product in the stores that have the product
            foreach (Store store in stores)
            {
                Product product = getProductHelper(store, productName);
                // Get how much to assign to each store
                int amount = avg;
                if (remainder > 0)
                {
                    amount++;
                    remainder--;
                }

                product.Quantity = amount;
            }

            return stores.Count == 0 ? new List<int>() { 0, 0, 0 } :
                new List<int>() { stores.Count, avg, extra ? 1 : 0 };
        }



        // ******** READ METHODS ********

        /// <summary>
        /// Get entire xml file and return the object.
        /// </summary>
        /// <returns>The xml in Inventory object form</returns>
        [WebMethod]
        public Inventory getInventory()
        {
            // Call the private helper method to get the xml as an inventory object
            return getInventoryHelper();
        }

        /// <summary>
        /// Get the specified store from the xml and return it.
        /// </summary>
        /// <param name="storeId">The id of the store to find</param>
        /// <returns>The Store object</returns>
        [WebMethod]
        public Store getStore(string storeId)
        {
            // Call the private helper method to get the xml as an inventory object
            Inventory inventory = getInventoryHelper();

            // Call the private helper method to get the specified store in the inventory
            return getStoreHelper(inventory, storeId);
        }

        /// <summary>
        /// Get the product from a store in the xml and return it.
        /// </summary>
        /// <param name="storeId">The id of the store to search</param>
        /// <param name="productName">The name of the product to find</param>
        /// <returns>The Product object</returns>
        [WebMethod]
        public Product getProduct(string storeId, string productName)
        {
            // Call the private helper method to get the xml as an inventory object
            Inventory inventory = getInventoryHelper();

            // Call the private helper method to get the specified store in the inventory
            Store store = getStoreHelper(inventory, storeId);

            // If the store is null, return null
            if (store == null)
                return null;

            // Call the private helper method to get the specified product in the store
            return getProductHelper(store, productName);
        }

        /// <summary>
        /// Get list of products in store with quantity less than specified threshold.
        /// </summary>
        /// <param name="storeId">Id of store to search</param>
        /// <param name="threshold">Threshold to check each product against</param>
        /// <returns>A list of products below threshold</returns>
        [WebMethod]
        public List<Product> lowStock(string storeId, int threshold)
        {
            // Call the private helper method to get the xml as an inventory object
            Inventory inventory = getInventoryHelper();

            List<Product> products = new List<Product>();

            // Make sure that the store exists
            Store store = getStoreHelper(inventory, storeId);
            if (store != null)
            {
                foreach (Product product in store.Stock)
                {
                    // If product quantity is below threshold, add to list
                    if (product.Quantity < threshold)
                        products.Add(product);
                }
                return products;
                // Else, return an null
            }
            else
            {
                return null;
            }
        }



        // ******** CREATE METHODS ********

        /// <summary>
        /// Add a store to inventory and return a string.
        /// </summary>
        /// <remarks>Checks to see if the store id already exists.</remarks>
        /// <param name="storeId">New id for the store</param>
        /// <returns>A string indicating success or failure</returns>
        [WebMethod]
        public String addStore(string storeId)
        {
            // Call the private helper method to get the xml as an inventory object
            Inventory inventory = getInventoryHelper();

            // If store with specified id doesn't exist, create a new store and add to inventory and write back to xml
            if (getStoreHelper(inventory, storeId) == null)
            {
                Store newStore = new Store();
                newStore.StoreId = storeId;
                inventory.Stores.Add(newStore);
                updateInventory(inventory);
                return $"Store {storeId} successfully added to inventory system";
                // Else, store with id exists so return message
            }
            else
            {
                return $"A store with id {storeId} already exists.";
            }
        }

        /// <summary>
        /// Add a product to store and return a string.
        /// </summary>
        /// <remarks>Checks to see if the product id and/or name already exists.</remarks>
        /// <param name="storeId">Id of store to add product to</param>
        /// <param name="productId">New id for the product</param>
        /// <param name="productName">Name for new product</param>
        /// <param name="productQuantity">Sarting quantity for new product (0 default)</param>
        /// <returns>A string indicating success or failure</returns>
        [WebMethod]
        public string addProduct(string storeId, string productId, string productName, int productQuantity = 0)
        {
            // Make sure new quantity is not negative
            if (productQuantity < 0)
                return "Cannot assign a negative quantity";

            // Call the private helper method to get the xml as an inventory object
            Inventory inventory = getInventoryHelper();

            // To create new product, add to store, and update xml
            // Specified store must exist and product with id or name must not exist
            Store store = getStoreHelper(inventory, storeId);

            if (store == null)
            {
                return $"No store with id {storeId} exists.";
            }

            // If requirements met, create new product and add to store and update xml
            if (getProductHelper(store, productName) == null && store.Stock.Find(p => p.ProductId == productId) == null)
            {
                Product newProduct = new Product();
                newProduct.ProductId = productId;
                newProduct.Name = productName;
                newProduct.Quantity = productQuantity;
                store.Stock.Add(newProduct);
                updateInventory(inventory);
                return $"{productQuantity} units of {productName} {productId} successfully added to store {storeId}";
                // Else, the product exists, so return message
            }
            else
            {
                return $"A product with id {productId} or name {productName} already exists in store {storeId}.";
            }
        }



        // ******** UPDATE METHODS ********

        /// <summary>
        /// Add product quantity to store.
        /// </summary>
        /// <remarks>Specified store and product needs to exist.</remarks>
        /// <param name="storeId">Id of store to update stock</param>
        /// <param name="productName">Name of product to add quantity to</param>
        /// <param name="amount">New amount to add to quantity</param>
        /// <returns>A string indicating success or failure</returns>
        [WebMethod]
        public string restockProduct(string storeId, string productName, int amount)
        {
            // Call the private helper method to get the xml as an inventory object
            Inventory inventory = getInventoryHelper();

            // Call private helper method to restock product
            return sellRestockHelper(inventory, storeId, productName, amount, "restock");
        }

        /// <summary>
        /// Remove/sell product for a store.
        /// </summary>
        /// <remarks>Specified store and product needs to exist.</remarks>
        /// <param name="storeId">Id of store to update stock</param>
        /// <param name="productName">Name of product to remove quantity to</param>
        /// <param name="amount">Amount of product sold</param>
        /// <returns>A string indicating success or failure</returns>
        [WebMethod]
        public string sellProduct(string storeId, string productName, int amount)
        {
            // Call the private helper method to get the xml as an inventory object
            Inventory inventory = getInventoryHelper();

            // Call private helper method to restock product
            return sellRestockHelper(inventory, storeId, productName, amount, "sell");
        }

        /// <summary>
        /// Balances the quantities of a specific product across all stores that contain the product.
        /// </summary>
        /// <param name="productName">Name of product to balance quantity</param>
        /// <returns>A string indicating results of balance</returns>
        [WebMethod]
        public string balanceProduct(string productName)
        {
            // Call the private helper method to get the xml as an inventory object
            Inventory inventory = getInventoryHelper();

            // Check to make sure productName is an actual product in system
            List<string> products = getAllProducts(inventory);

            if (products.Count == 0 || !products.Contains(productName.ToLower()))
            {
                return "Product does not exist in the inventory.";
            }

            List<int> balancing = balanceHelper(inventory, productName);
            string units = balancing[2] == 1 ?
                $"({balancing[1]}-{balancing[1] + balancing[2]} units per store)" :
                $"({balancing[1]} units per store)";

            updateInventory(inventory);
            return $"Product {productName} balanced between {balancing[0]} stores {units}.";
        }



        // ******** DELETE METHODS ********

        /// <summary>
        /// Remove a store from the inventory system.
        /// </summary>
        /// <param name="storeId">Id of store to remove</param>
        /// <returns>A string indicating success or failure</returns>
        [WebMethod]
        public string removeStore(string storeId)
        {
            // Call the private helper method to get the xml as an inventory object
            Inventory inventory = getInventoryHelper();
            int reallocations = 0;
            List<string> products = new List<string>();

            // Specified store must exist
            Store store = getStoreHelper(inventory, storeId);

            if (store == null)
            {
                return $"No store with id {storeId} exists.";
            }

            // Reallocate the product, remove it, and balance product quantiy among stores that have it
            foreach (Product product in store.Stock)
            {
                reallocations += reallocateProduct(inventory, storeId, product.Name) != null ? 1 : 0;
                products.Add(product.Name);
            }

            // Remove store, balance products from said store, and update xml
            inventory.Stores.RemoveAll(s => s.StoreId == storeId);
            foreach (string name in products)
            {
                balanceHelper(inventory, name);
            }
            updateInventory(inventory);

            return $"Store {storeId} removed and {reallocations} product(s) reallocated.";
        }

        /// <summary>
        /// Remove a product from a store.
        /// </summary>
        /// <param name="storeId">Id of store to remove from</param>
        /// <param name="productName">Name of product to remove</param>
        /// <returns>A string indicating success or failure</returns>
        [WebMethod]
        public string removeProduct(string storeId, string productName)
        {
            // Call the private helper method to get the xml as an inventory object
            Inventory inventory = getInventoryHelper();

            // Specified store must exist
            Store store = getStoreHelper(inventory, storeId);

            if (store == null)
            {
                return $"No store with id {storeId} exists.";
            }

            // Product must exist
            Product product = getProductHelper(store, productName);

            if (product == null)
            {
                return $"No product with name {productName} exists.";
            }

            // Reallocate product, remove it, balance product across stores, and update xml
            string id = reallocateProduct(inventory, storeId, productName);
            store.Stock.RemoveAll(p => p.Name.ToLower() == productName.ToLower());
            List<int> balancing = balanceHelper(inventory, productName);
            string units = balancing[2] == 1 ?
                $"({balancing[1]}-{balancing[1] + balancing[2]} units per store)" :
                $"({balancing[1]} units per store)";
            updateInventory(inventory);

            return id == null ? $"Product {productName} removed" :
                $"Product {productName} removed from store {storeId} and balanced between {balancing[0]} stores {units}.";
        }

        /// <summary>
        /// Resets the entire inventory to empty.
        /// </summary>
        [WebMethod]
        public void resetInventory()
        {
            // Call the private helper method to update inventory
            updateInventory(new Inventory());
        }



        // ******** MISC METHODS ********

        /// <summary>
        /// Checks if a store exists in the inventory system.
        /// </summary>
        /// <param name="storeId">storeId to check</param>
        /// <returns>A boolean indicating whether store exists or not</returns>
        [WebMethod]
        public bool storeExists(string storeId)
        {
            // Call the private helper method to get the store
            Store store = getStoreHelper(getInventoryHelper(), storeId);
            return (store == null) ? false : true;
        }

        /// <summary>
        /// Checks if a product exists in a specific store system.
        /// </summary>
        /// <param name="storeId">storeId to check</param>
        /// <param name="productName">productName to check</param>
        /// <returns>A boolean indicating whether product exists in the store or not</returns>
        [WebMethod]
        public bool productExists(string storeId, string productName)
        {
            // If the store doesn't exist, the product can't exist, so return false
            if (!storeExists(storeId))
            {
                return false;
            }

            // Call the private helper method to get the product
            Product product = getProductHelper(getStoreHelper(getInventoryHelper(), storeId), productName);

            // If the product doesn't exist, return false, else return true
            return (product == null) ? false : true;
        }
    }
}
