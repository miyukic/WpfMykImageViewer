﻿using System;
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
        private VersionWindow? versionWindow = null;
        private SettingsWindow? settingsWindow = null;

        public MainWindow() => InitializeComponent();

        private void SetImage(string imageFilePath) {
            System.Windows.Media.Imaging.BitmapImage bitmapImage = new();
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
                MessageBox.Show("画像ファイルを選んでください", "開けません。",MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            image.Source = bitmapImage;
            image.Width = bitmapImage.PixelWidth;
        }

        private void OpenButton(object sender, RoutedEventArgs e) {
            //MessageBox.Show("OpenButton");
            var dialog = new CommonOpenFileDialog();
            var combox = new CommonFileDialogComboBox();
            dialog.Controls.Add(combox);
            //dialog. = "Image File(*.bmp,*.jpg,*.png,*.tif)|*.bmp;*.jpg;*.png;*.tif|Bitmap(*.bmp)|*.bmp|Jpeg(*.jpg)|*.jpg|PNG(*.png)|*.png";
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result != CommonFileDialogResult.Ok) return;
            imageFilePath = dialog.FileName;
            Console.WriteLine("imageFilePath = " + imageFilePath);
            SetImage(imageFilePath);
        }
        private void image_Drop(object sender, DragEventArgs e) {
            var fileName = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (fileName == null) return;
            SetImage(fileName[0]);
        }

        private void NewWindowButton(object sender, RoutedEventArgs e) {

        }

        //終了メニュークリックイベントハンドラ
        private void MenuItem_Click_Close(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void Window_Drop(object sender, DragEventArgs e) {
            var fileName = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (fileName == null) return;
            SetImage(fileName[0]);
        }

        //バージョン表示メニュークリックイベントハンドラ
        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            if (versionWindow == null || (versionWindow != null && !versionWindow.IsVisible)) {
                versionWindow = new VersionWindow();
                versionWindow.Owner = this; //子ウィンドウが裏にならないように
                versionWindow.Show();
            } else if (versionWindow != null && versionWindow.IsVisible) {
                return;
            }
        }

        private void Window_Activated(object sender, EventArgs e) {
            Console.WriteLine("MainWindow#Window_Activated");
        }

        private void Window_Deactivated(object sender, EventArgs e) {
            Console.WriteLine("MainWindow#Window_Deativated");
            if (versionWindow != null) {
                Console.WriteLine("versionWindow#Topmost= " + versionWindow.Topmost);
            }
        }

        // マウスホイール
        private void Window_MouseWheel(object sender, MouseWheelEventArgs e) {
            Console.WriteLine(e.Delta);
            if (image == null) return;
            Console.WriteLine("image.Width = " + image.Width);
            if (!((image.Width + e.Delta) <= 0)) {
                image.Width += e.Delta;
            }
        }

        //オプションメニュークリックイベントハンドラ
        private void MenuItem_Click_Settings(object sender, RoutedEventArgs e) {
            settingsWindow = new();
            settingsWindow.Owner = this;
            settingsWindow.Show();
        }
    }

    public class UnitTest { 
        public int TestXUnit() => 0;
    }
}
