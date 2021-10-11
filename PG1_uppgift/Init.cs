using System;
using System.Security.Cryptography;

namespace PG1_uppgift
{
    class Init
    {
        private string ClientPath;
        private string ServerPath;


        AesManaged aes = new AesManaged();
        RNGCryptoServiceProvider rNG = new RNGCryptoServiceProvider();

        public Init(string[] command)
        {
            ClientPath = command[1];
            ServerPath = command[2];

            byte[] secretKey = new Byte[16];
            rNG.GetBytes(secretKey);

            initClient(secretKey);
            initServer(command, secretKey);
        }

        private void initClient(byte[] secretKey)
        {
            Client client = new Client(secretKey);
            Client.CreateJSON(ClientPath, client);
        }

        private void initServer(string[] command, byte[] secretKey) 
        {
            aes.GenerateIV();
            byte[] IV = aes.IV;

            Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(command[3], secretKey);
            byte[] valveKey = rfc2898.GetBytes(16);
            Vault vault = new Vault();
            Server server = new Server(IV, vault);

            Server.CreateJSON(ServerPath, server);
        }
    }
}
