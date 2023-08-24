
using Xunit;
using ALG.Data.Entities;
using ALG.Data.Services;

using Microsoft.EntityFrameworkCore;
using ALG.Data.Repositories;
using ALG.Data.Security;

namespace ALG.Test;
[Collection ("Sequential")]
public class ServiceTests
{
    private readonly IUserService service;

    public ServiceTests()
    {
        // configure the data context options to use sqlite for testing
        var options = DatabaseContext.OptionsBuilder                            
                        .UseSqlite("Filename=test.db")
                        //.LogTo(Console.WriteLine)
                        .Options;

        // create service with new context
        service = new UserServiceDb();
        service.Initialise();
    }

    [Fact]
    public void GetUsers_WhenNoneExist_ShouldReturnNone()
    {
        // act
        var users = service.GetUsers();
         // assert
        Assert.Equal(0, users.Count);
    }
        
    [Fact]
    public void AddUser_When2ValidUsersAdded_ShouldCreate2Users()
    {
        // arrange
         service.AddUser("admin", "admin@mail.com", "admin", Role.admin );
        service.AddUser("guest", "guest@mail.com", "guest", Role.guest);

        // act
        var users = service.GetUsers();

         // assert
        Assert.Equal(2, users.Count);
    }

    [Fact]
    public void GetPage1WithpageSize2_When3UsersExist_ShouldReturn2Pages()
    {
        // act
        service.AddUser("admin", "admin@mail.com", "admin", Role.admin );
        service.AddUser("manager", "manager@mail.com", "manager", Role.manager);
        service.AddUser("guest", "guest@mail.com", "guest", Role.guest);

        // return first page with 2 users per page
        var pagedUsers = service.GetUsers(1,2);

        // assert
        Assert.Equal(2, pagedUsers.TotalPages);
    }

    [Fact]
    public void GetPage1WithPageSize2_When3UsersExist_ShouldReturnPageWith2Users()
    {
        // act
        service.AddUser("admin", "admin@mail.com", "admin", Role.admin );
        service.AddUser("manager", "manager@mail.com", "manager", Role.manager);
        service.AddUser("guest", "guest@mail.com", "guest", Role.guest);

        var pagedUsers = service.GetUsers(1,2);

        // assert
        Assert.Equal(2, pagedUsers.Data.Count);
    }

    [Fact]
    public void GetPage1_When0UsersExist_ShouldReturn0Pages()
    {
        // act
        var pagedUsers = service.GetUsers(1,2);

        // assert
        Assert.Equal(0, pagedUsers.TotalPages);
        Assert.Equal(0, pagedUsers.TotalRows);
        Assert.Empty(pagedUsers.Data);
    }

    [Fact]
    public void UpdateUser_WhenUserExists_ShouldWork()
    {
        // arrange
        var user = service.AddUser("admin", "admin@mail.com", "admin", Role.admin );
            
        // act
        user.Name = "administrator";
        user.Email = "admin@mail.com";            
        var updatedUser = service.UpdateUser(user);

        // assert
        Assert.Equal("administrator", updatedUser.Name);
        Assert.Equal("admin@mail.com", updatedUser.Email);
    }

    [Fact]
    public void Login_WithValidCredentials_ShouldWork()
    {
        // arrange
        service.AddUser("admin", "admin@mail.com", "admin", Role.admin );
            
        // act            
        var user = service.Authenticate("admin@mail.com","admin");

        // assert
        Assert.NotNull(user);
           
    }

    [Fact]
    public void Login_WithInvalidCredentials_ShouldNotWork()
    {
        // arrange
        service.AddUser("admin", "admin@mail.com", "admin", Role.admin );

        // act      
        var user = service.Authenticate("admin@mail.com","xxx");

        // assert
        Assert.Null(user);
           
    }

    [Fact]
    public void ForgotPasswordRequest_ForValidUser_ShouldGenerateToken()
    {
         // arrange
        service.AddUser("admin", "admin@mail.com", "admin", Role.admin );

        // act      
        var token = service.ForgotPassword("admin@mail.com");

        // assert
        Assert.NotNull(token);
           
    }

    [Fact]
    public void ForgotPasswordRequest_ForInValidUser_ShouldReturnNull()
    {
       
        // act      
         var token = service.ForgotPassword("admin@mail.com");

         // assert
         Assert.Null(token);
           
    }
}

[Collection ("Sequential")]
public class AlgorithmServiceTests
{
    private readonly IAlgorithmService service;

    public AlgorithmServiceTests()
    {
        service = new AlgorithmService();
        service.Initialise();
    }

    [Fact]

    public void GetAllAlgoritihims_When0_ShouldReturn0()
    {
        var Algorithms = service.GetAlgorithms();
        var count = Algorithms.Count;
        Assert.Equal(0, count);
    }//get all Algorithms

    [Fact]
    public void AddAlgorithm_Return1()
    {
        var a1 = service.AddAlgorithm(
            new Algorithm { Name = "Rot13", Description = "rotation 13", Security = "bad", 
            SecurityRating = 1, Efficiency = "low", EfficiencyRating = 2, Speed = "average",
            SpeedRating = 3, DiagramUrl = "http://kerryb.github.io/enigma/images/components.png"
        });

        var Algorithms = service.GetAlgorithms();
        var count = Algorithms.Count;
        Assert.Equal(1, count);

    }

    [Fact]
    public void GetAlgorithms_Add2_ShouldReturn2()
    {
        //arranging database
        var a1 = service.AddAlgorithm(
            new Algorithm { Name = "Rot13", Description = "rotation 13", Security = "bad", 
            SecurityRating = 1, Efficiency = "low", EfficiencyRating = 2, Speed = "average",
            SpeedRating = 3, DiagramUrl = "http://kerryb.github.io/enigma/images/components.png"
        });
        var a2 = service.AddAlgorithm(
            new Algorithm { Name = "Blank", Description = "cipher", Security = "bad", 
            SecurityRating = 1, Efficiency = "low", EfficiencyRating = 2, Speed = "average",
            SpeedRating = 3, DiagramUrl = "http://kerryb.github.io/enigma/images/components.png"
        });
       
        //act
        var Algorithms = service.GetAlgorithms();
        var count = Algorithms.Count;
        Assert.Equal(2, count);
    }//if 2 count 2

    [Fact]
    public void GetAlgorithm_WhenNoneExist_ShouldReturnNull()
    {
        var Algorithm = service.GetAlgorithm(1);
        Assert.Null(Algorithm);
    }// if nonexistent return null

    [Fact]
    public void GetAlgorithm_WhenAdd_ShouldReturnAlgorithm()
    {
        var a = service.AddAlgorithm(
            new Algorithm { Name = "Rot13", Description = "rotation 13", Security = "bad", 
            SecurityRating = 1, Efficiency = "low", EfficiencyRating = 2, Speed = "average",
            SpeedRating = 3, DiagramUrl = "http://kerryb.github.io/enigma/images/components.png"
        });
        var na = service.GetAlgorithm(a.Id);
        Assert.NotNull(na);
        Assert.Equal(a.Id, na.Id);
    }// if addded, return

    [Fact]
    public void GetAlgorithmByName_WhenAdded_ShouldReturnData()
    {
        var a = service.AddAlgorithm(
            new Algorithm { Name = "Rot13", Description = "rotation 13", Security = "bad", 
            SecurityRating = 1, Efficiency = "low", EfficiencyRating = 2, Speed = "average",
            SpeedRating = 3, DiagramUrl = "http://kerryb.github.io/enigma/images/components.png"
        });
        var na = service.GetAlgorithmByName("Rot13");
        Assert.NotNull(na);
        Assert.Equal(a.Name, na.Name);
    }//get Algorithm by name 

    [Fact]
    public void AddAlgorithm_WhenValid_ShouldAdd()
    {
        var added = service.AddAlgorithm(
            new Algorithm { Name = "Rot13", Description = "rotation 13", Security = "bad", 
            SecurityRating = 1, Efficiency = "low", EfficiencyRating = 2, Speed = "average",
            SpeedRating = 3, DiagramUrl = "http://kerryb.github.io/enigma/images/components.png"
        });
        var a = service.GetAlgorithm(added.Id);
        Assert.NotNull(a);

        Assert.Equal(a.Id, a.Id);
        Assert.Equal("Rot13", a.Name);
        Assert.Equal("rotation 13", a.Description);
        Assert.Equal("bad", a.Security);
        Assert.Equal(1, a.SecurityRating);
        Assert.Equal("low", a.Efficiency);
        Assert.Equal(2, a.EfficiencyRating);
        Assert.Equal("average", a.Speed);
        Assert.Equal(3, a.SpeedRating);
        Assert.Equal("http://kerryb.github.io/enigma/images/components.png", a.DiagramUrl);
    }//valid entry produces data

    [Fact]
    public void AddAlgorithm_DuplicateName_ShouldReturnNull()
    {
        var a1 = service.AddAlgorithm(
            new Algorithm { Name = "Rot13", Description = "rotation 13", Security = "bad", 
            SecurityRating = 1, Efficiency = "low", EfficiencyRating = 2, Speed = "average",
            SpeedRating = 3, DiagramUrl = "http://kerryb.github.io/enigma/images/components.png"
        });
        var a2 = service.AddAlgorithm(
            new Algorithm { Name = "Rot13", Description = "roman cipher", Security = "bad", 
            SecurityRating = 1, Efficiency = "low", EfficiencyRating = 2, Speed = "average",
            SpeedRating = 3, DiagramUrl = "http://kerryb.github.io/enigma/images/components.png"
        });
        Assert.NotNull(a1);
        Assert.Null(a2);
    }//name duplication test

    [Fact]
    public void InvalidSecurityRate_ReturnNull()
    {
        var a = service.AddAlgorithm(
            new Algorithm { Name = "Rot13", Description = "rotation 13", Security = "bad", 
            SecurityRating = 7, Efficiency = "low", EfficiencyRating = 2, Speed = "average",
            SpeedRating = 3, DiagramUrl = "http://kerryb.github.io/enigma/images/components.png"
        });
        Assert.Null(a);
    }//invalid security rating 

    [Fact]
    public void InvalidEfficiencyRate_ReturnNull()
    {
        var a = service.AddAlgorithm(
            new Algorithm { Name = "Rot13", Description = "rotation 13", Security = "bad", 
            SecurityRating = 2, Efficiency = "low", EfficiencyRating = 7, Speed = "average",
            SpeedRating = 3, DiagramUrl = "http://kerryb.github.io/enigma/images/components.png"
        });
        Assert.Null(a);
    }//inavlid efficiency rating

    [Fact]
    public void InvalidSpeedRate_ReturnNull()
    {
        var a = service.AddAlgorithm(
            new Algorithm { Name = "Rot13", Description = "rotation 13", Security = "bad", 
            SecurityRating = 2, Efficiency = "low", EfficiencyRating = 2, Speed = "average",
            SpeedRating = 7, DiagramUrl = "http://kerryb.github.io/enigma/images/components.png"
        });
        Assert.Null(a);
    }//invalid speed rating

}


