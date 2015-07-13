using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Convenience
{
    /// <summary>
    /// Provides utility functions for managing system time.
    /// </summary>
    public class SystemTime
    {
        [DllImport("Kernel32.dll")]
        private extern static void GetSystemTime(ref SYSTEMTIME lpSystemTime);

        [DllImport("Kernel32.dll")]
        private extern static uint SetSystemTime(ref SYSTEMTIME lpSystemTime);

        private struct SYSTEMTIME
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMilliseconds;
        }

        /// <summary>
        /// Gets current system time.
        /// </summary>
        /// <returns></returns>
        public static DateTime GetTime()
        {
            // Call the native GetSystemTime method 
            // with the defined structure.
            SYSTEMTIME stime = new SYSTEMTIME();
            GetSystemTime(ref stime);

            var utc = new DateTime(stime.wYear, stime.wMonth, stime.wDay, stime.wHour, stime.wMinute, stime.wSecond, stime.wMilliseconds, DateTimeKind.Utc);
            return utc.ToLocalTime();
        }

        /// <summary>
        /// Sets current system time.
        /// </summary>
        /// <param name="dateTime">Time to set as current system time.</param>
        public static void SetTime(DateTime dateTime)
        {
            dateTime = dateTime.ToUniversalTime();
            SYSTEMTIME stime = new SYSTEMTIME()
            {
                wYear = (ushort)dateTime.Year,
                wMonth = (ushort)dateTime.Month,
                wDay = (ushort)dateTime.Day,
                wHour = (ushort)dateTime.Hour,
                wMinute = (ushort)dateTime.Minute,
                wSecond = (ushort)dateTime.Second,
                wMilliseconds = (ushort)dateTime.Millisecond
            };
            SetSystemTime(ref stime);
        }

        /// <summary>
        /// Stops Windows services which perform system time synchronization (w32time and vmictimesync).
        /// Changed system time while services are running will only be in place until those services
        /// fix it (matter of couple of seconds). Thus, services need to be disabled in order to change the time.
        /// </summary>
        public static void StopTimeSyncServices()
        {
            ServiceController sc;
            var services = ServiceController.GetServices();
            var w32time = services.FirstOrDefault(svc => "w32time".Equals(svc.ServiceName, StringComparison.CurrentCultureIgnoreCase));
            var hypervTime = services.FirstOrDefault(svc => "vmictimesync".Equals(svc.ServiceName, StringComparison.CurrentCultureIgnoreCase));
            if (w32time != null && w32time.Status == ServiceControllerStatus.Running)
                w32time.Stop();
            if (hypervTime != null && hypervTime.Status == ServiceControllerStatus.Running)
                hypervTime.Stop();
        }

        /// <summary>
        /// Starts Windows services which perform system time synchronization. See <see cref="StopTimeSyncServices"/> for mode details.
        /// </summary>
        public static void StartTimeSyncServices()
        {
            ServiceController sc;
            var services = ServiceController.GetServices();
            var w32time = services.FirstOrDefault(svc => "w32time".Equals(svc.ServiceName, StringComparison.CurrentCultureIgnoreCase));
            var hypervTime = services.FirstOrDefault(svc => "vmictimesync".Equals(svc.ServiceName, StringComparison.CurrentCultureIgnoreCase));
            if (w32time != null && w32time.Status == ServiceControllerStatus.Stopped)
                w32time.Start();
            if (hypervTime != null && hypervTime.Status == ServiceControllerStatus.Stopped)
                hypervTime.Start();
        }
    }
}
