using BarcodeScanner.Mobile;

namespace Maui.VisionTest;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        // 권한 획득
        BarcodeScanner.Mobile.Methods.AskForRequiredPermission();
    }

    private void Camera_OnDetected(object sender, BarcodeScanner.Mobile.OnDetectedEventArg e)
    {
        List<BarcodeResult> barcodeResults = e.BarcodeResults;

        string result = string.Empty;
        for (int i = 0; i < barcodeResults.Count; i++)
        {
            result += $"BarcodeType : {barcodeResults[i].BarcodeType}, Result : {barcodeResults[i].DisplayValue}{Environment.NewLine}";
        }

        Dispatcher.Dispatch(() =>
        {
            // 중복 결과 방지
            if (!resultLabel.Text.Contains(result))
            {
                // 결과 표시
                resultLabel.Text = result + Environment.NewLine + resultLabel.Text;
            }

            // 스캐딩 다시 시작
            Camera.IsScanning = true;
        });
    }

    private void FacingButton_Clicked(object sender, EventArgs e)
    {
        if (this.Camera.CameraFacing == CameraFacing.Front)
        {
            this.Camera.CameraFacing = CameraFacing.Back;
        }
        else
        {
            this.Camera.CameraFacing = CameraFacing.Front;
        }
    }

    private void ClearButton_Clicked(object sender, EventArgs e)
    {
        this.resultLabel.Text = "";
    }
}

