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
    class GetByArtist_Should
    {
        Mock<IEfRepository<Album>> repositoryMock;
        Mock<IEfRepository<Artist>> artistRepositoryMock;
        Mock<IUnitOfWork> unitOfWorkMock;
        Mock<IDateTimeProvider> dateTimeMock;
        AlbumService service;
        Guid guid;
        Album album;
        Album album2;
        Guid artistId;
        Artist artist;

        [SetUp]
        public void Setup()
        {
            repositoryMock = new Mock<IEfRepository<Album>>();
            artistRepositoryMock = new Mock<IEfRepository<Artist>>();
            unitOfWorkMock = new Mock<IUnitOfWork>();
            dateTimeMock = new Mock<IDateTimeProvider>();
            guid = Guid.NewGuid();
            artistId = Guid.NewGuid();
            album = new Album();
            album2 = new Album();
            artist = new Artist();
            artist.Id = artistId;
            album.Artist = artist;
            artist.Albums = new List<Album>();
            artist.Albums.Add(album);
            artist.Albums.Add(album2);
            
            service = new AlbumService(repositoryMock.Object, artistRepositoryMock.Object, unitOfWorkMock.Object, dateTimeMock.Object);
        }

        [Test]
        public void CallsRepositoryAll_WhenInvoked()
        {
            service.GetByArtist(artistId);

            repositoryMock.Verify(x => x.All, Times.Once);
        }
    }
}
