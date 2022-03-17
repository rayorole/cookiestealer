# C# Cookiestealer

> Made by Ray Orolé

## Support

Browsers      | Tested version
------------- | -------------
Chrome        | v99.0.4844.51
Edge          | v99.0.1150.39


## How it works
When `new.exe` (chrome) or `edge.exe` (edge) in the `dist` folders are clicked, chromedriver/msedgedriver will open. After a specific timeout the script will check when logged in...
The chromedriver copies the PHPSESSID (cookie) to a MongoDB database.

## Installation
- Requires chrome to be installed
- Clone this repository and change your database connection in the MongoDB.cs
- Change additional variables and drivers in the Stealer.cs file
- Build the new source files with `dotnet build` in your terminal
- Any errors/bugs, open a issue
