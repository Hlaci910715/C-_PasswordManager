using System.Data.Common;
using SQLitePCL;

string secretKey = "mysecretpassword"; // Must be 32 bytes for AES-256
var app = new PasswordManager(secretKey, new Database("Server=pc1;Database=Test;Integrated Security=SSPI;Encrypt=False;"));


app.Login();

while (app.Run) {
    Console.WriteLine("What's your plan?\nRead Passwords: 1\nAdd new password: 2\nExit:3");
    Console.Write("Add a number(1,2,3):");
    var service = Console.ReadLine();

    

    switch(service)
    {
        case "1":
            app.Readpass(app.Loggeduser, app.SecretKey);
            break;
        case "2":
            app.Writepass();
            break;
        case "3":
            app.Run = false;
            break;
        default:
            Console.WriteLine("Your choice is invalid!");
            break;
    }
}




