using System.Collections.Immutable;
using TemplateNetCore.Domain.Models.v1;

namespace TemplateNetCore.Domain.Interfaces.Services.v1
{
    public interface INotificationContext
    {
        ImmutableList<Error> Errors { get; }
        void AddError(Error error);
    }
}
