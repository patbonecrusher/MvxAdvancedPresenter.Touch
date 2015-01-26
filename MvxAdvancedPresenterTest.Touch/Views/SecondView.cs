using System;
using Cirrious.MvvmCross.Touch.Views;
using CoreGraphics;
using UIKit;
using Coc.MvxAdvancedPresenter.Touch;
using Coc.MvxAdvancedPresenter.Touch.Attributes;
using CoreGraphics;
using Cirrious.MvvmCross.Binding.BindingContext;

namespace MvxAdvancedPresenterTest.Touch.Views
{
	public class FadeTransition : UIViewControllerAnimatedTransitioning
	{
		public float Duration = 0.65f;
		public FadeTransition (float duration)
		{
			Duration = duration;
		}

		public override double TransitionDuration(IUIViewControllerContextTransitioning transitionContext)
		{
			return Duration;
		}

		public override void AnimateTransition(IUIViewControllerContextTransitioning transitionContext)
		{
			UIViewController toViewController = transitionContext.GetViewControllerForKey(UITransitionContext.ToViewControllerKey);
			UIViewController fromViewController = transitionContext.GetViewControllerForKey(UITransitionContext.FromViewControllerKey);
			transitionContext.ContainerView.AddSubview(toViewController.View);
			toViewController.View.Alpha = 0;

			UIView.Animate(TransitionDuration(transitionContext), () => {
				fromViewController.View.Alpha = 0;
				toViewController.View.Alpha = 1;
			}, () => {
				fromViewController.View.Transform = CGAffineTransform.MakeIdentity();
				fromViewController.View.Alpha = 1;
				transitionContext.CompleteTransition(!transitionContext.TransitionWasCancelled);
			});
		}
	}

	public class SecondViewPresenter : PresenterAttribute
	{
		public SecondViewPresenter ()
		{
			
		}

		public override Coc.MvxAdvancedPresenter.Touch.BaseTouchViewPresenter CreatePresenter ()
		{
			BaseTouchViewPresenter presenter = null;
			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad) {
				presenter = new SplitViewPresenter();
			} else {
				presenter = new NavigationViewPresenter();
				presenter.Transition = new FadeTransition(0.25f);
			}
			return presenter;
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

			UIButton button = new UIButton(new CGRect(10, 90, 300, 40));
			button.SetTitle("Logoff!", UIControlState.Normal);
			button.SetTitleColor(UIColor.Black, UIControlState.Normal);
			Add(button);

			var set = this.CreateBindingSet<SecondView, Core.ViewModels.SecondViewModel>();
			set.Bind(button).To(vm => vm.LogoffCommand);
			set.Apply();

		}
	}
}

