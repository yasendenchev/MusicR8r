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

namespace Services.Tests.SongServiceTest
{
    [TestFixture]
    class Constructor_Should
    {
        Mock<IEfRepository<Song>> songRepositoryMock;
        Mock<IEfRepository<Album>> albumRepositoryMock;
        Mock<IEfRepository<Artist>> artistRepositoryMock;
        Mock<IEfRepository<Genre>> genreRepositoryMock;
        Mock<IUnitOfWork> unitOfWorkMock;
        Mock<IDateTimeProvider> dateTimeMock;

        [SetUp]
        public void Setup()
        {
            albumRepositoryMock = new Mock<IEfRepository<Album>>();
            artistRepositoryMock = new Mock<IEfRepository<Artist>>();
            songRepositoryMock = new Mock<IEfRepository<Song>>();
            genreRepositoryMock = new Mock<IEfRepository<Genre>>();
            unitOfWorkMock = new Mock<IUnitOfWork>();
            dateTimeMock = new Mock<IDateTimeProvider>();

        }

        [Test]
        public void ThrowArgumentNullException_WhenSixthParameterIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new SongService(songRepositoryMock.Object,
                albumRepositoryMock.Object,
                genreRepositoryMock.Object,
                artistRepositoryMock.Object,
                unitOfWorkMock.Object,
                null));
        }

        [Test]
        public void ThrowArgumentNullException_WhenFifthParameterIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new SongService(songRepositoryMock.Object,
                albumRepositoryMock.Object,
                genreRepositoryMock.Object,
                artistRepositoryMock.Object,
                null,
                dateTimeMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenFourthParameterIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new SongService(songRepositoryMock.Object,
                   albumRepositoryMock.Object,
                   genreRepositoryMock.Object,
                   null,
                   unitOfWorkMock.Object,
                   dateTimeMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenThirdParameterIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new SongService(songRepositoryMock.Object,
                   albumRepositoryMock.Object,
                   null,
                   artistRepositoryMock.Object,
                   unitOfWorkMock.Object,
                   dateTimeMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenSecondParameterIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new SongService(songRepositoryMock.Object,
                   null,
                   genreRepositoryMock.Object,
                   artistRepositoryMock.Object,
                   unitOfWorkMock.Object,
                   dateTimeMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenFirstParameterIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new SongService(null,
                   albumRepositoryMock.Object,
                   genreRepositoryMock.Object,
                   artistRepositoryMock.Object,
                   unitOfWorkMock.Object,
                   dateTimeMock.Object));
        }

    }
}
