using System;
using System.Collections.Generic;
using DailyEZ.Web.Code.WidgetControllers;
using JetNettApi.Data;
using JetNettApi.Data.Contracts;
using JetNettApi.Models;
using Moq;
using NUnit.Framework;
using System.Linq;
namespace DailyEZ.Web.UnitTests
{
    public class AdGroupWidgetControllerTests
    {
         
        [Test]
        public void someTest()
        {
            var adGroupMoq = new AdGroup()
            {
                Ads = "19",
                FallbackAdGroup = 0,
                Id = 1,
                Name = "Sample Group",
                Seed = 1,
                ViewportSize = 6
            };

            var adAssignmentStub = new AdAssignment()
                {
                    AdGroup = 1,
                    AdId= 7,
                    AdLimit = 500,
                    AdMode = 1,
                    ClientId = 406,
                    HeightLimitation = 400,
                    Id = 1,
                    Name = "Some Ad Assignment",
                    ViewPrice = 0.05d
                };
            var adStub = new Ad()
                {
                    AdHeight = 400,
                    BorderStyle = "solid",
                    ClientId = 406,
                    DateCreated = DateTime.Now,
                    Description = "this doesn't matter",
                    Html = "<b>This is my test ad</b>",
                    Id = 7,
                    Name = "Test Ad"
                };
            List<AdAssignment> list = new List<AdAssignment>() {adAssignmentStub};
            

            Mock<IJetNettApiUnitOfWork> uow = new Mock<IJetNettApiUnitOfWork>();
            var queryable = list.AsQueryable();
            //when uow asks for a specific adGroup shoot back our mock ad group
            uow.Setup(a => a.AdGroups.GetById(It.IsAny<int>())).Returns(adGroupMoq);

            //when it asks for ad assignments, give it our mock list
            uow.Setup(a => a.AdAssignments.GetAll()).Returns(queryable);
     
            //when it asks for a specific ad, it had better be ad number 7 (if it's not return a null), and the return our adStub
            uow.Setup(a => a.Ads.GetById(It.Is<int>(response => response == 7)))
                .Returns(adStub);
        
           
            var controller = new AdGroupWidgetController {Uow = uow.Object};


            var result = controller.GetAdGroupHtml(1, false, null);

            Assert.IsTrue(result.Length > 1);


        }
        public interface IFakeUow
        {
            IRepository<AdGroup> AdGroups { get; set; } 
        }
        class FakeClass : IFakeUow
        {
            public IRepository<AdGroup> AdGroups { get; set; }
            
        }
       public class ClassUnderTest
        {
            public IFakeUow fake { get; set; }
            public AdGroup SomeMethod()
            {
                return fake.AdGroups.GetById(14);
            }
        }
     
        
    }
}