
using Xunit;
using ALG.Data.Entities;
using ALG.Data.Services;

using Microsoft.EntityFrameworkCore;
using ALG.Data.Repositories;
using ALG.Data.Security;

namespace ALG.Test;
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
            new Algorithm { Name = "Rot13", LongName="rot13", Description = "rotation 13", Security = "bad", 
            SecurityRating = 1, Efficiency = "low", EfficiencyRating = 2, Speed = "average",
            SpeedRating = 3, DiagramUrl = "http://kerryb.github.io/enigma/images/components.png",
            LongDescription="x", WorkDescription="x", UseCases="x"
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
            new Algorithm { Name = "Rot13",LongName="rot13", Description = "rotation 13", Security = "bad", 
            SecurityRating = 1, Efficiency = "low", EfficiencyRating = 2, Speed = "average",
            SpeedRating = 3, DiagramUrl = "http://kerryb.github.io/enigma/images/components.png",
            LongDescription="x", WorkDescription="x", UseCases="x"
        });
        var a2 = service.AddAlgorithm(
            new Algorithm { Name = "Blank",LongName="rot13", Description = "cipher", Security = "bad", 
            SecurityRating = 1, Efficiency = "low", EfficiencyRating = 2, Speed = "average",
            SpeedRating = 3, DiagramUrl = "http://kerryb.github.io/enigma/images/components.png",
            LongDescription="x", WorkDescription="x", UseCases="x"
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
            new Algorithm { Name = "Rot13",LongName="rot13", Description = "rotation 13", Security = "bad", 
            SecurityRating = 1, Efficiency = "low", EfficiencyRating = 2, Speed = "average",
            SpeedRating = 3, DiagramUrl = "http://kerryb.github.io/enigma/images/components.png",
            LongDescription="x", WorkDescription="x", UseCases="x"
        });
        var na = service.GetAlgorithm(a.Id);
        Assert.NotNull(na);
        Assert.Equal(a.Id, na.Id);
    }// if addded, return

    [Fact]
    public void GetAlgorithmByName_WhenAdded_ShouldReturnData()
    {
        var a = service.AddAlgorithm(
            new Algorithm { Name = "Rot13",LongName="rot13", Description = "rotation 13", Security = "bad", 
            SecurityRating = 1, Efficiency = "low", EfficiencyRating = 2, Speed = "average",
            SpeedRating = 3, DiagramUrl = "http://kerryb.github.io/enigma/images/components.png",
            LongDescription="x", WorkDescription="x", UseCases="x"
        });
        var na = service.GetAlgorithmByName("Rot13");
        Assert.NotNull(na);
        Assert.Equal(a.Name, na.Name);
    }//get Algorithm by name 

    [Fact]
    public void AddAlgorithm_WhenValid_ShouldAdd()
    {
        var added = service.AddAlgorithm(
            new Algorithm { Name = "Rot13",LongName="rot13", Description = "rotation 13", Security = "bad", 
            SecurityRating = 1, Efficiency = "low", EfficiencyRating = 2, Speed = "average",
            SpeedRating = 3, DiagramUrl = "http://kerryb.github.io/enigma/images/components.png",
            LongDescription="x", WorkDescription="x", UseCases="x"
        });
        var a = service.GetAlgorithm(added.Id);
        Assert.NotNull(a);

        Assert.Equal(a.Id, a.Id);
        Assert.Equal("Rot13", a.Name);
        Assert.Equal("rot13", a.LongName);
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
            new Algorithm { Name = "Rot13", LongName="rot13",Description = "rotation 13", Security = "bad", 
            SecurityRating = 1, Efficiency = "low", EfficiencyRating = 2, Speed = "average",
            SpeedRating = 3, DiagramUrl = "http://kerryb.github.io/enigma/images/components.png",
            LongDescription="x", WorkDescription="x", UseCases="x"
        });
        var a2 = service.AddAlgorithm(
            new Algorithm { Name = "Rot13", LongName="rot13",Description = "roman cipher", Security = "bad", 
            SecurityRating = 1, Efficiency = "low", EfficiencyRating = 2, Speed = "average",
            SpeedRating = 3, DiagramUrl = "http://kerryb.github.io/enigma/images/components.png",
            LongDescription="x", WorkDescription="x", UseCases="x"
        });
        Assert.NotNull(a1);
        Assert.Null(a2);
    }//name duplication test

    [Fact]
    public void InvalidSecurityRate_ReturnNull()
    {
        var a = service.AddAlgorithm(
            new Algorithm { Name = "Rot13", LongName="rot13",Description = "rotation 13", Security = "bad", 
            SecurityRating = 7, Efficiency = "low", EfficiencyRating = 2, Speed = "average",
            SpeedRating = 3, DiagramUrl = "http://kerryb.github.io/enigma/images/components.png",
            LongDescription="x", WorkDescription="x", UseCases="x"
        });
        Assert.Null(a);
    }//invalid security rating 

    [Fact]
    public void InvalidEfficiencyRate_ReturnNull()
    {
        var a = service.AddAlgorithm(
            new Algorithm { Name = "Rot13", LongName="rot13",Description = "rotation 13", Security = "bad", 
            SecurityRating = 2, Efficiency = "low", EfficiencyRating = 7, Speed = "average",
            SpeedRating = 3, DiagramUrl = "http://kerryb.github.io/enigma/images/components.png",
            LongDescription="x", WorkDescription="x", UseCases="x"
        });
        Assert.Null(a);
    }//inavlid efficiency rating

    [Fact]
    public void InvalidSpeedRate_ReturnNull()
    {
        var a = service.AddAlgorithm(
            new Algorithm { Name = "Rot13", LongName="rot13",Description = "rotation 13", Security = "bad", 
            SecurityRating = 2, Efficiency = "low", EfficiencyRating = 2, Speed = "average",
            SpeedRating = 7, DiagramUrl = "http://kerryb.github.io/enigma/images/components.png",
            LongDescription="x", WorkDescription="x", UseCases="x"
        });
        Assert.Null(a);
    }//invalid speed rating

}


