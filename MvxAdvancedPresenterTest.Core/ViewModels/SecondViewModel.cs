using System;
using Cirrious.MvvmCross.ViewModels;

namespace MvxAdvancedPresenterTest.Core.ViewModels
{
	public class SecondViewModel : MvxViewModel
	{
		private IMvxCommand _logoffCommand;
		public IMvxCommand LogoffCommand {
			get { return _logoffCommand; }
			set { _logoffCommand = value; RaisePropertyChanged (() => LogoffCommand); }
		}

		public SecondViewModel ()
		{
			_logoffCommand = new MvxCommand(() => ShowViewModel<FirstViewModel>());
		}
	}
}

