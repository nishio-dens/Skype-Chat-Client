using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SKYPE4COMLib;
using AxSKYPE4COMLib;
using System.Windows.Forms;

namespace SkypeChatClient
{
    public class ChatClient
    {
        AxSkype SkypeClient { get; set; }

        public ChatClient(AxSkype skype)
        {
            SkypeClient = skype;
            AttachToSkypeClient();
        }

        /// <summary>
        /// スカイプとの連携を開始します。
        /// </summary>
        /// <param name="skype"></param>
        public void AttachToSkypeClient()
        {
            if (!SkypeClient.Client.IsRunning)
            {
                if (MessageBox.Show("Skypeを起動してもよろしいですか？", "Client", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SkypeClient.Client.Start();
                }
                else
                {
                    return;
                }
            }
            try
            {
                SkypeClient.Attach();
            }
            catch
            {
                MessageBox.Show(
                    "Skypeと連携できませんでした。本プログラムからSkypeにアクセスできるよう許可してください。",
                    "Error");
            }
        }

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
