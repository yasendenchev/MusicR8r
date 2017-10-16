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
    public class Update_Should
    {
        Mock<IEfRepository<User>> userRepositoryMock;
        Mock<IEfRepository<Album>> albumRepositoryMock;
        Mock<IUnitOfWork> unitOfWorkMock;
        Mock<IDateTimeProvider> dateTimeProviderMock;
        UserService service;

        Guid guid;
        string email;
        string username;
        string firstName;
        string lastName;
        string bio;

        [SetUp]
        public void Init()
        {
            userRepositoryMock = new Mock<IEfRepository<User>>();
            albumRepositoryMock = new Mock<IEfRepository<Album>>();
            unitOfWorkMock = new Mock<IUnitOfWork>();
            dateTimeProviderMock = new Mock<IDateTimeProvider>();

            guid = Guid.NewGuid();
            email = "asd@mail.bg";
            username = "Ivan97";
            firstName = "Ivan";
            lastName = "Ivanov";
            bio = "biobio";
            
            var user = new User(guid.ToString(), username, firstName, lastName);

            userRepositoryMock.Setup(x => x.GetById(It.IsAny<String>())).Returns(user);

            service = new UserService(userRepositoryMock.Object, albumRepositoryMock.Object, unitOfWorkMock.Object, dateTimeProviderMock.Object);

            
        }

        [Test]
        public void CallRepositoryGetById_WhenInvoked()
        {
            

            service.Update(guid, email, username, firstName, lastName, bio);


            userRepositoryMock.Verify(r => r.GetById(guid.ToString()), Times.Once);
        }

        [Test]
        public void CallRepositoryUpdate_WhenInvoked()
        {

            Guid guid = Guid.NewGuid();
            string email = "asd@mail.bg";
            string username = "Ivan97";
            string firstName = "Ivan";
            string lastName = "Ivanov";
            string bio = "biobio";

            service.Update(guid, email, username, firstName, lastName, bio); ;


            userRepositoryMock.Verify(r => r.Update(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void CallUnitOfWorkCommit_WhenInvoked()
        {

            Guid guid = Guid.NewGuid();
            string email = "asd@mail.bg";
            string username = "Ivan97";
            string firstName = "Ivan";
            string lastName = "Ivanov";
            string bio = "biobio";

            service.Update(guid, email, username, firstName, lastName, bio);


            unitOfWorkMock.Verify(r => r.Commit(), Times.Once);
        }
    }
}
