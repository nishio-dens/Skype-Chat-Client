using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Growl.Connector;

namespace SkypeChatClient
{
    public class GrowlNotificator : MessageReceivedListener
    {
        public string ApplicationName { get; set; }
        readonly string NotificationType = "Message";
        GrowlConnector Growl { get; set; }

        public GrowlNotificator(string applicationName)
        {
            ApplicationName = applicationName;
            Growl = new GrowlConnector();
            Growl.Register(
                new Growl.Connector.Application(ApplicationName),
                new NotificationType[] { new NotificationType(NotificationType) });
        }

        public void ActionWhenReceivedMessage(SKYPE4COMLib.IChatMessage message)
        {
            var userName = message.FromDisplayName;
            if (String.IsNullOrEmpty(userName))
            {
                userName = message.FromHandle;
            }

            Growl.Notify(new Notification(
                ApplicationName,
                NotificationType,
                DateTime.Now.ToString(),
                String.Format("{0} ({1})", userName, message.Chat.FriendlyName),
                message.Body));
        }
    }
}
