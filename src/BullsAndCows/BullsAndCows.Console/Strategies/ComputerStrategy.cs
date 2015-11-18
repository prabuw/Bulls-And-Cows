using System;
using BullsAndCows.Computer.Interfaces;

namespace BullsAndCows.Console.Strategies
{
    internal class ComputerStrategy : IStrategy
    {
        private readonly IGuessGenerator _guessGenerator;

        public ComputerStrategy(IGuessGenerator guessGenerator)
        {
            _guessGenerator = guessGenerator;
        }

        public void Play()
        {
            var isGuessed = false;
            var guessCount = 1;
            var guess = _guessGenerator.Generate();

            PrintIntro();

            do
            {
                System.Console.WriteLine("Attempt {0}: {1}", guessCount, guess);
                System.Console.Write(">");

                var rawFeedback = System.Console.ReadLine();

                try
                {
                    guess.AddFeedback(rawFeedback);
                    _guessGenerator.Process(guess);

                    if (guess.Bulls == 4)
                    {
                        isGuessed = true;
                    }
                    else
                    {
                        guess = _guessGenerator.Generate();
                        guessCount++;
                    }
                }
                catch (ArgumentException ex)
                {
                    System.Console.WriteLine("Not a valid feedback: 2 digits only, the first number is the number of bulls, and the second is the number of cows!");
                }
            } while (isGuessed == false);

            PrintSummary();

            System.Console.WriteLine();
        }

        private void PrintIntro()
        {
            System.Console.Clear();
            System.Console.WriteLine("Bulls And Cows: The Computer Edition");
            System.Console.WriteLine();
            System.Console.WriteLine("Rules:");
            System.Console.WriteLine("1. Think of a secret random 4 digit code made up of numbers from 1 to 9.");
            System.Console.WriteLine("2. Don't repeat a number once you have used it.");
            System.Console.WriteLine("3. Maybe write it down somewhere, so you don't forget.");
            System.Console.WriteLine();
            System.Console.WriteLine("For example: 4561");
            System.Console.WriteLine();
            System.Console.WriteLine("Now, We will try to guess it!");
            System.Console.WriteLine("After each guess, we need you to tell us how we did by entering two numbers.");
            System.Console.WriteLine();
            System.Console.WriteLine("If we guess a number in the right position, we call that a bull.");
            System.Console.WriteLine("If we guess a number out of position, we call that a cow.");
            System.Console.WriteLine();
            System.Console.WriteLine("For example, if your secret 4 digit code is 1427, and we guess 1479.");
            System.Console.WriteLine("You should enter \"21\", to symbolise 2 bulls and 1 cow.");
            System.Console.WriteLine();
            System.Console.WriteLine("Let's get going - here comes our first guess!");
            System.Console.WriteLine();
        }
        
        private void PrintSummary()
        {
            System.Console.WriteLine("We got it! It took us only {0} guesses.", _guessGenerator.GuessHistory.Count);
        }
    }
}