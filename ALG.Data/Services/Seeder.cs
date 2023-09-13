
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
                TypeClass = "Cipher", 
                LongDescription="The Caesar cipher, named after Julius Caesar, is one of the simplest and earliest known encryption techniques. It is a substitution cipher that operates by shifting each letter in the plaintext a fixed number of positions down or up the alphabet. For example, with a shift of three positions, 'A' would become 'D,' 'B' would become 'E,' and so on. Caesar used this cipher to protect sensitive military messages during his time, with the shift value acting as the secret key. While the Caesar cipher is elementary and can be easily deciphered through brute force methods due to its limited number of possible keys, it serves as a foundational concept in the history of cryptography and encryption techniques.", 
                WorkDescription="Caesar works by shifting each letter in the plaintext a fixed number of positions down or up the alphabet. This fixed number is known as the 'key' or 'shift value.' For example, with a shift value of 3, 'A' would become 'D,' 'B' would become 'E,' and so on. To encrypt a message, each letter in the plaintext is replaced with the letter located a certain number of positions away in the alphabet, according to the chosen key. To decrypt the message, one simply reverses the process by shifting the letters back in the opposite direction by the same key value. ", 
                UseCases="The Caesar cipher is easy to understand and implement but lacks strong security because there are only 25 possible keys (since shifting by 0 positions does nothing), making it vulnerable to simple brute-force attacks. The Caesar cipher is sometimes used in puzzles, escape rooms, and educational games to engage participants in code-breaking challenges, but is not intended for every-day encryption"
            });
            var algo2 = svc.AddAlgorithm(new Algorithm {
                Name = "A1Z26", LongName="A1Z26 cipher", Description = "Substitution cipher that replaces each letter of the alphabet with its corresponding numerical position", Security = "bad", SecurityRating = 1,     
                Efficiency = "low", EfficiencyRating = 2, Speed = "average", SpeedRating = 3,   
                DiagramUrl = "http://kerryb.github.io/enigma/images/components.png",
                TypeClass = "Cipher", 
                LongDescription="The A1Z26 cipher is a straightforward substitution cipher that replaces each letter in the alphabet with its corresponding position in the alphabet, typically using numbers. This cipher is often used for encoding messages or text where letters are replaced with numbers for various purposes, such as encoding names or simple codes. It is one of the simplest encoding methods and is primarily used for educational and recreational purposes rather than for secure communication, due to its lack of complexity and susceptibility to decryption.", 
                WorkDescription="The A1Z26 cipher is a straightforward substitution cipher that operates by replacing each letter in the alphabet with its corresponding position in the alphabet as a number. 'A' is represented as '1,' 'B' as '2,' 'C' as '3,' and so on, until 'Z,' which is '26.' To encode a message using the A1Z26 cipher, you simply replace each letter with its respective numerical value. ", 
                UseCases="It serves as a fundamental tool for teaching encryption concepts, particularly to beginners. It may find occasional use in recreational puzzles, escape rooms, or games where participants need to decipher encoded messages for entertainment purposes. While it lacks robust security features and isn't suitable for safeguarding sensitive information, its ease of use and transparency make it a suitable choice for recreation"
            });
            var algo3 = svc.AddAlgorithm(new Algorithm {
                Name = "AES",LongName="Advanced Encryption Standard (Rijndael)", Description = "Widely used symmetric-key encryption algorithm that employs various key lengths (128, 192, or 256 bits) to securely encrypt ", Security = "Good", SecurityRating = 5,     
                Efficiency = "Mid", EfficiencyRating = 3, Speed = "fast", SpeedRating = 5,   
                DiagramUrl = "https://www.simplilearn.com/ice9/free_resources_article_thumb/process.png",
                TypeClass = "Symmetrical", 
                LongDescription="Advanced Encryption Standard (AES) is a widely adopted symmetric encryption algorithm established by the U.S. National Institute of Standards and Technology (NIST) in 2001. It was introduced as a replacement for the aging Data Encryption Standard (DES) and is designed to provide strong security for data encryption and protection. AES operates on blocks of data and supports key lengths of 128, 192, or 256 bits.", 
                WorkDescription=" AES operates on blocks of data and supports key lengths of 128, 192, or 256 bits. The algorithm employs a series of well-defined rounds, each consisting of several mathematical operations like substitution, permutation, and mixing, to transform plaintext data into ciphertext.",
                UseCases="AES's widespread adoption and rigorous scrutiny by cryptographers have established it as one of the most secure and efficient encryption standards used in various applications, from securing communications over the internet to protecting sensitive data on devices and servers."
            });
             var algo4 = svc.AddAlgorithm(new Algorithm {
                Name = "Blowfish", LongName ="Blowfish Cipher",Description = "Flexible symmetric-key block cipher algorithm supporting various key lengths with resistance to cryptographic attacks,", Security = "Good", SecurityRating = 5,     
                Efficiency = "High", EfficiencyRating = 5, Speed = "fast", SpeedRating = 5,   
                DiagramUrl = "https://www.simplilearn.com/ice9/free_resources_article_thumb/process.png",
                TypeClass = "Symmetrical", 
                LongDescription="x", 
                WorkDescription="x", 
                UseCases="x"
            });
            var algo5 = svc.AddAlgorithm(new Algorithm {
                Name = "RSA",LongName= "Rivest–Shamir–Adleman", Description = "Widely used asymmetric-key encryption algorithm based on the mathematical difficulty of factoring large semiprime numbers", Security = "Good", SecurityRating = 5,     
                Efficiency = "Good", EfficiencyRating = 4, Speed = "fast", SpeedRating = 5,   
                DiagramUrl = "https://www.researchgate.net/publication/318463561/figure/fig1/AS:631674199101441@1527614273039/ElGamal-cryptosystem-pseudocode.png",
                TypeClass = "Asymmetrical", 
                LongDescription="x", 
                WorkDescription="x", 
                UseCases="x"
            });
        }


    }

}