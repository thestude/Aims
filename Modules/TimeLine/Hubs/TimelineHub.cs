using System;
using System.Web;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Microsoft.AspNet.SignalR.Hubs;


namespace AIMS.Modules.TimeLine.Hubs
{
    public class Facility
    {
        public string Name { get; set; }
        public HashSet<string> ConnectionIds { get; set; }
    }

    public class DisplayMessage
    {
        public string Message { get; set; }
        public HashSet<string> Visibility { get; set; }
    }

    [HubName("time")]
    public class TimeLineHub : Hub
    {
        string[] fakeFacilityNames = { "Test Facility 1", "Test Facility 2", "Test Facility 3" };
        Random randomIndex = new Random();

        public void Send(string message, string visibility, string facilityName)
        {
            // Fake group creation NOTE: should be handled during OnConnected
            Groups.Add(Context.ConnectionId, facilityName);

            // Get the username 
            var userName = Context.User.Identity.GetUserName();

            //use the usename to query for facility name


            if (Convert.ToBoolean(visibility))
            {
                //var facilityName = fakeFacilityNames[randomIndex.Next(fakeFacilityNames.Length)];
                //send to faility users only
                Clients.Group(facilityName).AddTimelineEntry(true, message);
            }
            else
            {
                //Send to all
                Clients.All.newMessage(false, message);
            }
        }

        public override Task OnConnected()
        {

            // Get the username 
            var userName = Context.User.Identity.GetUserName();

            //use the usename to query for facility name
            var facilityName = fakeFacilityNames[randomIndex.Next(fakeFacilityNames.Length)];

            var groupName = facilityName;

            //Groups.Add(Context.ConnectionId, groupName);
            return base.OnConnected();
        }

        //public override Task OnDisconnected()
        //{
        //    //Remove Connection from group
        //    return base.OnDisconnected();
        //}
    }
}
