using Core.ExceptionHandling;
using Core.Models;
using System.Collections.Generic;

namespace Domain.ValuesObjects
{
    public sealed class UserName : ValueObject
    {
        public static int MiniumLengh = 3;

        public UserName(string value)
        {
            Value = value.ToLower();
            if (!IsValid())
            {
                throw new ValuesObjectsException<UserName>(this, $"Username inválido - {Value}");
            }
        }

        public string Value { get; private set; }
      
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Value) && Value.Length >= MiniumLengh;
        }

        public static implicit operator UserName(string value)
            => new UserName(value);

        public static implicit operator string(UserName value)
            => value.Value;

        public override string ToString() => Value;

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
