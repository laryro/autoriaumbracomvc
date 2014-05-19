
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.XPath;
using System.Text;
using System.Globalization;
using System.Net.Mail;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Web.Routing;
using umbraco.presentation.nodeFactory;
using Umbraco.Core;

namespace AutoriaUmbracoMvc.Helpers
{

    public static class FileUploaderHelpers
    {
        public static String GetName<TModel, TProperty>(this Expression<Func<TModel, TProperty>> expression)
        {
            return ((MemberExpression)expression.Body).Member.Name;
        }

        public static MvcHtmlString FileUpload(this HtmlHelper htmlHelper, String name)
        {
            return htmlHelper.FileUpload(name, (object)null);
        }

        public static MvcHtmlString FileUpload(this HtmlHelper htmlHelper, String name, object htmlAttributes)
        {
            return htmlHelper.FileUpload(name, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString FileUpload(this HtmlHelper htmlHelper, String name, IDictionary<String, Object> htmlAttributes)
        {
            var tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("type", "file", true);
            tagBuilder.MergeAttribute("name", name, true);
            tagBuilder.GenerateId(name);

            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString FileUploadFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
            where TModel : class
        {
            return htmlHelper.FileUpload(expression.GetName(), new RouteValueDictionary(htmlAttributes));
        }
    }

    public class Helpers
    {
        public static Dictionary<int, string> GetPrevalues(int dataTypeId)
        {
            XPathNodeIterator preValueRootElementIterator = umbraco.library.GetPreValues(dataTypeId);
            preValueRootElementIterator.MoveNext(); //move to first 
            XPathNodeIterator preValueIterator = preValueRootElementIterator.Current.SelectChildren("preValue", "");
            var retVal = new Dictionary<int, string>();

            while (preValueIterator.MoveNext())
            {
                retVal.Add(Convert.ToInt32(preValueIterator.Current.GetAttribute("id", "")), preValueIterator.Current.Value);
            }
            return retVal;
        }

        public static bool SendEmail(MailMessage msg)
        {
            try
            {
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();

                //smtp.EnableSsl = true;

                smtp.Send(msg);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static String GetCssFriendlyString(string text)
        {
            return GetCssFriendlyString(text, "", false);
        }
        public static String GetCssFriendlyString(string text, string separator, bool toLower)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();
            var retorno = "";

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            retorno = stringBuilder.ToString().Normalize(NormalizationForm.FormC).Replace(" ", separator);

            if (toLower)
            {
                return retorno.ToLower();
            }

            return retorno;
        }

    }
}