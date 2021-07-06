using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PdfStamper
{
    class SelectedDocument
    {
        public static ObservableCollection<SelectedDocument> CreateTasks(List<ExplorerItem> items)
        {
            ObservableCollection<SelectedDocument> list = new ObservableCollection<SelectedDocument>();

            items.ForEach(delegate (ExplorerItem result)
            {
                list.Add(new SelectedDocument(result.DataContext.ToString(), ""));
            });

            return list;
        }

        public string Name { get; }
        public string Description { get; }

        public SelectedDocument(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
    }
}
