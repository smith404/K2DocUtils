using K2IManageObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace PdfStamper
{
    public class TreeViewItemHolder : TreeViewItem
    {
        public TreeViewItemHolder() : base()
        {
            base.Header = "Place Holder";
            base.Tag = "Place Holder";
        }
    }

    public class ExplorerItem : TreeViewItem
    {
        public static ObservableCollection<ExplorerItem> CreateItems(List<ExplorerItem> items)
        {
            ObservableCollection<ExplorerItem> list = new ObservableCollection<ExplorerItem>();

            items.ForEach(delegate (ExplorerItem item)
            {
                list.Add(item);
            });

            return list;
        }

        // The main graphical element
        private readonly StackPanel stack;

        // Panel sub elements
        private readonly Label lbl;
        private readonly CheckBox cb;
        private readonly Image image;

        // Display Properties
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Owner { get; set; }

        public ExplorerItem(EntryType type, string text, object context)
        {
            // Use the Tag to store the node type
            Tag = type;

            // Store the object in the DataContext
            DataContext = context;

            // Create stack panel
            stack = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };

            // Add the label information
            lbl = new Label
            {
                Content = text,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            // If it's a document add a check box
            if (type == EntryType.Document)
            {
                cb = new CheckBox
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                stack.Children.Add(cb);

                Title = ((FileInfo)context).Name;
                Description = ((FileInfo)context).DirectoryName;
                Owner = ((FileInfo)context).CreationTime.ToLongDateString();
                Id = ((FileInfo)context).FullName;
            }

            // Add an Icon
            image = new Image();
            switch (type)
            {
                case EntryType.Workspace:
                    image.Source = new BitmapImage(new Uri(@"pack://application:,,,/images/workspace.ico"));
                    break;
                case EntryType.Folder:
                    image.Source = new BitmapImage(new Uri(@"pack://application:,,,/images/folder.ico"));
                    break;
                case EntryType.Document:
                    image.Source = new BitmapImage(new Uri(@"pack://application:,,,/images/document.ico"));
                    break;
                case EntryType.Email:
                    image.Source = new BitmapImage(new Uri(@"pack://application:,,,/images/email.ico"));
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
            if (!Tag.Equals(EntryType.Document))
            {
                return false;
            }

            return cb.IsChecked ?? false;
        }
    }

    // http://codesdirectory.blogspot.com/2013/01/c-wpf-treeview-file-explorer.html
    public class Explorer
    {
        private readonly TreeView treeView;

        public Explorer(TreeView treeView)
        {
            this.treeView = treeView;
            LoadDirectories();
        }

        public void LoadDirectories()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                treeView.Items.Add(GetItem(drive));
            }
        }

        private TreeViewItem GetItem(DriveInfo drive)
        {
            ExplorerItem item = new ExplorerItem(EntryType.Workspace, drive.Name, drive);

            AddHolder(item);
            item.Expanded += new RoutedEventHandler(itemExpanded);
            return item;
        }

        private TreeViewItem GetItem(DirectoryInfo directory)
        {
            ExplorerItem item = new ExplorerItem(EntryType.Folder, directory.Name, directory);

            AddHolder(item);
            item.Expanded += new RoutedEventHandler(itemExpanded);

            return item;
        }

        private TreeViewItem GetItem(FileInfo file)
        {
            ExplorerItem item = new ExplorerItem(EntryType.Document, file.Name, file);

            return item;
        }

        private void ExploreDirectories(TreeViewItem item)
        {
            DirectoryInfo directoryInfo = (DirectoryInfo)null;

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

            if (object.ReferenceEquals(directoryInfo, null))
            {
                return;
            }

            foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
            {
                bool isHidden = (directory.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
                bool isSystem = (directory.Attributes & FileAttributes.System) == FileAttributes.System;

                if (!isHidden && !isSystem)
                {
                    item.Items.Add(GetItem(directory));
                }
            }
        }

        private void ExploreFiles(TreeViewItem item)
        {
            DirectoryInfo directoryInfo = (DirectoryInfo)null;

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

            if (object.ReferenceEquals(directoryInfo, null))
            {
                return;
            }

            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                bool isHidden = (file.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
                bool isSystem = (file.Attributes & FileAttributes.System) == FileAttributes.System;
                if (!isHidden && !isSystem)
                {
                    item.Items.Add(GetItem(file));
                }
            }
        }

        void itemExpanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;

            if (HasHolder(item))
            {
                treeView.Cursor = Cursors.Wait;
                RemoveHolder(item);
                ExploreDirectories(item);
                ExploreFiles(item);
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
            List<TreeViewItem> holders = item.Items.OfType<TreeViewItem>().ToList().FindAll(tvi => tvi is TreeViewItemHolder);
            foreach (TreeViewItem holder in holders)
            {
                item.Items.Remove(holder);
            }
        }

        public List<ExplorerItem> FindSelected()
        {
            List<ExplorerItem> results = new List<ExplorerItem>();

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
                if (!(innerNode is TreeViewItemHolder))
                {
                    ProcessNodes((ExplorerItem)innerNode, results, level + 1);
                }
            }
        }

    }
}
