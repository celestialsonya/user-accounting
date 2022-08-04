using System.Text.Json;

namespace UserStorageConsole;

public class Storage
{
    
    private const string FILE = @".\Storage.txt";
    
    private async void writeText(string json)
    {
        using (StreamWriter writter = new StreamWriter(FILE, true))
        {
            await writter.WriteLineAsync(json);
        }
    }
    
    public List<User> GetAll()
    {
        string json = File.ReadAllText(FILE);
        if (json.Length == 0)
        {
            return new List<User>();
        }

        string[] jsonArr = json.Trim().Split("\n");
        
        List<User> users = new List<User>();
        foreach (var u in jsonArr)
        {
            User? user = JsonSerializer.Deserialize<User>(u);
            if (user != null)
            {
                users.Add(user);
            }
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

    public void DeleteUser(int id)
    {
        FileInfo fi = new FileInfo(FILE);
        List<User> users = this.GetAll();
        
        // filtered by id:
        users = users.FindAll(user => user.id != id);
        
        // open file by file info with mode 'truncate':
        var file = fi.Open(FileMode.Truncate);
        using (StreamWriter writter = new StreamWriter(file))
        {
            writter.Dispose();
        }
        
        foreach (User user in users)
        {
            string json = JsonSerializer.Serialize(user);
            this.writeText(json);
        }
    }

    public User? isExist(string name)
    {
        User? user = this.GetByName(name);
        
        if (user != null)
        {
            return user;
        }

        return null;
    }

    public User? GetByName(string name)
    {
        List<User> users = this.GetAll();
        User? user = users.Find(user => user.name == name);

        return user;
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