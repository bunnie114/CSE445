
using System;
using PasswordHashLibrary;

namespace PasswordHashTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string password = "Test123!";
            string hashedPassword = PasswordHasher.HashPassword(password);

            Console.WriteLine("Original Password: " + password);
            Console.WriteLine("Hashed Password: " + hashedPassword);

            bool correct = PasswordHasher.VerifyPassword(
                "Test123!", hashedPassword);

            bool incorrect = PasswordHasher.VerifyPassword(
                "Wrong123!", hashedPassword);

            Console.WriteLine("Correct Password: " + correct);
            Console.WriteLine("Incorrect Password: " + incorrect);

            Console.ReadLine();
        }
    }
}
