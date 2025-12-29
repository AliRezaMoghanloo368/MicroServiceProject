using System;

namespace SharedLibrary.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object key)
        : base($"Entity \"{name}\" and key \"{key}\" was not found") { }
    }
}