using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AxSKYPE4COMLib;
using SKYPE4COMLib;

namespace SkypeChatClient
{
    public static class ChatClientExtensions
    {
        /// <summary>
        /// 指定したチャット内の全メッセージを取得します。
        /// </summary>
        /// <param name="chat"></param>
        /// <returns></returns>
        public static IEnumerable<IChatMessage> GetMessages(this Chat chat)
        {
            var messages = new List<IChatMessage>();
            foreach (IChatMessage message in chat.Messages)
            {
                messages.Add(message);
            }
            return messages;
        }

        public static string GetFormattedMessage(this IChatMessage message)
        {
            return String.Format("{0:00}/{1:00}/{2:00}:{3:00} {4, -15}: {5}",
                message.Timestamp.Month,
                message.Timestamp.Day,
                message.Timestamp.Hour,
                message.Timestamp.Minute,
                message.FromHandle,
                message.Body);
        }
    }

    public class ChatClient
    {
        AxSkype Skype { get; set; }

        IList<IChatMessage> ReceivedMessages { get; set; }
        IList<Chat> ChatGroups { get; set; }

        public ChatClient(AxSkype skype)
        {
            Skype = skype;
            ReceivedMessages = new List<IChatMessage>();
            ChatGroups = new List<Chat>();
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

        /// <summary>
        /// 指定したBlobをもつチャットルームに対してメッセージを発信します。
        /// </summary>
        /// <param name="selectedBlob"></param>
        /// <param name="text"></param>
        public void SendMessage(string selectedBlob, string text)
        {
            var chat = ReceivedMessages.FirstOrDefault(i => i.Chat.Blob == selectedBlob);
            if (chat != null)
            {
                chat.Chat.SendMessage(text);
            }
            else
            {
                throw new InvalidOperationException("発言先が存在しませんでした。");
            }
        }

        /// <summary>
        /// メッセージ本文、ハンドル名、タイムスタンプに指定したキーワードを含んでいるメッセージを返します。
        /// </summary>
        /// <param name="blob"></param>
        /// <param name="searchWord"></param>
        /// <returns></returns>
        public IEnumerable<IChatMessage> SearchMessages(string searchWord)
        {
            return ReceivedMessages
                .Where(i => i.Body.Contains(searchWord) ||
                    i.FromHandle.Contains(searchWord) ||
                    i.Timestamp.ToString().Contains(searchWord))
                .OrderBy(i => i.Timestamp);
        }

        /// <summary>
        /// メッセージ本文、ハンドル名、タイムスタンプに指定したキーワードを含んでいるメッセージを返します。
        /// </summary>
        /// <param name="blob"></param>
        /// <param name="searchWord"></param>
        /// <returns></returns>
        public IEnumerable<IChatMessage> SearchMessages(string blob, string searchWord)
        {
            return SearchMessages(searchWord)
                .Where(i => i.Chat.Blob == blob);
        }

        public IEnumerable<string> GetBlobs()
        {
            return ReceivedMessages.Select(i => i.Chat.Blob)
                .Distinct();
        }

        public void ReloadAllMessages()
        {
            UpdateChatGroups();
            ReceivedMessages = ChatGroups.SelectMany(c => c.GetMessages())
                .ToList();
        }

        void UpdateChatGroups()
        {
            ChatGroups.Clear();
            foreach (Chat chat in Skype.Chats)
            {
                ChatGroups.Add(chat);
            }
        }
    }
}
