
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WordFilterService
{
    [ServiceContract]
    public interface IService1
    {
        /// <summary>
        /// Removes HTML/XML tags and common stop words
        /// from the provided text.
        /// </summary>
        /// <param name="str">
        /// The input text or webpage content.
        /// </param>
        /// <returns>
        /// A string containing the filtered text.
        /// </returns>
        [OperationContract]
        [WebInvoke(
            Method = "POST",
            UriTemplate = "/filter",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        string WordFilter(string str);
    }
}
