using System.Collections.Immutable;
using TemplateNetCore.Domain.Interfaces.Services.v1;
using TemplateNetCore.Domain.Models.v1;

namespace TemplateNetCore.Domain.Services.v1;

public class NotificationContext : INotificationContext
{
    private readonly List<Error> _errors;
    public ImmutableList<Error> Errors => _errors.ToImmutableList();

    public NotificationContext()
    {
        _errors = [];
    }

    public void AddError(Error error) =>
        _errors.Add(error);
}
