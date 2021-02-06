using Events.IO.Domain.Core.Commands;
using Events.IO.Domain.Core.Events;
using System.Threading.Tasks;

namespace Events.IO.Domain.Interfaces
{
    public interface IMediatorHanlder
    {
        Task SendCommand<T>(T command) where T : Command;
        Task PublishEvent<T>(T @event) where T : Event;
    }
}
