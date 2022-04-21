namespace Resto;

interface IDisplays
{
    
    void iDisplay();

    void iDisplayTimings();

}

class Restraunt_1: IDisplays
{
    MongoConnection db = new MongoConnection();
    void IDisplays.iDisplay()
    {
        Console.WriteLine("Restaurant 1");
    }

    void IDisplays.iDisplayTimings()
    {
        Console.WriteLine("Restaurant 1 Timings");
    }
}


class Restraunt_2: IDisplays
{
    void IDisplays.iDisplay()
    {
        Console.WriteLine("Restaurant 2");
    }

    void IDisplays.iDisplayTimings()
    {
        Console.WriteLine("Restaurant 2 Timings");
    }
    
}


class Selector
{
    private IDisplays _display;
    public Selector(IDisplays _display) // Indirectly Calling Interface
    {
        this._display = _display;
    }

    public void displayRestraunts()
    {
        this._display.iDisplay();
    }

    public void displayRestrauntTimings()
    {
        this._display.iDisplayTimings();
    }
}