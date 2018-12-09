using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GetTTS
{
    public class TTSWord: INotifyPropertyChanged
    {
        private int status = 0;

        public TTSWord(String word, int type)
        {
            Word = word;
            Type = type;
            AudioFileName = Path.Combine(TTSWord.GetBaseDirectory(), String.Format("{0}.mp3", Word));
        }
        public String Word { get; set; }
        public int Type { get; set;  }
        public String ContentType { get; set; }
        public String AudioFileName { get;}
        public int Status
        {
            get {
                return this.status;
            }
            set
            {
                if (value != this.status)
                {
                    this.status = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public String TypeDesc
        {
            get
            {
                return Type == 1 ? "英式" : "美式";
            }
        }

        public String StatusImage
        {
            get
            {
                switch(this.Status)
                {
                    case 1: return "Resources/checkmark_16.png";
                    case 2: return "Resources/x_mark_16.png";
                    default: return "Resources/download_16.png";
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public static String GetBaseDirectory()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "单词语音");
        }
    }
}
