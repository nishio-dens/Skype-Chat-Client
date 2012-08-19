﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SKYPE4COMLib;
using AxSKYPE4COMLib;
using System.Windows.Forms;

namespace SkypeChatClient
{
    public static class ChatClientExtensions
    {
    }

    public class ChatClient
    {
        AxSkype Skype { get; set; }

        IList<IChatMessage> ReceivedMessages { get; set; }

        public ChatClient(AxSkype skype)
        {
            Skype = skype;
            ReceivedMessages = new List<IChatMessage>();
        }

        /// <summary>
        /// スカイプとの連携を開始します。
        /// </summary>
        /// <param name="skype"></param>
        public void AttachToSkypeClient()
        {
            if (!Skype.Client.IsRunning)
            {
                if (MessageBox.Show("Skypeを起動してもよろしいですか？", "Client", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Skype.Client.Start();
                }
                else
                {
                    throw new InvalidOperationException("Skypeを起動していないため、連携に失敗しました。");
                }
            }
            try
            {
                Skype.Attach();
                SetChatMessageStatusEventHandler();
            }
            catch
            {
                MessageBox.Show(
                    "Skypeと連携できませんでした。本プログラムからSkypeにアクセスできるよう許可してください。",
                    "Error");
            }
        }

        void SetChatMessageStatusEventHandler()
        {
            Skype.MessageStatus += new AxSKYPE4COMLib._ISkypeEvents_MessageStatusEventHandler((_, statusEvent) =>
            {
                var chat = statusEvent.pMessage as ChatMessageClass;
                if (ReceivedMessages.Where(i => i.Id == chat.Id).Any())
                {
                    return;
                }
                ReceivedMessages.Add(chat);
            });
        }

        /// <summary>
        /// オンラインユーザのハンドル名を取得します。
        /// </summary>
        /// <returns></returns>
        public IEnumerable<String> GetOnlineUserNandleName()
        {
            var handles = new List<String>();
            foreach (Group group in Skype.Groups)
            {
                foreach (User user in group.OnlineUsers)
                {
                    handles.Add(user.Handle);
                }
            }

            return handles.Distinct()
                .OrderBy(s => s);
        }

        public void ReloadAllMessages()
        {
            throw new NotImplementedException();
        }

    }
}
