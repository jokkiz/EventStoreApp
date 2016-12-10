using System.Collections.Generic;
using System.Linq;
using EventStoreApp;
using EventStoreApp.Models.Abstract;
using EventStoreApp.Models.Entities;
using EventStoreApp.Controllers;
using Xunit;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace EventStoreApp.Tests   
{
    public class EventControllerTest
    {

        [Fact]
        public void Can_Paginate()
        {
            //Arrange
            Mock<IEventRepository> mock = new Mock<IEventRepository>();
            mock.Setup(m => m.Events).Returns(new Event[]
            {
                new Event { Id=1, ShortName = "Event1", Name = "Event1"},
                new Event { Id=2, ShortName = "Event2", Name = "Event2"},
                new Event { Id=3, ShortName = "Event3", Name = "Event3"},
                new Event { Id=4, ShortName = "Event4", Name = "Event4"},
                new Event { Id=5, ShortName = "Event5", Name = "Event5"},
            });

            EventController controller = new EventController(mock.Object, null);
            controller.PageSize = 3;
            //Act
            IEnumerable<Event> result = controller.Index("",1).ViewData.Model as IEnumerable<Event>;
            //Assert
            Event[] eventArray = result.ToArray();

            Assert.True(eventArray.Length == 2);
        }
    }
}
