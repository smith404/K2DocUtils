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
                list.Add(new SelectedDocument(result));
            });

            return list;
        }

        private ExplorerItem result;

        public string Name
        {
            get { return result.ToString(); } 
        }

        public string Description
        {
            get { return "a description"; }
        }

        public SelectedDocument(ExplorerItem result)
        {
            this.result = result;
        }
    }
}
