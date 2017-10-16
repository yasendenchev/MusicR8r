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

namespace Services.Tests.GenreServiceTest
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenGenreRepositoryIsNull()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var datetimeMock = new Mock<IDateTimeProvider>();

            Assert.Throws<ArgumentNullException>(() => new GenreService(null, unitOfWorkMock.Object, datetimeMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenGenreUnitOfWorkIsNull()
        {
            var repositoryMock = new Mock<IEfRepository<Genre>>();
            var datetimeMock = new Mock<IDateTimeProvider>();

            Assert.Throws<ArgumentNullException>(() => new GenreService(repositoryMock.Object, null, datetimeMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenDateTimeProviderIsNull()
        {
            var repositoryMock = new Mock<IEfRepository<Genre>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            Assert.Throws<ArgumentNullException>(() => new GenreService(repositoryMock.Object, unitOfWorkMock.Object, null));
        }

        [Test]
        public void NotThrow_WhenPassedDependenciesAreNotNull()
        {
            var repositoryMock = new Mock<IEfRepository<Genre>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var datetimeMock = new Mock<IDateTimeProvider>();

            Assert.DoesNotThrow(() => new GenreService(repositoryMock.Object, unitOfWorkMock.Object, datetimeMock.Object));
        }

        [Test]
        public void InitializeDependenciesCorrectly_WhenTheyAreNotNull()
        {
            var repositoryMock = new Mock<IEfRepository<Genre>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var datetimeMock = new Mock<IDateTimeProvider>();
            var service = new GenreService(repositoryMock.Object, unitOfWorkMock.Object, datetimeMock.Object);

            Assert.IsNotNull(service);
        }
    }
}
