using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBackaup.Models
{
   public class Backup :BindableBase
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string  Password { get; set; }

        public string BackupPath { get; set; }

        //public BackupState BackedupState { get; set; }


        public BackupState BackedupState
        {
            get { return _BackedupState; }
            set { _BackedupState = value; RaisePropertyChanged(nameof(BackedupState)); }
        }
        BackupState _BackedupState;


    }

    public enum BackupState
    {
        unpacked, 
        started,
        finished,
        failed
    }
}
