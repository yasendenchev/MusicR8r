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

namespace Services.Tests
{
    [TestFixture]
    public class GetById_Should
    {
        Mock<IEfRepository<Song>> songRepositoryMock;
        Mock<IEfRepository<Album>> albumRepositoryMock;
        Mock<IEfRepository<Artist>> artistRepositoryMock;
        Mock<IEfRepository<Genre>> genreRepositoryMock;
        Mock<IUnitOfWork> unitOfWorkMock;
        Mock<IDateTimeProvider> dateTimeMock;
        Guid genreId;
        Guid albumId;
        Genre genre;
        Album album;
        Artist artist;
        Song song;
        SongService service;
        int duration;
        string name;

        [SetUp]
        public void Setup()
        {
            albumRepositoryMock = new Mock<IEfRepository<Album>>();
            artistRepositoryMock = new Mock<IEfRepository<Artist>>();
            songRepositoryMock = new Mock<IEfRepository<Song>>();
            genreRepositoryMock = new Mock<IEfRepository<Genre>>();
            unitOfWorkMock = new Mock<IUnitOfWork>();
            dateTimeMock = new Mock<IDateTimeProvider>();
            service = new SongService(songRepositoryMock.Object,
                albumRepositoryMock.Object,
                genreRepositoryMock.Object,
                artistRepositoryMock.Object,
                unitOfWorkMock.Object,
                dateTimeMock.Object);
            albumRepositoryMock.Setup(x => x.GetById(albumId)).Returns(album);
            genreRepositoryMock.Setup(x => x.GetById(genreId)).Returns(genre);
        }

        [Test]
        public void CallSongRepositoryGetById_WhenInvoked()
        {

            albumRepositoryMock = new Mock<IEfRepository<Album>>();
            artistRepositoryMock = new Mock<IEfRepository<Artist>>();
            songRepositoryMock = new Mock<IEfRepository<Song>>();
            genreRepositoryMock = new Mock<IEfRepository<Genre>>();
            unitOfWorkMock = new Mock<IUnitOfWork>();
            dateTimeMock = new Mock<IDateTimeProvider>();
            service = new SongService(songRepositoryMock.Object,
                albumRepositoryMock.Object,
                genreRepositoryMock.Object,
                artistRepositoryMock.Object,
                unitOfWorkMock.Object,
                dateTimeMock.Object);

            albumRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(album);
            genreRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(genre);

            var guid = Guid.NewGuid();

            service.GetById(guid);

            songRepositoryMock.Verify(x => x.GetById(guid), Times.Once);
        }
    }
}
