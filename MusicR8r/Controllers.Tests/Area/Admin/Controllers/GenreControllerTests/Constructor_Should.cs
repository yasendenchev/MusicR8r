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
    public class GenresController_Should
    {

        [Test]
        public void ThrowArgumentNullException_WhenServiceIsNull()
        {
            var mapperMock = new Mock<IMapper>();
            var genreServiceMock = new Mock<IGenreService>();

            Assert.Throws<ArgumentNullException>(() => new GenresController(null, mapperMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenMapperIsNull()
        {
            var mapperMock = new Mock<IMapper>();
            var genreServiceMock = new Mock<IGenreService>();

            Assert.Throws<ArgumentNullException>(() => new GenresController(genreServiceMock.Object, null));
        }

        [Test]
        public void NotThrow_WhenDependenciesAreNotNull()
        {
            var mapperMock = new Mock<IMapper>();
            var genreServiceMock = new Mock<IGenreService>();
            Assert.DoesNotThrow(() => new GenresController(genreServiceMock.Object, mapperMock.Object));
        }

        [Test]
        public void InitializeGenresControllerCorrectly_WhenDependenciesAreNotNull()
        {
            var mapperMock = new Mock<IMapper>();
            var genreServiceMock = new Mock<IGenreService>();

            var controller = new GenresController(genreServiceMock.Object, mapperMock.Object);

            Assert.IsNotNull(controller);
        }
    }
}
