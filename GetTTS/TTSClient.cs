using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GetTTS
{
    public static class TTSClient
    {
        public delegate void DownloadFinished(TTSWord word, int result);

        public static void Download(TTSWord word, DownloadFinished downloadFinished)
        {
            WebClient client = new WebClient();

            client.Headers["Accept"] = "*/*";
            client.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) " +
                "AppleWebKit/537.36 (KHTML, like Gecko) " + "Chrome/70.0.3538.110 Safari/537.36";

            Uri wordUri = new Uri(String.Format("http://dict.youdao.com/dictvoice?audio={0}&type={1}", word.Word, word.Type));
            try
            {
                client.DownloadFile(wordUri, word.AudioFileName);
                downloadFinished(word, 1);
            }
            catch (Exception ex)
            {
                downloadFinished(word, 2);
            }
        }
        
    }
}
