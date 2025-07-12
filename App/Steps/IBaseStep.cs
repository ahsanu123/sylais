namespace Sylais.Steps;

public interface IBaseStep : IDisposable
{
    public Task Run();
}
