using System;

internal class Assignment1
{
    static void Main()
    {
        Random rng = new Random(); 
        int RNumber = rng.Next(1,101); //Making a random integer between 1-100
        int NumberOfTries = 1; //Int for tracking # of attempts
        bool PlayAgain = true; 

        while (PlayAgain == true) //Main game loop
        {
            Console.WriteLine("Please guess a number from 1-100");
            int guess = int.Parse(Console.ReadLine()); //Int for keeping track of the number they are guessing
            if (guess > RNumber && guess < 101) //Checks if the guess is too high
            {
                Console.WriteLine("Your guess was too high, please try again");
                NumberOfTries++; //Increasing attempts counter
            }
            if (guess < RNumber && guess > 0) //Checks if the guess is too low
            {
                Console.WriteLine("Your guess was too low, please try again");
                NumberOfTries++; //Increasing attempts counter
            }
            if (guess > 100 || guess < 0) //Makes sure guess is valid and doesn't increase # of tries if not
            {
                Console.WriteLine("That is not a valid answer please try again.");
            }
            if (guess == RNumber) //if they get it right ask to continue or not
            {
                Console.WriteLine($"Congratulations! You guessed the correct number {RNumber} in {NumberOfTries} tries.");
                Console.WriteLine("Do you want to play again? Y/N");
                string PAgain = Console.ReadLine();
                if (PAgain.ToLower() == "y") //if they say they want to play again setting a new random number
                {
                    RNumber = rng.Next(1,101); 
                    NumberOfTries = 1;
                }
                if (PAgain.ToLower() == "n") //if they say no set play again to false ending main loop and program
                {
                    Console.WriteLine("Thanks for Playing :)");
                    PlayAgain = false;
                }
            }
            
    
        }
        
    }
}