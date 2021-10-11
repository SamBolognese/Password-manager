using System;
using System.IO;
using System.Security.Cryptography;

namespace PG1_uppgift
{

    public class Structure
    {
        //A string to represent the users input and a string array to save the user inputs.
        private string[] UserCommandWords;
        private string LoggedInAs;
        //Constructor, takes a string as input from the user and sets it as UserCommand
        public Structure(string[] args)
        {
            ReadInput(args);
        }

        //Splits the user input into a string array
        private void ReadInput(string[] args)
        {
            UserCommandWords = args;
            InputHandling();        
        }      

        //
        private void InputHandling()
        {
            Client client = Client.JSONreader(UserCommandWords[1]);
            Server server = Server.JSONreader(UserCommandWords[2]);

            switch (UserCommandWords[0])
            {
                case "init":
                    if (UserCommandWords.Length == 3)
                    {
                        Console.WriteLine("Choose your password: ");
                        string pwd = Console.ReadLine();
                        Array.Resize(ref UserCommandWords, UserCommandWords.Length + 1);
                        UserCommandWords[UserCommandWords.Length - 1] = pwd;

                        Init init = new Init(UserCommandWords);
                    }
                    else
                    {
                        ErrorMessage();
                    }
                    break;
                case "login":
                    if(UserCommandWords.Length == 3)
                    {   
                        if (File.Exists(Directory.GetCurrentDirectory() + $@"\{UserCommandWords[2]}"))
                        {
                            if (PasswordPrompt() && SecretKeyPrompt())
                            {
                                LoggedInAs = UserCommandWords[1];
                            }
                        }
                        else
                        {
                            Console.WriteLine("The server does not exist");
                        }
                        PasswordPrompt();
                        Console.WriteLine("SecretKey: ");

                    }
                    else
                    {
                        ErrorMessage();
                    }
                    break;
                case "get":
                    if (UserCommandWords.Length == 3)
                    {
                        PasswordPrompt();
                        if (isLoggedIn() || SecretKeyPrompt())
                        {
                            
                        }

                    }
                    if (UserCommandWords.Length == 4)
                    {
                        PasswordPrompt();
                        if (isLoggedIn() || SecretKeyPrompt())
                        {

                        }
                    }
                    else
                    {
                        ErrorMessage();
                    }
                    break;
                case "set":
                    if (UserCommandWords.Length == 5)
                    {
                        PasswordPrompt();
                        if (isLoggedIn() || SecretKeyPrompt())
                        {

                        }
                    }
                    else
                    {
                        ErrorMessage();
                    }
                    break;
                case "drop":
                    if (UserCommandWords.Length == 5)
                    {
                        PasswordPrompt();
                        if (isLoggedIn() || SecretKeyPrompt())
                        {

                        }
                    }
                    else
                    {
                        ErrorMessage();
                    }
                    break;
                case "secret":
                    if (UserCommandWords.Length == 2)
                    {
                        foreach(var x in client.secretKey)
                        {
                            Console.Write(x);
                        }
                    }
                    else
                    {
                        ErrorMessage();
                    }
                    break;
                default:
                    ErrorMessage();
                    break;
            }
        }

        //Displays error message for user and let's the user type in new command.
        private void ErrorMessage()
        {
            Console.WriteLine("You did not enter valid command");
        }

        //A method to display password prompt and then check if the password is correct.
        private bool PasswordPrompt()
        {
            Console.Write("Master password: ");
            string masterPassword = Console.ReadLine();

            Client client = Client.JSONreader(UserCommandWords[1]);
            Server server = Server.JSONreader(UserCommandWords[2]);
            byte[] secretKey = client.secretKey;
            byte[] IV = server.InitializingVector;

            Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(masterPassword, secretKey);
            byte[] valvekey = rfc2898.GetBytes(16);

            //If master password and secret key , together with the initialization vector
            //stored in < server > cannot be used to decrypt the vault in < server > then the
            //command is aborted and an error message is printed.

            return false;
        }

        //Prompts user to enter Secret key and compares against the SecreyKey stored in Client
        private bool SecretKeyPrompt()
        {
            Console.Write("Enter secret key: ");
            string text = Console.ReadLine();
            string compare = string.Empty;
            Console.WriteLine();

            Client newClient = Client.JSONreader(UserCommandWords[1]);
            foreach(var x in newClient.secretKey)
            {
                compare += x;
            }
            if (compare == text)
            {
                return true;
            }
            return false;

        }

        //Compares two different byte-arrays and returns true if they are the same, false if they are different.
        private bool Compare(byte[] x, byte[] y)
        {
            if(x.Length != y.Length)
            {
                return false;
            }
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != y[i])
                {
                    return false;
                }
            }
            return true;
        }

        // Method to check if user is logged in
        private bool isLoggedIn()
        {
            if(LoggedInAs == UserCommandWords[1])
            {
                return true;
            }
            return false;
        }
    }
}
