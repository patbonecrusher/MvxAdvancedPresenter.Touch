using System.Drawing;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;
using MvxAdvancedPresenter.Touch.Attributes;

namespace MvxAdvancedPresenterTest.Touch.Views
{
    [Register("FirstView")]
	[PresenterAttribute(typeof(MvxAdvancedPresenter.Touch.SingleViewPresenter), TransitionType = typeof(FadeTransition))]
    public class FirstView : MvxViewController
    {
        public override void ViewDidLoad()
        {
            //View = new UIView(){ BackgroundColor = UIColor.White};
            base.ViewDidLoad();

			View.BackgroundColor = UIColor.White;
			UIButton button = new UIButton(new RectangleF(10, 90, 300, 40));
			button.SetTitle("Logon!", UIControlState.Normal);
			button.SetTitleColor(UIColor.Black, UIControlState.Normal);
			Add(button);

			// ios7 layout
            if (RespondsToSelector(new Selector("edgesForExtendedLayout")))
               EdgesForExtendedLayout = UIRectEdge.None;
			   
            var label = new UILabel(new RectangleF(10, 10, 300, 40));
            Add(label);
            var textField = new UITextField(new RectangleF(10, 50, 300, 40));
            Add(textField);

            var set = this.CreateBindingSet<FirstView, Core.ViewModels.FirstViewModel>();
            set.Bind(label).To(vm => vm.Hello);
            set.Bind(textField).To(vm => vm.Hello);
			set.Bind(button).To(vm => vm.LogonCommand);
            set.Apply();
        }
    }
}