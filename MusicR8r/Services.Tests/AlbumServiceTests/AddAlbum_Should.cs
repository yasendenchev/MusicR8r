using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using MusicR8r.Data.Repositories;
using MusicR8r.Data.Model.Models;
using MusicR8r.Data.UnitOfWork;
using MusicR8r.Services;
using MusicR8r.Services.Providers;

namespace Services.Tests.AlbumServiceTests
{
    [TestFixture]
    class AddAlbum_Should
    {
        
        [Test]
        public void CallRepositoryAdd_WhenInvoked()
        {
            var repositoryMock = new Mock<IEfRepository<Album>>();
            var repositoryArtistMock = new Mock<IEfRepository<Artist>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var service = new AlbumService(repositoryMock.Object, repositoryArtistMock.Object, unitOfWorkMock.Object, dateTimeProvider.Object);
            var name = "AlbumName";
            var year = 1998;
            var guid = Guid.NewGuid();

            service.AddAlbum(name, year, guid);

            repositoryMock.Verify(r => r.Add(It.IsAny<Album>()), Times.Once);

        }

        [Test]
        public void CallUnitOfWorkCommit_WhenInvoked()
        {
            var repositoryMock = new Mock<IEfRepository<Album>>();
            var repositoryArtistMock = new Mock<IEfRepository<Artist>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var service = new AlbumService(repositoryMock.Object, repositoryArtistMock.Object, unitOfWorkMock.Object, dateTimeProvider.Object);
            var name = "AlbumName";
            var year = 1998;
            var guid = Guid.NewGuid();

            service.AddAlbum(name, year, guid);

            unitOfWorkMock.Verify(s => s.Commit(), Times.Once);
        }
    }
}
