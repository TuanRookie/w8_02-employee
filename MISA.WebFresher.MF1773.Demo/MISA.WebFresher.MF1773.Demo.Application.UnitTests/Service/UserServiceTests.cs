using AutoMapper;
using MISA.WebFresher.MF1773.Demo.Application.Service;
using MISA.WebFresher.MF1773.Demo.Domain;
using MISA.WebFresher.MF1773.Demo.Domain.Repository;
using MISA.WebFresher.MF1773.Demo.Infractructure;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Application
{
    [TestFixture]
    public class UserServiceTests
    {
        public UserService? UserService { get; set; }

        public IUserRepository? UserRepository { get; set; }

        [SetUp]
        public void SetUp()
        {
            UserRepository = Substitute.For<IUserRepository>();
            UserService = Substitute.For<UserService>(UserRepository);
        }
        /// <summary>
        /// Test ham LoginAsync thành công
        /// </summary>
        /// <returns></returns>
        ///  CreatedBy: DCTuan 06/03/2024 
        [Test]
        public async Task LoginAsync_Success_ReturnUser()
        {
            //Arrange
            User user = new User();
            UserLogin userLogin = new UserLogin();
            UserRepository.GetUser(userLogin).Returns(user);

            //Act
            var result = await UserService.LoginAsync(userLogin);

            //Assert
            await UserRepository.Received(1).GetUser(userLogin);
            Assert.That(result, Is.Not.EqualTo(null));

        }
        /// <summary>
        /// Test hàm LoginAsync khong có tài khoản, trả về Exception
        /// </summary>
        /// <returns></returns>
        ///  CreatedBy: DCTuan 06/03/2024  
        [Test]
        public async Task LoginAsync_EmptyUser_ReturnValidateException()
        {
            //Arrange
            UserLogin userLogin = new UserLogin();
            UserRepository.GetUser(userLogin).Returns((User)null);

            //Act && Assert
            var exception = Assert.ThrowsAsync<ValidateException>(async () => await UserService.LoginAsync(userLogin));
            Assert.That(exception.ErrorCode, Is.EqualTo(400));
            await UserRepository.Received(1).GetUser(userLogin);
        }
        /// <summary>
        /// Test ham UpdateServiceAsync trả về true
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task UpdateServiceAsync_Success_ReturnTrue()
        {

            //Arrange
            var user = new User();
            var userId = new Guid();
            UserRepository.UpdateAsync(user, userId).Returns(true);

            //Act
            UserService.UpdateServiceAsync(user, userId);

            //Assert
            await UserRepository.Received(1).UpdateAsync(user, userId);

        }
    }
}
