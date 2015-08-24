using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace EPMCS.Service.Basic
{

        public class WinLogWirter
        {
            /// <summary>
            /// 事件源名称
            /// </summary>
            private string eventSourceName;
            EventLogEntryType eventLogType;
            public WinLogWirter()
            {
                eventSourceName = "EPMCS";
                eventLogType = EventLogEntryType.Error;
            }

            /// <summary>
            /// 消息事件源名称
            /// </summary>
            public string EventSourceName
            {
                set { eventSourceName = value; }
            }

            /// <summary>
            /// 消息事件类型
            /// </summary>
            public EventLogEntryType EventLogType
            {
                set { eventLogType = value; }
            }

            /// <summary>
            /// 写入系统日志
            /// </summary>
            /// <param name="message">事件内容</param>
            public void LogEvent(string message)
            {
                if (!EventLog.SourceExists(eventSourceName))
                {
                    EventLog.CreateEventSource(eventSourceName, "Application");
                }
                EventLog.WriteEntry(eventSourceName, message, eventLogType);
            }

            public void LogEvent(string message, EventLogEntryType eventLogType)
            {
                if (!EventLog.SourceExists(eventSourceName))
                {
                    EventLog.CreateEventSource(eventSourceName, "Application");
                }

                EventLog.WriteEntry(eventSourceName, message, eventLogType);
            }
        }

}
