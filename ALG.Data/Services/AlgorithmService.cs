using Microsoft.EntityFrameworkCore;
using ALG.Data.Entities;
using ALG.Data.Repositories;    

namespace ALG.Data.Services;

public class AlgorithmService : IAlgorithmService
{
    private readonly DatabaseContext ctx;

    public AlgorithmService(){
        ctx = new DatabaseContext(); 
    }

    public void Initialise(){
        ctx.Initialise();
    }

    public List<Algorithm> GetAlgorithms(){
        return ctx.Algorithms.ToList();
    }//get all

    public Algorithm GetAlgorithm(int id){
        return ctx.Algorithms.Find(id);
    }//get one

    public List<Algorithm> SearchAlgorithms(string searchString)
    {
        searchString = searchString == null ? "" : searchString.ToLower();
        var algos = ctx.Algorithms.Where(a => a.Name.ToLower()
                    .Contains(searchString)).ToList();
        return algos;
    }//search algos

    public Algorithm GetAlgorithmByName(string name){
        return ctx.Algorithms.FirstOrDefault(a => a.Name == name);
    }//get by name


    public Algorithm AddAlgorithm(Algorithm a){
        var exists = GetAlgorithmByName(a.Name);

        if (exists != null){
            return null;
        }//if

        //checks security rating 
        if (a.SecurityRating <= 0 || a.SecurityRating > 5){
            return null;
        }//if 
        //checks efficiency rating 
        if (a.EfficiencyRating <= 0 || a.EfficiencyRating > 5){
            return null;
        }//if 
        //checks speed rating
        if(a.SpeedRating <= 0 || a.SpeedRating > 5){
            return null;
        }//if

        //creating Algorithm
        var Algorithm = new Algorithm{
            Name = a.Name,
            LongName = a.LongName,
            Description = a.Description,
            DiagramUrl = a.DiagramUrl,
            Efficiency = a.Efficiency,
            EfficiencyRating = a.EfficiencyRating,
            Security = a.Security,
            SecurityRating = a.SecurityRating,
            Speed = a.Speed,
            SpeedRating = a.SpeedRating,
            TypeClass = a.TypeClass
        };
        ctx.Algorithms.Add(Algorithm);
        ctx.SaveChanges();
        return Algorithm;
    }//adds al
    
}