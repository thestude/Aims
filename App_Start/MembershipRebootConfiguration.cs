using AIMS.Models;
using BrockAllen.MembershipReboot;
using BrockAllen.MembershipReboot.WebHost;
using DebuggerEventHandler = AIMS.Notifications.Debugger.DebuggerEventHandler;
using EmailAccountEventsHandler = AIMS.Notifications.Email.EmailAccountEventsHandler;
using SmtpMessageDelivery = AIMS.Notifications.Email.SmtpMessageDelivery;

namespace AIMS
{
    public static class MembershipRebootConfig
    {
        public static MembershipRebootConfiguration<AimsUser> Create()
        {
            var config = new MembershipRebootConfiguration<AimsUser>();
            //config.RequireAccountVerification = false;
            config.AddEventHandler(new DebuggerEventHandler());

            var appinfo = new AspNetApplicationInformation(
                                                            "AIMS",
                                                            "Alabama Incident Management System",
                                                            "Session/New",
                                                            "Change-Email/Confirm/",
                                                            "Registration/Cancel/",
                                                            "Password-Reset/Confirm/");
            var emailFormatter = new EmailMessageFormatter<AimsUser>(appinfo);
            var emailDeliveryHandler = new SmtpMessageDelivery();
            // Email notifications -- smtp settings in web.config
            config.AddEventHandler(new EmailAccountEventsHandler(emailFormatter, emailDeliveryHandler));

            // SMS notifications
            //config.AddEventHandler(new TwilloSmsEventHandler(appinfo));

            // Password complexity
            //config.ConfigurePasswordComplexity();

            var debugging = false;
#if DEBUG
            debugging = true;
#endif
            // this config enables cookies to be issued once user logs in with mobile code
            config.ConfigureTwoFactorAuthenticationCookies(debugging);

            return config;
        }
    }

    //public class MembershipRebootConfiguration : MembershipRebootConfiguration<AimsUser>
    //{
    //    public MembershipRebootConfiguration()
    //        : this(SecuritySettings.Instance)
    //    {
    //    }

    //    public MembershipRebootConfiguration(SecuritySettings securitySettings)
    //        : base(securitySettings)
    //    {
    //    }
    //}
}