using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Room
{
    public string RoomId { get; set; }
    public string HostId { get; set; }

    public Room(string RoomId,string HostId)
    {
        this.RoomId = RoomId;
        this.HostId = HostId;
    }
}
