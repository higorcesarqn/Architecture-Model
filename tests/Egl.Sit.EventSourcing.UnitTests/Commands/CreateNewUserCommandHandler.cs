using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Egl.Sit.EventSourcing.UnitTests.Commands
{
    public class CreateNewUserCommandHandler : IRequestHandler<CreateNewUserCommand>
    {
        public Task<Unit> Handle(CreateNewUserCommand request, CancellationToken cancellationToken)
        {
            return Unit.Task;
        }
    }
}
