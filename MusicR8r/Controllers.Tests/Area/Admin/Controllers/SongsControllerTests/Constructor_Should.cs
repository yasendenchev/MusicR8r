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

namespace Controllers.Tests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenServiceIsNull()
        {
            var mapperMock = new Mock<IMapper>();
            var songServiceMock = new Mock<ISongService>();
            var genreServiceMock = new Mock<IGenreService>();

        Assert.Throws<ArgumentNullException>(() => new SongsController(null, genreServiceMock.Object, mapperMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenMapperNull()
        {
            var mapperMock = new Mock<IMapper>();
            var songServiceMock = new Mock<ISongService>();
            var genreServiceMock = new Mock<IGenreService>();

            Assert.Throws<ArgumentNullException>(() => new SongsController(songServiceMock.Object, genreServiceMock.Object, null));
        }

        [Test]
        public void ThrowArgumentNullException_WhenGenresServiceIsNull()
        {
            var mapperMock = new Mock<IMapper>();
            var songServiceMock = new Mock<ISongService>();
            var genreServiceMock = new Mock<IGenreService>();

            Assert.Throws<ArgumentNullException>(() => new SongsController(songServiceMock.Object, null , mapperMock.Object));
        }

        [Test]
        public void NotThrow_WhenDependenciesAreNotNull()
        {
            var mapperMock = new Mock<IMapper>();
            var songServiceMock = new Mock<ISongService>();
            var genreServiceMock = new Mock<IGenreService>();

            Assert.DoesNotThrow(() => new SongsController(songServiceMock.Object, genreServiceMock.Object, mapperMock.Object));
        }

        [Test]
        public void InitializeSongsControllerCorrectly_WhenDependenciesAreNotNull()
        {
            var mapperMock = new Mock<IMapper>();
            var songServiceMock = new Mock<ISongService>();
            var genreServiceMock = new Mock<IGenreService>();

            var controller = new SongsController(songServiceMock.Object, genreServiceMock.Object, mapperMock.Object);

            Assert.IsNotNull(controller);
        }
    }
}
