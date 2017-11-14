Desguised Eraser
=====================================

### Summary

A little application that deletes files from user personal folders. It's disguised to look like Google Chrome.The idea is to create an apliccation that copies itself to `C:/ProgramFiles/Google/Chrome/Application` and then change the `chrome.exe` file to `chromer.exe` or something like that. When `chrome.exe` is executed, the application should delete files and then start the Chrome browser.

Notice: the binary generated in the `debug` or `release` folder won't delete anything. By now, you should add the methods that would delete files to the class constructor.

More features will be added to the application in a soon future.

* This is a work in progress. Any help is welcome. 

``