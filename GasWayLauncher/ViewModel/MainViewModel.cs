using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GasWayLauncher.View;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace GasWayLauncher.ViewModel
{
    internal class MainViewModel : ViewModelBase
    {
        private Page GasWay = new GasWayPage();
        private Page DLC = new DLCPage();
        private Page Download = new DownloadPage();
        private Page _CurPage = new GasWayPage();

        public Page CurPage
        {
            get => _CurPage;
            set => Set(ref _CurPage, value);
        }

        public ICommand OpenGasWayPage
        {
            get
            {
                return new RelayCommand(() => CurPage = GasWay);
            }
        }
        public ICommand OpenDownloadPage
        {
            get
            {
                return new RelayCommand(() => CurPage = Download);
            }
        }
        public ICommand OpenDLCPage
        {
            get
            {
                return new RelayCommand(() => CurPage = DLC);
            }
        }
    }
}
