using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ISupportIncrementalLoadingExample.Incremental
{
    public class GroupModel<TKey, T> : INotifyPropertyChanged
       where TKey : class, IComparable
    {
        private TKey key;

        public TKey Key
        {
            get
            {
                return key;
            }
            set
            {
                key = value;
                RaisePropertyChanged();
            }
        }

        private IList<T> items;

        public IList<T> Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
                RaisePropertyChanged();
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}
