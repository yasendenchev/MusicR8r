using Moq;
using MusicR8r.Data.Model.Models;
using MusicR8r.Data.Repositories;
using MusicR8r.Data.UnitOfWork;
using MusicR8r.Services;
using MusicR8r.Services.Providers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Tests.UserServiceTest
{
    [TestFixture]
    public class UserContstructor_Should
    {

        Mock<IEfRepository<User>> userRepositoryMock;
        Mock<IEfRepository<Album>> albumRepositoryMock;
        Mock<IUnitOfWork> unitOfWorkMock;
        Mock<IDateTimeProvider> dateTimeProviderMock;


        [SetUp]
        public void Init()
        {
            userRepositoryMock = new Mock<IEfRepository<User>>();
            albumRepositoryMock = new Mock<IEfRepository<Album>>();
            unitOfWorkMock = new Mock<IUnitOfWork>();
            dateTimeProviderMock = new Mock<IDateTimeProvider>();

            var service = new UserService(userRepositoryMock.Object, albumRepositoryMock.Object, unitOfWorkMock.Object, dateTimeProviderMock.Object);
        }


        [Test]
        public void ThrowArgumentNullException_WhenTheFirstParameterIsNull()
        {
            var service = new UserService(userRepositoryMock.Object, albumRepositoryMock.Object, unitOfWorkMock.Object, dateTimeProviderMock.Object);
        }

        [Test]
        public void ThrowArgumentNullException_WhenTheSecondParameterIsNull()
        {
            var service = new UserService(userRepositoryMock.Object, albumRepositoryMock.Object, unitOfWorkMock.Object, dateTimeProviderMock.Object);
        }

        [Test]
        public void ThrowArgumentNullException_WhenTheThirdParameterIsNull()
        {
            var service = new UserService(userRepositoryMock.Object, albumRepositoryMock.Object, unitOfWorkMock.Object, dateTimeProviderMock.Object);
        }

        [Test]
        public void ThrowArgumentNullException_WhenTheFourthParameterIsNull()
        {
            var service = new UserService(userRepositoryMock.Object, albumRepositoryMock.Object, unitOfWorkMock.Object, dateTimeProviderMock.Object);
        }
    }
}
