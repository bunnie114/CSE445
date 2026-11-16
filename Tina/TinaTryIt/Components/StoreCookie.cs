using System;
using System.Web;

namespace TinaComponents
{

    public static class StoreCookie
    {
        public const string CookieName = "PreferredPharmacyStore";
        private const int MaxLength = 50;

        ///Saves the preferred store for 30 days.
        /// Input: storeId, storeName (non-empty, max 50 chars). Throws ArgumentException on bad input
        public static void SaveStore(string storeId, string storeName)
        {
            if (string.IsNullOrWhiteSpace(storeId) || string.IsNullOrWhiteSpace(storeName))
                throw new ArgumentException("Store ID and store name are required.");

            storeId = storeId.Trim();
            storeName = storeName.Trim();

            if (storeId.Length > MaxLength || storeName.Length > MaxLength)
                throw new ArgumentException("Store ID and name must be " + MaxLength + " characters or fewer.");

            HttpCookie cookie = new HttpCookie(CookieName);
            cookie.Values["StoreID"] = storeId;      // HttpCookie URL-encodes values automatically
            cookie.Values["StoreName"] = storeName;
            cookie.Expires = DateTime.Now.AddDays(30);
            cookie.HttpOnly = true;                  // not readable by client-side script
            cookie.Secure = HttpContext.Current.Request.IsSecureConnection;
            cookie.SameSite = SameSiteMode.Lax;

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>Reads the saved store. Returns false if no cookie exists
        public static bool TryGetStore(out string storeId, out string storeName)
        {
            storeId = null;
            storeName = null;

            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
            if (cookie == null) return false;

            storeId = cookie.Values["StoreID"];
            storeName = cookie.Values["StoreName"];
            return !string.IsNullOrEmpty(storeId);
        }

        /// Human-readable version of the saved store (for the TryIt page)
        public static string GetStore()
        {
            string id, name;
            if (!TryGetStore(out id, out name))
                return "No preferred store saved.";
            return "Store ID: " + id + ", Store Name: " + name;
        }

        /// Deletes the cookie by expiring it in the past
        public static void ClearStore()
        {
            HttpCookie cookie = new HttpCookie(CookieName);
            cookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}
