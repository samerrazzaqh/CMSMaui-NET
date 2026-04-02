using System;
namespace CMSBlazor.Client.Helpers
{
    public class CMSConstant
    {
        //public static string BaseApiAddress => "https://localhost:7164/";

        public static string BaseApiAddress =
        DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5264/" : "http://localhost:5264/";
    }
}

