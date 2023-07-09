using NUnit.Framework;
using Microsoft.Web.WebView2.Wpf;
using WpfPilot;

namespace WpfWebView2Sample.UITests;

[TestFixture]
public sealed class WpfWebView2SampleTests
{
    [Test]
    public void TestWebView2()
    {
        using var appDriver = AppDriver.Launch(@"..\..\..\..\..\src\bin\Debug\net7.0-windows\WpfWebView2Sample.exe");
        
        var webView2 = appDriver.GetElement<WebView2Element>(x => x["Name"] == "webView");
        webView2.NavigateTo("https://wpfpilot.dev/");
        webView2.WaitForElement("get-started");

        // Change the target="_blank" for testing purposes.
        webView2.InvokeAsync<WebView2, string>(x => x.CoreWebView2.ExecuteScriptAsync("document.getElementById('get-started').target = ''"));

        webView2.ClickById("get-started");

        webView2.WaitForElement("tutorial-id");
        webView2.GetElementInnerHtmlById("__docusaurus", out var html);
        
        Assert.True(html.Contains("Tutorial"));
    }
}