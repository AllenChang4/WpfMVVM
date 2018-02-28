using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfTest.Models;

using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace WpfTest.ViewModels
{
    public class myViewModel : ObservableObject
    {
        private ObservableCollection<mymember> _items;
        public ObservableCollection<mymember> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChangedEvent("Items");
            }
        }
        private mymember _selectedItem=null;
        public mymember SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChangedEvent("SelectedItem");
            }
        }
        public ICommand DeleteCommand { get; set; }

        public myViewModel()
        {
            Items = new ObservableCollection<mymember>();
            this.LoadJson();
            DeleteCommand = new DelegateCommand(DeleteSelected);
        }
        private void DeleteSelected()
        {
            if (null != SelectedItem)
            {
                Items.Remove(SelectedItem);
            }
        }

        public async void LoadJson()
        {
            //using (StreamReader r = new StreamReader(@"C:\Temp\WPF Test\WpfTest2\WpfTest\MyMemberList.json"))
            try { 
            using (StreamReader r = File.OpenText(@"MyMemberList.json"))
            {
                string json = await r. ReadToEndAsync();
                Items = JsonConvert.DeserializeObject<ObservableCollection<mymember>>(json);
            }
            }
            catch {
                Items = new ObservableCollection<mymember>();
            }
        }
        public ICommand SaveToJsonCommand
        {
            get { return new DelegateCommand(SaveToJson); }
        }

        private void SaveToJson()
        {
            SaveJson();
        }

        public async void SaveJson()
        {
            string jsonstring = JsonConvert.SerializeObject(Items);
            //File.WriteAllText(@"C:\Temp\WPF Test\WpfTest2\WpfTest\MyMemberList.json", jsonstring);
            using (StreamWriter writer = File.CreateText(@"MyMemberList.json"))
            {
                await writer.WriteAsync(jsonstring);
            }
        }
    }
}
