namespace UserStorageConsole;

public class User
{
    private string _name;
    private string _password;
    public int _id { get; private set; }
    
    public User(string name, string password)
    {
        this._name = name;
        this._password = password;
        this._id = DateTime.Now.GetHashCode();
    }

    public void PrintValues()
    {
        Console.WriteLine("id: {0}, name: {1}, password: {2}",this._id,this._name,this._password);

    }

}