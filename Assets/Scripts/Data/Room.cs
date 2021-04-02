using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Room
{
    public string Name { get;set;}
    public string RoomId { get; set; }
    public string HostId { get; set; }

    public Room(string Name,string RoomId,string HostId)
    {
        this.Name = Name;
        this.RoomId = RoomId;
        this.HostId = HostId;
    }
}
