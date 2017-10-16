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

namespace Controllers.Tests
{
    [TestFixture]
    public class ProfileControllerConstructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenServiceIsNull()
        {
            var mapperMock = new Mock<IMapper>();
            var userServiceMock = new Mock<IUserService>();
            var genreServiceMock = new Mock<IGenreService>();

        Assert.Throws<ArgumentNullException>(() => new ProfileController(null, mapperMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenMapperNull()
        {
            var mapperMock = new Mock<IMapper>();
            var userServiceMock = new Mock<IUserService>();
            var genreServiceMock = new Mock<IGenreService>();

            Assert.Throws<ArgumentNullException>(() => new ProfileController(userServiceMock.Object, null));
        }

        //[Test]
        //public void ThrowArgumentNullException_WhenGenresServiceIsNull()
        //{
        //    var mapperMock = new Mock<IMapper>();
        //    var userServiceMock = new Mock<IUserService>();
        //    var genreServiceMock = new Mock<IGenreService>();

        //    Assert.Throws<ArgumentNullException>(() => new ProfileController(userServiceMock.Object, null , mapperMock.Object));
        //}

        [Test]
        public void NotThrow_WhenDependenciesAreNotNull()
        {
            var mapperMock = new Mock<IMapper>();
            var userServiceMock = new Mock<IUserService>();
            var genreServiceMock = new Mock<IGenreService>();

            Assert.DoesNotThrow(() => new ProfileController(userServiceMock.Object, mapperMock.Object));
        }

        [Test]
        public void InitializeProfileControllerCorrectly_WhenDependenciesAreNotNull()
        {
            var mapperMock = new Mock<IMapper>();
            var userServiceMock = new Mock<IUserService>();
            var genreServiceMock = new Mock<IGenreService>();

            var controller = new ProfileController(userServiceMock.Object, mapperMock.Object);

            Assert.IsNotNull(controller);
        }
    }
}
