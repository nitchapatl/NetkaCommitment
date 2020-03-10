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
        public static async Task<List<SendResponse>> SendPushNotificationAsync(List<string> lsRegistationToken) {
            var message = new MulticastMessage()
            {
                Tokens = lsRegistationToken,
                Data = new Dictionary<string, string>()
                {
                    { "score", "850" },
                    { "time", "2:45" },
                },
            };

            var response = await FirebaseMessaging.DefaultInstance.SendMulticastAsync(message);
           return response.Responses.ToList();
        }

        public static async Task<string> SendPushNotificationAsync(string registationToken)
        {
            var message = new Message()
            {
                Token = registationToken,
                Data = new Dictionary<string, string>()
                {
                    { "score", "850" },
                    { "time", "2:45" },
                },
            };

            return await FirebaseMessaging.DefaultInstance.SendAsync(message);
        }
    }
}
