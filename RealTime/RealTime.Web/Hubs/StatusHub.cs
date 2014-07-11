using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using RealTime.Web.Models;

namespace RealTime.Web.Hubs
{
    public class StatusHub : Hub
    {
        private static List<Status> _statuses { get; set; }

        static StatusHub()
        {
            _statuses = new List<Status>();
        }

        public void AddStatus(Status newStatus)
        {
            newStatus.Id = _statuses.Any() ? _statuses.Max(p => p.Id) + 1 : 0;
            newStatus.Liked = new List<string>();
            newStatus.StatusDate = DateTime.UtcNow;

            _statuses.Add(newStatus);
            Clients.All.OnNewStatus(newStatus);
        }

        public void LikeStatus(int id)
        {
            var status = _statuses.FirstOrDefault(p => p.Id == id);
            if (status != null)
                status.Liked.Add(Context.ConnectionId);

            Clients.All.OnStatusLike(id, Context.ConnectionId);
        }

        public List<Status> GetStatuses()
        {
            return _statuses;
        }
    }
}