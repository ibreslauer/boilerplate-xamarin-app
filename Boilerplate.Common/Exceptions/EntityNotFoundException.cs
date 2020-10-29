using System;

namespace Boilerplate.Common.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public object Entity { get; private set; }
        public EntityNotFoundException() { }

        public EntityNotFoundException(object entity)
        {
            Entity = entity;
        }
    }
}
