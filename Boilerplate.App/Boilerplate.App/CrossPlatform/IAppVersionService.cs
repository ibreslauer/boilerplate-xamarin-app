namespace Boilerplate.App.CrossPlatform
{
    public interface IAppVersionService
    {
        string VersionName { get; }
        long VersionCode { get; }
    }
}
