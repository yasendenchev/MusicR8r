using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using MusicR8r.Services.Contracts;
using MusicR8r.Areas.Admin.Controllers;
using MusicR8r.Services.Providers;

namespace Controllers.Tests
{
    [TestFixture]
    public class AlbumsController_Should
    {

        [Test]
        public void ThrowArgumentNullException_WhenServiceIsNull()
        {
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();

                Assert.Throws<ArgumentNullException>(() => new AlbumsController(null, mapperMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenMapperIsNull()
        {
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();

            Assert.Throws<ArgumentNullException>(() => new AlbumsController(albumServiceMock.Object,null));
        }

        [Test]
        public void NotThrow_WhenDependenciesAreNotNull()
        {
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();
            Assert.DoesNotThrow(() => new AlbumsController(albumServiceMock.Object, mapperMock.Object));
        }

        [Test]
        public void InitializeAlbumsControllerCorrectly_WhenDependenciesAreNotNull()
        {
            var mapperMock = new Mock<IMapper>();
            var albumServiceMock = new Mock<IAlbumService>();

            var controller = new AlbumsController(albumServiceMock.Object, mapperMock.Object);

            Assert.IsNotNull(controller);
        }
    }
}
