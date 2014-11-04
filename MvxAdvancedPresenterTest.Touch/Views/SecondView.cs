using System;
using MvxAdvancedPresenter.Touch.Attributes;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.UIKit;
using MvxAdvancedPresenter.Touch;

namespace MvxAdvancedPresenterTest.Touch.Views
{
	public class SecondViewPresenter : PresenterAttribute
	{
		public SecondViewPresenter ()
		{
			
		}

		public override MvxAdvancedPresenter.Touch.BaseTouchViewPresenter CreatePresenter ()
		{
			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad) {
				return new SplitViewPresenter();
			} else {
				return new NavigationViewPresenter();
			}
		}
	}

	[SecondViewPresenter()]
	public class SecondView : MvxViewController
	{
		public SecondView ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			View.BackgroundColor = UIColor.Purple;
		}
	}
}

