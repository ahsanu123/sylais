namespace Sylais.Steps;

public interface IBaseStep : IDisposable
{
    public void Run();
    public void TakeAudioEngine();
}
