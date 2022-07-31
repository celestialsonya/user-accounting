namespace UserStorageConsole;

public class User
{
    public string name { get; set; }
    public string password { get; set; }
    public int id { get; set; }
    
    public User(string name, string password)
    {
        this.name = name;
        this.password = password;
        this.id = DateTime.Now.GetHashCode();
    }

    public void PrintValues()
    {
        Console.WriteLine("id: {0}, name: {1}, password: {2}",this.id,this.name,this.password);

    }

}