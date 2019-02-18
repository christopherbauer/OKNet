# OKNet
An open source kiosk wpf application for displaying sites

## Configuring the Host

Both the machine and the display will need to be set to not sleep, this can be handled via manually setting Power Options on the target machine or via GPO.

Note that some organizations might enforce GPOs that turn off displays/sleep machines after a set amount of time. To determine if you are being affected by this, use CMD and run `gpresult /Scope User /v`.

### Checklist

 - [ ] No GPO for sleeping the machine
 - [ ] Power and Sleep Settings
   - [ ] Screen > Never
   - [ ] Sleep > Never
    
## Configuring the Kiosk

Configuration of the Kiosk is handled by adding an appsettings.json file in the application root. An example configuration file is included in the project and can be used to display information after adding credentials/details.

This section will be updated for clarity in the future

## Organization

The core project is comprised of a WPF app in .NET Framework 4.6. This will eventually migrate to .NET Core when WPF support is added.

- OKNet.App - Main WPF app which houses views/commands/frameworks for displaying the data
- OKNet.Common - The common functions of the app library. Eventually most of the functionality that is not WPF specific should go here so that other presentation frameworks (such as an MVC site) could utilize some of the functionality.
- OKNet.Core - Core functions which are not WPF-specific and have no WPF dependencies which can be utilized by many projects, IE a service for making REST requests and serializing them which would be used by multiple integration points
- OKNet.Infrastructure.* - Add additional infrastructure projects as needed to isolate the different integrations from the main project.

## Roadmap

 - [x] Proof of Concept
   - [x] Show a website without authentication
   - [x] Show basic jira project view
   - [x] Show basic jira completed view
   - [x] Show basic jira in-progress view
 - [ ] JIRA Integration
   - [x] Login as user and request API resources
   - [ ] In Progress View
     - [x] Refresh on some regular basis (default 1m)
     - [x] Basic Configuration
     - [ ] Additional Configuration
       - [ ] Parameterize update frequency 
   - [ ] Recently Completed View
 - [ ] Teams Integration
 - [ ] Jenkins Integration
