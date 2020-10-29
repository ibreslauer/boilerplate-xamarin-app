using Plugin.Connectivity.Abstractions;

namespace Boilerplate.App.Services.General.Contracts
{
    public interface IConnectionService
    {
        bool IsConnected { get; }
        event ConnectivityChangedEventHandler ConnectivityChanged;
    }
}
