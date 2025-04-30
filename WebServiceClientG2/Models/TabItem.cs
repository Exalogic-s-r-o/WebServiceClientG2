using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceClientG2.Models
{
    /// <summary>
    /// Jedna položka v liste záložek.
    /// </summary>
    public class TabItem : ObservableObject
    {
        private string name = string.Empty;
        private bool isSelected;
        private ContentView contentView = new ContentView();

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public ContentView ContentView
        {
            get { return contentView; }
            set
            {
                contentView = value;
                OnPropertyChanged("ContentView");
            }
        }

        public TabItem(string name, 
                       ContentView contentView, 
                       bool isSelected = false)
        {
            this.Name = name;
            this.ContentView = contentView;
            this.IsSelected = false;
        }
    }
}
