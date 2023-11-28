using Microsoft.EntityFrameworkCore;
using Moq;
using MyFirstWebApi.Conctext;
using MyFirstWebApi.Controllers;
using System.Linq.Expressions;
using User = MyFirstWebApi.Entities.User;

namespace MyFirstUnitTest;

public class AuthControllerUnitTest
{
    //[Fact]
    //public async Task Login_UserNotFound_ThrowException()
    //{
    //    var controller = CreateControllerWithMockedDbSet(null);
    //    await Assert.ThrowsAsync<Exception>(() => controller.Login(new LoginDto("test"), CancellationToken.None));
    //}
    //private AuthController CreateControllerWithMockedDbSet(User user)
    //{
    //    Mock<IAppDbContext> dbContextMock = new Mock<IAppDbContext>(); ;
    //    var dbSetMock = new Mock<DbSet<User>>();

    //    dbContextMock
    //        .Setup(x => x.Users)
    //        .Returns(dbSetMock.Object);

    //    dbSetMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);

    //    return new AuthController(dbContextMock.Object);
    //}
}
