using System.Collections.Generic;
using System.Net;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using mvc.crud.online.Models;
using mvc.crud.online.Controllers;

namespace mvc.crud.online.Tests.Controller
{
    [TestFixture]
    public class GlossaryControllerTest
    {
        [Test]
        public void ShouldReturnTheDefalutIndexView()
        {
            List<Glossary> gItems = new List<Glossary>()
            {
                new Glossary{Term = "te1", Defination = "ddd1"},
                new Glossary{Term = "te2", Defination = "ddd2"},
                new Glossary{Term = "te3", Defination = "ddd3"}
            };

            var fakerepo = new Mock<IGlossaryDBSource>();
            fakerepo.Setup(x => x.GetAllGlossary()).Returns(gItems);
            var ctor = new GlossaryController(fakerepo.Object);
            ctor.WithCallTo(x => x.Index()).ShouldRenderDefaultView().WithModel(gItems);
        }

        [Test]
        public void ShouldReturnNotFoundHttpStatusCode()
        {
            var fakerepo = new Mock<IGlossaryDBSource>();
            var ctor = new GlossaryController(fakerepo.Object);
            ctor.WithCallTo(x => x.Edit(2)).ShouldGiveHttpStatus(HttpStatusCode.NotFound);
        }

        [Test]
        public void ShouldReturnTheDefaultEditView()
        {
            var gItem = new Glossary
            {
                Term = "jitu",
                Defination = "jitu is a good boy"
            };

            var fakerepo = new Mock<IGlossaryDBSource>();
            fakerepo.Setup(x => x.Find(99)).Returns(gItem);
            var ctor = new GlossaryController(fakerepo.Object);
            ctor.WithCallTo(x => x.Edit(99)).ShouldRenderDefaultView().WithModel(gItem);
        }

        [Test]
        public void FakeForTesting()
        {
            var fakerepo = new Mock<IGlossaryDBSource>();
            var ctor = new GlossaryController(fakerepo.Object);
            ctor.WithCallTo(x => x.Fake()).ShouldRenderDefaultView();
        }

    }
}
