using Moq;
using MusicR8r.Data.Model.Models;
using MusicR8r.Data.Repositories;
using MusicR8r.Data.SaveContext;
using MusicR8r.Services;
using MusicR8r.Services.Providers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Tests.GenreServiceTest
{
    [TestFixture]
    public class GetAllAndDeleted_Should
    {
        [Test]
        public void CallRepositoryAllAndDeleted_WhenInvoked()
        {
            var repositoryMock = new Mock<IEfRepository<Genre>>();
            var saveContextMock = new Mock<ISaveContext>();
            var datetimeMock = new Mock<IDateTimeProvider>();
            var service = new GenreService(repositoryMock.Object, saveContextMock.Object, datetimeMock.Object);

            service.GetAllAndDeleted();

            repositoryMock.Verify(r => r.AllAndDeleted, Times.Once);
        }

        [Test]
        public void ReturnCorrectGenres_WhenInvoked()
        {
            var genre1 = new Genre("Rock");
            var genre2 = new Genre("Rap");
            var genres = new List<Genre> { genre1, genre2 }.AsQueryable();
            var repositoryMock = new Mock<IEfRepository<Genre>>();
            var saveContextMock = new Mock<ISaveContext>();
            var datetimeMock = new Mock<IDateTimeProvider>();
            var service = new GenreService(repositoryMock.Object, saveContextMock.Object, datetimeMock.Object);

            repositoryMock.Setup(r => r.AllAndDeleted).Returns(genres);

            var result = service.GetAllAndDeleted();

            CollectionAssert.AreEqual(genres, result);

        }
    }
}
