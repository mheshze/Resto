using System;

namespace Resto //Abstract Factory
{
    public interface IItalian
    {
        void service();
        void cuisine();
    }

    public interface IAsian
    {
        void service();
        void cuisine();
    }

    public class BrikOven : IItalian
    {
        public void service()
        {
            Console.WriteLine("BrikOven service starts from 11am - 11pm!");
        }

        public void cuisine()
        {
            Console.WriteLine("We serve Pizza's and Italian Dishes!");
        }
    }

    public class Dominos : IItalian
    {
        public void service()
        {
            Console.WriteLine("Dominos service is 24/7!!");
        }

        public void cuisine()
        {
            Console.WriteLine("Domino's Serves Pizza's. Delivery by 30mins at your doorstep!");
        }
    }

    public class Dofu : IAsian
    {
        public void service()
        {
            Console.WriteLine("Dofu service is from 10am to 11pm!");
        }

        public void cuisine()
        {
            Console.WriteLine("At Dofu we offer a mixture of asian cuisine!");
        }
    }

    public class Mainland_China : IAsian
    {
        public void service()
        {
            Console.WriteLine("Mainland China service is from 10am to 11pm!");
        }

        public void cuisine()
        {
            Console.WriteLine("Mainland China offers tasty Chinese food!");
        }
    }

    public interface ICuisineFactory
    {
        IItalian IGetItalianRestraunt();
        IAsian IGetAsianRestraunt();
    }

    public class IFastFood : ICuisineFactory
    {
        public IItalian IGetItalianRestraunt()
        {
            return new Dominos();
        }

        public IAsian IGetAsianRestraunt()
        {
            return new Mainland_China();
        }
    }

    public class IBistero : ICuisineFactory
    {
        public IItalian IGetItalianRestraunt()
        {
            return new BrikOven();
        }

        public IAsian IGetAsianRestraunt()
        {
            return new Dofu();
        }
    }
}