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

namespace Services.Tests.UserServiceTest
{
    [TestFixture]
    public class GetById_Should
    {
        Mock<IEfRepository<User>> userRepositoryMock;
        Mock<IEfRepository<Album>> albumRepositoryMock;
        Mock<IUnitOfWork> unitOfWorkMock;
        Mock<IDateTimeProvider> dateTimeProviderMock;
        UserService service;

        [SetUp]
        public void Init()
        {
            userRepositoryMock = new Mock<IEfRepository<User>>();
            albumRepositoryMock = new Mock<IEfRepository<Album>>();
            unitOfWorkMock = new Mock<IUnitOfWork>();
            dateTimeProviderMock = new Mock<IDateTimeProvider>();


            service = new UserService(userRepositoryMock.Object, albumRepositoryMock.Object, unitOfWorkMock.Object, dateTimeProviderMock.Object);
        }

        [Test]
        public void CallRepositoryAll_WhenInvoked()
        {
            var user = new User();
            

            Guid guid = Guid.NewGuid();
            service.GetUserById(guid);


            userRepositoryMock.Verify(r => r.GetById(guid.ToString()), Times.Once);
        }
    }
}
