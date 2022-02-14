namespace Resto;

interface iDispRestos // Displays items from database
{
    public void iDisplayRes();
    public void iDisplayTimes(OperatingModel stuff);
}

interface iReservation
{
    public void iReserve();
}

interface iCalculations
{
    public double iAvgRatings(RestrauntModel record);
}

public class iOverrideDispResto : iDispRestos
{
    
    private string[] days = new[]{"Sunday", "Monday", "Tueday", "Wednesday", "Thursday", "Friday", "Saturday"};
    MongoConnection db = new MongoConnection();
    public void iDisplayRes()
    {
        List<RestrauntModel> recs = db.LoadAllRecords<RestrauntModel>("restos");
        iCalc c = new iCalc();
        Console.WriteLine("List of Restraunts : ");
        for (int i = 0; i < recs.Count; i++)
        {
            Console.Write($"\t{recs[i].id}.{recs[i].name} ");
            Console.Write("----");
            Console.WriteLine($" Rating : {c.iAvgRatings(recs[i])}\n");
            
        }
    }

    public void iDisplayTimes(OperatingModel stuff)
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
public class iCalc : iCalculations
{
    public double iAvgRatings(RestrauntModel record)
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

public class iOverrideReservation : iReservation
{
    private MongoConnection db = new MongoConnection();
    
    public void iReserve()
    {
        Console.WriteLine("RESERVATION DETAILS");
        // pick restraunts for reservation
        iOverrideDispResto d = new iOverrideDispResto();
        up:
        d.iDisplayRes();
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
                    iDays d1 = new iMonday();
                    d1.displayDay(record);
                    break;
                case 0 :
                    d.iDisplayTimes(record.operating_hours);
                    break;
                case 2:
                    iDays d2 = new iTuesday();
                    d2.displayDay(record);
                    break;
                case 3:
                    iDays d3 = new iWednesday();
                    d3.displayDay(record);
                    break;
                case 4:
                    iDays d4 = new iThursday();
                    d4.displayDay(record);
                    break;
                case 5:
                    iDays d5 = new iFriday();
                    d5.displayDay(record);
                    break;
                case 6:
                    iDays d6 = new iSaturday();
                    d6.displayDay(record);
                    break;
                case 7:
                    iDays d7 = new iSunday();
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
public class iDays
{
    public virtual void displayDay(RestrauntModel op)
    {
        Console.WriteLine($"The timings");
    }
}

public sealed class iMonday : iDays
{
    public override void displayDay(RestrauntModel op)
    {
        Console.WriteLine($"The timings for Monday are {op.operating_hours.Monday}");
    }
}
public sealed class iTuesday : iDays
{
    public override void displayDay(RestrauntModel op)
    {
        Console.WriteLine($"The timings for Tuesday are {op.operating_hours.Tuesday}");
    }
}public sealed class iWednesday : iDays
{
    public override void displayDay(RestrauntModel op)
    {
        Console.WriteLine($"The timings for Wednesday are {op.operating_hours.Wednesday}");
    }
}public sealed class iThursday : iDays
{
    public override void displayDay(RestrauntModel op)
    {
        Console.WriteLine($"The timings for Thursday are {op.operating_hours.Thursday}");
    }
}public sealed class iFriday : iDays
{
    public override void displayDay(RestrauntModel op)
    {
        Console.WriteLine($"The timings for Friday are {op.operating_hours.Friday}");
    }
}public sealed class iSaturday : iDays
{
    public override void displayDay(RestrauntModel op)
    {
        Console.WriteLine($"The timings for Saturday are {op.operating_hours.Saturday}");
    }
}public sealed class iSunday : iDays
{
    public override void displayDay(RestrauntModel op)
    {
        Console.WriteLine($"The timings for Sunday are {op.operating_hours.Sunday}");
    }
}
