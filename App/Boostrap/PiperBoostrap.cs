using Sylais.Steps;

namespace Sylais.Boostrap;

public class PiperBoostrap : IBaseStep
{

    public void CreatePiperVenv()
    {

    }

    public void InstallPiper()
    {

    }

    public void DownloadPiperVoice()
    {

    }

    public void Run()
    {
        CreatePiperVenv();
        InstallPiper();
        DownloadPiperVoice();
    }

    public void Dispose()
    {
    }
}
