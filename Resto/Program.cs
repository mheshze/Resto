// look so what we are gonna do now is show restraunts ,
// get the use to choose a restraunt
// and then show the menus

namespace Resto;

public class Restos
{
    // private string[] days = new[]{"Sunday", "Monday", "Tueday", "Wednesday", "Thursday", "Friday", "Saturday"};
    MongoConnection db = new MongoConnection();

    public void display() // SRP _ 1
    {
        List<RestrauntModel> recs = db.LoadAllRecords<RestrauntModel>("restos");
        Console.WriteLine("List of Restraunts : ");
        for (int i = 0; i < recs.Count; i++)
        {
            Console.Write($"\t{recs[i].id}.{recs[i].name} ");
            Console.Write("----");
            Console.WriteLine($" Rating : {avgRatings(recs[i])}\n");
            
        }
    }

    public void displayTimes(OperatingModel stuff)
    {
        Console.WriteLine("OPEN ON : ");
        Console.WriteLine($"Monday    : {stuff.Monday}");
        Console.WriteLine($"Tuesday   : {stuff.Tuesday}");
        Console.WriteLine($"Wednesday : {stuff.Wednesday}");
        Console.WriteLine($"Thursday  : {stuff.Thursday}");
        Console.WriteLine($"Friday    : {stuff.Friday}");
        Console.WriteLine($"Saturday  : {stuff.Saturday}");
        Console.WriteLine($"Sunday    : {stuff.Sunday}");
        Console.WriteLine("\n");
    }

    public void displaySpecTime(OperatingModel stuff,string day)
    {
        // Console.Write("OPEN ON : ");
        // OperatingModel time = db.GetTimeDay<OperatingModel>("restos", day);
        // Console.WriteLine($"{time}");
    }

    public void reserve() //srp_2 -- make a seperate class and call this with different functions
    {
        Console.WriteLine("RESERVATION DETAILS");
        // pick restraunts for reservation
        up:
        display();
        try
        {
            Console.Write("Pick the number : ");
            int num = Convert.ToInt32(Console.ReadLine());
            var record = db.LoadById<RestrauntModel>("restos", num);
            Console.WriteLine($"\n\n\t{record.name.ToUpper()}");
            displayTimes(record.operating_hours);
            Console.WriteLine("When do you wanna book your table??");
            Console.Write("Day : ");
            string reserve_day = Console.ReadLine();
            // Datetime implementation
            time:
            Console.Write($"What time on {reserve_day}?: ");
            int time = Convert.ToInt32(Console.ReadLine());
            if (time >= 1 && time < 12)
            {
                Console.WriteLine($"Booked Table at {time}:00am...");
            }
            else if(time >= 12 && time <= 24)
            {
                Console.WriteLine($"Booked Table at {time}:00pm...");
            }
            else
            {
                Console.WriteLine("Invalid Time Input!!");
                goto time;
            }
            // function(,record,day)


        }
        catch (Exception e)
        {
            Console.WriteLine("\n\tOH NO :(\n\tWe noticed a problem!\n");
            confirm:
            Console.Write("Want to continue??? : ");
            string confirmation = Console.ReadLine();
            if (confirmation.ToUpper().StartsWith("Y"))
            {
                goto up;
            }
            else if (confirmation.ToUpper().StartsWith("N"))
            {
                Console.WriteLine("\nExiting...");
            }
            else
            {
                Console.WriteLine("Please enter Yes or No!!");
                goto confirm;
            }
        }
    } 
    public double avgRatings(RestrauntModel record)
    {
        double total = 0;
        int i = 0;
        foreach (var review in record.reviews)
        {
            total += review.rating;
            i++;
        }

        return Math.Round(total / i, 2);
    }

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
                    Restos r = new Restos();
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



            // demo implementations
            // Restos r = new Restos();
            // r.reserve();

            //abstact classes implementations
            // OverrideReservation r = new OverrideReservation();
            //  r.absReserve();

            // interface implementations
            // iOverrideReservation interfacer = new iOverrideReservation();
            // interfacer.iReserve();

            // virtual methods implementations
            // virOvrReservation virtualReserve = new virOvrReservation();
            // virtualReserve.virReserve();


        }
    }
}