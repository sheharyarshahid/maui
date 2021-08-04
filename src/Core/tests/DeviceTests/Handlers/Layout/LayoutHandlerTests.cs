using System;
using System.Threading.Tasks;
using Microsoft.Maui.DeviceTests.Stubs;
using Microsoft.Maui.Handlers;
using Xunit;

namespace Microsoft.Maui.DeviceTests.Handlers.Layout
{
	[Category(TestCategory.Layout)]
	public partial class LayoutHandlerTests : HandlerTestBase<LayoutHandler, LayoutStub>
	{
		[Fact(DisplayName = "Empty layout")]
		public async Task EmptyLayout()
		{
			var layout = new LayoutStub();
			await ValidatePropertyInitValue(layout, () => layout.Count, GetNativeChildCount, 0);
		}

		[Fact(DisplayName = "Handler view count matches layout view count")]
		public async Task HandlerViewCountMatchesLayoutViewCount()
		{
			var layout = new LayoutStub();

			layout.Add(new SliderStub());
			layout.Add(new SliderStub());

			await ValidatePropertyInitValue(layout, () => layout.Count, GetNativeChildCount, 2);
		}

		[Fact(DisplayName = "Handler removes child from native layout")]
		public async Task HandlerRemovesChildFromNativeLayout()
		{
			var layout = new LayoutStub();
			var slider = new SliderStub();
			layout.Add(slider);

			var handler = await CreateHandlerAsync(layout);

			var count = await InvokeOnMainThreadAsync(() =>
			{
				handler.Remove(slider);
				return GetNativeChildCount(handler);
			});

			Assert.Equal(0, count);
		}

		[Fact(DisplayName = "DisconnectHandler removes child from native layout")]
		public async Task DisconnectHandlerRemovesChildFromNativeLayout()
		{
			var layout = new LayoutStub();
			var slider = new SliderStub();
			layout.Add(slider);

			var handler = await CreateHandlerAsync(layout);

			var count = await InvokeOnMainThreadAsync(() =>
			{
				layout.Handler.DisconnectHandler();
				return GetNativeChildCount(handler);
			});

			Assert.Equal(0, count);
		}

		[Fact]
		public async Task ClearRemovesChildrenFromNativeLayout()
		{
			var layout = new LayoutStub();
			var slider = new SliderStub();
			var button = new ButtonStub();
			
			layout.Add(slider);
			layout.Add(button);

			var handler = await CreateHandlerAsync(layout);

			var count = await InvokeOnMainThreadAsync(() =>
			{
				return GetNativeChildCount(handler);
			});

			Assert.Equal(2, count);

			count = await InvokeOnMainThreadAsync(() =>
			{
				handler.Clear();
				return GetNativeChildCount(handler);
			});

			Assert.Equal(0, count);
		}

		[Fact]
		public async Task InsertAddsChildToNativeLayout()
		{
			var layout = new LayoutStub();
			var slider = new SliderStub();
			var button = new ButtonStub();

			layout.Add(slider);
			
			var handler = await CreateHandlerAsync(layout);

			var count = await InvokeOnMainThreadAsync(() =>
			{
				return GetNativeChildCount(handler);
			});

			Assert.Equal(1, count);

			count = await InvokeOnMainThreadAsync(() =>
			{
				handler.Insert(0, button);
				return GetNativeChildCount(handler);
			});

			Assert.Equal(2, count);
		}
	}
}