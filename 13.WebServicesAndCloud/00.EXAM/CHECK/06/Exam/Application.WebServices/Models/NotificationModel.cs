using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.WebServices.Models
{
    public class NotificationModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int Type { get; set; }

        public string State { get; set; }

        public string RedPlayeId { get; set; }

        public string BluelayeId { get; set; }
    }
}