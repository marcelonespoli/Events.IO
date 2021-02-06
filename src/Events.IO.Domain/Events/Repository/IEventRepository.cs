using Events.IO.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Events.IO.Domain.Events.Repository
{
    public interface IEventRepository : IRepository<Occasion>
    {
    }
}
