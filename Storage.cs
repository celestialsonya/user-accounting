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
    
}