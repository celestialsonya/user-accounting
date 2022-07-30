namespace UserStorageConsole
{
    static class App
    {

        static void Main(string[] args)
        {
            
            var storage = new Storage();
            var handler = new Handler(storage);
            
            handler.Start();

        }

    }
}