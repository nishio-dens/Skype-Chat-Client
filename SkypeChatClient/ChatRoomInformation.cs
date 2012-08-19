using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkypeChatClient
{
    public class ChatRoomInformation
    {
        public string Blob { get; set; }
        public string FriendlyRoomName { get; set; }
        public int UnreadCount { get; set; }

        public void ClearUnreadCount()
        {
            UnreadCount = 0;
        }
    }
}
