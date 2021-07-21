using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace PdfStamper
{
    class SelectedDocument
    {
        public static ObservableCollection<SelectedDocument> CreateTasks(List<ExplorerItem> items)
        {
            ObservableCollection<SelectedDocument> list = new ObservableCollection<SelectedDocument>();

            items.ForEach(delegate (ExplorerItem item)
            {
                list.Add(new SelectedDocument(item));
            });

            return list;
        }

        public ExplorerItem Item { get; set; }

        public SelectedDocument(ExplorerItem item)
        {
            Item = item;
        }
    }
}
