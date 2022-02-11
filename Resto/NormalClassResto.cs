// look so what we are gonna do now is show restraunts ,
// get the use to choose a restraunt
// and then show the menus

namespace Resto;

public class displayRestos
{
    MongoConnection db = new MongoConnection();

    public void display() // SRP _ 1
    {
        List<RestrauntModel> recs = db.LoadAllRecords<RestrauntModel>("restos");
        Calculations c1 = new Calculations();
        Console.WriteLine("List of Restraunts : ");
        for (int i = 0; i < recs.Count; i++)
        {
            Console.Write($"\t{recs[i].id}.{recs[i].name} ");
            Console.Write("----");
            Console.WriteLine($" Rating : {c1.avgRatings(recs[i])}\n");

        }
    }

    public void displayTimes(OperatingModel stuff)
    {
        Console.WriteLine("OPEN ON : ");
        Console.WriteLine($"1.Monday    : {stuff.Monday}");
        Console.WriteLine($"2.Tuesday   : {stuff.Tuesday}");
        Console.WriteLine($"3.Wednesday : {stuff.Wednesday}");
        Console.WriteLine($"4.Thursday  : {stuff.Thursday}");
        Console.WriteLine($"5.Friday    : {stuff.Friday}");
        Console.WriteLine($"6.Saturday  : {stuff.Saturday}");
        Console.WriteLine($"7.Sunday    : {stuff.Sunday}");
        Console.WriteLine("\n");
    }

    // public void displaySpecTime(OperatingModel stuff, string day)
    // {
    //     // Console.Write("OPEN ON : ");
    //     // OperatingModel time = db.GetTimeDay<OperatingModel>("restos", day);
    //     // Console.WriteLine($"{time}");
    // }
}

public class Reservation
{
    public void reserve() //srp_2 -- make a seperate class and call this with different functions
    {
        MongoConnection db = new MongoConnection();
        Console.WriteLine("RESERVATION DETAILS");
        // pick restraunts for reservation
        displayRestos dp = new displayRestos();
        up:
        dp.display();
        try
        {
            Console.Write("Pick the number : ");
            int num = Convert.ToInt32(Console.ReadLine());
            var record = db.LoadById<RestrauntModel>("restos", num);
            Console.WriteLine($"\n\n\t{record.name.ToUpper()}");
            dp.displayTimes(record.operating_hours);
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
            else if (time >= 12 && time <= 24)
            {
                Console.WriteLine($"Booked Table at {time}:00pm...");
            }
            else
            {
                Console.WriteLine("Invalid Time Input!!");
                goto time;
            }
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
}

sealed public class Calculations{
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
}


public class Days
{
    public void dayTimings(OperatingModel op)
    {
        Console.WriteLine("Please choose a date : ");
    }
    
}

public class Monday : Days
{
    public void dayTimings(OperatingModel op)
    {
        Console.WriteLine($"The timings for Monday are : {op.Monday}");
    }
}