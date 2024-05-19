using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Olli.Wpf.TreeViews.ViewModels
{
    public class DirectoryTreeViewModel
    {
        BitmapImage folderBMI;
        Dictionary<string, BitmapImage> fileBMIs;

        public DirectoryTreeViewModel()
        {
            Items = new ObservableCollection<TreeViewItem>();
        }

        public DirectoryTreeViewModel(BitmapImage folderBMI, Dictionary<string, BitmapImage> fileBMIs)
        {
            this.folderBMI = folderBMI;
            this.fileBMIs = fileBMIs;

            Items = new ObservableCollection<TreeViewItem>();
        }

        public ObservableCollection<TreeViewItem> Items {
            get; set;
        }

        public void Initialize(DirectoryInfo rootDirInfo)
        {
            Collection<TreeViewItem> newItems = CreateItemsFromDirRecursive(rootDirInfo);

            Items.Clear();
            foreach (TreeViewItem tvi in newItems)
                Items.Add(tvi);
        }

        private Collection<TreeViewItem> CreateItemsFromDirRecursive(DirectoryInfo rootDirInfo)
        {
            DirectoryInfo[] subDirs = rootDirInfo.GetDirectories();

            Collection<TreeViewItem> dirViewItems = new Collection<TreeViewItem>();
            for (int i = 0; i < subDirs.Length; i++)
            {
                DirectoryInfo dirInfo = subDirs[i];
                TreeViewItem tvi = CreateDirTreeViewItem(dirInfo);

                // Recursively call self with sub directories
                Collection<TreeViewItem> subTreeViewItems = CreateItemsFromDirRecursive(dirInfo);
                foreach (TreeViewItem subTreeViewItem in subTreeViewItems)
                {
                    tvi.Items.Add(subTreeViewItem);
                }

                // Add files after directories
                foreach (FileInfo fInfo in dirInfo.EnumerateFiles())
                {
                    // Skip files that have no icon
                    if (!fileBMIs.ContainsKey(fInfo.Extension))
                        continue;

                    TreeViewItem fileTvi = CreateFileTreeViewItem(fInfo);
                    tvi.Items.Add(fileTvi);
                }

                // Store generated view item
                dirViewItems.Add(tvi);
            }

            return dirViewItems;
        }

        private TreeViewItem CreateDirTreeViewItem(DirectoryInfo dirInfo)
        {
            Image image = new Image() { Source = folderBMI };
            Label label = new Label() { Content = dirInfo.Name };

            // Construct stack panel to combine image and label
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Horizontal;
            sp.Children.Add(image);
            sp.Children.Add(label);

            return new DirectoryTreeViewItem(dirInfo) { Header = sp };
        }

        private TreeViewItem CreateFileTreeViewItem(FileInfo fInfo)
        {
            Image image = new Image() { Source = fileBMIs[fInfo.Extension] };
            Label label = new Label() { Content = fInfo.Name };

            // Construct stack panel to combine image and label
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Horizontal;
            sp.Children.Add(image);
            sp.Children.Add(label);

            return new FileTreeViewItem(fInfo) { Header = sp };
        }
    }
}
