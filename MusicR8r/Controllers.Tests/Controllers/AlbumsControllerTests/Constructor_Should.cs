using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using MusicR8r.Services.Contracts;
using MusicR8r.Controllers;
using MusicR8r.Services.Providers;

namespace Controllers.Tests
{
    [TestFixture]
    public class AlbumController_Should
    {

        [Test]
        public void ThrowArgumentsNullException_WhenServiceIsNull()
        {
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();

            Assert.Throws<ArgumentNullException>(() => new AlbumsController(null, mapperMock.Object));
        }

        [Test]
        public void ThrowArgumentsNullException_WhenMapperIsNull()
        {
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();

            Assert.Throws<ArgumentNullException>(() => new AlbumsController(albumServiceMock.Object, null));
        }

        [Test]
        public void NotThrow_WhenDependenciesAreNotNulll()
        {
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();
            Assert.DoesNotThrow(() => new AlbumsController(albumServiceMock.Object, mapperMock.Object));
        }

        [Test]
        public void InitializeAlbumsConttrollerCorrectly_WhenDependenciesAreNotNull()
        {
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();

            var controller = new AlbumsController(albumServiceMock.Object, mapperMock.Object);

            Assert.IsNotNull(controller);
        }
    }
}
