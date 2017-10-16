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

namespace Services.Tests.AlbumServiceTest
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenAlbumRepositoryIsNull()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var datetimeMock = new Mock<IDateTimeProvider>();
            var artistRepositoryMock = new Mock<IEfRepository<Artist>>();

            Assert.Throws<ArgumentNullException>(() => new AlbumService(null, artistRepositoryMock.Object, unitOfWorkMock.Object, datetimeMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenAlbumUnitOfWorkIsNull()
        {
            var repositoryMock = new Mock<IEfRepository<Album>>();
            var datetimeMock = new Mock<IDateTimeProvider>();
            var artistRepositoryMock = new Mock<IEfRepository<Artist>>();

            Assert.Throws<ArgumentNullException>(() => new AlbumService(repositoryMock.Object, artistRepositoryMock.Object, null, datetimeMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenDateTimeProviderIsNull()
        {
            var repositoryMock = new Mock<IEfRepository<Album>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var artistRepositoryMock = new Mock<IEfRepository<Artist>>();

            Assert.Throws<ArgumentNullException>(() => new AlbumService(repositoryMock.Object, artistRepositoryMock.Object, unitOfWorkMock.Object, null));
        }

        [Test]
        public void ThrowArgumentNullException_WhenArtistRepositoryIsNull()
        {
            var repositoryMock = new Mock<IEfRepository<Album>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var datetimeMock = new Mock<IDateTimeProvider>();

            Assert.Throws<ArgumentNullException>(() => new AlbumService(repositoryMock.Object, null, unitOfWorkMock.Object, datetimeMock.Object));
        }

        [Test]
        public void NotThrow_WhenPassedDependenciesAreNotNull()
        {
            var repositoryMock = new Mock<IEfRepository<Album>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var datetimeMock = new Mock<IDateTimeProvider>();
            var artistRepositoryMock = new Mock<IEfRepository<Artist>>();

            Assert.DoesNotThrow(() => new AlbumService(repositoryMock.Object, artistRepositoryMock.Object, unitOfWorkMock.Object, datetimeMock.Object));
        }

        [Test]
        public void InitializeDependenciesCorrectly_WhenTheyAreNotNull()
        {
            var repositoryMock = new Mock<IEfRepository<Album>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var datetimeMock = new Mock<IDateTimeProvider>();
            var artistRepositoryMock = new Mock<IEfRepository<Artist>>();
            var service = new AlbumService(repositoryMock.Object, artistRepositoryMock.Object, unitOfWorkMock.Object, datetimeMock.Object);

            Assert.IsNotNull(service);
        }
    }
}
