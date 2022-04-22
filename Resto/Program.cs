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
            Console.WriteLine("Choose and implement SRP ...\n1.Normal Classes\n2.Abstract Classes\n3.Interfaces\n4.Virtual Methods\n\n-----Dependency Injection-----\n\t5.Parameter Injection\n\n6.Design Pattern");
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
                case 6:
                    Console.Write("Choose Pattern : \n1.Creational Pattern(Abstract Factory)\n2.Structural Pattern (Facade)\n3.Behavioural Pattern (Mediator)\n");
                    int pattern = Convert.ToInt32(Console.ReadLine());
                    if (pattern == 1)
                    {
                        Console.WriteLine("\n------ CREATIONAL PATTERN EXAMPLE ------\n");
                        ICuisineFactory fastfoodFactory = new IFastFood();
                        IItalian fast_italian = fastfoodFactory.IGetItalianRestraunt();
                        IAsian fast_asian = fastfoodFactory.IGetAsianRestraunt();

                        ICuisineFactory bisteroFactory = new IBistero();
                        IItalian bist_italian = bisteroFactory.IGetItalianRestraunt();
                        IAsian bist_asian = bisteroFactory.IGetAsianRestraunt();

                        Console.WriteLine("\nArea : FastFood\n");

                        fast_italian.service();
                        fast_italian.cuisine();

                        fast_asian.service();
                        fast_asian.cuisine();

                        Console.WriteLine("\nArea : Bistero\n");

                        bist_italian.service();
                        bist_italian.cuisine();

                        bist_asian.service();
                        bist_asian.cuisine();

                    }
                    else if (pattern == 2)
                    {
                        Console.WriteLine("\n------ STRUCTURAL PATTERN EXAMPLE ------\n");
                        RestoFacade rf = new RestoFacade();
                        creationChoice:
                        Console.Write(
                            "Select Options..\n1.Create a Restaurant\n2.Create a Shop\n3.Create Restaurant with in-built shop\n4.Shop with Restaurant Service\n\n");
                        int creationChoice = Convert.ToInt32(Console.ReadLine());
                        switch (creationChoice)
                        {
                            case 1:
                                rf.CreateRestaurantOnly();
                                break;
                            case 2:
                                rf.CreateShopOnly();
                                break;
                            case 3:
                                rf.CreateRestaurantWithShop();
                                break;
                            case 4:
                                rf.CreateShopWithRestaurant();
                                break;
                            default:
                                Console.WriteLine("\nInvalid Option\n");
                                goto creationChoice;
                        }
                    }
                    else if (pattern == 3)
                    {
                        Console.WriteLine("\n------ BEHAVIOURAL PATTERN EXAMPLE ------\n");
                        ConcreteMediator mediator = new ConcreteMediator();

                        User Mahesh = new User(mediator, "Mahesh");
                        DeliveryClient dc1 = new DeliveryClient(mediator, "Rajashekar");
                        Helpline hp1 = new Helpline(mediator, "Arun");

                        mediator.User = Mahesh;
                        mediator.Delivery_Client = dc1;
                        mediator.Helpline = hp1;

                        Mahesh.Send(dc1, "I have not yet recieved my order yet, its been almost 1 hour....");
                        dc1.Send(Mahesh, "Sorry for the wait will be there in 5 mins...");
                        hp1.Send(Mahesh, "Sorry for the Inconvienience, You can choose to cancel the order...");

                        dc1.Status = "Off";
                        
                        Mahesh.Send(dc1,"Still not recieved order!! I want to cancel");
                        hp1.Send(Mahesh,"We have cancelled your order, we will make sure to give you better service next time...");

                        Console.ReadLine();
                    }
                    
                    break;
                    
                default:
                    Console.WriteLine("ERROR : Please choose a valid input!!");
                    goto switcher;
                    
            }
        }
    }
}