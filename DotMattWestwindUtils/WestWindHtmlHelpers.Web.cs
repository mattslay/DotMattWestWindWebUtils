using System;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Text;
using System.Globalization;
using System.Threading;
using System.Web.Mvc.Html;
using System.Text.RegularExpressions;
using System.Collections.Generic;

using Westwind.Web.Mvc;

namespace DotMattLibrary.Web
{
    /// <summary>
    /// A set of Razor Html Helpers for West Wind items styled for Bootstrap 3.
    /// By Matt Slay
    /// </summary>
    public static class WestWindHtmlHelpers
    {
        /// <summary>
        /// Add a "_" to the beginning of field name on the validation error method if you do not want the passed fieldIdPrefix to be added here.
        /// Parameter timeoutToClearErrorHighlightCss controls how long the control will have the errorHighlight class after use clicks on the error
        /// message. After timeout value, it will be removed. Pass 0 to leave it permanently on the control.
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="fieldIdPrefix"></param>
        /// <returns></returns>
        public static IHtmlString BootstrapValidationErrors(ErrorDisplay errors, string fieldIdPrefix, int timeoutToClearErrorHighlightCss = 1000)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<div class=\"panel panel-danger\">");
            sb.AppendLine("<div class=\"panel-heading\">Errors:</div>");
            sb.AppendLine("<div class=\"panel-body\">");
            sb.AppendLine("<div class=\"list-group\">");

            string fieldId = "";

            foreach (Westwind.Utilities.ValidationError error in errors.DisplayErrors)
            {
                if (error.ControlID.StartsWith("_"))
                    fieldId = error.ControlID.Substring(1);
                else
                    fieldId = fieldIdPrefix + "_" + error.ControlID;
                sb.Append("<a href='#' class=\"list-group-item list-group-item-danger\" onclick=\"_errorLinkClick('" + fieldId + "'); return false;\">");
                sb.AppendLine("<span class=\"glyphicon glyphicon-exclamation-sign\">&nbsp;</span>" + error.Message + "</a>");
            }
    
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");


            var script = @"
                <script>
                    function _errorLinkClick(id) {
                        var $t = $('#' + id).focus().addClass('errorhighlight');            
                       _timeoutCode_
                    }
                </script>";

            string timeoutCode = "";

            if (timeoutToClearErrorHighlightCss > 0)
            {
                timeoutCode = @"setTimeout(function() {
                                   $t.removeClass('errorhighlight');
                                }, timeoutValue)";

                timeoutCode = timeoutCode.Replace("timeoutValue", timeoutToClearErrorHighlightCss.ToString());
            }

            script = script.Replace("_timeoutCode_", timeoutCode);

            sb.AppendLine(script);

            return new HtmlString(sb.ToString());
        }
    }
}