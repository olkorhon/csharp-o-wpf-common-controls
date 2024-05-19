using Olli.Wpf.TreeViews.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Olli.Wpf.TreeViewExamples
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        static BitmapImage folderBMI;
        static BitmapImage fileBMI;

        public MainWindow()
        {
            InitializeComponent();

            folderBMI = new BitmapImage(new Uri("pack://application:,,,/Icons/folder.png"));
            fileBMI = new BitmapImage(new Uri("pack://application:,,,/Icons/file.png"));

            DirectoryTreeViewModel dtvm = new DirectoryTreeViewModel(folderBMI, new Dictionary<string, BitmapImage>() {
                { ".txt", fileBMI }
            });

            dtvm.Initialize(new DirectoryInfo("C:\\PortableTools"));

            directoryTreeView_example.FileSelectionChanged += SelectionChanged;
            directoryTreeView_example.DataContext = dtvm;

            DataContext = this;
        }

        public string FileContents { get; set; }

        public void SelectionChanged(FileInfo newFile)
        {
            FileContents = File.ReadAllText(newFile.FullName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FileContents)));
        }
    }
}
