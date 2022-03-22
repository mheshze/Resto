// look so what we are gonna do now is show restraunts ,
// get the use to choose a restraunt
// and then show the menus

namespace Resto;

public class Restos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            switcher:
            Console.WriteLine("Choose and implement SRP ...\n1.Normal Classes\n2.Abstract Classes\n3.Interfaces\n4.Virtual Methods\n\n-----Dependency Injection-----\n\t5.Parameter Injection");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine("-------NORMAL CLASSES-------\n");
                    Reservation r = new Reservation();
                    r.ireserve();
                    break;
                case 2:
                    Console.WriteLine("-------ABSTRACT CLASSES-------\n");
                    OverrideReservation abstractR = new OverrideReservation();
                    abstractR.absReserve();
                    break;
                case 3:
                    Console.WriteLine("-------INTERFACE IMPLEMENTATIONS-------\n");
                    iOverrideReservation interfacer = new iOverrideReservation();
                    interfacer.iReserve();
                    break;
                case 4:
                    Console.WriteLine("-------VIRTUAL METHOD IMPLEMENTATIONS-------\n");
                    virOvrReservation virtualReserve = new virOvrReservation();
                    virtualReserve.virReserve();
                    break;
                case 5:
                    rest:
                    // Dependancy Injection -->> Parameter Injection
                    Console.Write("Which restaurants (1 or 2)??? : ");
                    int rest = Convert.ToInt32(Console.ReadLine());
                    if (rest == 1)
                    {
                        Restraunt_1 r1 = new Restraunt_1();
                        Selector s1 = new Selector(r1);
                        s1.displayRestraunts();
                        s1.displayRestrauntTimings();
                    }
                    else if(rest == 2)
                    {
                        Restraunt_2 r2 = new Restraunt_2();
                        Selector s2 = new Selector(r2);
                        s2.displayRestraunts();
                        s2.displayRestrauntTimings();
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid Option\n");
                        goto rest;
                    }
                    
                    break;
                default:
                    Console.WriteLine("ERROR : Please choose a valid input!!");
                    goto switcher;
                    
            }
        }
    }
}