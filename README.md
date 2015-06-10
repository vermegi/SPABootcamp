# SPABootcamp

This repository contains all code and examples of the SPA session for the 2015 QFrame bootcamp. 

To get started for the introsession, please install the following:

* git (you don't necessarily need to use git from the command line, but the git command must be known for bower (installed later) being able to run correctly)
* NodeJS: https://nodejs.org/
* from the command line, install Bower:

```
npm install -g bower
```

* Gulp:
```
npm install -g gulp
```

Restart your command prompt and make sure both bower and gulp got installed correctly and are known commands. 
After cloning this repository to your desktop, in the command prompt go to the /src/IntroSession/EventPlanner/EventPlanner directory and issue the following commands:

```
npm install
bower install
gulp
```

Make sure you have a c:\temp directory on your system and IISExpress is installed. 

This should do it. You can now open and run the eventplanner.sln solution. 

If you get an error that says "Unable to load DLL 'SQLite.Interop.dll': The specified module could not be found.": clean solution and rebuild.
