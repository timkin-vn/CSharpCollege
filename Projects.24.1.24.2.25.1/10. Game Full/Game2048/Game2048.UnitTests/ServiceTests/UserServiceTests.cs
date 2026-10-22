using Game2048.Business.Services;
using Game2048.Common.BusinessModels;
using Game2048.Common.Contracts.Repositories;
using Moq;

namespace Game2048.UnitTests.ServiceTests;

[TestClass]
public class UserServiceTests
{
    private readonly Mock<IUserRepository> _repoMock = new();
    private readonly UserService _service;

    public UserServiceTests()
    {
        _service = new UserService(_repoMock.Object);
    }

    [TestMethod]
    public void GetOrCreateUser_ExistingUser_ReturnsExisting()
    {
        var existing = new UserModel { Id = 1, Name = "Alice" };
        _repoMock.Setup(r => r.GetByName("Alice")).Returns(existing);

        var result = _service.GetOrCreateUser("Alice");

        Assert.AreEqual(1, result.Id);
        _repoMock.Verify(r => r.Create(It.IsAny<string>()), Times.Never);
    }

    [TestMethod]
    public void GetOrCreateUser_NewUser_CreatesUser()
    {
        _repoMock.Setup(r => r.GetByName("Bob")).Returns((UserModel?)null);
        _repoMock.Setup(r => r.Create("Bob")).Returns(new UserModel { Id = 2, Name = "Bob" });

        var result = _service.GetOrCreateUser("Bob");

        Assert.AreEqual(2, result.Id);
        Assert.AreEqual("Bob", result.Name);
        _repoMock.Verify(r => r.Create("Bob"), Times.Once);
    }

    [TestMethod]
    public void GetAllUsers_ReturnsAllUsers()
    {
        _repoMock.Setup(r => r.GetAll()).Returns(new List<UserModel>
        {
            new() { Id = 1, Name = "Alice" },
            new() { Id = 2, Name = "Bob" },
        });

        var result = _service.GetAllUsers().ToList();

        Assert.AreEqual(2, result.Count);
    }
}
