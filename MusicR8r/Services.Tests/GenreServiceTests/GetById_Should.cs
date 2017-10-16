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

namespace Services.Tests.GenreServiceTest
{
    [TestFixture]
    public class GetById_Should
    {
        [Test]
        public void CallRepositoryGetById_WhenInvoked()
        {
            var repositoryMock = new Mock<IEfRepository<Genre>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var datetimeMock = new Mock<IDateTimeProvider>();
            var service = new GenreService(repositoryMock.Object, unitOfWorkMock.Object, datetimeMock.Object);
            var guid = new Guid("05fababb-f897-49ef-b42a-4943fcc38148");

            service.GetById(guid);

            repositoryMock.Verify(r => r.GetById(guid), Times.Once);
        }

        [Test]
        public void ReturnCorrectGenre_WhenInvoked()
        {
            var genre1 = new Genre("Rock");
            var guid = new Guid("05fababb-f897-49ef-b42a-4943fcc38148");
            var repositoryMock = new Mock<IEfRepository<Genre>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var datetimeMock = new Mock<IDateTimeProvider>();
            var service = new GenreService(repositoryMock.Object, unitOfWorkMock.Object, datetimeMock.Object);

            repositoryMock.Setup(r => r.GetById(guid)).Returns(genre1);

            var result = service.GetById(guid);

            Assert.AreEqual(genre1, result);

        }
    }
}
