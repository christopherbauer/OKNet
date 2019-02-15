# OKNet
An open source kiosk wpf application for displaying sites

## Configuring the Host

Both the machine and the display will need to be set to not sleep, this can be handled via manually setting Power Options on the target machine or via GPO.

Note that some organizations might enforce GPOs that turn off displays/sleep machines after a set amount of time. To determine if you are being affected by this, use CMD and run `gpresult /Scope User /v`.

###Checklist

[ ] No GPO for sleeping the machine
[ ] Power and Sleep Settings
    [ ] Screen > Never
    [ ] Sleep > Never
    
## Configuring the Kiosk

Configuration of the Kiosk is handled by adding an appsettings.json file in the application root. An example configuration file is included in the project and can be used to display information after adding credentials/details.

This section will be updated for clarity in the future
