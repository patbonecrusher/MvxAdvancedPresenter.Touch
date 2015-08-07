using System;
using Cirrious.MvvmCross.ViewModels;

namespace MvxAdvancedPresenterTest.Core.ViewModels
{
	public class SecondViewModel : MvxViewModel
	{
		public IMvxCommand LogoffCommand { get; set; }
		public IMvxCommand GarbageCommand { get; set; }

		public SecondViewModel ()
		{
			LogoffCommand = new MvxCommand(() => ShowViewModel<FirstViewModel>());
			GarbageCommand = new MvxCommand(() => {
				#if DEBUG
				GC.Collect(); 
				GC.WaitForPendingFinalizers();
				#endif
			});
		}
	}
}

