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
    class GetSongs_Should
    {
        Mock<IEfRepository<Album>> repositoryMock;
        Mock<IEfRepository<Artist>> artistRepositoryMock;
        Mock<IUnitOfWork> unitOfWorkMock;
        Mock<IDateTimeProvider> dateTimeMock;
        AlbumService service;
        Guid guid;
        Album album;
        Song song1;
        Song song2;

        [SetUp]
        public void Setup()
        {
            repositoryMock = new Mock<IEfRepository<Album>>();
            artistRepositoryMock = new Mock<IEfRepository<Artist>>();
            unitOfWorkMock = new Mock<IUnitOfWork>();
            dateTimeMock = new Mock<IDateTimeProvider>();
            guid = Guid.NewGuid();
            song1 = new Song();
            song2 = new Song();
            album = new Album();
            album.Songs.Add(song1);
            album.Songs.Add(song2);
            service = new AlbumService(repositoryMock.Object, artistRepositoryMock.Object, unitOfWorkMock.Object, dateTimeMock.Object);
            repositoryMock.Setup(x => x.GetById(guid)).Returns(album);
            
        }

        [Test]
        public void CallAlbumRepositoryGetById_WhenInvoked()
        {
            var songs = service.GetSongs(guid);
            CollectionAssert.AreEqual(songs, album.Songs);
        }
    }
}
