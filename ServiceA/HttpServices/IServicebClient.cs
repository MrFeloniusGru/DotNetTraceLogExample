public interface IServicebClient
{
    Task<string> GetPathA(CancellationToken cancellationToken);
    Task<string> GetPathB(CancellationToken cancellationToken);
}