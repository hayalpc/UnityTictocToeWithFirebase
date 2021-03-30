using System.Collections;
using System.Collections.Generic;

public class UserData : Singleton<UserData>
{
    public GameState GameState { get; set; }

    public string UserId { get; set; }
    public string Username { get; set; }
    public int Score { get; set; }
    
}
