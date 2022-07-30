using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Dialogs.Controls;

namespace WpfMykImageViewer {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private string? imageFilePath = null;
        private System.Windows.Media.Imaging.BitmapImage bitmapImage = new();

        public MainWindow() => InitializeComponent();

        private void OpenButton(object sender, RoutedEventArgs e) {
            //MessageBox.Show("OpenButton");
            var dialog = new CommonOpenFileDialog();
            var combox = new CommonFileDialogComboBox();
            dialog.Controls.Add(combox);
            //dialog.fi = "Image File(*.bmp,*.jpg,*.png,*.tif)|*.bmp;*.jpg;*.png;*.tif|Bitmap(*.bmp)|*.bmp|Jpeg(*.jpg)|*.jpg|PNG(*.png)|*.png";
            CommonFileDialogResult result = dialog.ShowDialog();
            imageFilePath = dialog.FileName;
            Console.WriteLine(imageFilePath);
            try {
                using (var fs = new FileStream(imageFilePath, FileMode.Open)) {
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = fs;
                    //bitmapImage.DecodePixelWidth = 500;
                    //画像をデコードすることでメモリ消費量を抑えられる。
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.CreateOptions = BitmapCreateOptions.None;
                    bitmapImage.EndInit();
                    bitmapImage.Freeze();
                }
            } catch(Exception ex) {
                System.Console.Error.WriteLine(ex.StackTrace);
                MessageBox.Show("画像ファイルを選んでください");
            }


            image.Source = bitmapImage;
        }
        private void image_Drop(object sender, DragEventArgs e) {
            var fileName = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (fileName == null) return;
        }

    }

    public class UnitTest { 
        public int TestXUnit() => 0;
    }
}
