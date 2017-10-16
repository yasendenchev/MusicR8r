using System;
using Moq;
using NUnit.Framework;
using MusicR8r.Data.Repositories;
using MusicR8r.Data.Model.Models;
using MusicR8r.Data.UnitOfWork;
using MusicR8r.Services;
using MusicR8r.Services.Providers;

namespace Services.Tests.ArtistServiceTests
{
    [TestFixture]
    class GetById_Should
    {
        Mock<IEfRepository<Album>> albumRepositoryMock;
        Mock<IEfRepository<Artist>> artistRepositoryMock;
        Mock<IUnitOfWork> unitOfWorkMock;
        Mock<IDateTimeProvider> dateTimeMock;
        Mock<IAlbumService> albumServiceMock;
        ArtistService service;

        [SetUp]
        public void Setup()
        {

            artistRepositoryMock = new Mock<IEfRepository<Artist>>();
            unitOfWorkMock = new Mock<IUnitOfWork>();
            dateTimeMock = new Mock<IDateTimeProvider>();
            albumServiceMock = new Mock<IAlbumService>();

            service = new ArtistService(artistRepositoryMock.Object, unitOfWorkMock.Object, dateTimeMock.Object, albumServiceMock.Object);
        }
        [Test]
        public void CallRepositoryGetById_WhenInvoked()
        {
           
            var guid = new Guid();

            service.GetById(guid);

            artistRepositoryMock.Verify(r => r.GetById(guid), Times.Once);
        }
    }
}
