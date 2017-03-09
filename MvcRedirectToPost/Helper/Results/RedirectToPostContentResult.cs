using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MvcRedirectToPost.Helper.Results
{
    public class RedirectToPostContentResult : ContentResult
    {
        public Dictionary<string, object> PostData { get; set; }
        public string Url { get; set; }
        
        public RedirectToPostContentResult() { }
        public RedirectToPostContentResult(string url, Dictionary<string, object> postData)
        {
            this.Url = url;
            this.PostData = postData;
        }

        public virtual string GetResult()
        {
            return PreparePOSTForm(this.Url, PostData);
        }

        /// <summary>
        /// This method prepares an Html form which holds all data in hidden field in the addetion to form submitting script.
        /// </summary>
        /// <param name="url">The destination Url to which the post and redirection will occur, the Url can be in the same App or ouside the App.</param>
        /// <param name="data">A collection of data that will be posted to the destination Url.</param>
        /// <returns>Returns a string representation of the Posting form.</returns>
        /// <Author>Samer Abu Rabie</Author>
        private static String PreparePOSTForm(string url, Dictionary<string, object> data)
        {
            //Set a name for the form
            string formID = "PostForm";

            //Build the form using the specified data to be posted.
            StringBuilder strForm = new StringBuilder();
            strForm.Append("<form id=\"" + formID + "\" name=\"" + formID + "\" action=\"" + url + "\" method=\"POST\">");
            foreach (KeyValuePair<string, object> pair in data)
            {
                strForm.Append("<input type=\"hidden\" name=\"" + pair.Key + "\" value=\"" + pair.Value + "\">");
            }
            strForm.Append("</form>");

            //Build the JavaScript which will do the Posting operation.
            StringBuilder strScript = new StringBuilder();
            strScript.Append("<script language='javascript'>");
            strScript.Append("var v" + formID + " = document." + formID + ";");
            strScript.Append("v" + formID + ".submit();");
            strScript.Append("</script>");

            //Return the form and the script concatenated. (The order is important, Form then JavaScript)
            return strForm.ToString() + strScript.ToString();
        }
    }
}