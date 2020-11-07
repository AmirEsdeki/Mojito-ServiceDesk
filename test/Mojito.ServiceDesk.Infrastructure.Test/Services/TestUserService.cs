using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Mojito.ServiceDesk.Application.Common.DTOs.Identity.In;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.JWTService;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.SendMessagesService;
using Mojito.ServiceDesk.Core.Entities.Identity;
using Mojito.ServiceDesk.Infrastructure.Data.EF;
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
        private readonly Mock<IJwtService> jwtService;
        private readonly Mock<ApplicationDBContext> db;
        private readonly Mock<IMapper> mapper;

        public TestUserService()
        {
            this.userManager = new Mock<UserManager<User>>();
            this.signInManager = new Mock<SignInManager<User>>();
            this.emailService = new Mock<ISendEmailService>();
            this.jwtService = new Mock<IJwtService>();
            this.db = new Mock<ApplicationDBContext>();
            this.mapper = new Mock<IMapper>();
        }
        #endregion

        [Theory]
        [ClassData(typeof(ShouldSignUpUserTestData))]
        public void ShouldSignUpUser(SignUpDTO arg)
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
                jwtService.Object,
                db.Object,
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
                new SignUpDTO { Username = "SomeOne", Password = "someonepass", ConfirmPassword = "someonepass", Email = "someone@gmail.com" },
                new SignUpDTO { Username = "SomeOne", Password = "someonepass", ConfirmPassword = "someonepass", Email = "someone@gmail.com" }
            }
        };

        IEnumerator<object[]> IEnumerable<object[]>.GetEnumerator() => _testData.GetEnumerator();
        public IEnumerator GetEnumerator() => _testData.GetEnumerator();

    }
}
