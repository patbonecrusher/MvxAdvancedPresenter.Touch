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
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Touch.Views.Presenters;
using MonoTouch.UIKit;
using MvxAdvancedPresenter.Touch.Attributes;

namespace MvxAdvancedPresenter.Touch
{
	public class PresenterDispatcher : MvxBaseTouchViewPresenter
	{
		private readonly UIWindow _window;

		public BaseTouchViewPresenter CurrentPresenter { get; private set; }

		public PresenterDispatcher (MvxApplicationDelegate appDelegate, UIWindow window) {
			_window = window;
		}

		public void SwapPresenter(BaseTouchViewPresenter newPresenter, IMvxTouchView view) {
			newPresenter.Present(_window, view, CurrentPresenter);
			CurrentPresenter = newPresenter;
		}

		public override void Show (Cirrious.MvvmCross.ViewModels.MvxViewModelRequest request)
		{
			var view = this.CreateViewControllerFor(request);
			var presenterFactory = GetPresenter(view);

			if (presenterFactory != null) {
				var newPresenter = presenterFactory.CreatePresenter();
				SwapPresenter(newPresenter, view);
			} else {
				CurrentPresenter.Show(view);
			}
		}

		#region Helpers

		private PresenterAttribute GetPresenter(IMvxTouchView view)
		{
			Attribute[] attributes = Attribute.GetCustomAttributes (view.GetType ());
			return (PresenterAttribute) attributes.FirstOrDefault (a => a is PresenterAttribute);
		}

		#endregion 
	}
}


