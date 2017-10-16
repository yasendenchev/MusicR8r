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

namespace Services.Tests.AlbumServiceTests
{
    [TestFixture]
    class GetById_Should
    {
        [Test]
        public void CallRepositoryGetById_WhenInvoked()
        {
            var repositoryMock = new Mock<IEfRepository<Album>>();
            var artistRepositoryMock = new Mock<IEfRepository<Artist>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var datetimeMock = new Mock<IDateTimeProvider>();
            var service = new AlbumService(repositoryMock.Object, artistRepositoryMock.Object, unitOfWorkMock.Object, datetimeMock.Object);
            var guid = new Guid("05fababb-f897-49ef-b42a-4943fcc38148");

            service.GetById(guid);

            repositoryMock.Verify(r => r.GetById(guid), Times.Once);
        }
    }
}
