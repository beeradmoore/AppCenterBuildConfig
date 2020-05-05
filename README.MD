Sample script to pull and compile this repository.

run_test.sh
```
#!/bin/sh

timestamp=$( date +%s )
buildfolder="AppCenterBuildConfig_$timestamp"

# Clone repo to unique folder
git clone -b master https://github.com/beeradmoore/AppCenterBuildConfig.git $buildfolder

# Go to that folder
pushd $buildfolder

# Update the CFBundleVersion with the current time in unix timestamp
/usr/libexec/PListBuddy -c "Set :CFBundleVersion $timestamp" AppCenterTestBuild.iOS/Info.plist

# Restore the same way appcenter does.
find . -name '*.sln' -type f -print0 | /usr/bin/xargs -0 grep -l 'AppCenterTestBuild.iOS.csproj' | /usr/bin/xargs -I '{}' nuget restore '{}' -DisableParallelProcessing

# Print msbuild version
msbuild /version /nologo

# Preform build and output log.
buildlogfile="../build_$timestamp.log"

msbuild ./AppCenterTestBuild.iOS/AppCenterTestBuild.iOS.csproj \
    /p:Configuration=AppCenter \
    /p:Platform=iPhone \
    /p:BuildIpa=true \
    /t:Build \
    /p:Codesignkey="REDACTED" \
    /p:CodesignProvision="REDACTED" > $buildlogfile

# Move ipa out to initial folder.
cp ./AppCenterTestBuild.iOS/bin/iPhone/AppCenter/AppCenterTestBuild.iOS.ipa ../AppCenterTestBuild.iOS.$timestamp.ipa 

popd

# This file isn't shared, but it moves the final IPA over to a server so I can download it.
php move_file.php AppCenterTestBuild.iOS.$timestamp.ipa 
```