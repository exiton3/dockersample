using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using SignalRProgressBarSimpleExample.Hubs;

namespace SignalRProgressBarSimpleExample.Util
{
    public class BulkUpdateProgress
    {
        public BulkUpdateProgress(string progressMessage, int progressCount, int totalItems)
        {
            ProgressMessage = progressMessage;
            ProgressCount = progressCount;
            TotalItems = totalItems;
            Task.Factory.StartNew(() => { });
        }

        public string ProgressMessage { get; private set; }
        public int ProgressCount { get; private set; }
        public int TotalItems { get; private set; }
    }

    public class Functions
    {
        public static void SendProgress(BulkUpdateProgress bulkUpdateProgress)
        {
            //IN ORDER TO INVOKE SIGNALR FUNCTIONALITY DIRECTLY FROM SERVER SIDE WE MUST USE THIS
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ProgressHub>();

            //CALCULATING PERCENTAGE BASED ON THE PARAMETERS SENT
            var percentage = (bulkUpdateProgress.ProgressCount * 100) / bulkUpdateProgress.TotalItems;

            //PUSHING DATA TO ALL CLIENTS
            hubContext.Clients.All.AddProgress(bulkUpdateProgress.ProgressMessage, percentage + "%");
        }
    }
}