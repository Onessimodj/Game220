#include <stdio.h>
#include <stdlib.h> //Needed for random number generation
#include <time.h>   //^^^
#include "io.h"

#define OK 0
#define ERROR 1

typedef enum {
    false, true
} bool;

int main(int argc, char** argv)
{
    printf("Guess a number from 1-100\n");
    
    srand(time(0));

    bool isRunning = true;

    while (isRunning)
    {
        int number = (rand() % 100) + 1; //Generates a random number between 1 and 100
        int NumberofG = 0;

        while (true)
        {
            printf("Enter your guess: ");
            int guess;
            scanf("%d", &guess);

            NumberofG++;

            if (guess == number)
            {
                printf("You got it! The number was %d.\n", number);
                printf("Number of attempts: %d\n", NumberofG);
                break; 
            }
            else if (guess > number)
            {
                println("Your guess was too high!");
            }
            else if (guess < number)
            {
                println("Your guess was too low!");
            }
        }

        
        println("Do you want to play again? (y/n): ");
        char pAgain;
        scanf(" %c", &pAgain);

        if (pAgain == 'n' || pAgain == 'N')
        {
            println("Thanks for playing!!! :) ");
            isRunning = false;
        }
    }

    return OK;
}

