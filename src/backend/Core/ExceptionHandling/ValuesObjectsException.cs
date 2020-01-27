using System;
using Core.Models;

namespace Core.ExceptionHandling
{
    public class ValuesObjectsException<TValueObject> : Exception
        where TValueObject : ValueObject<TValueObject>
    {

        public ValuesObjectsException(TValueObject valueObject, string message) : base(message)
        {
            ValueObject = valueObject;
        }

        public TValueObject ValueObject { get; private set; }
    }
}
