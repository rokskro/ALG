
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
                Name = "Caesar", LongName="Caesar Cipher", Description = "Simple substitution cipher that shifts each letter in a message a fixed number of positions down or up the alphabet", 
                Security = "Low", SecurityRating = 1, 
                Efficiency = "Low", EfficiencyRating = 1, 
                Speed = "Below Average", SpeedRating = 2, 
                DiagramUrl = "https://i.imgur.com/sUTGYn6.png",
                TypeClass = "Cipher", 
                LongDescription="The Caesar cipher, named after Julius Caesar, is one of the simplest and earliest known encryption techniques. It is a substitution cipher that operates by shifting each letter in the plaintext a fixed number of positions down or up the alphabet. For example, with a shift of three positions, 'A' would become 'D,' 'B' would become 'E,' and so on. Caesar used this cipher to protect sensitive military messages during his time, with the shift value acting as the secret key. While the Caesar cipher is elementary and can be easily deciphered through brute force methods due to its limited number of possible keys, it serves as a foundational concept in the history of cryptography and encryption techniques.", 
                WorkDescription="Caesar works by shifting each letter in the plaintext a fixed number of positions down or up the alphabet. This fixed number is known as the 'key' or 'shift value.' For example, with a shift value of 3, 'A' would become 'D,' 'B' would become 'E,' and so on. To encrypt a message, each letter in the plaintext is replaced with the letter located a certain number of positions away in the alphabet, according to the chosen key. To decrypt the message, one simply reverses the process by shifting the letters back in the opposite direction by the same key value. ", 
                UseCases="The Caesar cipher is easy to understand and implement but lacks strong security because there are only 25 possible keys (since shifting by 0 positions does nothing), making it vulnerable to simple brute-force attacks. The Caesar cipher is sometimes used in puzzles, escape rooms, and educational games to engage participants in code-breaking challenges, but is not intended for every-day encryption"
            });
            var algo2 = svc.AddAlgorithm(new Algorithm {
                Name = "A1Z26", LongName="A1Z26 cipher", Description = "Substitution cipher that replaces each letter of the alphabet with its corresponding numerical position", 
                Security = "Low", SecurityRating = 1,     
                Efficiency = "Below Average", EfficiencyRating = 2, 
                Speed = "Average", SpeedRating = 3,   
                DiagramUrl = "https://i.imgur.com/TiT6jbe.png",
                TypeClass = "Cipher", 
                LongDescription="The A1Z26 cipher is a straightforward substitution cipher that replaces each letter in the alphabet with its corresponding position in the alphabet, typically using numbers. This cipher is often used for encoding messages or text where letters are replaced with numbers for various purposes, such as encoding names or simple codes. It is one of the simplest encoding methods and is primarily used for educational and recreational purposes rather than for secure communication, due to its lack of complexity and susceptibility to decryption.", 
                WorkDescription="The A1Z26 cipher is a straightforward substitution cipher that operates by replacing each letter in the alphabet with its corresponding position in the alphabet as a number. 'A' is represented as '1,' 'B' as '2,' 'C' as '3,' and so on, until 'Z,' which is '26.' To encode a message using the A1Z26 cipher, you simply replace each letter with its respective numerical value. ", 
                UseCases="It serves as a fundamental tool for teaching encryption concepts, particularly to beginners. It may find occasional use in recreational puzzles, escape rooms, or games where participants need to decipher encoded messages for entertainment purposes. While it lacks robust security features and isn't suitable for safeguarding sensitive information, its ease of use and transparency make it a suitable choice for recreation"
            });
            var algo3 = svc.AddAlgorithm(new Algorithm {
                Name = "AES",LongName="Advanced Encryption Standard (Rijndael)", Description = "Widely used symmetric-key encryption algorithm that employs various key lengths (128, 192, or 256 bits) to securely encrypt ", Security = "Great", SecurityRating = 5,     
                Efficiency = "Great", EfficiencyRating = 5, 
                Speed = "Fast", SpeedRating = 5,   
                DiagramUrl = "https://i.imgur.com/lSbreVZ.png",
                TypeClass = "Symmetrical", 
                LongDescription="Advanced Encryption Standard (AES) is a widely adopted symmetric encryption algorithm established by the U.S. National Institute of Standards and Technology (NIST) in 2001. It was introduced as a replacement for the aging Data Encryption Standard (DES) and is designed to provide strong security for data encryption and protection. AES operates on blocks of data and supports key lengths of 128, 192, or 256 bits.", 
                WorkDescription=" AES operates on blocks of data and supports key lengths of 128, 192, or 256 bits. The algorithm employs a series of well-defined rounds, each consisting of several mathematical operations like substitution, permutation, and mixing, to transform plaintext data into ciphertext.",
                UseCases="AES's widespread adoption and rigorous scrutiny by cryptographers have established it as one of the most secure and efficient encryption standards used in various applications, from securing communications over the internet to protecting sensitive data on devices and servers."
            });
             var algo4 = svc.AddAlgorithm(new Algorithm {
                Name = "Blowfish", LongName ="Blowfish Algorithm",Description = "Flexible symmetric-key block cipher algorithm supporting various key lengths with resistance to cryptographic attacks,", 
                Security = "Good", SecurityRating = 4,     
                Efficiency = "Good", EfficiencyRating = 4, 
                Speed = "Fast", SpeedRating = 5,   
                DiagramUrl = "https://i.imgur.com/bRyg1cm.png",
                TypeClass = "Symmetrical", 
                LongDescription="Blowfish is a symmetric-key block cipher algorithm designed by Bruce Schneier in 1993. It was developed as a response to concerns about the security of existing encryption standards at the time, such as the Data Encryption Standard (DES). Blowfish gained recognition for its speed and security features, particularly its ability to efficiently encrypt and decrypt data.", 
                WorkDescription="It operates on fixed-size blocks of data and supports key lengths ranging from 32 to 448 bits, making it adaptable to different security needs. Blowfish uses a series of substitution and permutation (a mathematical calculation of the number of ways a particular set can be arranged) operations to transform plaintext into ciphertext", 
                UseCases="It has been largely replaced by newer encryption algorithms like AES, Blowfish remains a notable historical milestone in the field of cryptography and is still used in some niche applications where its features are advantageous. Blowfish is occasionally used to secure stored passwords or may be used in legacy systems where upgrading to newer encryption standards is difficult or costly."
            });
            var algo5 = svc.AddAlgorithm(new Algorithm {
                Name = "RSA",LongName= "Rivest–Shamir–Adleman", Description = "Widely used asymmetric-key encryption algorithm based on the mathematical difficulty of factoring large semiprime numbers", 
                Security = "Great", SecurityRating = 5,     
                Efficiency = "Above Average", EfficiencyRating = 4, 
                Speed = "Fast", SpeedRating = 5,   
                DiagramUrl = "https://i.imgur.com/dAb1K4V.png",
                TypeClass = "Asymmetrical", 
                LongDescription="RSA (Rivest–Shamir–Adleman) is a widely used asymmetric or public-key cryptosystem invented by Ron Rivest, Adi Shamir, and Leonard Adleman in 1977. It revolutionized the field of cryptography by introducing a secure method for key exchange and digital signatures without requiring both parties to share a secret key beforehand.", 
                WorkDescription="RSA relies on the mathematical properties of large prime numbers; it involves the generation of a public key for encryption and a private key for decryption (factoring the product of two large prime numbers). This asymmetry between the public and private keys makes it extremely difficult for anyone to decipher the message without access to the private key, ensuring the confidentiality and integrity of the encrypted data.", 
                UseCases="RSA is commonly used to establish secure communication channels over the internet, such as HTTPS for secure web browsing, email encryption, and virtual private networks (VPNs). Additonally, RSA is employed to create digital signatures, which verify the authenticity and integrity of digital documents, contracts, and messages. "
            });
        }


    }

}