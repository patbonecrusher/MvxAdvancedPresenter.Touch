using CoreGraphics;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using Foundation;
using ObjCRuntime;
using UIKit;
using Coc.MvxAdvancedPresenter.Touch.Attributes;
using System;

namespace MvxAdvancedPresenterTest.Touch.Views
{
    [Register("FirstView")]
	[PresenterAttribute(typeof(Coc.MvxAdvancedPresenter.Touch.SingleViewPresenter), TransitionType = typeof(FadeTransition))]
    public class FirstView : MvxViewController
    {
		UIButton button;
		~FirstView ()
		{
			Console.WriteLine ("First view killed");
		}
        public override void ViewDidLoad()
        {
            //View = new UIView(){ BackgroundColor = UIColor.White};
            base.ViewDidLoad();

			View.BackgroundColor = UIColor.White;
			button = new UIButton(new CGRect(10, 90, 300, 40));
			button.SetTitle("Logon!", UIControlState.Normal);
			button.SetTitleColor(UIColor.Black, UIControlState.Normal);
			Add(button);

			// ios7 layout
            if (RespondsToSelector(new Selector("edgesForExtendedLayout")))
               EdgesForExtendedLayout = UIRectEdge.None;
			   
//            var label = new UILabel(new CGRect(10, 10, 300, 40));
//            Add(label);
//            var textField = new UITextField(new CGRect(10, 50, 300, 40));
//            Add(textField);
//
            var set = this.CreateBindingSet<FirstView, Core.ViewModels.FirstViewModel>();
//            set.Bind(label).To(vm => vm.Hello);
//            set.Bind(textField).To(vm => vm.Hello);
			set.Bind(button).To(vm => vm.LogonCommand);
            set.Apply();
        }

		bool _disposed;
		protected override void Dispose (bool disposing)
		{
			if (_disposed) { return; }
			if (disposing) {
				FreeManagedResources();
				_disposed = true;
			}

			base.Dispose(disposing);
		}

		protected virtual void FreeManagedResources ()
		{
			button.RemoveFromSuperview();
			button.Dispose();
			button = null;

			//ViewModel.Dispose();
			DataContext = null;
			View.Dispose();
			View = null;
		}

    }
}