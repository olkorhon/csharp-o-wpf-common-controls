using System.IO;
using System.Windows.Controls;

namespace Olli.Wpf.TreeViews
{
    public class DirectoryTreeViewItem : TreeViewItem
    {
        public DirectoryTreeViewItem(DirectoryInfo info) : base()
        {
            Info = info;
        }

        public DirectoryInfo Info { get; private set; }
    }
}
