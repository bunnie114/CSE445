using System.Collections.Generic;

namespace InventoryService
{
    // Defines the data model for a product in the inventory system
    public class Product
    {
        private string productId;
        private string name;
        private int quantity;

        // Constructor for the product object
        public Product()
        {
            this.productId = "";
            this.name = "";
            this.quantity = 0;
        }

        // Getter and setter accessors
        public string ProductId
        {
            get => productId;
            set => productId = value;
        }
        public string Name
        {
            get => name;
            set => name = value;
        }
        public int Quantity
        {
            get => quantity;
            set => quantity = value;
        }
    }

    // Defines the data model for a single store in the inventory system
    public class Store
    {
        private string storeId;
        private List<Product> stock;

        // Constructor for the store object
        public Store()
        {
            this.storeId = "";
            stock = new List<Product>();
        }

        // Getter and setter accessors  
        public string StoreId
        {
            get => storeId;
            set => storeId = value;
        }
        public List<Product> Stock
        {
            get => stock;
            set => stock = value;
        }
    }

    // Defines the data model for the entire inventory system, consisting of multiple stores
    public class Inventory
    {
        private List<Store> stores;

        // Constructor for the inventory object
        public Inventory()
        {
            stores = new List<Store>();
        }

        // Getter and setter accessors
        public List<Store> Stores
        {
            get => stores;
            set => stores = value;
        }
    }
}