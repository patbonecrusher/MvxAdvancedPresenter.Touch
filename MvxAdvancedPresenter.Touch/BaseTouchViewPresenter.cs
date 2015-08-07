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
using UIKit;
using System;
using MvxAdvancedPresenter.Touch.Transition;

namespace Coc.MvxAdvancedPresenter.Touch
{
	public abstract class BaseTouchViewPresenter : MvxBaseTouchViewPresenter, IDisposable
	{
		public UIViewController RootViewController { get; protected set; }
		public UIViewControllerAnimatedTransitioning Transition { get; set; }

		private bool _disposed = false;

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
			WillAttachedToWindow(window);
			window.RootViewController = RootViewController;
			window.Add(RootViewController.View);
			DidAttachedToWindow(window);

		}

		public virtual void DetachFromWindow ()
		{
			UIWindow window = RootViewController.View.Superview as UIWindow;
			WillDetachedFromWindow(window);
//			RootViewController.View.RemoveFromSuperview();
//			RootViewController.RemoveFromParentViewController();
			DidDetachedFromWindow(window);
		}

		public virtual bool IsPresentingSameViewModel(Type vmType)
		{
			return false;
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

		public override void ChangePresentation (MvxPresentationHint hint)
		{
			// Analysis disable once CanBeReplacedWithTryCastAndCheckForNull
			if (hint is MvxClosePresentationHint)
			{
				Close((hint as MvxClosePresentationHint).ViewModelToClose);
				return;
			}
		}

		public void Dispose () {
			Dispose(true);
			//GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool isDisposing) {
			if (_disposed) { return; }
			if (isDisposing) { FreeManagedResources(); _disposed = true; }
			FreeNativeResources();
		}

		protected virtual void FreeManagedResources () {}
		protected virtual void FreeNativeResources () {}

		protected virtual void OnRootControllerCreated() {}
		protected virtual void WillAttachedToWindow(UIWindow window) {}
		protected virtual void DidAttachedToWindow(UIWindow window) {}
		protected virtual void WillDetachedFromWindow(UIWindow window) {}
		protected virtual void DidDetachedFromWindow(UIWindow window) {}
		protected virtual UIViewControllerAnimatedTransitioning GetAnimatedTransition() {return Transition;}
	}
}
