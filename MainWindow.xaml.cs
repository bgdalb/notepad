using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NotepadPlusPlusDoi
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

        List<FileElement> listOfFiles = new List<FileElement>();
        Helper helper = new Helper();

        private void NewFile(object sender, RoutedEventArgs e)
        {
            helper.createNewFile(ref listOfFiles, ref TabControl1);
            ((TabControl1.SelectedItem as TabItem).Content as TextBox).TextChanged += TextArea_TextChanged;
        }

        int indexOfFileInListOfFiles(String Name)
        {
            for (int i = 0; i < listOfFiles.Count; i++) if  (listOfFiles[i].fileName == Name) return i; 
            return -1;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            TabItem selectedItem = (TabItem)TabControl1.SelectedItem;
            helper.save(ref listOfFiles, indexOfFileInListOfFiles(selectedItem.Header.ToString()), ref TabControl1);
        }

        private void SaveAs(object sender, RoutedEventArgs e)
        {

            TabItem selectedItem = (TabItem)TabControl1.SelectedItem;
            helper.saveAsFile(ref listOfFiles, indexOfFileInListOfFiles(selectedItem.Header.ToString()), ref TabControl1);
        }

        private void Open(object sender, RoutedEventArgs e)
        {
            helper.openFile(ref listOfFiles, ref TabControl1);
            ((TabControl1.SelectedItem as TabItem).Content as TextBox).TextChanged += TextArea_TextChanged;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you really want to close this file?", "Message", MessageBoxButton.YesNo);
            switch (result)
            {

                case MessageBoxResult.Yes:
                    TabItem selectedItem = (TabItem)TabControl1.SelectedItem;
                    helper.saveAsFile(ref listOfFiles, indexOfFileInListOfFiles(selectedItem.Header.ToString()), ref TabControl1);
                    helper.close(ref listOfFiles, indexOfFileInListOfFiles(selectedItem.Header.ToString()), ref TabControl1);
                 
                    break;
                case MessageBoxResult.No:
                    break;

            }

        }

        private void TextArea_TextChanged(object sender, TextChangedEventArgs e)
        {
            TabItem selectedItem = (TabItem)TabControl1.SelectedItem;
            selectedItem.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0, 0));
        }

        private void About(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.Show();
        }



        
    }

}
