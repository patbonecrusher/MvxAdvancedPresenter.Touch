using Cirrious.MvvmCross.ViewModels;

namespace MvxAdvancedPresenterTest.Core.ViewModels
{
    public class FirstViewModel 
		: MvxViewModel
    {
		private string _hello = "Hello MvvmCross";
        public string Hello
		{ 
			get { return _hello; }
			set { _hello = value; RaisePropertyChanged(() => Hello); }
		}

		private IMvxCommand _logonCommand;
		public IMvxCommand LogonCommand {
			get { return _logonCommand; }
			set { _logonCommand = value; RaisePropertyChanged (() => LogonCommand); }
		}

		public FirstViewModel ()
		{
			_logonCommand = new MvxCommand(() => ShowViewModel<SecondViewModel>());
		}
    }
}
