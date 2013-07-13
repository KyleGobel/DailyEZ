using DailyEZ.Web.ViewModels;
using JetNettApi.Data.Contracts;
using JetNettApi.Models;
using Moq;
using NUnit.Framework;

namespace DailyEZ.Web.UnitTests.VM_Tests.DefaultVM
{
    public class MiddleStack
    {
        [Test]
        public void returns_empty_string_when_null_dailyEZ_and_uow_object_passed()
        {
            //create vm with dailyez and uow set to null
            var vm = new DefaultViewModel(null, null);

            //return the left stack html
            var result = vm.MiddleStackHtml();

            //make sure its an empty string
            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void return_empty_string_when_middle_stack_is_non_existant()
        {
            var dailyEZ = new JetNettApi.Models.DailyEZ { DefaultMiddleStack = 0 };

            //mock the unit of work
            Mock<IJetNettApiUnitOfWork> mock = new Mock<IJetNettApiUnitOfWork>();
            //make it so Stacks.GetById returns a null object
            mock.Setup(m => m.Stacks.GetById(It.IsAny<int>())).Returns((Stack)null);

            //initalize the vm
            var vm = new DefaultViewModel(dailyEZ, mock.Object);

            //return left stack html
            var result = vm.MiddleStackHtml();

            //make sure string is empty
            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void removes_seperators_from_widget_string()
        {
            var stackToReturn = new JetNettApi.Models.Stack()
            {
                RawWidgetsString = "SomeWidget###AndAnotherWidget###AndAnother",
            };

            //mock the unit of work
            Mock<IJetNettApiUnitOfWork> mock = new Mock<IJetNettApiUnitOfWork>();
            //make it so Stacks.GetById returns a null object
            mock.Setup(m => m.Stacks.GetById(It.IsAny<int>())).Returns(stackToReturn);

            var vm = new DefaultViewModel(new JetNettApi.Models.DailyEZ() { DefaultMiddleStack = 0 }, mock.Object);
            var result = vm.MiddleStackHtml();

            Assert.AreEqual(stackToReturn.RawWidgetsString.Replace("###", ""), result);
        } 
        [Test]
        public void returns_empty_string_when_middle_stack_id_is_null()
        {
            var dailyEZ = new JetNettApi.Models.DailyEZ {};

            //mock the unit of work
            Mock<IJetNettApiUnitOfWork> mock = new Mock<IJetNettApiUnitOfWork>();
            //make it so Stacks.GetById returns a null object
            mock.Setup(m => m.Stacks.GetById(It.IsAny<int>())).Returns((Stack)null);

            //initalize the vm
            var vm = new DefaultViewModel(dailyEZ, mock.Object);

            //return left stack html
            var result = vm.MiddleStackHtml();

            //make sure string is empty
            Assert.AreEqual(string.Empty, result);
            
        }
    }
}