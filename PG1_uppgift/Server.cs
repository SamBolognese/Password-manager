using System.Text.Json;
using System.IO;

namespace PG1_uppgift
{
    class Server
    {
       public Vault Vault { get; private set; }
       public byte[] InitializingVector { get; private set; }

        public Server(byte[] initializingVector, Vault vault)
        {
            InitializingVector = initializingVector;
            Vault = vault;
        }

        public static void CreateJSON(string fileName, Server JSONobject)
        {
            string jsonString = JsonSerializer.Serialize(JSONobject);
            File.WriteAllText(fileName, jsonString);
        }

        public static Server JSONreader(string fileName)
        {
            string jsonString = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<Server>(jsonString);
        }
    }
}
