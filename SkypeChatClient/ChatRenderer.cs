using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkypeChatClient
{
    public class ChatRenderer : MessageReceivedListener
    {
        MainForm MainWindow { get; set; }
        public ChatRenderer(MainForm mainWindow)
        {
            MainWindow = mainWindow;
        }

        public void ActionWhenReceivedMessage(SKYPE4COMLib.IChatMessage message)
        {
            MainWindow.AppendDecoratedChatMessage(message.Chat.Blob, message);
        }
    }
}
