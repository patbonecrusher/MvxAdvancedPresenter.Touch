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
using Cirrious.MvvmCross.Touch.Views.Presenters;
using Cirrious.MvvmCross.ViewModels;
using MonoTouch.UIKit;
using System;
using MvxAdvancedPresenter.Touch.Transition;

namespace Coc.MvxAdvancedPresenter.Touch
{
	public abstract class BaseTouchViewPresenter : MvxBaseTouchViewPresenter, IDisposable
	{
		private bool _disposed = false;

		public UIViewController RootViewController { get; protected set; }
		public UIViewControllerAnimatedTransitioning Transition { get; set; }

		public abstract void ShowFirstView (IMvxTouchView view);
		public virtual void ShowFirstView (MvxViewModelRequest request)
		{
			var view = this.CreateViewControllerFor(request);
			ShowFirstView(view);
			OnRootControllerCreated();
		}

		public abstract void Show(IMvxTouchView view);
		public override void Show(MvxViewModelRequest request)
		{
			var view = this.CreateViewControllerFor(request);
			Show(view);
		}

		public abstract void Close(IMvxViewModel viewModel);
		public virtual void AttachToWindow (UIWindow window)
		{
			window.RootViewController = RootViewController;
			window.Add(RootViewController.View);
		}

		public virtual void DetachFromWindow ()
		{
			RootViewController.View.RemoveFromSuperview();
			RootViewController.RemoveFromParentViewController();
		}

		public virtual void Present(UIWindow inWindow, MvxViewModelRequest withRequest, BaseTouchViewPresenter fromViewPresenter, Action presented)
		{
			ShowFirstView (withRequest);

			UIViewControllerAnimatedTransitioning animation = GetAnimatedTransition ();
			if (fromViewPresenter != null && animation != null) {
				ViewControllerContextTransitioning vcTransitionContext = new ViewControllerContextTransitioning (
					inWindow, fromViewPresenter.RootViewController, RootViewController, () => 
					{
						fromViewPresenter.DetachFromWindow ();
						AttachToWindow (inWindow);
						presented ();
					}
				);

				animation.AnimateTransition (vcTransitionContext);
				return;
			}
			if (fromViewPresenter != null) { fromViewPresenter.DetachFromWindow (); }
			AttachToWindow (inWindow);
			presented ();
		}

		public override void ChangePresentation (Cirrious.MvvmCross.ViewModels.MvxPresentationHint hint)
		{
			// Analysis disable once CanBeReplacedWithTryCastAndCheckForNull
			if (hint is MvxClosePresentationHint)
			{
				Close((hint as MvxClosePresentationHint).ViewModelToClose);
				return;
			}
		}

		#region IDisposable implementation

		public virtual void Dispose(bool disposing)
		{
			if (!_disposed) {
				if (disposing) { 
					RootViewController.View.Dispose(); 
					RootViewController.Dispose(); 
				}
				_disposed = true;
			}
		}

		public void Dispose ()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion

		protected virtual void OnRootControllerCreated() {}
		protected virtual UIViewControllerAnimatedTransitioning GetAnimatedTransition() {return Transition;}
	}
}
