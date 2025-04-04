

using System.Data.Common;
using Azure.Identity;
using Microsoft.Identity.Client;

class PasswordManager
{
    public string SecretKey;
    public bool Run;
    public Database Data;

    public string Loggeduser;

    Hash h = new Hash();

    public PasswordManager(string secretKey, Database data, bool run = true, string loggeduser = "")
    {
        SecretKey = secretKey;
        Run = run;
        Data = data;
        Loggeduser = loggeduser;
    }   


    public void Readpass(string username, string secret) {
        Data.get_passwords(username, secret);
    }

    public void Writepass() {
        int id;
        string page;
        string password;
        Console.WriteLine("Writepass");

        id = Convert.ToInt32(Data.max_id_pass("passwords")) + 1;

        Console.WriteLine("Add a page/app:");
        page = Console.ReadLine();

        Console.WriteLine("Add a password:");
        password = Console.ReadLine();

        password = EncryptionHelper.EncryptString(password, SecretKey);

        Console.WriteLine(Loggeduser);
        Data.insert_pass(id, page, password, Loggeduser);

    }

    public void Login() {
        string username;
        string password;
        int id;

        Hash h = new Hash();

        Console.WriteLine("Wat's your plan: Login: 1, Sign up: 2");
        var service = Console.ReadLine();
        switch(service)
        {
            case "1":
                Console.WriteLine("You selected 1 User Login!");

                Console.WriteLine("Add your username:");
                username = Console.ReadLine();
                Console.WriteLine(Data.check_user(username));
                if (Data.check_user(username))
                {
                    Console.WriteLine("Username is ok!");
                }
                else 
                {
                    Console.WriteLine("Invalid Username!");
                    Login();
                    break;
                }                

                Console.WriteLine("Add your password:");
                password = Console.ReadLine();

                byte[] hash = h.CalculateSHA256(password);
                Console.WriteLine(Data.check_pass(username, hash));
                if (Data.check_pass(username, hash))
                {
                    Console.WriteLine("Password is ok!");
                    Loggeduser = username;
                }  
                else 
                {   
                    Console.WriteLine("Invalid Password!");
                    Login();
                    break;
                }

                break;
            case "2":
                Console.WriteLine("You selected 2 Sign up!");

                Console.WriteLine("Add username:");
                username = Console.ReadLine();

                Console.WriteLine("Add password:");
                password = Console.ReadLine();

                id = Convert.ToInt32(Data.max_id("Users")) +1 ;

                byte[] has = h.CalculateSHA256(password);
                var user = new User(id, username, has); 
                Console.WriteLine(user.Username, user.Password);

                Data.insert_user(id, user.Username, user.Password);
                break;
            default:
                Console.WriteLine("You add invalid input!");
                break;
        }

    }

}


