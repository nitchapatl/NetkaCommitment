using FirebaseAdmin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetkaCommitment.Common
{
    public static class NotificationHelpers
    {
        public static async Task<List<SendResponse>> SendPushNotificationAsync(List<string> lsRegistationToken, Notification model)
        {
            var response = await FirebaseMessaging.DefaultInstance.SendMulticastAsync(new MulticastMessage()
            {
                Tokens = lsRegistationToken,
                Notification = model
            });
            return response.Responses.ToList();
        }

        public static async Task<string> SendPushNotificationAsync(string registationToken, Notification model)
        {
            return await FirebaseMessaging.DefaultInstance.SendAsync(new Message()
            {
                Token = registationToken,
                Notification = model
            });
        }
    }
}
