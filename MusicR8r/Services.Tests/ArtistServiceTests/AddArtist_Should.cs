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

namespace Services.Tests.ArtistServiceTests
{
    [TestFixture]
    class AddArtist_Should
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

            service.AddArtist("ivan", "bulgaria", "bio");
        }

        [Test]
        public void CallArtistRepositoryAdd_WhenInvoked()
        {
            artistRepositoryMock.Verify(x => x.Add(It.IsAny<Artist>()), Times.Once);
        }

        [Test]
        public void CallUnitOfWorkCommit_WhenInvoked()
        {
            unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
        }
    }
}
