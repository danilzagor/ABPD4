using System;
using JetBrains.Annotations;
using LegacyApp;
using Xunit;

namespace LegacyApp.Tests;

[TestSubject(typeof(UserService))]
public class UserServiceTest
{
    private readonly UserService _userService;
    private readonly UserTest _userTest;

    public UserServiceTest()
    {
        _userService = new UserService();
        _userTest = new UserTest
        {
            FirstName = "Joe", LastName = "Doe", EmailAddress = "johndoe@gmail.com",
            DateOfBirth = DateTime.Parse("1999-01-23"), ClientId = 1
        };
    }

    [Fact]
    public void AddUser_Should_Return_False_When_FirstName_Is_Incorrect()
    {
        var addResult = _userService.AddUser("", _userTest.LastName, _userTest.EmailAddress, _userTest.DateOfBirth,
            _userTest.ClientId);
        Assert.False(addResult);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_SecondName_Is_Incorrect()
    {
        var addResult = _userService.AddUser(_userTest.FirstName, "", _userTest.EmailAddress, _userTest.DateOfBirth,
            _userTest.ClientId);
        Assert.False(addResult);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Email_Is_Incorrect()
    {
        _userTest.EmailAddress = "jogndoegmailcom";
        var addResult = _userService.AddUser(_userTest.FirstName, _userTest.LastName, _userTest.EmailAddress,
            _userTest.DateOfBirth,
            _userTest.ClientId);
        Assert.False(addResult);
    }

    [Fact]
    public void AddUser_Should_Throw_ArgumentException_When_Client_Id_Doesnt_Exist()
    {
        _userTest.ClientId = -1;
        Assert.Throws<ArgumentException>(() =>
        {
            _userService.AddUser(_userTest.FirstName, _userTest.LastName, _userTest.EmailAddress, _userTest.DateOfBirth,
                _userTest.ClientId);
        });
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Age_Is_Less_Then_21()
    {
        _userTest.DateOfBirth = DateTime.Parse("2009-04-12");
        var addResult = _userService.AddUser(_userTest.FirstName, _userTest.LastName, _userTest.EmailAddress,
            _userTest.DateOfBirth,
            _userTest.ClientId);
        Assert.False(addResult);
    }
    [Fact]
    public void AddUser_Should_Return_False_When_CreditLimit_Is_Less_Then_500()
    {
        _userTest.LastName = "Kowalski";
        _userTest.EmailAddress = "kowalski@wp.pl";
        var addResult = _userService.AddUser(_userTest.FirstName, _userTest.LastName, _userTest.EmailAddress,
            _userTest.DateOfBirth,
            _userTest.ClientId);
        Assert.False(addResult);
    }
    [Fact]
    public void AddUser_Should_Return_True_When_Client_Is_Very_Important_And_Everything_Is_Correct()
    {
        _userTest.ClientId = 2;
        _userTest.LastName = "Malewski";
        var addResult = _userService.AddUser(_userTest.FirstName, _userTest.LastName, _userTest.EmailAddress,
            _userTest.DateOfBirth,
            _userTest.ClientId);
        Assert.True(addResult);
    }
    [Fact]
    public void AddUser_Should_Return_True_When_Client_Is_Important_And_Everything_Is_Correct()
    {
        _userTest.ClientId = 4;
        var addResult = _userService.AddUser(_userTest.FirstName, _userTest.LastName, _userTest.EmailAddress,
            _userTest.DateOfBirth,
            _userTest.ClientId);
        Assert.True(addResult);
    }
    [Fact]
    public void AddUser_Should_Throw_ArgumentException_When_Client_Last_Name_Doesnt_Exist()
    {
        _userTest.LastName = "Boe";
        Assert.Throws<ArgumentException>(() =>
        {
            _userService.AddUser(_userTest.FirstName, _userTest.LastName, _userTest.EmailAddress, _userTest.DateOfBirth,
                _userTest.ClientId);
        });
    }
}