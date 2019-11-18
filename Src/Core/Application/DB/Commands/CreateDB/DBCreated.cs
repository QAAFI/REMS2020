using MediatR;
using Rems.Application.Common.Interfaces;
using Rems.Application.Notifications.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.DB.Commands.SetCurrentDB
{
    public class DBCreated : INotification
    {
        public IEnumerable<string> Tables { get; set; }

        public class DBCreatedHandler : INotificationHandler<DBCreated>
        {
            private readonly INotificationService _notification;

            public DBCreatedHandler(INotificationService notification)
            {
                _notification = notification;
            }

            public async Task Handle(DBCreated notification, CancellationToken cancellationToken)
            {
                await _notification.SendAsync(new MessageDto());
            }
        }
    }
}
