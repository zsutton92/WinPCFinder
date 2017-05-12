
/* Applications : WinPCFinder
 * Class: frmMain
 * Developer: Zack Sutton
 * Date: 05/08/17
 * Purpose: This application will find Windows PCs based on their IP Address and display data about each Windows PC found.
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace WinPCFinder
{
    public partial class frmMain : Form
    {
        Thread workThread;
        string strWinPCXml = Directory.GetCurrentDirectory() + "\\winpcresults.xml";

        public frmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the btnStart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (File.Exists(strWinPCXml))
            {
                File.Delete(strWinPCXml);
            }

            string strBeginIP = String.Empty;
            string strEndIP = String.Empty;
            bool validIP = false;

            if (txtBeginIP.Text != String.Empty)
            {
                strBeginIP = txtBeginIP.Text;
            }
            else
            {
                MessageBox.Show("You must enter a value for the beggining IP address.");
                txtBeginIP.Focus();
            }

            if (txtEndingIP.Text != String.Empty)
            {
                strEndIP = txtEndingIP.Text;
            }
            else
            {
                MessageBox.Show("You must enter a value for the ending IP address.");
                txtEndingIP.Focus();
            }

            validIP = ValidateIP(strBeginIP);

            if (!validIP)
            {
                MessageBox.Show("The value entered for the beginning IP address is not valid. Please correct and try again.");
                txtBeginIP.Clear();
                txtBeginIP.Focus();
            }

            validIP = ValidateIP(strEndIP);

            if (!validIP)
            {
                MessageBox.Show("The value entered for the ending IP address is not valid. Please correct and try again.");
                txtEndingIP.Clear();
                txtEndingIP.Focus();
            }

            List<string> lstIPs = BuildIPList(strBeginIP, strEndIP);

            workThread = new Thread(() => GatherWinPCInfo(lstIPs));
            workThread.Start();

        }

        /// <summary>
        /// Validates the ip.
        /// </summary>
        /// <param name="strIpAddress">String containing the IP address to be validated..</param>
        /// <returns></returns>
        private bool ValidateIP(string strIpAddress)
        {
            bool validIP = false;
            try
            {
                IPAddress ip = IPAddress.Parse(strIpAddress);
                validIP = true;
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
            }
            return validIP;
        }

        /// <summary>
        /// Builds a list of IP address with the values between the beginning IP address
        /// and ending IP address. It also includes the beginning and ending IP addresses.
        /// </summary>
        /// <param name="strBeginIP">The string containing the beginning IP address.</param>
        /// <param name="strEndingIP">The string containing the ending IP address.</param>
        /// <returns></returns>
        private List<string> BuildIPList(string strBeginIP, string strEndingIP)
        {           
            List<string> lstIPRange = new List<string>();
            string strOctetOne, strOctetTwo, strOctetThree, strOctetFour, strIPScheme, strNextIP, strBeginIPOctetFour, strEndIPOctetFour;
            int nEndIPOctetFour, nBeginIPOctetFour;

            string[] arrSplitIP = strBeginIP.Split('.');

            strOctetOne = arrSplitIP[0];
            strOctetTwo = arrSplitIP[1];
            strOctetThree = arrSplitIP[2];
            strOctetFour = arrSplitIP[3];
            strBeginIPOctetFour = strOctetFour;

            strIPScheme = strOctetOne + "." + strOctetTwo + "." + strOctetThree + ".";

            arrSplitIP = null;
            arrSplitIP = strEndingIP.Split('.');
            strOctetFour = arrSplitIP[3];
            strEndIPOctetFour = strOctetFour;

            nEndIPOctetFour = Convert.ToInt32(strEndIPOctetFour);
            nBeginIPOctetFour = Convert.ToInt32(strBeginIPOctetFour);

            for (int i = nBeginIPOctetFour; i < nEndIPOctetFour + 1; i++)
            {
                strNextIP = strIPScheme + i.ToString();
                lstIPRange.Add(strNextIP);
            }
            return lstIPRange;            
        }

        /// <summary>
        /// Gathers the winpc information.
        /// </summary>
        /// <param name="lstIPs">list object containing IP addresses.</param>
        private void GatherWinPCInfo(List<string> lstIPs)
        {           
            List<string> lstIPsWithReply = new List<string>();
            foreach (string strIP in lstIPs)
            {
                UpdateStatusLabel(lblStatus, "Pinging " + strIP);
                using (Ping p = new Ping())
                {
                    IPAddress ip = IPAddress.Parse(strIP);
                    PingReply pingReply = p.Send(ip, 5000);
                    if (pingReply.Status == IPStatus.Success)
                    {
                        lstIPsWithReply.Add(strIP);
                    }
                }
            }

            UpdateStatusLabel(lblStatus, "Done pinging! Now gathering data...");
            if (lstIPsWithReply.Count > 0)
            {
                foreach (string strIP in lstIPsWithReply)
                {
                    try
                    {
                        WinPC winpc = new WinPC();
                        winpc.GetPCSpecs(strIP);

                        if (winpc.PCName != null)
                        {
                            UpdateStatusLabel(lblStatus, "Gathering data for: " + winpc.PCName);
                            AddWinPCToXML(winpc);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogError(ex.Message);
                    }
                }
            }

            if (File.Exists(strWinPCXml))
            {
                DataSet dsWinPC = new DataSet();
                dsWinPC.ReadXml(strWinPCXml);

                UpdateDataGridView(dgvWinPCResults, dsWinPC);
            }

            UpdateStatusLabel(lblStatus, "WinPCFinder Results:");

        }

        /// <summary>
        /// Adds the winPC object to the XML file.
        /// </summary>
        /// <param name="winPC">a WinPC object.</param>
        private void AddWinPCToXML(WinPC winPC)
        {          
            if (File.Exists(strWinPCXml))
            {
                XDocument xDoc = XDocument.Load(strWinPCXml);
                XElement xWinPC = xDoc.Element("WinPC_Results");
                xWinPC.Add(
                          new XElement("WinPC",
                          new XElement("PC_Name", winPC.PCName),
                          new XElement("PC_IP_Address", winPC.IPAddress),
                          new XElement("PC_CPU_Name", winPC.CPUName),
                          new XElement("PC_CPU_Cores", winPC.CPUCores),
                          new XElement("PC_CPU_Arch", winPC.CPUAddressWidth),
                          new XElement("PC_OS", winPC.OS),
                          new XElement("PC_OS_Install_Date", winPC.OSInstallDate),
                          new XElement("PC_RAM_Installed", winPC.RAMInstalled),
                          new XElement("PC_Mobo_Mfg", winPC.MoboMfg),
                          new XElement("PC_Mobo_Product", winPC.MoboProduct)));
                xDoc.Save(strWinPCXml);
                    
            }
            else
            {
                XmlWriterSettings xmlwSettings = new XmlWriterSettings();
                xmlwSettings.Indent = true;

                using (XmlWriter xmlWriter = XmlWriter.Create(strWinPCXml, xmlwSettings))
                {
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("WinPC_Results");
                    xmlWriter.WriteStartElement("WinPC");
                    xmlWriter.WriteElementString("PC_Name", winPC.PCName);
                    xmlWriter.WriteElementString("PC_IP_Address", winPC.IPAddress);
                    xmlWriter.WriteElementString("PC_CPU_Name", winPC.CPUName);
                    xmlWriter.WriteElementString("PC_CPU_Cores", winPC.CPUCores);
                    xmlWriter.WriteElementString("PC_CPU_Arch", winPC.CPUAddressWidth);
                    xmlWriter.WriteElementString("PC_OS", winPC.OS);
                    xmlWriter.WriteElementString("PC_OS_Install_Date", winPC.OSInstallDate);
                    xmlWriter.WriteElementString("PC_RAM_Installed", winPC.RAMInstalled);
                    xmlWriter.WriteElementString("PC_Mobo_Mfg", winPC.MoboMfg);
                    xmlWriter.WriteElementString("PC_Mobo_Product", winPC.MoboProduct);
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndDocument();
                }
            }
        }

        /// <summary>
        /// Updates the status label from another thread.
        /// </summary>
        /// <param name="lbl">The label.</param>
        /// <param name="text">The text.</param>
        private void UpdateStatusLabel(Label lbl, string text)
        {          
            this.Invoke((MethodInvoker)delegate
           {
               lbl.Text = text;
           });
        }

        /// <summary>
        /// Updates the data grid view frowm another thread.
        /// </summary>
        /// <param name="dgv">The datagridview.</param>
        /// <param name="ds">The dataset.</param>
        private void UpdateDataGridView(DataGridView dgv, DataSet ds)
        {          
            this.Invoke((MethodInvoker)delegate
            {
                dgv.DataSource = ds.Tables[0];
            });
        }


        /// <summary>
        /// Maintains the error log. 
        /// </summary>
        /// <param name="strMessage">The string message.</param>
        private void LogError(string strMessage)
        {            
            string logFile = "winpcfinder_errors.txt";

            if (File.Exists(logFile))
            {
                // Check logfile size
                FileInfo fInfo = new FileInfo(logFile);
                long fSize = fInfo.Length;
                if (fSize > 10000000)
                {
                    string replaceFile = "errorLog" + DateTime.Now.ToString() + ".log";
                    replaceFile = replaceFile.Replace("/", String.Empty);
                    replaceFile = replaceFile.Replace(":", String.Empty);
                    replaceFile = replaceFile.Replace(' ', '_');
                    File.Move(logFile, replaceFile);
                }

            }
            strMessage = DateTime.Now.ToShortTimeString() + " " + DateTime.Now.ToShortDateString() + " "  + strMessage + Environment.NewLine;
            File.AppendAllText(logFile, strMessage);

        }

        /// <summary>
        /// Handles the TextChanged event of the txtBeginIP control. This copies the first three octets of the 
        /// beginning IP address to the txtEndingIP control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtBeginIP_TextChanged(object sender, EventArgs e)
        {
            int nCount = txtBeginIP.Text.Count(n => n == '.');
            if (nCount <= 2)
            {
                txtEndingIP.Text = txtBeginIP.Text;
            }                    
        }        
    }
}
