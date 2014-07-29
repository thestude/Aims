using AIMS.Models;
using BrockAllen.MembershipReboot;

namespace AIMS.Notifications.Email
{
    public class EmailAccountEventsHandler : EmailAccountEventsHandler<AimsUser>
    {
        public EmailAccountEventsHandler(IMessageFormatter<AimsUser> messageFormatter)
            : base(messageFormatter)
        {
        }
        public EmailAccountEventsHandler(IMessageFormatter<AimsUser> messageFormatter, IMessageDelivery messageDelivery)
            : base(messageFormatter, messageDelivery)
        {
        }
    }
}