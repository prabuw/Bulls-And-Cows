using System;
using System.Collections.Generic;
using System.Linq;
using BullsAndCows.Human.Interfaces;
using BullsAndCows.Human.Models;
using SimpleInjector;

namespace BullsAndCows.Console.Strategies
{
    internal class HumanStrategy : IHumanStrategy
    {
        private readonly string _randomCode;
        private readonly IVerifier _verifier;
        private readonly Queue<VerificationResult> _guessHistory; 

        public HumanStrategy(Container container)
        {
            var randomCodeGenerator = container.GetInstance<IRandomCodeGenerator>();
            _verifier = container.GetInstance<IVerifier>();
            _guessHistory = new Queue<VerificationResult>();

            _randomCode = randomCodeGenerator.Generate();
        }

        public void Play()
        {
            var isGuessed = false;

            PrintIntro();

            do
            {
                System.Console.Write(">");
                var rawGuess = System.Console.ReadLine();

                try
                {
                    var result = _verifier.Verify(_randomCode, rawGuess);
                    _guessHistory.Enqueue(result);
                    
                    PrintResult(result);

                    if (result.Bulls == 4)
                        isGuessed = true;
                }
                catch (ArgumentException ex)
                {
                    System.Console.WriteLine("Not a valid guess: 4 digits and no repeating of digits!");
                }
            } while (isGuessed == false);

            PrintSummary(_guessHistory);
            
            System.Console.WriteLine();
        }
        
        private static void PrintIntro()
        {
            System.Console.Clear();
            System.Console.WriteLine("Bulls And Cows: The Human Edition");
            System.Console.WriteLine();
            System.Console.WriteLine("Rules:");
            System.Console.WriteLine("We have generated a random 4 digit code made up of numbers from 1 to 9.");
            System.Console.WriteLine("We don't repeat a number once used.");
            System.Console.WriteLine();
            System.Console.WriteLine("For example: 4561");
            System.Console.WriteLine();
            System.Console.WriteLine("Your objective: Guess it in the least number of goes!");
            System.Console.WriteLine();
            System.Console.WriteLine("If you guess a number in the right position, we call that a bull.");
            System.Console.WriteLine("If you guess a number out of position, we call that a cow.");
            System.Console.WriteLine();
            System.Console.WriteLine("Let's get going - Make a guess!");
            System.Console.WriteLine();
        }

        private void PrintResult(VerificationResult result)
        {
            System.Console.WriteLine("Bulls: {0} Cows: {1}", result.Bulls, result.Cows);
            System.Console.WriteLine();
        }

        private void PrintSummary(Queue<VerificationResult> guessHistory)
        {
            var guessCount = 0;
            var maxListingNumberLength = (int)(Math.Floor(Math.Log10(guessHistory.Count()) + 1) + 1);

            System.Console.WriteLine("----------------------------------");
            System.Console.WriteLine("    |  Guess  |  Bulls  |  Cows  |");
            System.Console.WriteLine("----------------------------------");

            while (guessHistory.Any())
            {
                var guess = guessHistory.Dequeue();
                guessCount++;
                var paddedGuessCount = guessCount.ToString().PadLeft(maxListingNumberLength);

                System.Console.WriteLine("{0}.|  {1}   |    {2}    |   {3}    |", paddedGuessCount, guess.Value, guess.Bulls, guess.Cows);
            }

            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine("Congratulations - It took you {0} guesses!", guessCount);
        }
    }
}