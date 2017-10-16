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
    public class Update_Should
    {
        Mock<IEfRepository<Song>> songRepositoryMock;
        Mock<IEfRepository<Album>> albumRepositoryMock;
        Mock<IEfRepository<Artist>> artistRepositoryMock;
        Mock<IEfRepository<Genre>> genreRepositoryMock;
        Mock<IUnitOfWork> unitOfWorkMock;
        Mock<IDateTimeProvider> dateTimeMock;

        SongService service;

        Guid genreId;
        Guid songId;
        Genre genre;
        Album album;
        Artist artist;
        Song song;
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
            artist = new Artist();
            album = new Album();
            album.Artist = artist;
            genre = new Genre();
            genreId = Guid.NewGuid();
            songId = Guid.NewGuid();
            song = new Song();
            name = "song1";
            duration = 5;
            service = new SongService(songRepositoryMock.Object,
                albumRepositoryMock.Object,
                genreRepositoryMock.Object,
                artistRepositoryMock.Object,
                unitOfWorkMock.Object,
                dateTimeMock.Object);
            songRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(song);
            genreRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(genre);

            service.Update(songId, genreId, name, 52);
        }

        [Test]
        public void CallGenreRepositoryGetById_WhenInvoked()
        {
            genreRepositoryMock.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void CallAlbumRepositoryGetById_WhenInvoked()
        {

            songRepositoryMock.Verify(x => x.GetById(songId), Times.Once);
        }

        [Test]
        public void CallSongRepositoryAdd_WhenInvoked()
        {
            var song = new Song();
            song.Name = name;
            song.Duration = duration;
            song.Artist = artist;
            song.Album = album;
            songRepositoryMock.Verify(x => x.Update(It.IsAny<Song>()), Times.Once);
        }

        [Test]
        public void CallUnitOfWorkCommmit_WhenInvoked()
        {
            var song = new Song();
            song.Name = name;
            song.Duration = duration;
            song.Artist = artist;
            song.Album = album;
            unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
        }
    }
}
