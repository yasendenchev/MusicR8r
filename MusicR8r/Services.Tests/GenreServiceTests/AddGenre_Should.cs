using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using MusicR8r.Data.Repositories;
using MusicR8r.Data.Model.Models;
using MusicR8r.Data.SaveContext;
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
            var saveContextMock = new Mock<ISaveContext>();
            var datetimeMock = new Mock<IDateTimeProvider>();
            var service = new GenreService(repositoryMock.Object, saveContextMock.Object, datetimeMock.Object);
            string name = "Rock";
            //Genre genre = new Genre(name);

            service.AddGenre(name);

            repositoryMock.Verify(r => r.Add(It.IsAny<Genre>()), Times.Once);
        }

        [Test]
        public void CallSaveContextCommit_WhenInvoked()
        {
            var repositoryMock = new Mock<IEfRepository<Genre>>();
            var saveContextMock = new Mock<ISaveContext>();
            var datetimeMock = new Mock<IDateTimeProvider>();
            var service = new GenreService(repositoryMock.Object, saveContextMock.Object, datetimeMock.Object);
            string name = "Rock";
            //Genre genre = new Genre(name);

            service.AddGenre(name);

            saveContextMock.Verify(s => s.Commit(), Times.Once);
        }
    }
}
