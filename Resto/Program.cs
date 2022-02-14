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
            Console.WriteLine("Choose and implement SRP ...\n1.Normal Classes\n2.Abstract Classes\n3.Interfaces\n4.Virtual Methods\n");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine("-------NORMAL CLASSES-------\n");
                    Reservation r = new Reservation();
                    r.reserve();
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
                default:
                    Console.WriteLine("ERROR : Please choose a valid input!!");
                    goto switcher;
                    
            }
        }
    }
}