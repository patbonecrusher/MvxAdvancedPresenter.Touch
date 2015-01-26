using Cirrious.MvvmCross.ViewModels;

namespace MvxAdvancedPresenterTest.Core.ViewModels
{
    public class FirstViewModel 
		: MvxViewModel
    {
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
