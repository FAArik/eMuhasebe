using eMuhasebeApi.Domain.Entities;
using FluentEmail.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace eMuhasebeApi.Domain.Events;
public class SendConfirmEmailEvent(UserManager<AppUser> userManager, IFluentEmail fluentEmail) : INotificationHandler<AppUserEvent>
{
    public async Task Handle(AppUserEvent notification, CancellationToken cancellationToken)
    {
        AppUser? appUser = await userManager.FindByIdAsync(notification.UserId.ToString());
        if (appUser is not null)
        {
            await fluentEmail
                .To(appUser.Email)
                .Subject("Mail onayı")
                .Body(CreateBody(appUser), true)
                .SendAsync(cancellationToken);
        }
    }

    private static string CreateBody(AppUser appUser)
    {
        return $@"Mail adresinizi onaylamak için aşşağıdaki linke tıklayın.
                       <a href='http://localhost:4200/confirm-email/{appUser.Email}' target='_blank'>Onaylamak için tıklayın</a>";
    }
}
