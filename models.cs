
class User
{
    public int Id;
    public string Username;
    public byte[] Password;

    

    public User(int id, string username, byte[] password)
    {
        Id = id;
        Username = username;
        Password = password;
    }
}

class Password 
{
    public int Id;
    public string Page;
    public string Pass;
    public string User;

    public Password(int id, string page, string pass, string user)
    {
        Id = id;
        Page = page;
        Pass = pass;
        User = user;
    }
}
