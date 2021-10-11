using System;
using System.IO;
using System.Text.Json;

namespace PG1_uppgift
{
    class Client
    {
        public byte[] secretKey { get; private set; }

        public Client(byte[] secretkey)
        {
            secretKey = secretkey;
        }

        public void getSK()
        {
            foreach(var x in secretKey)
            {
                Console.Write(x);
            }
            Console.WriteLine();    
        }

        public static void CreateJSON(string fileName, Client JSONobject)
        {
            string jsonString = JsonSerializer.Serialize(JSONobject);
            File.WriteAllText(fileName, jsonString);
        }

        public static Client JSONreader(string fileName)
        {
            string jsonString = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<Client>(jsonString);
        }
    }
}
