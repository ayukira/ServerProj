using System.Net;
using System.Net.NetworkInformation;

namespace ServerProj.Base.Utils
{
    public class CUtils
    {
        /// <summary>
        /// 检查主机:端口，true表示已被占用
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static bool IsPortOccuped(string host,int port)
        {
            var address = IPAddress.Parse(host);
            IPGlobalProperties iproperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = iproperties.GetActiveTcpListeners();
            foreach (var item in ipEndPoints)
            {
                if (item.Port == port && item.Address.Equals(address))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
