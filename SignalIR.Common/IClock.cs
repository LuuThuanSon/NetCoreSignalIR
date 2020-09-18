using System;
using System.Threading.Tasks;

namespace SignalIR.Common
{
    public interface IClock
    {
        Task ShowTime(DateTime currentTime);
        Task ReceiveMessage(string user, string message);
        Task ReceiveMessage(string message);
    }
}