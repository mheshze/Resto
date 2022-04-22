namespace Resto;

interface IMediator
{
    void Send(Participant fromUser, Participant deliveryClient, string message);
}

class ConcreteMediator : IMediator
{
    private Participant user, delClient, helpline;

    public Participant User
    {
        set { this.user = value; }
    }

    public Participant Delivery_Client
    {
        set { this.delClient = value; }
    }

    public Participant Helpline
    {
        set { this.helpline = value; }
    }

    public void Send(Participant user, Participant deliveryClient, string message)
    {
        if (deliveryClient.Status == "On")
        {
            Console.WriteLine($"[{user.Name}->{deliveryClient.Name}] : {message} Last Posted Message {DateTime.Now}");
            System.Threading.Thread.Sleep(1000);
        }
        else
            Console.WriteLine(
                $"[{user.Name}->{deliveryClient.Name}] : {user.Name}, delivery client is not available, message was not forwarded!");

    }
}

abstract class Participant
{
    protected IMediator _mediator;
    private string name;
    private string status;
    
    public string Name
    {
        get => name;
        set => name = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Status
    {
        get => status;
        set => status = value ?? throw new ArgumentNullException(nameof(value));
    }

    public Participant(IMediator _mediator)
    {
        this._mediator = _mediator; // dependancy injection
    }
}


class User : Participant
{
    public User(IMediator mediator,string name)
    :base(mediator)
    {
        this.Name = name;
        this.Status = "On";
    }

    public void Send(Participant delClient, string message)
    {
        _mediator.Send(this,delClient,message);
    }
}

class DeliveryClient : Participant
{
    public DeliveryClient(IMediator mediator, string name)
    :base(mediator)
    {
        this.Name = name;
        this.Status = "On";
    }

    public void Send(Participant reciever, string message)
    {
        _mediator.Send(this,reciever,message);
    }
}

class Helpline : Participant
{
    public Helpline(IMediator mediator, string name)
        :base(mediator)
    {
        this.Name = name;
        this.Status = "On";
    }

    public void Send(Participant reciever, string message)
    {
        _mediator.Send(this,reciever,message);
    }
}