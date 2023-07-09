using WpfPilot;
using Microsoft.Web.WebView2.Wpf;

namespace WpfWebView2Sample.UITests;

class WebView2Element : Element<WebView2Element>
{
    public WebView2Element(Element element) : base(element)
    {
        if (element.TypeName != nameof(WebView2))
            throw new InvalidOperationException($"Element is not a `{nameof(WebView2)}`.");
    }

    public WebView2Element WaitForElement(string elementId)
    {
        while (true)
        {
            var isElementNull = InvokeAsync<WebView2, string>(x => x.CoreWebView2.ExecuteScriptAsync($@"document.getElementById('{elementId}') == null"));
            if (isElementNull == "false")
                break;
        }

        return this;
    }

    public WebView2Element ClickById(string elementId) =>
        InvokeAsync<WebView2>(x => x.CoreWebView2.ExecuteScriptAsync($"document.getElementById('{elementId}').click()"));

    public WebView2Element GetElementInnerHtmlById(string elementId, out string html) =>
        InvokeAsync<WebView2, string>(x => x.CoreWebView2.ExecuteScriptAsync($"document.getElementById('{elementId}').innerHTML"), out html!);

    public WebView2Element NavigateTo(string url) =>
        Invoke<WebView2>(x => x.CoreWebView2.Navigate(url));
}
