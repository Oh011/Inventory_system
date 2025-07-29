using MediatR;

namespace Application.Common
{
    internal class UpdateCommandWrapper<T, TCommand> : IRequest<TCommand>
    {

        T Id { get; set; }

        public TCommand Command { get; set; }
    }
}
