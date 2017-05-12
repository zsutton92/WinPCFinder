/* Applications : WinPCFinder
 * Class: WinPC
 * Developer: Zack Sutton
 * Date: 05/08/17
 * Purpose: The WinPC class is used to gather data based on an IP Address. A WinPC object is created and stores info found by WMI calls.
 */
using System;
using System.Management;

namespace WinPCFinder
{
 
    public class WinPC
    {
        public string CPUName { get; set; }
        public string CPUCores { get; set; }
        public string CPUAddressWidth { get; set; }
        public string OS { get; set; }
        public string OSInstallDate { get; set; }
        public string RAMInstalled { get; set; }
        public string MoboMfg { get; set; }
        public string MoboProduct { get; set; }
        public string IPAddress { get; set; }
        public string PCName { get; set; }

        /// <summary>
        /// Gets the PC data and sets the WinPC properties.
        /// </summary>
        /// <param name="strIP">The string ip.</param>
        public void GetPCSpecs(string strIP)
        {           
            // Build the required objects to connect to the PC
            ConnectionOptions conOptions = new ConnectionOptions();
            conOptions.Impersonation = ImpersonationLevel.Impersonate;       
            ManagementScope manScope = new ManagementScope(@"\\" + strIP + @"\root\cimv2", conOptions);

            manScope.Connect();

            if (manScope.IsConnected)
            {
                // Build the required objects to make the call to WMI
                ObjectQuery query;
                ManagementObjectSearcher searcher;
                ManagementObjectCollection queryCollection;

                // Set the IP
                this.IPAddress = strIP;

                // Get the PC name
                query = new ObjectQuery("SELECT Name FROM Win32_ComputerSystem");
                searcher = new ManagementObjectSearcher(manScope, query);
                queryCollection = searcher.Get();

                foreach (ManagementObject m in queryCollection)
                {
                    this.PCName = m["Name"].ToString();
                }
               
                // Get CPU Information 
                query = new ObjectQuery("SELECT * FROM Win32_Processor");
                searcher = new ManagementObjectSearcher(manScope, query);
                queryCollection = searcher.Get();

                foreach (ManagementObject m in queryCollection)
                {
                    this.CPUName = m["Name"].ToString();
                    this.CPUAddressWidth = m["AddressWidth"].ToString();
                    this.CPUCores = m["NumberOfCores"].ToString();
                }

                // Get OS Information
                query = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
                searcher = new ManagementObjectSearcher(manScope, query);
                queryCollection = searcher.Get();

                foreach (ManagementObject m in queryCollection)
                {
                    this.OS = m["Caption"].ToString();
                    this.OSInstallDate = m["InstallDate"].ToString();
                }

                // Get RAM Information
                query = new ObjectQuery("SELECT * FROM Win32_PhysicalMemory");
                searcher = new ManagementObjectSearcher(manScope, query);
                queryCollection = searcher.Get();

                long actualGbsRam = 0;
                foreach (ManagementObject m in queryCollection)
                {
                    string strCapacity = m["Capacity"].ToString();
                    long gbsRam = ((Convert.ToInt64(strCapacity) / 1024) / 1024) / 1024;
                    actualGbsRam += gbsRam;
                    // this.strRAMCapacity += gbsRam.ToString();
                    this.RAMInstalled = actualGbsRam.ToString() + " GB";
                }

                // Get Motherboard Information
                query = new ObjectQuery("SELECT * FROM Win32_BaseBoard");
                searcher = new ManagementObjectSearcher(manScope, query);
                queryCollection = searcher.Get();

                foreach (ManagementObject m in queryCollection)
                {
                    this.MoboMfg = m["Manufacturer"].ToString();
                    this.MoboProduct = m["Product"].ToString();
                }               
            }
        }
    }
}
