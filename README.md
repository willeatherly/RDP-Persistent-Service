# RDP-Persistent-Service

## What does it do?

This service turns on RDP every 30 seconds on a users computer. If the service is turned off, the device will shut down. This makes it a useful competition persistance tool, as well as being an annoyance if the team discovers that the service is running. The main program is located 

## Usage

To use, follow the installation link at the end of this readme. It is recommended that you set the task to start at boot, though this is not required for the service to funciton. 

Note: You will need to download the code here and compile it yourself in Visual Studio before these steps can be taken. The files here are project files for it. Download the whole file, move it to the repos folder, then open the .sln file in visual studio. You should be able to 'publish it from there and use the .exe in your installutils command.

Permissions: Administrator

Use: Persistance

Installing a service: https://docs.microsoft.com/en-us/dotnet/framework/windows-services/how-to-install-and-uninstall-services


### To fix

- Naming needs to be changed.
- More thourough testing needs to be done on module.
