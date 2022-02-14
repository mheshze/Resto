namespace Resto;

public abstract class absDispRestos // Displays items from database
{
    public abstract void abDisplayRes();
    public abstract void abDisplayTimes(OperatingModel stuff);
}

public abstract class absReservation
{
    public abstract void absReserve();
}

public abstract class absCalculations
{
    public abstract double absAvgRatings(RestrauntModel record);
}

public class OverrideDispResto : absDispRestos
{
    
    private string[] abDays = new[]{"Sunday", "Monday", "Tueday", "Wednesday", "Thursday", "Friday", "Saturday"};
    MongoConnection db = new MongoConnection();
    public override void abDisplayRes()
    {
        List<RestrauntModel> recs = db.LoadAllRecords<RestrauntModel>("restos");
        OverrideCalculations c = new OverrideCalculations();
        Console.WriteLine("List of Restraunts : ");
        for (int i = 0; i < recs.Count; i++)
        {
            Console.Write($"\t{recs[i].id}.{recs[i].name} ");
            Console.Write("----");
            Console.WriteLine($" Rating : {c.absAvgRatings(recs[i])}\n");
            
        }
    }

    public override void abDisplayTimes(OperatingModel stuff)
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
public class OverrideCalculations : absCalculations
{
    public override double absAvgRatings(RestrauntModel record)
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

public class OverrideReservation : absReservation
{
    private MongoConnection db = new MongoConnection();
    
    public override void absReserve()
    {
        Console.WriteLine("RESERVATION DETAILS");
        // pick restraunts for reservation
        OverrideDispResto d = new OverrideDispResto();
        up:
        d.abDisplayRes();
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
            // string reserve_day = Console.ReadLine();
            switch (day)
            {
                case 1:
                    abDays d1 = new abMonday();
                    d1.abdisplayDay(record);
                    break;
                case 0 :
                    d.abDisplayTimes(record.operating_hours);
                    break;
                case 2:
                    abDays d2 = new abTuesday();
                    d2.abdisplayDay(record);
                    break;
                case 3:
                    abDays d3 = new abWednesday();
                    d3.abdisplayDay(record);
                    break;
                case 4:
                    abDays d4 = new abThursday();
                    d4.abdisplayDay(record);
                    break;
                case 5:
                    abDays d5 = new abFriday();
                    d5.abdisplayDay(record);
                    break;
                case 6:
                    abDays d6 = new abSaturday();
                    d6.abdisplayDay(record);
                    break;
                case 7:
                    abDays d7 = new abSunday();
                    d7.abdisplayDay(record);
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
} // Responsibility 3 - Reservation


// OCP Implementation
public abstract class abDays
{
    public abstract void abdisplayDay(RestrauntModel op);
}

public sealed class abMonday : abDays
{
    public override void abdisplayDay(RestrauntModel op)
    {
        Console.WriteLine($"The timings for Monday are {op.operating_hours.Monday}");
    }
}
public sealed class abTuesday : abDays
{
    public override void abdisplayDay(RestrauntModel op)
    {
        Console.WriteLine($"The timings for Tuesday are {op.operating_hours.Tuesday}");
    }
}public sealed class abWednesday : abDays
{
    public override void abdisplayDay(RestrauntModel op)
    {
        Console.WriteLine($"The timings for Wednesday are {op.operating_hours.Wednesday}");
    }
}public sealed class abThursday : abDays
{
    public override void abdisplayDay(RestrauntModel op)
    {
        Console.WriteLine($"The timings for Thursday are {op.operating_hours.Thursday}");
    }
}public sealed class abFriday : abDays
{
    public override void abdisplayDay(RestrauntModel op)
    {
        Console.WriteLine($"The timings for Friday are {op.operating_hours.Friday}");
    }
}public sealed class abSaturday : abDays
{
    public override void abdisplayDay(RestrauntModel op)
    {
        Console.WriteLine($"The timings for Saturday are {op.operating_hours.Saturday}");
    }
}public sealed class abSunday : abDays
{
    public override void abdisplayDay(RestrauntModel op)
    {
        Console.WriteLine($"The timings for Sunday are {op.operating_hours.Sunday}");
    }
}
