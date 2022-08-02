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

    public void Start()
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
                    
                    var id = this.CreateUser(name, password);
                    
                    Console.WriteLine("The user has been created, id: {0}\n", id);
                    Console.Write("If you want to see users send 'get users'\n");

                    break;
                
                case "get users":
                    
                    List<User>? users = this.GetUsers();
                    
                    if (users == null)
                    {
                        Console.WriteLine("Users have not been created yet:(\n");
                        break;
                    }

                    foreach (User user in users)
                    {
                        Console.WriteLine($"id: {user.id}, name: {user.name}");
                    }
                    Console.Write("\n");
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
    
    private List<User>? GetUsers()
    {
        return this.storage.GetAll();
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