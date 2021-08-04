using System;
using NativeView = UIKit.UIView;

namespace Microsoft.Maui.Handlers
{
	public partial class LayoutHandler : ViewHandler<ILayout, LayoutView>
	{
		protected override LayoutView CreateNativeView()
		{
			if (VirtualView == null)
			{
				throw new InvalidOperationException($"{nameof(VirtualView)} must be set to create a LayoutViewGroup");
			}

			var view = new LayoutView
			{
				CrossPlatformMeasure = VirtualView.LayoutManager.Measure,
				CrossPlatformArrange = VirtualView.LayoutManager.ArrangeChildren,
			};

			return view;
		}

		public override void SetVirtualView(IView view)
		{
			base.SetVirtualView(view);

			_ = NativeView ?? throw new InvalidOperationException($"{nameof(NativeView)} should have been set by base class.");
			_ = VirtualView ?? throw new InvalidOperationException($"{nameof(VirtualView)} should have been set by base class.");
			_ = MauiContext ?? throw new InvalidOperationException($"{nameof(MauiContext)} should have been set by base class.");

			NativeView.View = view;
			NativeView.CrossPlatformMeasure = VirtualView.LayoutManager.Measure;
			NativeView.CrossPlatformArrange = VirtualView.LayoutManager.ArrangeChildren;

			// Remove any previous children 
			var oldChildren = NativeView.Subviews;
			foreach (var child in oldChildren)
			{
				child.RemoveFromSuperview();
			}

			foreach (var child in VirtualView)
			{
				NativeView.AddSubview(child.ToNative(MauiContext));
			}
		}

		public void Add(IView child)
		{
			_ = NativeView ?? throw new InvalidOperationException($"{nameof(NativeView)} should have been set by base class.");
			_ = VirtualView ?? throw new InvalidOperationException($"{nameof(VirtualView)} should have been set by base class.");
			_ = MauiContext ?? throw new InvalidOperationException($"{nameof(MauiContext)} should have been set by base class.");

			NativeView.AddSubview(child.ToNative(MauiContext));
			NativeView.SetNeedsLayout();
		}

		public void Remove(IView child)
		{
			_ = NativeView ?? throw new InvalidOperationException($"{nameof(NativeView)} should have been set by base class.");
			_ = VirtualView ?? throw new InvalidOperationException($"{nameof(VirtualView)} should have been set by base class.");

			if (child?.Handler?.NativeView is NativeView nativeView)
			{
				nativeView.RemoveFromSuperview();
				NativeView.SetNeedsLayout();
			}
		}

		public void Clear() 
		{
			if (NativeView == null)
			{
				return;
			}

			var subViews = NativeView.Subviews;

			foreach (var subView in subViews)
			{
				subView.RemoveFromSuperview();
			}
		}

		public void Insert(int index, IView child)
		{
			_ = NativeView ?? throw new InvalidOperationException($"{nameof(NativeView)} should have been set by base class.");
			_ = VirtualView ?? throw new InvalidOperationException($"{nameof(VirtualView)} should have been set by base class.");
			_ = MauiContext ?? throw new InvalidOperationException($"{nameof(MauiContext)} should have been set by base class.");

			NativeView.InsertSubview(child.ToNative(MauiContext), index);
			NativeView.SetNeedsLayout();
		}

		protected override void DisconnectHandler(LayoutView nativeView)
		{
			base.DisconnectHandler(nativeView);
			Clear();
		}
	}
}
