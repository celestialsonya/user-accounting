using System;
using System.ComponentModel;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;

namespace UserStorageConsole
{
    class Program
    {

        static void Main(string[] args)
        {
            
            var storage = new Storage().create();
            var handler = new Handler(storage);
            
            handler.Start();

        }

    }
}


