// Copyright (C) 20134 Pat Laplante
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
using MonoTouch.UIKit;

namespace MvxAdvancedPresenter.Touch
{
    public abstract class BaseTouchViewPresenter : MvxBaseTouchViewPresenter
    {
        public UIViewController RootViewController { get; protected set; }

		public abstract void ShowFirstView (IMvxTouchView view);
        public abstract void Show(IMvxTouchView view);

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

		public virtual void Present(UIWindow inWindow, IMvxTouchView withView, BaseTouchViewPresenter fromViewPresenter)
		{
			if (fromViewPresenter != null) { fromViewPresenter.DetachFromWindow(); }
			ShowFirstView(withView);
			AttachToWindow(inWindow);
		}
    }
    
}

//			CustomAnimatedTransition animation = GetPushAnimation(newPresenter.TopViewController);
//			if (CurrentPresenter != null && animated && animation != null) {
//
//				ViewControllerContextTransitioning vcTransitionContext = new ViewControllerContextTransitioning(_window, () => {
//					CurrentPresenter.DetachFromWindow();
//					newPresenter.AttachToWindow(_window);
//				}, CurrentPresenter.ContainerViewController, newPresenter.ContainerViewController);
//				animation.AnimateTransition(vcTransitionContext);
//			} else {
//			}
//		private CustomAnimatedTransition GetPushAnimation(UIViewController view) 
//		{
//			Attribute[] attributes = Attribute.GetCustomAttributes (view.GetType ());
//			PushTransitionAttribute attribute = (PushTransitionAttribute) attributes.FirstOrDefault (a => a is PushTransitionAttribute);
//			if (attribute == null) return null;
//
//			CustomAnimatedTransition transition = GetTransitionOfType<CustomAnimatedTransition>(Type.GetType(attribute.Name));
//			if (transition != null) {
//				transition.Duration = attribute.Duration;
//				return transition;
//			}
//
//			return null;
//		}
//

//		public static T GetTransitionOfType<T>(Type t)
//		{
//			try
//			{
//				return (T) t.GetConstructor(new Type[] {}).Invoke(new object[] {});
//			}
//			catch
//			{
//				return default(T);
//			}
//		}

