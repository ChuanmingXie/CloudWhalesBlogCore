using Spire.OCR;
using System.IO;
using System.Windows;

namespace CloudWhalesBlogCore.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OcrScanner scanner = new OcrScanner();
            //var text = ImageAddWater.WordsOCR(@"D:\Software\2.png");
            //text += ImageAddWater.WordsOCR(@"D:\Software\16-503.jpg");

            FileStream contentStream = new(@"D:\Software\2.png", FileMode.Open, FileAccess.Read);
            scanner.Scan(contentStream, OCRImageFormat.Png);
            var s = scanner.Text.ToString();
        }
    }
}
