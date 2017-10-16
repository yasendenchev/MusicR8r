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
    public class DeleteById_Should
    {
        [Test]
        public void ShouldCallGenreRepositoryGetById_WhenInvoked()
        {
            var repositoryMock = new Mock<IEfRepository<Genre>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var datetimeMock = new Mock<IDateTimeProvider>();
            var service = new GenreService(repositoryMock.Object, unitOfWorkMock.Object , datetimeMock.Object);
            var guid = new Guid("05fababb-f897-49ef-b42a-4943fcc38148");

            service.DeleteById(guid);

            repositoryMock.Verify(r => r.GetById(guid), Times.Once);
        }

        [Test]
        public void ShouldCallDateTimeProviderNow_WhenInvoked()
        {
            var repositoryMock = new Mock<IEfRepository<Genre>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var datetimeMock = new Mock<IDateTimeProvider>();
            var service = new GenreService(repositoryMock.Object, unitOfWorkMock.Object, datetimeMock.Object);
            var guid = new Guid("05fababb-f897-49ef-b42a-4943fcc38148");

            service.DeleteById(guid);

            repositoryMock.Verify(r => r.GetById(guid), Times.Once);
        }

        [Test]
        public void ShouldSetGenreIsDeletedToTrue_WhenGenreExists()
        {
            var repositoryMock = new Mock<IEfRepository<Genre>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var datetimeMock = new Mock<IDateTimeProvider>();
            var service = new GenreService(repositoryMock.Object, unitOfWorkMock.Object, datetimeMock.Object);
            var genre = new Genre("Rock");
            var guid = new Guid("05fababb-f897-49ef-b42a-4943fcc38148");
            var sampleDateTime = new DateTime(2017, 10, 22);
            
            datetimeMock.Setup(d => d.Now()).Returns(sampleDateTime);

            repositoryMock.Setup(r => r.GetById(guid)).Returns(genre);

            service.DeleteById(guid);

            Assert.That(genre.IsDeleted == true);
        }

        [Test]
        public void ShouldSetGenreDeletedOnToCurrentDate_WhenGenreExists()
        {
            var repositoryMock = new Mock<IEfRepository<Genre>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var datetimeMock = new Mock<IDateTimeProvider>();
            var service = new GenreService(repositoryMock.Object, unitOfWorkMock.Object, datetimeMock.Object);
            var genre = new Genre("Rock");
            var guid = new Guid("05fababb-f897-49ef-b42a-4943fcc38148");
            var currentDate = new DateTime(2017, 10, 22);

            datetimeMock.Setup(d => d.Now()).Returns(currentDate);

            repositoryMock.Setup(r => r.GetById(guid)).Returns(genre);

            service.DeleteById(guid);

            Assert.That(genre.DeletedOn == currentDate);
        }

        [Test]
        public void ShouldCallRepositoryUpdate_WhenInvoked()
        {
            var repositoryMock = new Mock<IEfRepository<Genre>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var datetimeMock = new Mock<IDateTimeProvider>();
            var service = new GenreService(repositoryMock.Object, unitOfWorkMock.Object, datetimeMock.Object);
            var genre = new Genre("Rock");
            var guid = new Guid("05fababb-f897-49ef-b42a-4943fcc38148");
            var sampleDateTime = new DateTime(2017, 10, 22);

            datetimeMock.Setup(d => d.Now()).Returns(sampleDateTime);

            repositoryMock.Setup(r => r.GetById(guid)).Returns(genre);

            service.DeleteById(guid);

            repositoryMock.Verify(x => x.Update(genre), Times.Once());
        }

        [Test]
        public void ShouldCallUnitOfWorkCommit_WhenInvoked()
        {
            var repositoryMock = new Mock<IEfRepository<Genre>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var datetimeMock = new Mock<IDateTimeProvider>();
            var service = new GenreService(repositoryMock.Object, unitOfWorkMock.Object, datetimeMock.Object);
            var genre = new Genre("Rock");
            var guid = new Guid("05fababb-f897-49ef-b42a-4943fcc38148");
            var sampleDateTime = new DateTime(2017, 10, 22);

            datetimeMock.Setup(d => d.Now()).Returns(sampleDateTime);

            repositoryMock.Setup(r => r.GetById(guid)).Returns(genre);

            service.DeleteById(guid);

            unitOfWorkMock.Verify(x => x.Commit(), Times.Once());
        }
    }
}
