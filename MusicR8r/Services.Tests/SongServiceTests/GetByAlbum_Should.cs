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
    public class GetByAlbum_Should
    {
        Mock<IEfRepository<Song>> songRepositoryMock;
        Mock<IEfRepository<Album>> albumRepositoryMock;
        Mock<IEfRepository<Artist>> artistRepositoryMock;
        Mock<IEfRepository<Genre>> genreRepositoryMock;
        Mock<IUnitOfWork> unitOfWorkMock;
        Mock<IDateTimeProvider> dateTimeMock;

        Song song;

        SongService service;
        

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

            song = new Song();
            
        }

        [Test]
        public void CallSongRepositoryGetById_WhenInvoked()
        {
            var guid = Guid.NewGuid();

            service.GetByAlbum(guid);

            songRepositoryMock.Verify(x => x.All, Times.Once);
        }
    }
}
