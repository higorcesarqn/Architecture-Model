using System.Threading.Tasks;
using Core.Events;
using FluentValidation.Results;

namespace Core.Commands
{
    public abstract class Command : Event
    {
        public ValidationResult ValidationResult { get; set; }

        public abstract Task<bool> IsValid();
    }
}