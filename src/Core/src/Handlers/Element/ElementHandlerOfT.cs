using System;

namespace Microsoft.Maui.Handlers
{
	public abstract partial class ElementHandler<TVirtualView, TNativeView> : ElementHandler, IElementHandler
		where TVirtualView : class, IElement
		where TNativeView : class
	{
		[HotReload.OnHotReload]
		internal static void OnHotReload()
		{
			Defaults<ElementHandler<TVirtualView, TNativeView>>.HasSetDefaults = false;
		}

		protected ElementHandler(PropertyMapper mapper)
			: base(mapper)
		{
		}

		public new TNativeView NativeView
		{
			get => (TNativeView?)base.NativeView ?? throw new InvalidOperationException($"NativeView cannot be null here");
			private set => base.NativeView = value;
		}

		public new TVirtualView VirtualView
		{
			get => (TVirtualView?)base.VirtualView ?? throw new InvalidOperationException($"VirtualView cannot be null here");
			private protected set => base.VirtualView = value;
		}

		IElement? IElementHandler.VirtualView => base.VirtualView;

		object? IElementHandler.NativeView => base.NativeView;

		protected override bool HasSetDefaults
		{
			get => Defaults<ElementHandler<TVirtualView, TNativeView>>.HasSetDefaults;
			set => Defaults<ElementHandler<TVirtualView, TNativeView>>.HasSetDefaults = value;
		}

		protected abstract TNativeView CreateNativeElement();

		protected virtual void SetupDefaults(TNativeView nativeView)
		{
		}

		protected virtual void ConnectHandler(TNativeView nativeView)
		{
		}

		protected virtual void DisconnectHandler(TNativeView nativeView)
		{
		}

		private protected override object OnCreateNativeElement() =>
			CreateNativeElement();

		private protected override void OnSetupDefaults(object nativeView) =>
			SetupDefaults((TNativeView)nativeView);

		private protected override void OnConnectHandler(object nativeView) =>
			ConnectHandler((TNativeView)nativeView);

		private protected override void OnDisconnectHandler(object nativeView) =>
			DisconnectHandler((TNativeView)nativeView);
	}
}