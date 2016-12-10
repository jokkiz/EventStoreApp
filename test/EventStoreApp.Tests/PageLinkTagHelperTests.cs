using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventStoreApp.Infrastructure;
using EventStoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using Xunit;

namespace EventStoreApp.Tests
{
    public class PageLinkTagHelperTests
    {
        [Fact]
        public void Can_Generate_PageLinks()
        {
            var urlHelper = new Mock<IUrlHelper>();
            urlHelper.SetupSequence(x => x.Action(It.IsAny<UrlActionContext>()))
                .Returns("Test/page1")
                .Returns("Test/page2")
                .Returns("Test/page3");
            var urlHelperFactory = new Mock<IUrlHelperFactory>();
            urlHelperFactory.Setup(f => f.GetUrlHelper(It.IsAny<ActionContext>())).Returns(urlHelper.Object);

            PageLinkTagHelper helper = new PageLinkTagHelper(urlHelperFactory.Object)
            {
                PageModel = new PagingInfo
                {
                    CurrentPage = 2,
                    TotalItems = 28,
                    ItemsPerPage = 10
                },
                PageAction = "Test"
            };

            var ctx = new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), "");

            var content = new Mock<TagHelperContent>();
            TagHelperOutput output = new TagHelperOutput("div", new TagHelperAttributeList(), (cache, encoder)=> Task.FromResult(content.Object));

            helper.Process(ctx, output);

            Assert.Equal(@"<a href=""Test/page1"">1</a>" +
                @"<a href=""Test/page2"">2</a>" +
                @"<a href=""Test/page3"">3</a>", output.Content.GetContent()
                );
        }
    }
}
