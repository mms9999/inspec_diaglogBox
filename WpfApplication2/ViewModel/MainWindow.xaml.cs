﻿using System;
using System.Collections.Generic;
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

/* Newly add */
using System.IO.Packaging;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;


namespace WpfApplication2.ViewModel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = UIOutput.GetUIDisplay();
        }

        private void Saved_Diagnostic(object sender, RoutedEventArgs e)
        {
            //create a zip folder 
            string path = string.Empty;

            //open the save diaglog to choose dir to save zip
            Microsoft.Win32.SaveFileDialog fbd = new Microsoft.Win32.SaveFileDialog();
            fbd.FileName = "InspecDiagCollect"; // Default file name
            fbd.DefaultExt = ".zip"; // Default file extension
            fbd.Filter = "Zip files (*.zip)|*.*"; // Filter files by extension
            Nullable<bool> result = fbd.ShowDialog();
            if (result == true)
            {
                path = fbd.FileName;
            }
            try
            {
                //using the package, open the zipfile and then create a file
                using (Package package = ZipPackage.Open(path, FileMode.Create))
                {
                    //get the Temp folder *.txt
                    try
                    {
                        string startFolder1 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                        string startFolder = System.IO.Path.Combine(startFolder1, "Temp");

                        foreach (string currentFile in Directory.GetFiles(startFolder, "*.txt", SearchOption.AllDirectories))
                        {
                            //Console.WriteLine("Packing " + currentFile);
                            Uri relUri = GetRelativeUri(currentFile);
                            //Console.WriteLine("this is: " + relUri);

                            PackagePart packagePart = package.CreatePart(relUri, System.Net.Mime.MediaTypeNames.Application.Octet, CompressionOption.Maximum);
                            using (FileStream fileStream = new FileStream(currentFile, FileMode.Open, FileAccess.Read))
                            {
                                CopyStream(fileStream, packagePart.GetStream());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //System.Windows.MessageBox.Show(ex.Message);
                    }
                    Console.WriteLine("end of the first zip");
                    //get the inspect for windows folder
                    try
                    {
                        string startFolder2 = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                        string startFolder_2 = System.IO.Path.Combine(startFolder2, "Micro-Vu Corporation", "InSpec for Windows");

                        foreach (string currentFile in Directory.GetFiles(startFolder_2, "*.*", SearchOption.AllDirectories))
                        {
                            //Console.WriteLine("Packing " + currentFile);
                            Uri relUri = GetRelativeUri(currentFile);
                            //Console.WriteLine("this is: " + relUri);

                            PackagePart packagePart = package.CreatePart(relUri, System.Net.Mime.MediaTypeNames.Application.Octet, CompressionOption.Maximum);
                            using (FileStream fileStream = new FileStream(currentFile, FileMode.Open, FileAccess.Read))
                            {
                                CopyStream(fileStream, packagePart.GetStream());
                            }
                        }
                        MessageBoxResult msgBox = System.Windows.MessageBox.Show("Collection complete for " + path, "Finished", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    catch (Exception ex)
                    {
                        //System.Windows.MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception exAll)
            {
                //System.Windows.MessageBox.Show(exAll.Message);
            }
        }
        private static void CopyStream(Stream source, Stream target)
        {
            const int bufSize = 16384;
            byte[] buf = new byte[bufSize];
            int bytesRead = 0;
            while ((bytesRead = source.Read(buf, 0, bufSize)) > 0)
                target.Write(buf, 0, bytesRead);
        }

        private static Uri GetRelativeUri(string currentFile)
        {
            string relPath = currentFile.Substring(currentFile.IndexOf('\\')).Replace('\\', '/').Replace(' ', '_');
            return new Uri(RemoveAccents(relPath), UriKind.Relative);
        }

        private static string RemoveAccents(string input)
        {
            string normalized = input.Normalize(NormalizationForm.FormKD);
            Encoding removal = Encoding.GetEncoding(Encoding.ASCII.CodePage, new EncoderReplacementFallback(""), new DecoderReplacementFallback(""));
            byte[] bytes = removal.GetBytes(normalized);
            return Encoding.ASCII.GetString(bytes);
        }

        private void Close_Button(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

}