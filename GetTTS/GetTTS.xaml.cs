using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GetTTS
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            String word = WordInput.Text;
            int type = (AmericanEnglishRadioButton.IsChecked == true) ? 2 : 1;

            TTSWord ttsWord = new TTSWord(word, type);

            WordList.Items.Add(ttsWord);

            WordInput.Clear();

            TTSClient.Download(ttsWord, this.DownloadFinished);
        }

        private void WordValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("清空当前列表（不会删除已下载的发音文件）？", "确认", 
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                WordList.Items.Clear();
            }
        }

        private void DownloadAgainButton_Click(object sender, RoutedEventArgs e)
        {
            TTSWord ttsWord = WordList.SelectedItem as TTSWord;
            TTSClient.Download(ttsWord, this.DownloadFinished);
            
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            TTSWord ttsWord = WordList.SelectedItem as TTSWord;
            mediaPlayer.Open(new Uri(ttsWord.AudioFileName));
            mediaPlayer.MediaEnded += delegate { mediaPlayer.Close(); };
            mediaPlayer.Play();
           
        }

        public void DownloadFinished(TTSWord word, int result)
        {
            word.Status = result;
            WordList.Items.Refresh();
        }
    }

}
