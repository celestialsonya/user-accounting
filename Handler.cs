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
                    
                    // checking for the identity of the name:
                    List<User>? usersList = this.GetUsers();
                    
                    User? userFinded = this.storage.CheckName(usersList!, name);
                    if (userFinded != null)
                    {
                        Console.WriteLine("User with this name already exist");
                        break;
                    }
                    
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
                
                case "delete user":
                    
                    List<User>? usersAll = this.GetUsers();
                    if (usersAll == null)
                    {
                        Console.WriteLine("Users have not been created yet:(\n");
                        break;
                    }
                    
                    string deletedName = this.GetConsoleValue(NAME);
                    string deletedPassword = this.GetConsoleValue(PASSWORD);
                    
                    var deletedUser = this.DeleteUser(deletedName, deletedPassword, usersAll);

                    if (deletedUser == null)
                    {
                        break;
                    }
                    
                    Console.WriteLine("This user has been deleted, id: {0}, name: {1}",
                        deletedUser?.id, deletedUser?.name);
                    
                    break;
                
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

    private User? DeleteUser(string name, string password, List<User> users)
    {
        User? user = this.storage.CheckName(users, name);
        if (user != null)
        {
            bool ok = this.storage.CheckPassword(user, password);
            if (ok)
            {
                return this.storage.DeleteUser(name, password, users);
            }

            if (!ok)
            {
                Console.WriteLine("Incorrect password");
                return null;
            }
        }
        Console.WriteLine("Icorrect name");
        return null;
    }
    
}