using BarcodeScanner.Mobile;

namespace Maui.VisionTest;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        BarcodeScanner.Mobile.Methods.AskForRequiredPermission();
    }

    private void Camera_OnDetected(object sender, BarcodeScanner.Mobile.OnDetectedEventArg e)
    {
        List<BarcodeResult> barcodeResults = e.BarcodeResults;

        string result = string.Empty;
        for (int i = 0; i < barcodeResults.Count; i++)
        {
            result += $"Type : {barcodeResults[i].BarcodeType}, Value : {barcodeResults[i].DisplayValue}{Environment.NewLine}";
        }

        Dispatcher.Dispatch(async () =>
        {
            await DisplayAlert("Result", result, "OK");
            // If you want to start scanning again
            Camera.IsScanning = true;
        });
    }
}

