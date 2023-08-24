
using ALG.Data.Entities;

namespace ALG.Data.Services
{
    public static class Seeder
    {
    
        public static void Seed(){
            IAlgorithmService algorithmService = new AlgorithmService();
            SeedAlgorithms(algorithmService);
        }
        public static void SeedAlgorithms(IAlgorithmService svc)
        {
            svc.Initialise();

            var algo1 = svc.AddAlgorithm(new Algorithm {
                Name = "Caesar", Description = "cipher desc", Security = "bad", SecurityRating = 1, 
                Efficiency = "low", EfficiencyRating = 1, Speed = "fast", SpeedRating = 4, 
                DiagramUrl = "https://i.imgur.com/sUTGYn6.png",
                TypeClass = "Cipher"
            });
            var algo2 = svc.AddAlgorithm(new Algorithm {
                Name = "Engima", Description = "cipher desc", Security = "bad", SecurityRating = 1,     
                Efficiency = "low", EfficiencyRating = 2, Speed = "average", SpeedRating = 3,   
                DiagramUrl = "http://kerryb.github.io/enigma/images/components.png",
                TypeClass = "Cipher"
            });
            var algo3 = svc.AddAlgorithm(new Algorithm {
                Name = "AES", Description = "algo desc", Security = "Good", SecurityRating = 5,     
                Efficiency = "Mid", EfficiencyRating = 3, Speed = "fast", SpeedRating = 5,   
                DiagramUrl = "https://www.simplilearn.com/ice9/free_resources_article_thumb/process.png",
                TypeClass = "Symmetrical"
            });
            var algo4 = svc.AddAlgorithm(new Algorithm {
                Name = "El Gamal", Description = "algo desc", Security = "Good", SecurityRating = 5,     
                Efficiency = "Good", EfficiencyRating = 4, Speed = "fast", SpeedRating = 5,   
                DiagramUrl = "https://www.researchgate.net/publication/318463561/figure/fig1/AS:631674199101441@1527614273039/ElGamal-cryptosystem-pseudocode.png",
                TypeClass = "Asymmetrical"
            });
        }
        // use this class to seed the database with dummy test data using an IUserService 
        public static void SeedUsers(IUserService svc)
        {
            // seeder destroys and recreates the database - NOT to be called in production!!!
            svc.Initialise();

            // add users
            svc.AddUser("Administrator", "admin@mail.com", "admin", Role.admin);
            svc.AddUser("Manager", "manager@mail.com", "manager", Role.manager);
            svc.AddUser("Guest", "guest@mail.com", "guest", Role.guest);    
        }


    }

}