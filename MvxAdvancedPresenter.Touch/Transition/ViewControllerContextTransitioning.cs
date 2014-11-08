using System;
using MonoTouch.UIKit;

namespace MvxAdvancedPresenter.Touch.Transition
{
	internal class ViewControllerContextTransitioning : UIViewControllerContextTransitioning
	{
		private Action _completedAction;
		private UIViewController _toController;
		private UIViewController _fromController;
		private UIView _containerView;

		public ViewControllerContextTransitioning (
			UIView containerView, UIViewController fromController, 
			UIViewController toController, Action completedAction
		)
		{
			_completedAction = completedAction;
			_fromController = fromController;
			_toController = toController;
			_containerView = containerView;
		}

		public override UIViewController GetViewControllerForKey (MonoTouch.Foundation.NSString uiTransitionKey)
		{
			if (uiTransitionKey == UITransitionContext.FromViewControllerKey) { return _fromController; }
			if (uiTransitionKey == UITransitionContext.ToViewControllerKey) { return _toController; }
			return null;
		}

		public override void CompleteTransition (bool didComplete)
		{
			_completedAction();
		}

		public override UIView ContainerView { get { return _containerView; } }
		public override bool IsAnimated { get { return false; } }
		public override bool IsInteractive { get { return false; } }
		public override UIModalPresentationStyle PresentationStyle { get { return UIModalPresentationStyle.Custom; } }
		public override bool TransitionWasCancelled { get { return false; } }

		public override System.Drawing.RectangleF GetFinalFrameForViewController (UIViewController vc)
		{
			return System.Drawing.RectangleF.Empty;
		}

		public override System.Drawing.RectangleF GetInitialFrameForViewController (UIViewController vc)
		{
			return System.Drawing.RectangleF.Empty;
		}

		public override void UpdateInteractiveTransition (float percentComplete) { }

		public override void CancelInteractiveTransition () { }

		public override void FinishInteractiveTransition () { }
	}

}


