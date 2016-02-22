using UnityEngine;
using System.Collections;
using System.Net;

public class UtilitiesToAdd : MonoBehaviour {
     
    public static string GetIP()
    {
        string strHostName = "";
        strHostName = System.Net.Dns.GetHostName();
     
        IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
     
        IPAddress[] addr = ipEntry.AddressList;
     
        return addr[addr.Length-1].ToString();
    }
}
