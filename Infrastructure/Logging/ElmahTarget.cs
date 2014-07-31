using Elmah;
using NLog;
using NLog.Targets;

namespace AIMS.Infrastructure.Logging
{
    [Target("ElmahTarget")]
    public sealed class ElmahTarget : TargetWithLayout
    {
        protected override void Write(LogEventInfo logEvent)
        {
            if (logEvent.Exception != null)
            {
                ErrorSignal.FromCurrentContext().Raise(logEvent.Exception);
            }
        }
    }
}