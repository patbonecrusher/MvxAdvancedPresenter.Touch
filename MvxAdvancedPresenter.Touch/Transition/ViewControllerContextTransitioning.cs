using System;
using UIKit;

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

		public override UIViewController GetViewControllerForKey (Foundation.NSString uiTransitionKey)
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

		public override CoreGraphics.CGRect GetFinalFrameForViewController (UIViewController vc)
		{
			return CoreGraphics.CGRect.Empty;
		}

		public override CoreGraphics.CGRect GetInitialFrameForViewController (UIViewController vc)
		{
			return CoreGraphics.CGRect.Empty;
		}

		#region implemented abstract members of UIViewControllerContextTransitioning

		public override UIView GetViewFor (Foundation.NSString uiTransitionContextToOrFromKey)
		{
			throw new NotImplementedException ();
		}

		public override CoreGraphics.CGAffineTransform TargetTransform {
			get {
				throw new NotImplementedException ();
			}
		}

		#endregion

		public override void UpdateInteractiveTransition (nfloat percentComplete) { }

		public override void CancelInteractiveTransition () { }

		public override void FinishInteractiveTransition () { }
	}

}


