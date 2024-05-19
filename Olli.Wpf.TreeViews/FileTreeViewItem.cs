using System.IO;
using System.Windows.Controls;

namespace Olli.Wpf.TreeViews
{
    public class FileTreeViewItem : TreeViewItem
    {
        public FileTreeViewItem(FileInfo info) : base()
        {
            Info = info;
        }

        public FileInfo Info { get; private set; }
    }
}
