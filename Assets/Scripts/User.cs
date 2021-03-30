using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class User
{
    public string username;
    public int level;
    public bool loginStatus;


    public User(string username, int level, bool loginStatus)
    {
        this.username = username;
        this.level = level;
        this.loginStatus = loginStatus;
    }
}
