using UserStorageConsole.errors;

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
        Console.WriteLine("App has started!!\n");
        while (true)
        {
            string? str = Console.ReadLine();

            switch (str)
            {
                case null:
                    Console.WriteLine("Incorrect input\n");
                    continue;
                
                case "create user":

                    try
                    {
                        string name = this.GetConsoleValue(NAME);

                        // checking for the identity of the name:
                        User? found = this.storage.GetByName(name);
                        
                        if (found != null)
                        {
                            throw new UserAlreadyExistException();
                        }

                        string password = this.GetConsoleValue(PASSWORD);
                        
                        int id = this.CreateUser(name, password);

                        Console.WriteLine("The user has been created, id: {0}\n", id);
                        Console.Write("If you want to see users send 'get users'\n");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    break;
                
                case "get users":
                    
                    try
                    {
                        List<User> users = this.GetUsers();

                        foreach (User user in users)
                        {
                            Console.WriteLine($"id: {user.id}, name: {user.name}");
                        }
                        Console.Write("\n");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    
                    break;
                
                case "quit":
                    Console.WriteLine("bye!!\n");
                    return;
                
                case "delete user":

                    try
                    {
                        string delName = this.GetConsoleValue(NAME);
                        string delPassword = this.GetConsoleValue(PASSWORD);
                    
                        User deletedUser = this.DeleteUser(delName, delPassword);

                        Console.WriteLine("This user has been deleted, id: {0}, name: {1}\n",
                            deletedUser.id, deletedUser.name);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    
                    break;

                default:
                    Console.WriteLine("Unknown command\n");
                    break;
            }
        }
    }

    private int CreateUser(string name, string password)
    {
        return this.storage.CreateUser(name, password);
    }
    
    private List<User> GetUsers()
    {
        List<User> users = this.storage.GetAll();
        if (users.Count == 0)
        {
            throw new NoUsersException();
        }

        return users;
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

    private User DeleteUser(string name, string password)
    {
        User? candidate = this.storage.isExist(name);
        if (candidate != null)
        {
            bool ok = this.storage.CheckPassword(candidate, password);
            if (ok)
            {
                this.storage.DeleteUser(candidate.id);
                return candidate;
            }

            if (!ok)
            {
                throw new InvalidPasswordException();
            }
        }

        throw new UserDoesNotExistException();
    }
    
}