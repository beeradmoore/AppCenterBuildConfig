using System;
using System.Collections.Generic;
using AppCenterTestBuild.Services;

namespace AppCenterTestBuild.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        string _formsBuildType = string.Empty;
        public string FormsBuildType
        {
            get { return _formsBuildType; }
            set { SetProperty(ref _formsBuildType, value); }
        }


        string _deviceBuildType = string.Empty;
        public string DeviceBuildType
        {
            get { return _deviceBuildType; }
            set { SetProperty(ref _deviceBuildType, value); }
        }

        public MainPageViewModel()
        {
            var deviceBuildInfo = Xamarin.Forms.DependencyService.Get<IDeviceBuildInfo>();
            DeviceBuildType = deviceBuildInfo.GetDeviceBuildType();

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

            FormsBuildType = String.Join(", ", buildTypes);
        }
    }
}
