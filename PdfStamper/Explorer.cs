using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace PdfStamper
{
    public enum EntryType
    {
        Workspace,
        Folder,
        Document
    }

    public class TreeViewItemHolder : TreeViewItem
    {
        public TreeViewItemHolder()
            : base()
        {
            base.Header = "Place Holder";
            base.Tag = "Place Holder";
        }
    }

    class ExplorerItem : TreeViewItem
    {
        // The main graphical element
        private StackPanel stack;

        // Panel sub elements
        private readonly Label lbl;
        private readonly CheckBox cb;
        private readonly Image image;

        public ExplorerItem(EntryType type, string text, object context)
        {
            // Use the Tag to store the node type
            Tag = type;

            // Store the object in the DataContext
            DataContext = context;

            // Create stack panel
            stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;

            // Add the label information
            lbl = new Label();
            lbl.Content = text;
            lbl.HorizontalAlignment = HorizontalAlignment.Center;
            lbl.VerticalAlignment = VerticalAlignment.Center;

            // If it's a document add a check box
            if (type == EntryType.Document)
            {
                cb = new CheckBox();
                cb.HorizontalAlignment = HorizontalAlignment.Center;
                cb.VerticalAlignment = VerticalAlignment.Center;
                stack.Children.Add(cb);
            }

            // Add an Icon
            image = new Image();
            switch (type)
            {
                case EntryType.Workspace:
                    image.Source = new BitmapImage(new Uri(@"pack://application:,,,/images/im_workspace.ico"));
                    break;
                case EntryType.Folder:
                    image.Source = new BitmapImage(new Uri(@"pack://application:,,,/images/im_folder.ico"));
                    break;
                case EntryType.Document:
                    image.Source = new BitmapImage(new Uri(@"pack://application:,,,/images/im_document.ico"));
                    break;
            }

            image.Stretch = System.Windows.Media.Stretch.UniformToFill;
            image.Width = 20;
            image.Height = 20;
            image.HorizontalAlignment = HorizontalAlignment.Center;
            image.VerticalAlignment = VerticalAlignment.Center;

            stack.Children.Add(image);
            stack.Children.Add(lbl);

            Header = stack;
        }

        public bool isSelected()
        {
            if (!Tag.Equals(EntryType.Document)) return false;

            return cb.IsChecked ?? false;
        }

    }

    // http://codesdirectory.blogspot.com/2013/01/c-wpf-treeview-file-explorer.html
    class Explorer
    {
        private TreeView treeView;

        public Explorer(TreeView treeView)
        {
            this.treeView = treeView;
            LoadDirectories();
        }

        public void LoadDirectories()
        {
            var drives = DriveInfo.GetDrives();
            foreach (var drive in drives)
            {
                this.treeView.Items.Add(this.GetItem(drive));
            }
        }

        private TreeViewItem GetItem(DriveInfo drive)
        {
            //var item = CreateItem(EntryType.Workspace, drive.Name, drive);
            var item = new ExplorerItem(EntryType.Workspace, drive.Name, drive);

            this.AddHolder(item);
            item.Expanded += new RoutedEventHandler(itemExpanded);
            return item;
        }

        private TreeViewItem GetItem(DirectoryInfo directory)
        {
            //var item = CreateItem(EntryType.Folder, directory.Name, directory);
            var item = new ExplorerItem(EntryType.Folder, directory.Name, directory);

            this.AddHolder(item);
            item.Expanded += new RoutedEventHandler(itemExpanded);

            return item;
        }

        private TreeViewItem GetItem(FileInfo file)
        {
            //var item = CreateItem(EntryType.Document, file.Name, file);
            var item = new ExplorerItem(EntryType.Document, file.Name, file);

            return item;
        }

        private void ExploreDirectories(TreeViewItem item)
        {
            var directoryInfo = (DirectoryInfo)null;

            if (item.Tag.Equals(EntryType.Workspace))
            {
                directoryInfo = ((DriveInfo)item.DataContext).RootDirectory;
            }
            else if (item.Tag.Equals(EntryType.Folder))
            {
                directoryInfo = (DirectoryInfo)item.DataContext;
            }
            else if (item.Tag.Equals(EntryType.Document))
            {
                directoryInfo = ((FileInfo)item.DataContext).Directory;
            }

            if (object.ReferenceEquals(directoryInfo, null)) return;

            foreach (var directory in directoryInfo.GetDirectories())
            {
                var isHidden = (directory.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
                var isSystem = (directory.Attributes & FileAttributes.System) == FileAttributes.System;

                if (!isHidden && !isSystem)
                {
                    item.Items.Add(this.GetItem(directory));
                }
            }
        }

        private void ExploreFiles(TreeViewItem item)
        {
            var directoryInfo = (DirectoryInfo)null;

            if (item.Tag.Equals(EntryType.Workspace))
            {
                directoryInfo = ((DriveInfo)item.DataContext).RootDirectory;
            }
            else if (item.Tag.Equals(EntryType.Folder))
            {
                directoryInfo = (DirectoryInfo)item.DataContext;
            }
            else if (item.Tag.Equals(EntryType.Document))
            {
                directoryInfo = ((FileInfo)item.DataContext).Directory;
            }

            if (object.ReferenceEquals(directoryInfo, null)) return;

            foreach (var file in directoryInfo.GetFiles())
            {
                var isHidden = (file.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
                var isSystem = (file.Attributes & FileAttributes.System) == FileAttributes.System;
                if (!isHidden && !isSystem)
                {
                    item.Items.Add(this.GetItem(file));
                }
            }
        }

        void itemExpanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;

            if (this.HasHolder(item))
            {
                treeView.Cursor = Cursors.Wait;
                this.RemoveHolder(item);
                this.ExploreDirectories(item);
                this.ExploreFiles(item);
                treeView.Cursor = Cursors.Arrow;
            }
        }

        private void AddHolder(TreeViewItem item)
        {
            item.Items.Add(new TreeViewItemHolder());
        }

        private bool HasHolder(TreeViewItem item)
        {
            return item.HasItems && (item.Items.OfType<TreeViewItem>().ToList().FindAll(tvi => tvi is TreeViewItemHolder).Count > 0);
        }

        private void RemoveHolder(TreeViewItem item)
        {
            var holders = item.Items.OfType<TreeViewItem>().ToList().FindAll(tvi => tvi is TreeViewItemHolder);
            foreach (var holder in holders)
            {
                item.Items.Remove(holder);
            }
        }

        public List<ExplorerItem> FindSelected()
        {
            var results = new List<ExplorerItem>();

            foreach (TreeViewItem item in treeView.Items)
            {
                ProcessNodes((ExplorerItem)item, results, 0);
            }

            return results;
        }

        private void ProcessNodes(ExplorerItem node, List<ExplorerItem> results, int level)
        {
            if (node.isSelected())
            {
                results.Add(node);
            }

            foreach (TreeViewItem innerNode in node.Items)
            {
                if (!(innerNode is TreeViewItemHolder)) ProcessNodes((ExplorerItem)innerNode, results, level + 1);
            }
        }

    }
}
