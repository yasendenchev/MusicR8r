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
    class DeleteById_Should
    {
        Mock<IEfRepository<Album>> repositoryMock;
        Mock<IEfRepository<Artist>> artistRepositoryMock;
        Mock<IUnitOfWork> unitOfWorkMock;
        Mock<IDateTimeProvider> dateTimeMock;
        AlbumService service;
        Guid guid;
        Album album;
        Song song1;
        DateTime dt;

        [SetUp]
        public void Setup()
        {
            repositoryMock = new Mock<IEfRepository<Album>>();
            artistRepositoryMock = new Mock<IEfRepository<Artist>>();
            unitOfWorkMock = new Mock<IUnitOfWork>();
            dateTimeMock = new Mock<IDateTimeProvider>();
            dt = DateTime.Now;
            guid = Guid.NewGuid();
            song1 = new Song();
            album = new Album();
            album.Songs.Add(song1);
            service = new AlbumService(repositoryMock.Object, artistRepositoryMock.Object, unitOfWorkMock.Object, dateTimeMock.Object);
            repositoryMock.Setup(x => x.GetById(guid)).Returns(album);
            dateTimeMock.Setup(x => x.Now()).Returns(dt);
            service.DeleteById(guid);
        }

        [Test]
        public void CallRepositoryGetById_WhenInvoked()
        {
            repositoryMock.Verify(x => x.GetById(guid), Times.Once());
        }

        [Test]
        public void CallDateTimeProviderNow_WhenInvoked()
        {
            dateTimeMock.Verify(x => x.Now(), Times.Once());
        }

        [Test]
        public void ShouldSetAlbumSongsIsDeletedToTrue_WhenCollectionIsNotEmpty()
        {
            Assert.AreEqual(album.Songs.ElementAt(0).IsDeleted, true);
        }

        [Test]
        public void ShouldSetAlbumSongsDateDeletedToCurrentDate_WhenCollectionIsNotEmpty()
        {
            Assert.AreEqual(album.Songs.ElementAt(0).DeletedOn, dt);
        }

        [Test]
        public void ShouldSetAlbumIsDeletedToTrue_WhenAlbumExists()
        {
            Assert.AreEqual(album.IsDeleted, true);
        }

        [Test]
        public void ShouldSetAlbumDeletedOnToCurrentDate_WhenAlbumExists()
        {
            Assert.AreEqual(album.DeletedOn, dt);
        }

        [Test]
        public void ShouldCallRepositoryUpdate_WhenInvoked()
        {
            repositoryMock.Verify(x => x.Update(album), Times.Once());
        }

        [Test]
        public void ShouldCallUnitOfWorkCommit_WhenInvoked()
        {
            unitOfWorkMock.Verify(x => x.Commit(), Times.Once());
        }
    }
}
