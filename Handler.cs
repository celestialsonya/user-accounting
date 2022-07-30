namespace UserStorageConsole;

public class Handler
{

    private const string NAME = "name";
    private const string PASSWORD = "password";
    
    private Storage storage;
    public Handler(Storage storage)
    {
        this.storage = storage;
    }

    public async void Start()
    {
        Console.WriteLine("App has started!!");
        while (true)
        {
            string? str = Console.ReadLine();

            switch (str)
            {
                case null:
                    Console.WriteLine("Incorrect input");
                    continue;
                
                case "create user":
                    
                    string name = this.GetConsoleValue(NAME);
                    string password = this.GetConsoleValue(PASSWORD);
                    
                    int id = this.CreateUser(name, password);
                    
                    Console.WriteLine("The user has been created, id: {0}\n", id);
                    Console.Write("If you want to see users send 'get users'\n");

                    break;
                
                case "get users":
                    this.GetUsers();
                    break;
                
                case "quit":
                    Console.WriteLine("bye!!");
                    return;
                
                default:
                    Console.WriteLine("Unknown command");
                    break;
            }
        }
    }

    private int CreateUser(string name, string password)
    {
        return this.storage.CreateUser(name, password);
    }
    private void GetUsers()
    {
        this.storage
            .GetAll().Values
            .ToList()
            .ForEach((user) => user.PrintValues());
       
    }
    private string GetConsoleValue(string v)
    {
        while (true)
        {
            Console.Write("Enter a {0}: ", v);
            string? vv = Console.ReadLine();
            if (vv == null || vv.Trim().Length == 0)
            {
                Console.WriteLine("Incorrect format of {0}", v);
                continue;
            }

            return vv;
        }
    }
    
   
    
    
}