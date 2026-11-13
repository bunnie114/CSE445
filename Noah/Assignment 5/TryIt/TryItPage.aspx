<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TryItPage.aspx.cs" Inherits="TryIt.TryItPage" %>
<%@ Register TagPrefix = "cse" TagName="captcha" src="CaptchaControl.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Assignment 5 TryIt</title>
    <style>
        .section{
            margin-bottom: 50px;
        }

        h2 {
            font-size: 1.25em;
            font-weight: 600;
            margin-bottom: 0px;
        }

        h3 {
            font-size: 1.15em;
            font-weight: 600;
            margin-bottom: 0px;
        }

        .main {
            margin: 12px 8px 8px 8px;
            min-height: 420px;
        }

        div {
            margin: 10px 0px 10px 10px;
        }

        input{
            margin-left: 10px;
        }

        .result{
            margin-top: 10px;
            margin-left: 20px;
            display: block
        }

        .label{
            display: inline-block;
        }

        .row{
            display: inline-block;
        }

        .row div{
            margin: 0px
        }

        .inputSection{
            padding-left: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="main">
            <div class="section">
                <h2>Captcha</h2>
                <div>
                    <cse:captcha runat = "server" />
                </div>
            </div>

            <div class="section">
                <h2>Inventory Management System</h2>
                <div>
                    <h3>getInventory() : Inventory</h3>
                    <div>Get a json representation of the current inventory system stored in XML</div>
                    <asp:Button ID="GetInventoryBtn" runat="server" OnClick="GetInventoryBtn_Click" Text="Retrieve" />
                    <asp:Label ID="GetInventoryResult" class="result" runat="server">
                        Result
                    </asp:Label>
                </div>

                <div>
                    <h3>addStore(string storeID) : string</h3>
                    <div>Add a store to the inventory system</div>
                    <div>
                        <p class="label">Store ID: </p>
                        <asp:TextBox ID="AddStoreInput" runat="server"></asp:TextBox>
                        <asp:Button ID="AddStoreBtn" runat="server" OnClick="AddStoreBtn_Click" Text="Add" />
                    </div>
                    <asp:Label ID="AddStoreResult" class="result" runat="server">
                        Result
                    </asp:Label>
                </div>

                <div>
                    <h3>addProduct(string storeID, string productId, string productName, int quantity) : string</h3>
                    <div>Add a product to a specific store in the inventory system</div>
                    <div class="row">
                        <div class="row">
                            <p class="label">Store ID: </p>
                            <asp:TextBox ID="AddProductInput1" runat="server"></asp:TextBox>
                        </div>
                        <div class="row inputSection">
                            <p class="label">Product ID: </p>
                            <asp:TextBox ID="AddProductInput2" runat="server"></asp:TextBox>
                        </div>
                        <div class="row inputSection">
                            <p class="label">Product Name: </p>
                            <asp:TextBox ID="AddProductInput3" runat="server"></asp:TextBox>
                        </div>
                        <div class="row inputSection">
                            <p class="label">Quantity: </p>
                            <asp:TextBox ID="AddProductInput4" runat="server"></asp:TextBox>
                        </div>
                        <asp:Button ID="AddProductBtn" runat="server" OnClick="AddProductBtn_Click" Text="Add" />
                    </div>
                    <asp:Label ID="AddProductResult" class="result" runat="server">
                        Result
                    </asp:Label>
                </div>

                <div>
                    <h3>storeExists(string storeID) : bool</h3>
                    <div>Check if a specific store exists</div>
                    <div>
                        <p class="label">Store ID: </p>
                        <asp:TextBox ID="StoreExistsInput" runat="server"></asp:TextBox>
                        <asp:Button ID="StoreExistsBtn" runat="server" OnClick="StoreExistsBtn_Click" Text="Check" />
                    </div>
                    <asp:Label ID="StoreExistsResult" class="result" runat="server">
                        Result
                    </asp:Label>
                </div>

                <div>
                    <h3>getStore(string storeID) : Store</h3>
                    <div>Get a specific store from the inventory system</div>
                    <div>
                        <p class="label">Store ID: </p>
                        <asp:TextBox ID="GetStoreInput" runat="server"></asp:TextBox>
                        <asp:Button ID="GetStoreBtn" runat="server" OnClick="GetStoreBtn_Click" Text="Search" />
                    </div>
                    <asp:Label ID="GetStoreResult" class="result" runat="server">
                        Result
                    </asp:Label>
                </div>

                <div>
                    <h3>productExists(string storeID, string productName) : bool</h3>
                    <div>Check if a specific product exists in a specific store</div>
                    <div class="row">
                        <div class="row">
                            <p class="label">Store ID: </p>
                            <asp:TextBox ID="ProductExistsInput1" runat="server"></asp:TextBox>
                        </div>
                        <div class="row inputSection">
                            <p class="label">Product Name: </p>
                            <asp:TextBox ID="ProductExistsInput2" runat="server"></asp:TextBox>
                        </div>
                        <asp:Button ID="ProductExistsBtn" runat="server" OnClick="ProductExistsBtn_Click" Text="Check" />
                    </div>
                    <asp:Label ID="ProductExistsResult" class="result" runat="server">
                        Result
                    </asp:Label>
                </div>

                <div>
                    <h3>getProduct(string storeID, string productName) : Product</h3>
                    <div>Get a specific product from a specific store in the inventory system</div>
                    <div class="row">
                        <div class="row">
                            <p class="label">Store ID: </p>
                            <asp:TextBox ID="GetProductInput1" runat="server"></asp:TextBox>
                        </div>
                        <div class="row inputSection">
                            <p class="label">Product Name: </p>
                            <asp:TextBox ID="GetProductInput2" runat="server"></asp:TextBox>
                        </div>
                        <asp:Button ID="GetProductBtn" runat="server" OnClick="GetProductBtn_Click" Text="Search" />
                    </div>
                    <asp:Label ID="GetProductResult" class="result" runat="server">
                        Result
                    </asp:Label>
                </div>

                <div>
                    <h3>lowStock(string storeID, int threshold) : List&lt;Product&gt;</h3>
                    <div>Get all products in a store whose quantity is less than threshold</div>
                    <div class="row">
                        <div class="row">
                            <p class="label">Store ID: </p>
                            <asp:TextBox ID="LowStockInput1" runat="server"></asp:TextBox>
                        </div>
                        <div class="row inputSection">
                            <p class="label">Threshold: </p>
                            <asp:TextBox ID="LowStockInput2" runat="server"></asp:TextBox>
                        </div>
                        <asp:Button ID="LowStockBtn" runat="server" OnClick="LowStockBtn_Click" Text="Search" />
                    </div>
                    <asp:Label ID="LowStockResult" class="result" runat="server">
                        Result
                    </asp:Label>
                </div>

                <div>
                    <h3>restockProduct(string storeID, string productName, int amount) : string</h3>
                    <div>Add quantity to a specific product in a specific store</div>
                    <div class="row">
                        <div class="row">
                            <p class="label">Store ID: </p>
                            <asp:TextBox ID="RestockProductInput1" runat="server"></asp:TextBox>
                        </div>
                        <div class="row inputSection">
                            <p class="label">Product Name: </p>
                            <asp:TextBox ID="RestockProductInput2" runat="server"></asp:TextBox>
                        </div>
                        <div class="row inputSection">
                            <p class="label">Amount: </p>
                            <asp:TextBox ID="RestockProductInput3" runat="server"></asp:TextBox>
                        </div>
                        <asp:Button ID="RestockProductBtn" runat="server" OnClick="RestockProductBtn_Click" Text="Restock" />
                    </div>
                    <asp:Label ID="RestockProductResult" class="result" runat="server">
                        Result
                    </asp:Label>
                </div>

                <div>
                    <h3>sellProduct(string storeID, string productName, int amount) : string</h3>
                    <div>Remove quantity of a specific product in a specific store</div>
                    <div class="row">
                        <div class="row">
                            <p class="label">Store ID: </p>
                            <asp:TextBox ID="SellProductInput1" runat="server"></asp:TextBox>
                        </div>
                        <div class="row inputSection">
                            <p class="label">Product Name: </p>
                            <asp:TextBox ID="SellProductInput2" runat="server"></asp:TextBox>
                        </div>
                        <div class="row inputSection">
                            <p class="label">Amount: </p>
                            <asp:TextBox ID="SellProductInput3" runat="server"></asp:TextBox>
                        </div>
                        <asp:Button ID="SellProductBtn" runat="server" OnClick="SellProductBtn_Click" Text="Sell" />
                    </div>
                    <asp:Label ID="SellProductResult" class="result" runat="server">
                        Result
                    </asp:Label>
                </div>

                <div>
                    <h3>balanceProduct(string productName) : string</h3>
                    <div>Balance quantity of a specific product across all stores that have the product</div>
                    <div>
                        <p class="label">Product Name: </p>
                        <asp:TextBox ID="BalanceProductInput" runat="server"></asp:TextBox>
                        <asp:Button ID="BalanceProductBtn" runat="server" OnClick="BalanceProductBtn_Click" Text="Balance" />
                    </div>
                    <asp:Label ID="BalanceProductResult" class="result" runat="server">
                        Result
                    </asp:Label>
                </div>

                <div>
                    <h3>removeStore(string storeId) : string</h3>
                    <div>Remove a specific store from the inventory system and redistribute its products to other stores</div>
                    <div>
                        <p class="label">Store ID: </p>
                        <asp:TextBox ID="RemoveStoreInput" runat="server"></asp:TextBox>
                        <asp:Button ID="RemoveStoreBtn" runat="server" OnClick="RemoveStoreBtn_Click" Text="Remove" />
                    </div>
                    <asp:Label ID="RemoveStoreResult" class="result" runat="server">
                        Result
                    </asp:Label>
                </div>

                <div>
                    <h3>removeProduct(string storeID, string productName) : string</h3>
                    <div>Remove a specific product from a specific store</div>
                    <div class="row">
                        <div class="row">
                            <p class="label">Store ID: </p>
                            <asp:TextBox ID="RemoveProductInput1" runat="server"></asp:TextBox>
                        </div>
                        <div class="row inputSection">
                            <p class="label">Product Name: </p>
                            <asp:TextBox ID="RemoveProductInput2" runat="server"></asp:TextBox>
                        </div>
                        <asp:Button ID="RemoveProductBtn" runat="server" OnClick="RemoveProductBtn_Click" Text="Remove" />
                    </div>
                    <asp:Label ID="RemoveProductResult" class="result" runat="server">
                        Result
                    </asp:Label>
                </div>

                <div>
                    <h3>resetInventory() : void</h3>
                    <div>Completely reset the inventory system XML</div>
                    <asp:Button ID="ResetInventoryBtn" runat="server" OnClick="ResetInventoryBtn_Click" Text="Reset" />
                    <asp:Label ID="ResetInventoryResult" class="result" runat="server">
                        Result
                    </asp:Label>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
