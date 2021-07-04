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
            var item = CreateItem(EntryType.Workspace, drive.Name, drive, "workspace");

            this.AddHolder(item);
            item.Expanded += new RoutedEventHandler(item_Expanded);
            return item;
        }

        private TreeViewItem GetItem(DirectoryInfo directory)
        {
            var item = CreateItem(EntryType.Folder, directory.Name, directory, "folder");

            this.AddHolder(item);
            item.Expanded += new RoutedEventHandler(item_Expanded);

            return item;
        }

        private TreeViewItem GetItem(FileInfo file)
        {
            var item = CreateItem(EntryType.Document, file.Name, file, "document");

            return item;
        }

        private TreeViewItem CreateItem(EntryType type, string text, object context, string tag)
        {
            var item = new TreeViewItem
            {
                DataContext = tag,
                Tag = context
            };

            // Create stack panel
            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;

            Label lbl = new Label();
            lbl.Content = text;
            lbl.HorizontalAlignment = HorizontalAlignment.Center;
            lbl.VerticalAlignment = VerticalAlignment.Center;

            // Add into stack
            if (type == EntryType.Document)
            {
                CheckBox cb = new CheckBox();
                cb.HorizontalAlignment = HorizontalAlignment.Center;
                cb.VerticalAlignment = VerticalAlignment.Center;
                stack.Children.Add(cb);
            }

            Image image = new Image();
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

            item.Header = stack;

            return item;
        }

        private void ExploreDirectories(TreeViewItem item)
        {
            var directoryInfo = (DirectoryInfo)null;
            if (item.Tag is DriveInfo)
            {
                directoryInfo = ((DriveInfo)item.Tag).RootDirectory;
            }
            else if (item.Tag is DirectoryInfo)
            {
                directoryInfo = (DirectoryInfo)item.Tag;
            }
            else if (item.Tag is FileInfo)
            {
                directoryInfo = ((FileInfo)item.Tag).Directory;
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
            if (item.Tag is DriveInfo)
            {
                directoryInfo = ((DriveInfo)item.Tag).RootDirectory;
            }
            else if (item.Tag is DirectoryInfo)
            {
                directoryInfo = (DirectoryInfo)item.Tag;
            }
            else if (item.Tag is FileInfo)
            {
                directoryInfo = ((FileInfo)item.Tag).Directory;
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

        void item_Expanded(object sender, RoutedEventArgs e)
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

        public class TreeViewItemHolder : TreeViewItem
        {
            public TreeViewItemHolder()
                : base()
            {
                base.Header = "Place Holder";
                base.Tag = "Place Holder";
            }
        }

        public string FindSelected()
        {
            StringBuilder builder = new StringBuilder();

            foreach (TreeViewItem item in treeView.Items)
            {
                ProcessNodes(item, builder, 0);
            }

            return builder.ToString();
        }

        private void ProcessNodes(TreeViewItem node, StringBuilder builder, int level)
        {
            if (node.DataContext.ToString().Equals("document"))
            {
                if (node.Header.)
                builder.Append(new string('\t', level) + node.Tag.ToString() + Environment.NewLine);
            }

            foreach (TreeViewItem l_innerNode in node.Items)
            {
                ProcessNodes(l_innerNode, builder, level + 1);
            }
        }

    }
}
