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
using System;
using MonoTouch.UIKit;

namespace MvxAdvancedPresenter.Touch.Attributes
{
	[AttributeUsage(AttributeTargets.Class)]
	public class PresenterAttribute : Attribute
	{
		public Type PresenterType { get; private set; }
		public virtual Type TransitionType { get; set; }
		public virtual bool bla { get; set; }

		public PresenterAttribute () { 
		}

		public PresenterAttribute (Type classType) { 
			PresenterType = classType; 
		}

		public virtual BaseTouchViewPresenter CreatePresenter()
		{
			if (PresenterType != null) {
				BaseTouchViewPresenter presenter = (BaseTouchViewPresenter) PresenterType.GetConstructor(new Type[] {}).Invoke(new object[] {});

				UIViewControllerAnimatedTransitioning transition = null;
				if (TransitionType != null) {
					transition = (UIViewControllerAnimatedTransitioning) TransitionType.GetConstructor(new Type[] {}).Invoke(new object[] {});
				}

				presenter.Transition = transition;
				return presenter;
			}
			return null;
		}
	}
}

