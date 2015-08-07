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
using UIKit;
using System.Linq;

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
			_navViewController.PopViewController(true);
		}

		protected virtual UINavigationController CreateNavController(IMvxTouchView view)
		{
			return new UINavigationController(view as UIViewController);
		}

		public override bool IsPresentingSameViewModel (System.Type vmType)
		{
			if (_navViewController.TopViewController == null) { return false; }

			return ((IMvxTouchView)_navViewController.TopViewController).ViewModel.GetType() == vmType;
		}

        protected override void WillDetachedFromWindow (UIWindow window)
        {
            _navViewController.ViewControllers.ToList().ForEach(vc => vc.RemoveFromParentViewController ());
            _navViewController.ViewControllers = new UIViewController[] {};
            base.WillDetachedFromWindow(window);
        }

	}
}
