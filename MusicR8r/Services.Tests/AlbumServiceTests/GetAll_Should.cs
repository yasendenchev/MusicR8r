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
    class GetAll_Should
    {
        Mock<IEfRepository<Album>> repositoryMock;
        Mock<IEfRepository<Artist>> artistRepositoryMock;
        Mock<IUnitOfWork> unitOfWorkMock;
        Mock<IDateTimeProvider> dateTimeMock;
        AlbumService service;
        Guid guid;
        Album album;
        string name;
        int year;

        [SetUp]
        public void Setup()
        {
            repositoryMock = new Mock<IEfRepository<Album>>();
            artistRepositoryMock = new Mock<IEfRepository<Artist>>();
            unitOfWorkMock = new Mock<IUnitOfWork>();
            dateTimeMock = new Mock<IDateTimeProvider>();
            name = "album1";
            year = 1992;
            guid = Guid.NewGuid();
            album = new Album();
            service = new AlbumService(repositoryMock.Object, artistRepositoryMock.Object, unitOfWorkMock.Object, dateTimeMock.Object);
            repositoryMock.Setup(x => x.GetById(guid)).Returns(album);
            service.Update(guid, name, year);
        }

        [Test]
        public void CallRepositoryAdd_WhenInvoked()
        {
            var repositoryMock = new Mock<IEfRepository<Album>>();
            var repositoryArtistMock = new Mock<IEfRepository<Artist>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var service = new AlbumService(repositoryMock.Object, repositoryArtistMock.Object, unitOfWorkMock.Object, dateTimeProvider.Object);

            service.GetAll();

            repositoryMock.Verify(r => r.All, Times.Once);
        }

       
    }
}
