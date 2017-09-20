using System;
using System.Text;
using System.Web.Mvc;
using LabaManageSys.WebUI.Models;

namespace LabaManageSys.WebUI.HtmlHelpers
{
    public static class StarsHelpers
    {
        private static int maxEval = 5;

        public static MvcHtmlString Stars(this HtmlHelper html, double currEval)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= maxEval; i++)
            {
                result.Append("<li style=\"display: inherit\">");
                TagBuilder tag = new TagBuilder("a");
                if (currEval >= i)
                {
                    tag.MergeAttribute("style", "background:url(/images/star.jpg) center; display:inherit");
                }
                else
                {
                    if (currEval >= i - 0.2)
                    {
                        tag.MergeAttribute("style", "background:url(/images/star1.jpg) center; display:inherit");
                    }
                    else
                    {
                        if (currEval >= i - 0.5)
                        {
                            tag.MergeAttribute("style", "background:url(/images/star1.jpg) top; display:inherit");
                        }
                        else
                        {
                            if (currEval >= i - 0.8)
                            {
                                tag.MergeAttribute("style", "background:url(/images/star1.jpg) bottom; display:inherit");
                            }
                            else
                            {
                                tag.MergeAttribute("style", "background:url(/images/star.jpg) bottom; display:inherit");
                            }
                        }
                    }
                }

                result.Append(tag.ToString());
                result.Append("</li>");
            }

            return MvcHtmlString.Create(result.ToString());
        }
    }
}