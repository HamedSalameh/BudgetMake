using System;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;

namespace BudgetMake.Presentation.Web.Helpers
{
    public static class CustomHtmlHelpers
    {
        public static MvcHtmlString bsModal(this HtmlHelper helper, string ModalId, string ModalHeader)
        {
            StringBuilder strHtml = new StringBuilder();
            
            strHtml.AppendFormat(@"<div class='modal fade' id='{0}Modal' role='dialog'>", ModalId);
            strHtml.AppendFormat(@"<div class='modal-dialog'>");
            strHtml.AppendFormat(@"<div class='modal-content'>");
            strHtml.AppendFormat(@"<div class='modal-header'>");
            strHtml.AppendFormat(@"{0}", ModalHeader);
            strHtml.AppendFormat(@"</div>");
            strHtml.AppendFormat(@"<div class='modal-body'>",ModalId);
            strHtml.AppendFormat("<div id='{0}_alertBox'></div>", ModalId);
            strHtml.AppendFormat("<div id='{0}FormBody'></div>", ModalId);
            strHtml.AppendFormat(@"</div>");
            strHtml.AppendFormat(@"<div class='modal-footer'>");
            strHtml.AppendFormat(@"</div>");
            strHtml.AppendFormat(@"</div>");
            strHtml.AppendFormat(@"</div>");
            strHtml.Append(@"</div>");
            return MvcHtmlString.Create(strHtml.ToString());
        }

    }
}