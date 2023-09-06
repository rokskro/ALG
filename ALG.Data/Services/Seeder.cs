
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
                Name = "Caesar", LongName="Caesar Cipher", Description = "Simple substitution cipher that shifts each letter in a message a fixed number of positions down or up the alphabet", Security = "bad", SecurityRating = 1, 
                Efficiency = "low", EfficiencyRating = 1, Speed = "fast", SpeedRating = 4, 
                DiagramUrl = "https://i.imgur.com/sUTGYn6.png",
                TypeClass = "Cipher"
            });
            var algo2 = svc.AddAlgorithm(new Algorithm {
                Name = "A1Z26", LongName="A1Z26 cipher", Description = "Substitution cipher that replaces each letter of the alphabet with its corresponding numerical position", Security = "bad", SecurityRating = 1,     
                Efficiency = "low", EfficiencyRating = 2, Speed = "average", SpeedRating = 3,   
                DiagramUrl = "http://kerryb.github.io/enigma/images/components.png",
                TypeClass = "Cipher"
            });
            var algo3 = svc.AddAlgorithm(new Algorithm {
                Name = "AES",LongName="Advanced Encryption Standard (Rijndael)", Description = "Widely used symmetric-key encryption algorithm that employs various key lengths (128, 192, or 256 bits) to securely encrypt ", Security = "Good", SecurityRating = 5,     
                Efficiency = "Mid", EfficiencyRating = 3, Speed = "fast", SpeedRating = 5,   
                DiagramUrl = "https://www.simplilearn.com/ice9/free_resources_article_thumb/process.png",
                TypeClass = "Symmetrical"
            });
             var algo4 = svc.AddAlgorithm(new Algorithm {
                Name = "Blowfish", LongName ="Blowfish Cipher",Description = "Symmetric-key block cipher algorithm known for its flexibility in supporting various key lengths and its resistance to known cryptographic attacks,", Security = "Good", SecurityRating = 5,     
                Efficiency = "High", EfficiencyRating = 5, Speed = "fast", SpeedRating = 5,   
                DiagramUrl = "https://www.simplilearn.com/ice9/free_resources_article_thumb/process.png",
                TypeClass = "Symmetrical"
            });
            var algo5 = svc.AddAlgorithm(new Algorithm {
                Name = "RSA",LongName= "Rivest–Shamir–Adleman", Description = "Asymmetric-key encryption algorithm widely used for secure communication based on the mathematical difficulty of factoring large semiprime numbers", Security = "Good", SecurityRating = 5,     
                Efficiency = "Good", EfficiencyRating = 4, Speed = "fast", SpeedRating = 5,   
                DiagramUrl = "https://www.researchgate.net/publication/318463561/figure/fig1/AS:631674199101441@1527614273039/ElGamal-cryptosystem-pseudocode.png",
                TypeClass = "Asymmetrical"
            });
        }


    }

}