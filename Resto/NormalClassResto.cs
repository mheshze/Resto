// look so what we are gonna do now is show restaurants
// get the use to choose a Restaurant
// and then show the menus

namespace Resto;

interface IDisplay // Interface Segregation Principle
{ 
    void idisplay();
    void idisplayTimes(OperatingModel op);
}


interface IReserve
{ 
    void ireserve();
}

interface ICalculate
{
    double iavgRatings(RestrauntModel record);
}

public class displayRestos : IDisplay
{
    MongoConnection db = new MongoConnection();

    public void idisplay() // SRP _ 1
    {
        List<RestrauntModel> recs = db.LoadAllRecords<RestrauntModel>("restos");
        Calculations c1 = new Calculations();
        Console.WriteLine("List of Restaurants : ");
        for (int i = 0; i < recs.Count; i++)
        {
            Console.Write($"\t{recs[i].id}.{recs[i].name} ");
            Console.Write("----");
            Console.WriteLine($" Rating : {c1.iavgRatings(recs[i])}\n");

        }
    }

    public void idisplayTimes(OperatingModel stuff)
    {
        Console.WriteLine("\nOPEN ON : ");
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

public class Reservation : IReserve
{
    public void ireserve() 
        //srp_2 -- make a seperate class and call this with different functions
    {
        MongoConnection db = new MongoConnection();
        Console.WriteLine("RESERVATION DETAILS");
        // pick restraunts for reservation
        displayRestos dp = new displayRestos();
        up:
        dp.idisplay();
        try
        {
            Console.Write("Pick the number : ");
            int num = Convert.ToInt32(Console.ReadLine());
            var record = db.LoadById<RestrauntModel>("restos", num);
            Console.WriteLine($"\n\n\t{record.name.ToUpper()}");
            toptime:
            Console.WriteLine("When do you wanna book your table??" +
                              "\n\t1.Monday" +
                              "\n\t2.Tuesday" +
                              "\n\t3.Wednesday" +
                              "\n\t4.Thursday" +
                              "\n\t5.Friday" +
                              "\n\t6.Saturday" +
                              "\n\t7.Sunday" +
                              "\n\t0.All Timings");
            Console.Write("Day : ");
            int day = Convert.ToInt32(Console.ReadLine());
            
            // LSP Implementations - Each case represents a subtype acting as objects for a base-type called Days
            switch (day)
            {
                case 1:
                    Days d1 = new Monday();  // LSP -1 T1
                    d1.displayDay(record);
                    break;
                case 0 :
                    dp.idisplayTimes(record.operating_hours);
                    break;
                case 2:
                    Days d2 = new Tuesday(); // LSP -1 T2
                    d2.displayDay(record);
                    break;
                case 3:
                    Days d3 = new Wednesday(); // LSP -1 T3
                    d3.displayDay(record);
                    break;
                case 4:
                    Days d4 = new Thursday(); // LSP -1 T4
                    d4.displayDay(record);
                    break;
                case 5:
                    Days d5 = new Friday(); // LSP -1 T5
                    d5.displayDay(record);
                    break;
                case 6:
                    Days d6 = new Saturday(); // LSP -1 T6
                    d6.displayDay(record);
                    break;
                case 7:
                    Days d7 = new Sunday(); // LSP -1 T7
                    d7.displayDay(record);
                    break;
                case 8:
                    Console.WriteLine("Invalid Option!!!");
                    goto toptime;
            }
            // Datetime implementation
            // time:
            // Console.Write($"What time on {reserve_day}?: ");
            // int time = Convert.ToInt32(Console.ReadLine());
            // if (time >= 1 && time < 12)
            // {
            //     Console.WriteLine($"Booked Table at {time}:00am...");
            // }
            // else if (time >= 12 && time <= 24)
            // {
            //     Console.WriteLine($"Booked Table at {time}:00pm...");
            // }
            // else
            // {
            //     Console.WriteLine("Invalid Time Input!!");
            //     goto time;
            // }
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

sealed public class Calculations : ICalculate
{
    public double iavgRatings(RestrauntModel record)
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


// OCP Implementation
public class Days 
{
    public virtual void displayDay(RestrauntModel op)
    {
        if (op.operating_hours.Saturday == "Closed") // 3b variable rule
        {
            Console.WriteLine("Restaurant is Closed");
        }
        Console.WriteLine($"The timings");
    }
}

public sealed class Monday : Days
{
    public override void displayDay(RestrauntModel op)
    {
        if (op.operating_hours.Monday == "Closed")
        {
            Console.WriteLine("Restaurant is Closed");
        }
        else
        {
            Console.WriteLine($"The timings for Monday are {op.operating_hours.Monday}");
        }

    }
}
public sealed class Tuesday : Days
{
    public override void displayDay(RestrauntModel op)
    {
        if (op.operating_hours.Tuesday == "Closed")
        {
            Console.WriteLine("Restaurant is Closed");
        }
        else
        {
            Console.WriteLine($"The timings for Tuesday are {op.operating_hours.Tuesday}");
        }

    }
}
public sealed class Wednesday : Days
{
    public override void displayDay(RestrauntModel op)
    {
        if (op.operating_hours.Wednesday == "Closed")
        {
            Console.WriteLine("Restaurant is Closed");
        }
        else
        {
            Console.WriteLine($"The timings for Wednesday are {op.operating_hours.Wednesday}");
        }

    }
}
public sealed class Thursday : Days
{
    public override void displayDay(RestrauntModel op)
    {
        if(op.operating_hours.Thursday == "Closed")
        {
            Console.WriteLine("Restaurant is Closed");
        }
        else
        {
            Console.WriteLine($"The timings for Thursday are {op.operating_hours.Thursday}");
        }

    }
}
public sealed class Friday : Days
{
    public override void displayDay(RestrauntModel op)
    {
        if (op.operating_hours.Friday == "Closed")
        {
            Console.WriteLine("Restaurant is Closed");
        }
        else
        {
            Console.WriteLine($"The timings for Friday are {op.operating_hours.Friday}");
        }
    }
}
public sealed class Saturday : Days

{
    public override void displayDay(RestrauntModel op)
    {
        if (op.operating_hours.Saturday == "Closed")
        {
            Console.WriteLine("Restaurant is Closed");
        }
        else
        {
            Console.WriteLine($"The timings for Saturday are {op.operating_hours.Saturday}");
        }
    }
}
public sealed class Sunday : Days
{
    public override void displayDay(RestrauntModel op)
    {
        if (op.operating_hours.Sunday == "Closed")
        {
            Console.WriteLine("Restaurant is Closed");
        }
        else
        {
            Console.WriteLine($"The timings for Sunday are {op.operating_hours.Sunday}");
        }
    }
}

