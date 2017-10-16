using Moq;
using MusicR8r.Data.Model.Models;
using MusicR8r.Data.Repositories;
using MusicR8r.Data.UnitOfWork;
using MusicR8r.Services;
using MusicR8r.Services.Providers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Tests.SongServiceTests
{
    [TestFixture]
    public class DeleteById_Should
    {
        Mock<IEfRepository<Song>> songRepositoryMock;
        Mock<IEfRepository<Album>> albumRepositoryMock;
        Mock<IEfRepository<Artist>> artistRepositoryMock;
        Mock<IEfRepository<Genre>> genreRepositoryMock;
        Mock<IUnitOfWork> unitOfWorkMock;
        Mock<IDateTimeProvider> dateTimeMock;
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
        }


        [Test]
        public void CallSongRepositoryGetById_WhenInvoked()
        {
            var guid = Guid.NewGuid();
            service.DeleteById(guid);

            songRepositoryMock.Verify(x => x.GetById(guid), Times.Once());
        }

        [Test]
        public void SetIsDeletedToTrue_WhenInvoked()
        {
            var dt = DateTime.Now;
            var guid = Guid.NewGuid();
            var song = new Song();


            dateTimeMock.Setup(x => x.Now()).Returns(dt);

            songRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(song);

            service.DeleteById(guid); service.DeleteById(guid);

            Assert.IsTrue(song.IsDeleted);
        }

        [Test]
        public void SetDeletedOnToCorrectDateTime_WhenInvoked()
        {
            var dt = DateTime.Now;
            var guid = Guid.NewGuid();
            var song = new Song();


            dateTimeMock.Setup(x => x.Now()).Returns(dt);

            songRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(song);

            service.DeleteById(guid);

            Assert.AreEqual(song.DeletedOn, dt);
        }
    }
}
