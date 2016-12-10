using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventStoreApp.Controllers;
using EventStoreApp.Models;
using EventStoreApp.Models.Abstract;
using EventStoreApp.Models.Entities;
using EventStoreApp.Models.EventViewModel;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace EventStoreApp.Tests
{
    public class EventControllerTests
    {
        [Fact]
        public void Can_Paginate()
        {
            Mock<IEventRepository> mock = new Mock<IEventRepository>();
            mock.Setup(m => m.Events).Returns(new Event[]
            {
                new Event {Id = 1, ShortName = "E1", Name = "Event1"},
                new Event {Id = 2, ShortName = "E2", Name = "Event2"},
                new Event {Id = 3, ShortName = "E3", Name = "Event3"},
                new Event {Id = 4, ShortName = "E4", Name = "Event4"},
                new Event {Id = 5, ShortName = "E5", Name = "Event5"}
            });

            EventController controller = new EventController(mock.Object, null);
            controller.PageSize = 3;
            EventListViewModel result = controller.Index("", 2).ViewData.Model as EventListViewModel;

            Event[] eventArray = result.EventList.ToArray();
            Assert.True(eventArray.Length == 2);
            Assert.Equal("E4", eventArray[0].ShortName);
            Assert.Equal("E5", eventArray[1].ShortName);
        }

        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            //Arrange
            Mock<IEventRepository> mock = new Mock<IEventRepository>();
            mock.Setup(m => m.Events).Returns(new Event[]
            {
                new Event {Id = 1, ShortName = "E1", Name = "Event1"},
                new Event {Id = 2, ShortName = "E2", Name = "Event2"},
                new Event {Id = 3, ShortName = "E3", Name = "Event3"},
                new Event {Id = 4, ShortName = "E4", Name = "Event4"},
                new Event {Id = 5, ShortName = "E5", Name = "Event5"}
            });

            //Act
            EventController controller = new EventController(mock.Object, null) {PageSize = 3};
            EventListViewModel result = controller.Index("", 2).ViewData.Model as EventListViewModel;
            
            //Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }   
    }
}
