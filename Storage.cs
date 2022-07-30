namespace UserStorageConsole;

public class Storage
{
    private Dictionary<int, User> data = new Dictionary<int, User>();
    
    public Dictionary<int, User> GetAll()
    {
        if (data.Count == 0)
        {
            Console.WriteLine("Users have not been created yet:(\n");
        }
        
        return this.data;
    }

    public int CreateUser(string name, string password)
    {
        // create user and id:
        User user = new User(name, password);

        // add a user to storage:
        int uniqueIdx = this.data.Count + 1;
        this.data.Add(uniqueIdx, user);

        return user._id;
    }
    
}