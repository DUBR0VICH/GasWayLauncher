using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GasWayLauncher.View;

namespace GasWayLauncher.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private Page _curPage = new GasWayPage();
        private string _loggedInUser;

        public MainViewModel()
        {
            GasWay = new GasWayPage();
            Comments = new CommentsPage();
            Download = new DownloadPage();
        }

        private Page GasWay { get; set; }
        private Page Comments { get; set; }
        private Page Download { get; set; }

        public Page CurPage
        {
            get => _curPage;
            set => Set(ref _curPage, value);
        }

        public string LoggedInUser
        {
            get => _loggedInUser;
            set
            {
                Set(ref _loggedInUser, value);
                // Optionally, update the pages if they need to know the logged-in user
                if (GasWay is IUserPage gasWayPage) gasWayPage.SetUser(_loggedInUser);
                if (Comments is IUserPage commentsPage) commentsPage.SetUser(_loggedInUser);
                if (Download is IUserPage downloadPage) downloadPage.SetUser(_loggedInUser);
            }
        }

        public ICommand OpenGasWayPage => new RelayCommand(() => CurPage = GasWay);
        public ICommand OpenDownloadPage => new RelayCommand(() => CurPage = Download);
        public ICommand OpenCommentsPage => new RelayCommand(() => CurPage = Comments);
    }

    public interface IUserPage
    {
        void SetUser(string user);
    }
}