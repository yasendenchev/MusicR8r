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
    public class AddGenre_Should
    {
        [Test]
        public void CallRepositoryAdd_WhenInvoked()
        {
            var repositoryMock = new Mock<IEfRepository<Genre>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var datetimeMock = new Mock<IDateTimeProvider>();
            var service = new GenreService(repositoryMock.Object, unitOfWorkMock.Object, datetimeMock.Object);
            string name = "Rock";
            //Genre genre = new Genre(name);

            service.AddGenre(name);

            repositoryMock.Verify(r => r.Add(It.IsAny<Genre>()), Times.Once);
        }

        [Test]
        public void CallUnitOfWorkCommit_WhenInvoked()
        {
            var repositoryMock = new Mock<IEfRepository<Genre>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var datetimeMock = new Mock<IDateTimeProvider>();
            var service = new GenreService(repositoryMock.Object, unitOfWorkMock.Object, datetimeMock.Object);
            string name = "Rock";
            //Genre genre = new Genre(name);

            service.AddGenre(name);

            unitOfWorkMock.Verify(s => s.Commit(), Times.Once);
        }
    }
}
