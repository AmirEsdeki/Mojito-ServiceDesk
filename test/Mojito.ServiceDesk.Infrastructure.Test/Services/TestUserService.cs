using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Mojito.ServiceDesk.Application.Common.DTOs.Identity;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.Common;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.SendMessagesService;
using Mojito.ServiceDesk.Core.Entities.Identity;
using Mojito.ServiceDesk.Infrastructure.Services.UserService;
using Moq;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Mojito.ServiceDesk.Infrastructure.Test.Services
{
    public class TestUserService
    {

        #region Ctor
        private readonly Mock<UserManager<User>> userManager;
        private readonly Mock<SignInManager<User>> signInManager;
        private readonly Mock<ISendEmailService> emailService;
        private readonly Mock<IRandomService> randomService;
        private readonly Mock<IMapper> mapper;

        public TestUserService()
        {
            this.userManager = new Mock<UserManager<User>>();
            this.signInManager = new Mock<SignInManager<User>>();
            this.emailService = new Mock<ISendEmailService>();
            this.randomService = new Mock<IRandomService>();
            this.mapper = new Mock<IMapper>();
        }
        #endregion

        [Theory]
        [ClassData(typeof(ShouldSignUpUserTestData))]
        public void ShouldSignUpUser(SignupDTO arg)
        {
            var emailSent = false;
            var identiyResult = new Mock<IdentityResult>();


            identiyResult.Setup(s => s.Succeeded).Returns(true);

            emailService.Setup(s => s.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), false)).Callback(() =>
            {
                emailSent = true;
            });

            userManager
                .Setup(s => s.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(identiyResult.Object);


            var service = new UserService(userManager.Object,
                signInManager.Object,
                emailService.Object,
                randomService.Object,
                mapper.Object);
            Assert.True(emailSent);
        }

    }
    class ShouldSignUpUserTestData : IEnumerable<object[]>
    {
        private readonly List<object[]> _testData = new List<object[]>
        {
            new object[]
            {
                new SignupDTO { Username = "SomeOne", Password = "someonepass", ConfirmPassword = "someonepass", Email = "someone@gmail.com" },
                new SignupDTO { Username = "SomeOne", Password = "someonepass", ConfirmPassword = "someonepass", Email = "someone@gmail.com" }
            }
        };

        IEnumerator<object[]> IEnumerable<object[]>.GetEnumerator() => _testData.GetEnumerator();
        public IEnumerator GetEnumerator() => _testData.GetEnumerator();

    }
}
