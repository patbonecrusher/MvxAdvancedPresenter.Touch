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
using System;
using System.Linq;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.Touch.Views.Presenters;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Views;
using MonoTouch.UIKit;
using Coc.MvxAdvancedPresenter.Touch.Attributes;

namespace Coc.MvxAdvancedPresenter.Touch
{
	public class PresenterDispatcher : MvxBaseTouchViewPresenter
	{
		private readonly UIWindow _window;

		public BaseTouchViewPresenter CurrentPresenter { get; private set; }

		public PresenterDispatcher (MvxApplicationDelegate appDelegate, UIWindow window) {
			_window = window;
		}

		public void SwapPresenter(BaseTouchViewPresenter newPresenter, MvxViewModelRequest view) {
			newPresenter.Present(_window, view, CurrentPresenter, () => { CurrentPresenter = newPresenter; });
		}

		public override void Show (MvxViewModelRequest request)
		{
			var viewType = Mvx.Resolve<IMvxViewsContainer>().GetViewType(request.ViewModelType);
			var presenterFactory = GetPresenter(viewType);

			if (presenterFactory != null) {
				var newPresenter = presenterFactory.CreatePresenter();
				SwapPresenter(newPresenter, request);
			} else {
				// TODO throw up if Current presenter is null
				CurrentPresenter.Show(request);
			}
		}

		public override void ChangePresentation (MvxPresentationHint hint) {
			if (CurrentPresenter != null) { CurrentPresenter.ChangePresentation (hint); }
		}

		public override bool PresentModalViewController (UIViewController viewController, bool animated) {
			return CurrentPresenter != null && CurrentPresenter.PresentModalViewController (viewController, animated);
		}

		public override void NativeModalViewControllerDisappearedOnItsOwn () {
			if (CurrentPresenter != null) { CurrentPresenter.NativeModalViewControllerDisappearedOnItsOwn (); }
		}

		#region Helpers

		private PresenterAttribute GetPresenter(Type viewType)
		{
			Attribute[] attributes = Attribute.GetCustomAttributes (viewType);
			return (PresenterAttribute) attributes.FirstOrDefault (a => a is PresenterAttribute);
		}

		#endregion 
	}
}


