// look so what we are gonna do now is show restraunts ,
// get the use to choose a restraunt
// and then show the menus

namespace Resto;

public class Restos
{
    private string[] days = new[]{"Sunday", "Monday", "Tueday", "Wednesday", "Thursday", "Friday", "Saturday"};
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
            Restos r = new Restos();
            r.reserve();
        }
    }
}