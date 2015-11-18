using BullsAndCows.Computer;
using BullsAndCows.Computer.Interfaces;
using BullsAndCows.Console.Strategies;
using BullsAndCows.Human;
using BullsAndCows.Human.Interfaces;
using SimpleInjector;

namespace BullsAndCows.Console
{
    public static class Bootstrap
    {
        public static Container Initialise()
        {
            var container = new Container();

            //container.Register<IStrategy, HumanStrategy>();
            container.Register<IStrategy, ComputerStrategy>();
            container.Register<IGuessValidator, GuessValidator>();
            container.Register<IRandomCodeGenerator, RandomCodeGenerator>();
            container.Register<IVerifier, Verifier>();
            container.Register<IGuessGenerator, GuessGenerator>();

            container.Verify();

            return container;
        }
    }
}