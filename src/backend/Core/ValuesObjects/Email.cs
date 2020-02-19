using System.Collections.Generic;
using System.Text.RegularExpressions;
using Core.ExceptionHandling;
using Core.Models;

namespace Core.ValuesObjects
{
    public class Email : ValueObject
    {
        private const string _regex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                     @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
        public Email(string address)
        {
            Address = address?.ToLower();
            if (!IsValid())
            {
                throw new ValuesObjectsException<Email>(this, $"O Email é inválido - {address}");
            }
        }

        public string Address { get; private set; }
       
        public static implicit operator Email(string value)
          => new Email(value);

        public static implicit operator string(Email value)
          => value.Address;

        public override string ToString() => Address;

        public bool IsValid()
        {
            return Regex.IsMatch(Address, _regex);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Address;
        }
    }
}
