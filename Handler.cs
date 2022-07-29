namespace UserStorageConsole;

public class Handler
{
    private Dictionary<int,User> storage;
    public Handler(Dictionary<int, User> s)
    {
        this.storage = s;
    }

    public void Start()
    {

        while (true)
        {
            string str = Console.ReadLine();

            if (str == "create user")
            {
                Console.Write("Enter a name: ");
                string name = Console.ReadLine();
                Console.Write("Enter a password: ");
                string password = Console.ReadLine();

                int id = this.CreateUser(name, password);
                Console.WriteLine("The user has been created, id: {0}\n", id);
                
                Console.Write("Do you want to see users? ");
                string response = Console.ReadLine();
                if (response == "yes" || response == "да")
                {
                    this.GetUsers();
                }
            }

            if (str == "get users")
            {
                int count = this.GetUsers();
                if (count == 0)
                {
                    Console.WriteLine("Users have not been created yet:(\n");
                }
            }
            
        }
    }
    
    public int CreateUser(string name, string password)
    {
        // create user:
        User user = new User(name, password);

        // add a user to storage:
        int id = DateTime.Now.GetHashCode();
        this.storage.Add(id, user);
        
        return id;
    }
    public int GetUsers()
    {
        foreach (KeyValuePair<int, User> el2 in this.storage)
        {
            Console.WriteLine("id: {0}, name: {1}, password: {2}", el2.Key, el2.Value.name, el2.Value.password);
        }
        return this.storage.Count;
    }
}