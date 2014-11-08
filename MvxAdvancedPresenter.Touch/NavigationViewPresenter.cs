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

namespace Coc.MvxAdvancedPresenter.Touch
{
	public class NavigationViewPresenter : BaseTouchViewPresenter
	{
		private UINavigationController _navViewController;

		public override void ShowFirstView (IMvxTouchView view)
		{
			_navViewController = CreateNavController(view);
			RootViewController = _navViewController;
		}

		public override void Show (IMvxTouchView view)
		{
			_navViewController.PushViewController(view as UIViewController, true);
		}

		public override void Close (Cirrious.MvvmCross.ViewModels.IMvxViewModel viewModel)
		{
			// TODO Add some check to validate we are closing the right view!
			_navViewController.PopViewControllerAnimated(true);
		}

		protected virtual UINavigationController CreateNavController(IMvxTouchView view)
		{
			return new UINavigationController(view as UIViewController);
		}
	}
}
