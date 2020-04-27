using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SparkAuto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparkAuto.TagHelpers
{
    [HtmlTargetElement("div",Attributes="page-model")]
    public class PageLinkTagHelper
    {
        private IUrlHelperFactory urlHelperFactory;

        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }
        
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public PagingInfo pageModel { get; set; }

        public string PageAction { get; set; }

        public string PageClass { get; set; }

        public string pageClassNormal { get; set; }

        public string PageClassSelected { get; set; }
    }
}
