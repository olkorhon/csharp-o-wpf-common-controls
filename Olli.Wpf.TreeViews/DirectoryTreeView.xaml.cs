using System;
using System.Collections.Generic;
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

namespace Olli.Wpf.TreeViews
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class DirectoryTreeView : UserControl
    {
        public delegate void FileSelectionChangedDelegate(FileInfo info);
        public delegate void DirSelectionChangedDelegate(DirectoryInfo info);

        public event FileSelectionChangedDelegate FileSelectionChanged;
        public event DirSelectionChangedDelegate DirectorySelectionChanged;

        public DirectoryTreeView()
        {
            InitializeComponent();
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is FileTreeViewItem)
                FileSelectionChanged?.Invoke(((FileTreeViewItem)e.NewValue).Info);
            else if (e.NewValue is DirectoryTreeViewItem)
                DirectorySelectionChanged?.Invoke(((DirectoryTreeViewItem)e.NewValue).Info);
        }
    }
}
