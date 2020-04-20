using System;
using System.Collections.Generic;
using AppCenterTestBuild.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppCenterTestBuild.iOS.Services.DeviceBuildInfo))]

namespace AppCenterTestBuild.iOS.Services
{
    public class DeviceBuildInfo : IDeviceBuildInfo
    {
        public string GetDeviceBuildType()
        {
            var buildTypes = new List<string>();

#if DEBUG
            buildTypes.Add("Debug");
#endif

#if RELEASE
            buildTypes.Add("Release");
#endif

#if __APPCENTER__
            buildTypes.Add("AppCenter");
#endif

            return String.Join(", ", buildTypes);
        }
    }
}
