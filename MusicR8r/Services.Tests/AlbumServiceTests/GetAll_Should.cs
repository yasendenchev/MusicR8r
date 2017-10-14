using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using MusicR8r.Data.Repositories;
using MusicR8r.Data.Model.Models;
using MusicR8r.Data.SaveContext;
using MusicR8r.Services;
using MusicR8r.Services.Providers;

namespace Services.Tests.AlbumServiceTests
{
    [TestFixture]
    class GetAll_Should
    {
        [Test]
        public void CallRepositoryAdd_WhenInvoked()
        {
            var repositoryMock = new Mock<IEfRepository<Album>>();
            var saveContextMock = new Mock<ISaveContext>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var service = new AlbumService(repositoryMock.Object, saveContextMock.Object, dateTimeProvider.Object);

            service.GetAll();

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        //[Test]
        //public void ReturnCorrectData_WhenInvoked()
        //{
        //    var repositoryMock = new Mock<IEfRepository<Album>>();
        //    var saveContextMock = new Mock<ISaveContext>();
        //    var dateTimeProvider = new Mock<IDateTimeProvider>();
        //    var service = new AlbumService(repositoryMock.Object, saveContextMock.Object, dateTimeProvider.Object);
        //    var sampleAlbum1 = new Album();
        //}
    }
}
