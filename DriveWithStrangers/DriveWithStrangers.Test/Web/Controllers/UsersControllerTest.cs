namespace DriveWithStrangers.Test.Web.Controllers
{
    using Data.Models;
    using DriveWithStrangers.Services;
    using DriveWithStrangers.Services.Models.Users;
    using DriveWithStrangers.Web.Controllers;
    using FluentAssertions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System.Linq;
    using Xunit;

    public class UsersControllerTest
    {
        [Fact]
        public void YourProfileShouldBeOnlyForAuthorizedUsers()
        {
            // Arrange
            var method = typeof(UsersController)
                .GetMethod(nameof(UsersController.YourProfile));

            // Act
            var attributes = method.GetCustomAttributes(true);

            // Assert
            attributes
                .Should()
                .Match(attr => attr.Any(a => a.GetType() == typeof(AuthorizeAttribute)));
        }

        [Fact]
        public void ProfileShouldReturnNotFoundWithInvalidUsername()
        {
            // Arrange
            var userManager = this.GetUserManagerMock();

            var controller = new UsersController(userManager.Object, null);

            // Act
            var result = controller.Profile("someId");

            // Assert
            result
                .Should()
                .BeOfType<NotFoundResult>();
        }

        [Fact]
        public void EditShouldReturnViewWithCorrectModelWithValidUsername()
        {
            // Arrange
            const string userId = "SomeId";
            const string username = "SomeUsername";
            const int userAge = 23;
            const string userFullName = "Some Full Name";
            const string userEmail = "someEmail@mail.mail0";

            var userManager = this.GetUserManagerMock();
            userManager
                .Setup(u => u.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new User { Id = userId });

            var userService = new Mock<IUserService>();
            userService
                .Setup(u => u.UserDetailsById(It.Is<string>(id => id == userId)))
                .Returns(new UserEditServiceModel { Username = username, UserAge = userAge, UserFullName = userFullName, Email = userEmail});

            var controller = new UsersController(
               userManager.Object, userService.Object);

            // Act
            var result = controller.Edit(userId);

            // Assert
            result
                .Should()
                .BeOfType<ViewResult>()
                .Subject
                .Model
                .Should()
                .Match(m => m.As<UserEditServiceModel>().Username == username);
        }

        private Mock<UserManager<User>> GetUserManagerMock()
            => new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
    }
}
