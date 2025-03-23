using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.OrmFramework.Events
{
    public interface IEventPublisher
    {
        void PublishEvent(string eventName, object eventData);
    }
}
