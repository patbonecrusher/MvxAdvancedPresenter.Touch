// Copyright (C) 2014 Pat Laplante
//
// Permission is hereby granted, free of charge, to  any person obtaining a copy 
// of this software and associated documentation files (the "Software"), to deal 
// in the Software without  restriction, including without limitation the rights 
// to use, copy,  modify,  merge, publish,  distribute,  sublicense, and/or sell 
// copies of the  Software,  and  to  permit  persons  to   whom the Software is 
// furnished to do so, subject to the following conditions:
//
// The above  copyright notice  and this permission notice shall be included all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT  WARRANTY OF  ANY KIND,  EXPRESS OR
// IMPLIED, INCLUDING  BUT NOT  LIMITED TO  THE WARRANTIES  OF  MERCHANTABILITY,  
// FITNESS  FOR  A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
// 
// IN NO EVENT SHALL  THE AUTHORS OR COPYRIGHT  HOLDERS BE  LIABLE FOR ANY CLAIM
// DAMAGES  OR  OTHER  LIABILITY, WHETHER  IN  AN  ACTION  OF  CONTRACT, TORT OR 
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE
// OR OTHER DEALINGS IN THE SOFTWARE.
// -----------------------------------------------------------------------------
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.UIKit;

namespace MvxAdvancedPresenter.Touch
{
	public class SplitViewPresenter : BaseTouchViewPresenter
	{
		private UISplitViewController _splitViewController;

		public override void ShowFirstView (IMvxTouchView view)
		{
			_splitViewController = CreateSplitViewController(view);
			RootViewController = _splitViewController;
		}

		public override void Show (IMvxTouchView view)
		{
			//_splitViewController.PushViewController(view as UIViewController, true);
		}

		protected virtual UISplitViewController CreateSplitViewController(IMvxTouchView view)
		{
			UISplitViewController svc = new UISplitViewController();
			svc.ViewControllers = new UIViewController[] {
				view as UIViewController,
				new UIViewController()
			};

			return svc;
		}

		public override void Close (Cirrious.MvvmCross.ViewModels.IMvxViewModel viewModel)
		{
		}
	}
}
