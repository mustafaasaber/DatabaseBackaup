using DatabaseBackaup.Helpers;
using DatabaseBackaup.Models;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DatabaseBackaup
{
    public class MainWindowViewModel : BindableBase
    {

        #region Constructors
        public MainWindowViewModel()
        {
            BackupList = new ObservableCollection<Backup>();
            BackupList.Add(new Backup
            {
                Server = @".\sqlexpress",
                Database = "datbase1",
                Username = "sa",
                Password = "Admin",
                BackupPath = @"F:\Tasks",
                BackedupState = BackupState.unpacked

            });


            BackupList.Add(new Backup
            {
                Server = @".\sqlexpress2",
                Database = "datbase2",
                Username = "sa",
                Password = "Admin",
                BackupPath = @"F:\Tasks",
                BackedupState = BackupState.unpacked

            });


        }
        #endregion


        #region Properities


        public ObservableCollection<Backup> BackupList
        {
            get { return _BackupList; }
            set { _BackupList = value; RaisePropertyChanged(nameof(BackupList)); }
        }
        ObservableCollection<Backup> _BackupList;



        #endregion





        #region Commands





        public ICommand StartPackingCommand { get { return _StartPackingCommand ?? (_StartPackingCommand = new DelegateCommand(StartPackingMethod, canBackup)); } }
        private DelegateCommand _StartPackingCommand;

        async void StartPackingMethod()
        {
            await Task.Factory.StartNew(() =>
            {
                IsBacking = true;
               
                foreach (var item in BackupList)
                {
                    item.BackedupState = BackupState.started;
                    var result = SqlHelper.Backup(item);
                    item.BackedupState = result;
                }

                IsBacking = false;
            });
        }
        public bool IsBacking { get; set; } = false;
        public bool canBackup()
        {
            return !IsBacking;
        }
        #endregion


    }
}
