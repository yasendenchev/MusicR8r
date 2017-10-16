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
    class Update_Should
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
        public void CallAlbumRepositoryGetById_WhenInvoked()
        {
            repositoryMock.Verify(x => x.GetById(guid), Times.Once);
        }

        [Test]
        public void CallAlbumRepositoryUpdate_WhenInvoked()
        {
            repositoryMock.Verify(x => x.Update(album), Times.Once);
        }

        [Test]
        public void CallUnitOfWorkCommit()
        {
            unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
        }
    }
}
