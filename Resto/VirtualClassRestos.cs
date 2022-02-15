namespace Resto;

public class virDispRestos // Displays items from database
{
    public virtual void virDisplayRes()
    {
     Console.WriteLine("Displays the list of restaurants!");   
    }

    public virtual void virDisplayTimes(OperatingModel stuff)
    {
        Console.WriteLine("Displays time of all restaurants!");
    }
}

public class virReservation
{
    public virtual void virReserve()
    {
        Console.WriteLine("Helps reserve a table!!");
    }
}

public class virCalculations
{
    public virtual double virAvgRatings(RestrauntModel record)
    {
        Console.WriteLine("Calculates average ratings for each of the restaurants!");
        return 0.00;
    }
}

public class virDispResto : virDispRestos
{
    
    private string[] days = new[]{"Sunday", "Monday", "Tueday", "Wednesday", "Thursday", "Friday", "Saturday"};
    MongoConnection db = new MongoConnection();
    public override void virDisplayRes()
    {
        List<RestrauntModel> recs = db.LoadAllRecords<RestrauntModel>("restos");
        virOvrCalculations c = new virOvrCalculations();
        Console.WriteLine("List of Restaurants : ");
        for (int i = 0; i < recs.Count; i++)
        {
            Console.Write($"\t{recs[i].id}.{recs[i].name} ");
            Console.Write("----");
            Console.WriteLine($" Rating : {c.virAvgRatings(recs[i])}\n");
            
        }
    }

    public override void virDisplayTimes(OperatingModel stuff)
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
}  // Responsibility 1 - Displaying
public class virOvrCalculations : virCalculations
{
    public override double virAvgRatings(RestrauntModel record)
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
} // Responsibility 2 - Calculations

public class virOvrReservation : virReservation
{
    private MongoConnection db = new MongoConnection();
    
    public override void virReserve()
    {
        Console.WriteLine("RESERVATION DETAILS");
        // pick restraunts for reservation
        virDispResto d = new virDispResto();
        up:
        d.virDisplayRes();
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
                    virDays d1 = new virMonday(); // LSP -1 T1
                    d1.displayDay(record);
                    break;
                case 0 :
                    d.virDisplayTimes(record.operating_hours);
                    break;
                case 2:
                    virDays d2 = new virTuesday(); // LSP -1 T2
                    d2.displayDay(record);
                    break;
                case 3:
                    virDays d3 = new virWednesday(); // LSP -1 T3
                    d3.displayDay(record);
                    break;
                case 4:
                    virDays d4 = new virThursday(); // LSP -1 T4
                    d4.displayDay(record);
                    break;
                case 5:
                    virDays d5 = new virFriday(); // LSP -1 T5
                    d5.displayDay(record);
                    break;
                case 6:
                    virDays d6 = new virSaturday(); // LSP -1 T6
                    d6.displayDay(record);
                    break;
                case 7:
                    virDays d7 = new virSunday(); // LSP -1 T7
                    d7.displayDay(record);
                    break;
                case 8:
                    Console.WriteLine("Invalid Option!!!");
                    goto toptime;
            }
            // // Datetime implementation
            // time:
            // Console.Write($"What time on {reserve_day}?: ");
            // int time = Convert.ToInt32(Console.ReadLine());
            // if (time >= 1 && time < 12)
            // {
            // Console.WriteLine($"Booked Table at {time}:00am...");
            // }
            // else if(time >= 12 && time <= 24)
            // {
            // Console.WriteLine($"Booked Table at {time}:00pm...");
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
            
            //Confirmations 
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


// OCP Implementation
public class virDays
{
    public virtual void displayDay(RestrauntModel op)
    {
        Console.WriteLine($"The timings");
    }
}

public sealed class virMonday : virDays
{
    public override void displayDay(RestrauntModel op)
    {
        Console.WriteLine($"The timings for Monday are {op.operating_hours.Monday}");
    }
}
public sealed class virTuesday : virDays
{
    public override void displayDay(RestrauntModel op)
    {
        Console.WriteLine($"The timings for Tuesday are {op.operating_hours.Tuesday}");
    }
}public sealed class virWednesday : virDays
{
    public override void displayDay(RestrauntModel op)
    {
        Console.WriteLine($"The timings for Wednesday are {op.operating_hours.Wednesday}");
    }
}public sealed class virThursday : virDays
{
    public override void displayDay(RestrauntModel op)
    {
        Console.WriteLine($"The timings for Thursday are {op.operating_hours.Thursday}");
    }
}public sealed class virFriday : virDays
{
    public override void displayDay(RestrauntModel op)
    {
        Console.WriteLine($"The timings for Friday are {op.operating_hours.Friday}");
    }
}public sealed class virSaturday : virDays
{
    public override void displayDay(RestrauntModel op)
    {
        Console.WriteLine($"The timings for Saturday are {op.operating_hours.Saturday}");
    }
}public sealed class virSunday : virDays
{
    public override void displayDay(RestrauntModel op)
    {
        Console.WriteLine($"The timings for Sunday are {op.operating_hours.Sunday}");
    }
}

