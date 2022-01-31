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
        Console.WriteLine("List of Restraunts : ");
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
        up:
        virDispResto d = new virDispResto();
        d.virDisplayRes();
        try
        {
            Console.Write("Pick the number : ");
            int num = Convert.ToInt32(Console.ReadLine());
            var record = db.LoadById<RestrauntModel>("restos", num);
            Console.WriteLine($"\n\n\t{record.name.ToUpper()}");
            
            d.virDisplayTimes(record.operating_hours);
            
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
