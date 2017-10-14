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
    class AddAlbum_Should
    {
        
        [Test]
        [Ignore("Null reference because of invalid query")]
        public void CallRepositoryAdd_WhenInvoked()
        {
            var repositoryMock = new Mock<IEfRepository<Album>>();
            var saveContextMock = new Mock<ISaveContext>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var service = new AlbumService(repositoryMock.Object, saveContextMock.Object, dateTimeProvider.Object);
            var name = "AlbumName";
            var year = 1998;
            var guid = Guid.NewGuid();

            service.AddAlbum(name, year, guid);

            repositoryMock.Verify(r => r.Add(It.IsAny<Album>()), Times.Once);

        }

        //[Test]
        //public void CallRepositoryAll_WhenInvoked()
        //{
        //    var repositoryMock = new Mock<IEfRepository<Album>>();
        //    var saveContextMock = new Mock<ISaveContext>();
        //    var dateTimeProvider = new Mock<IDateTimeProvider>();
        //    var service = new AlbumService(repositoryMock.Object, saveContextMock.Object, dateTimeProvider.Object);

        //}

        [Test]
        [Ignore("Null reference because of invalid query")]
        public void CallSaveContextCommit_WhenInvoked()
        {
            var repositoryMock = new Mock<IEfRepository<Album>>();
            var saveContextMock = new Mock<ISaveContext>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var service = new AlbumService(repositoryMock.Object, saveContextMock.Object, dateTimeProvider.Object);
            var name = "AlbumName";
            var year = 1998;
            var guid = Guid.NewGuid();

            service.AddAlbum(name, year, guid);

            saveContextMock.Verify(s => s.Commit(), Times.Once);
        }


    }
}
