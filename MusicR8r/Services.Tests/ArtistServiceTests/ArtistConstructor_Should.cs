
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

namespace Services.Tests.ArtistServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        Mock<IEfRepository<Artist>> artistRepositoryMock;
        Mock<IUnitOfWork> unitOfWorkMock;
        Mock<IDateTimeProvider> dateTimeMock;
        Mock<IAlbumService> albumServiceMock;

        [SetUp]
        public void Setup()
        {

            artistRepositoryMock = new Mock<IEfRepository<Artist>>();
            unitOfWorkMock = new Mock<IUnitOfWork>();
            dateTimeMock = new Mock<IDateTimeProvider>();
            albumServiceMock = new Mock<IAlbumService>();
            
        }

        [Test]
        public void ThrowArgumentNullException_WhenFourthParameterIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ArtistService(artistRepositoryMock.Object, unitOfWorkMock.Object, dateTimeMock.Object, null));
        }

        [Test]
        public void ThrowArgumentNullException_WhenThirdParameterIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ArtistService(artistRepositoryMock.Object, unitOfWorkMock.Object, null, albumServiceMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenSecondParameterIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ArtistService(artistRepositoryMock.Object, null, dateTimeMock.Object, albumServiceMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenFirstParameterIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ArtistService(null, unitOfWorkMock.Object, dateTimeMock.Object, albumServiceMock.Object));
        }
    }
}
