
using System;
using System.Web;

public class StoreCookie
{
    private const string CookieName = "PreferredPharmacyStore";

    // Save the user's preferred pharmacy store.
    public static void SaveStore(string storeId, string storeName)
    {
        HttpCookie cookie = new HttpCookie(CookieName);

        cookie.Values["StoreID"] = storeId;
        cookie.Values["StoreName"] = storeName;

        cookie.Expires = DateTime.Now.AddDays(30);
        cookie.HttpOnly = true;
        cookie.Secure = HttpContext.Current.Request.IsSecureConnection;

        HttpContext.Current.Response.Cookies.Add(cookie);
    }

    // Retrieve the saved pharmacy store.
    public static string GetStore()
    {
        HttpCookie cookie =
            HttpContext.Current.Request.Cookies[CookieName];

        if (cookie == null)
            return "No preferred store saved.";

        string storeId = cookie.Values["StoreID"];
        string storeName = cookie.Values["StoreName"];

        return "Store ID: " + storeId +
               ", Store Name: " + storeName;
    }

    // Delete the saved pharmacy preference.
    public static void ClearStore()
    {
        HttpCookie cookie = new HttpCookie(CookieName);

        cookie.Expires = DateTime.Now.AddDays(-1);

        HttpContext.Current.Response.Cookies.Add(cookie);
    }
}
