using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SKYPE4COMLib;
using System.Windows.Forms;

namespace SkypeChatClient
{
    public class Baloon : MessageReceivedListener
    {
        public NotifyIcon NotifyIcon { get; private set; }

        public Baloon(NotifyIcon icon)
        {
            NotifyIcon = icon;
        }

        public void ActionWhenReceivedMessage(IChatMessage message)
        {
            NotifyIcon.BalloonTipTitle = message.Sender.Handle + "(" + message.Chat.FriendlyName + ")";
            NotifyIcon.BalloonTipText = message.Body;
            NotifyIcon.ShowBalloonTip(1000);
        }
    }
}
