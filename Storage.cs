using System.Text.Json;

namespace UserStorageConsole;

public class Storage
{
    
    private const string FILE = @"C:\Users\Сонечка\RiderProjects\user-accounting\Storage.txt";
    
    private async void writeText(string json)
    {
        using (StreamWriter writter = new StreamWriter(FILE, true))
        {
            await writter.WriteLineAsync(json);
        }
    }
    
    public List<User>? GetAll()
    {
        string json = File.ReadAllText(FILE);
        if (json.Length == 0)
        {
            return null;
        }

        string[] jsonArr = json.Trim().Split("\n");
        
        List<User> users = new List<User>();
        foreach (var u in jsonArr)
        {
            User? user = JsonSerializer.Deserialize<User>(u);
            users.Add(user!);
        }

        return users;
    }

    public int CreateUser(string name, string password)
    {
        // create user and id:
        User user = new User(name, password);

        // add a user to storage:
        string json = JsonSerializer.Serialize(user);
        this.writeText(json);
            
        return user.id;
    }

    public User DeleteUser(string name, string password, List<User> users)
    {
        User? deletedUser = users.FindAll(user => (user.name == name) && (user.password == password))[0];
        FileInfo fi = new FileInfo(FILE);

        users = users.FindAll(user => user != deletedUser);

        using (StreamWriter writter = new StreamWriter(fi.Open(FileMode.Truncate)))
        {
            writter.Dispose();
        }
        
        foreach (User user in users)
        {
            string json = JsonSerializer.Serialize(user);
            this.writeText(json);
        }
        
        return deletedUser;
    }

    public User? CheckName(List<User> users, string name)
    {
        User? user = users.Find(user => user.name == name);
        
        if (user != null)
        {
            return user;
        }

        return null;
    }
    public bool CheckPassword(User user, string password)
    {
        if (user.password == password)
        {
            return true;
        }

        return false;
    }
}