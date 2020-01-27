using System.Text.RegularExpressions;
using Core.ExceptionHandling;
using Core.Models;

namespace Core.ValuesObjects
{
    public class Email : ValueObject<Email>
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
        protected override bool EqualsCore(Email other)
        {
            return Address == other.Address;
        }

        protected override int GetHashCodeCore()
        {
            return Address.GetHashCode() * 254;
        }

        public static implicit operator Email(string value)
          => new Email(value);

        public static implicit operator string(Email value)
          => value.Address;

        public override string ToString() => Address;

        public bool IsValid()
        {
            return Regex.IsMatch(Address, _regex);
        }
    }
}
