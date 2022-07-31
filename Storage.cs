using System.Text.Json;

namespace UserStorageConsole;

public class Storage
{
    
    private const string FILE = @"C:\Users\Сонечка\RiderProjects\Storage.txt";
    
    private async void writeText(string json)
    {
        using (StreamWriter writter = new StreamWriter(FILE, true))
        {
            await writter.WriteLineAsync(json);
        }
    }
    
    public void GetAll()
    {
        string[] json = File.ReadAllText(FILE).Trim().Split("\n");
        
        foreach (var u in json)
        {
            User? user = JsonSerializer.Deserialize<User>(u);
            Console.WriteLine($"id: {user?.id}, name: {user?.name}");
        }

        if (json.Length == 0)
        {
            Console.WriteLine("Users have not been created yet:(\n");
        }
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