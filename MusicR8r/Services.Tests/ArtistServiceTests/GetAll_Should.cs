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
    class GetAll_Should
    {
        Mock<IEfRepository<Album>> albumRepositoryMock;
        Mock<IEfRepository<Artist>> artistRepositoryMock;
        Mock<IUnitOfWork> unitOfWorkMock;
        Mock<IDateTimeProvider> dateTimeMock;
        Mock<IAlbumService> albumServiceMock;
        ArtistService service;
        Guid guid;
        Artist artist1;
        Album album1;
        Song song1;
        DateTime dt;

        [SetUp]
        public void Setup()
        {

            artistRepositoryMock = new Mock<IEfRepository<Artist>>();
            unitOfWorkMock = new Mock<IUnitOfWork>();
            dateTimeMock = new Mock<IDateTimeProvider>();
            albumServiceMock = new Mock<IAlbumService>();

            dt = DateTime.Now;
            guid = Guid.NewGuid();
            song1 = new Song();
            album1 = new Album();
            album1.Songs.Add(song1);
            artist1 = new Artist();
            artist1.Albums.Add(album1);

            service = new ArtistService(artistRepositoryMock.Object, unitOfWorkMock.Object, dateTimeMock.Object, albumServiceMock.Object);
        }

        [Test]
        public void CallRepositoryAll_WhenInvoked()
        {
            service.GetAll();

            artistRepositoryMock.Verify(r => r.All, Times.Once);
        }

       
    }
}
