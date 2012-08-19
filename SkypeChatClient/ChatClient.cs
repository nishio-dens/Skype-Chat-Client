using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SKYPE4COMLib;

namespace SkypeChatClient
{
    public class ChatClient
    {
        /// <summary>
        /// 指定したチャットに指定したメッセージを送信します。
        /// </summary>
        /// <param name="chat"></param>
        /// <param name="text"></param>
        public static void SendMessage(Chat chat, string text)
        {
            chat.SendMessage(text);
        }
    }
}
