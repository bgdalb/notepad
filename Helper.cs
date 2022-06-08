using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NotepadPlusPlusDoi
{
    public class Helper
    {
       // public string remove

        public void createNewFile(ref List<FileElement> listOfFiles, ref TabControl tabcontrol)
        {
            TabItem ti = new TabItem();
            TextBox tb = new TextBox();
            tb.TextWrapping = TextWrapping.Wrap;
            tb.AcceptsReturn = true;
            ti.Name = "FileNr" + tabcontrol.Items.Count.ToString();
            ti.Header = "FileNr" + tabcontrol.Items.Count.ToString();
            ti.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0, 0));
            ti.Background = new SolidColorBrush(Color.FromArgb(0xFF, 1, 1, 1));
            
            ti.Content = tb;
            tabcontrol.Items.Add(ti);
            FileElement newFile = new FileElement();
            newFile.fileName = ti.Name;
            listOfFiles.Add(newFile);
            tabcontrol.SelectedItem = ti;
        }


        public void save(ref List<FileElement> listOfFiles, int index, ref TabControl tabcontrol)
        {
            TabItem selectedItem = (TabItem)tabcontrol.SelectedItem;
            TextBox tb = (TextBox)selectedItem.Content;
            if (listOfFiles[index].filePath != null)
            {
                File.WriteAllText(listOfFiles[index].filePath, tb.Text.ToString());
                selectedItem.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            }
            else
            {
                saveAsFile(ref listOfFiles, index, ref tabcontrol);
            }
        }



        public void saveAsFile(ref List<FileElement> listOfFiles, int index, ref TabControl tabcontrol)
        {

            TabItem selectedItem = (TabItem)tabcontrol.SelectedItem;
            if (selectedItem != null)
            {
                TextBox tb = (TextBox)selectedItem.Content;
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.FileName = listOfFiles[index].fileName;
                dialog.DefaultExt = ".txt";
                dialog.Filter = "Text Documents (.txt) |*.txt";
                dialog.ShowDialog();
                File.WriteAllText(dialog.FileName, tb.Text.ToString());
                listOfFiles[index].filePath = dialog.FileName;
                selectedItem.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                selectedItem.Header = dialog.SafeFileName;
                listOfFiles[index].fileName = dialog.SafeFileName;

            }
            else
                MessageBox.Show("Please select a file!");
        }

        public void openFile(ref List<FileElement> listOfFiles, ref TabControl tabcontrol)
        {
            TabItem ti = new TabItem();
            TextBox tb = new TextBox();
            tb.AcceptsReturn = true;
            FileElement tabList = new FileElement();
            tb.TextWrapping = TextWrapping.Wrap;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text File (*.txt) | *.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                using (StreamReader streamReader = new StreamReader(openFileDialog.FileName))
                {
                    tabList.filePath = openFileDialog.FileName;
                    Task<String> text = streamReader.ReadToEndAsync();
                    tb.Text = text.Result;
                    ti.Content = tb;
                    ti.Header = openFileDialog.SafeFileName;
                    ti.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                    ti.Background = new SolidColorBrush(Color.FromArgb(0xFF, 1, 1, 1));

                    tabcontrol.Items.Add(ti);
                    tabList.fileName = openFileDialog.SafeFileName;
                    listOfFiles.Add(tabList);
                    tabcontrol.SelectedItem = ti;
                }
            }
        }

        public void close(ref List<FileElement> listOfFiles, int index , ref TabControl tabcontrol)
        {
            TabItem selectedItem = (TabItem)tabcontrol.SelectedItem;
            tabcontrol.Items.Remove(selectedItem);
            listOfFiles.Remove(listOfFiles[index]);
        }

    }
}
