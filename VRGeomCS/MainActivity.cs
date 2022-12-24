using Android.Content.PM;
using Com.Htc.VR.Sdk;
using System.Runtime.InteropServices;
using CC = Android.Content.PM.ConfigChanges;

namespace VRGeomCS;

// Based on AndroidManifest.xml file in HelloVR example, automatically adds these parameters to app's manifest
[Activity(
    Label = "@string/app_name", 
    MainLauncher = true, 
    LaunchMode = LaunchMode.SingleTask, 
    ScreenOrientation = ScreenOrientation.Landscape,
    ConfigurationChanges = CC.Density | CC.FontScale | CC.Keyboard | CC.KeyboardHidden | CC.LayoutDirection | 
                           CC.Locale | CC.Mnc | CC.Mcc | CC.Navigation | CC.Orientation | CC.ScreenLayout | 
                           CC.ScreenSize | CC.SmallestScreenSize | CC.UiMode | CC.Touchscreen, 
    EnableVrMode = "com.htc.vr.core.server/com.htc.vr.core.server.VRListenerService")]

public class MainActivity : VRActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
    }
}