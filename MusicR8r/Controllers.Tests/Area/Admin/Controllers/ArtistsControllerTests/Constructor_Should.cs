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
using MusicR8r.Contracts.Services;

namespace Controllers.Tests
{
    [TestFixture]
    public class ArtistsController_Should
    {

        [Test]
        public void ThrowArgumentNullException_WhenServiceIsNull()
        {
            var mapperMock = new Mock<IMapper>();
            var artistServiceMock = new Mock<IArtistService>();

                Assert.Throws<ArgumentNullException>(() => new ArtistsController(null, mapperMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenMapperNull()
        {
            var mapperMock = new Mock<IMapper>();
            var artistServiceMock = new Mock<IArtistService>();

            Assert.Throws<ArgumentNullException>(() => new ArtistsController(artistServiceMock.Object,null));
        }

        [Test]
        public void NotThrow_WhenDependenciesAreNotNull()
        {
            var mapperMock = new Mock<IMapper>();
            var artistServiceMock = new Mock<IArtistService>();
            Assert.DoesNotThrow(() => new ArtistsController(artistServiceMock.Object, mapperMock.Object));
        }

        [Test]
        public void InitializeArtistsControllerCorrectly_WhenDependenciesAreNotNull()
        {
            var mapperMock = new Mock<IMapper>();
            var artistServiceMock = new Mock<IArtistService>();

            var controller = new ArtistsController(artistServiceMock.Object, mapperMock.Object);

            Assert.IsNotNull(controller);
        }
    }
}
