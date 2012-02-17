Thank you for checking out the StreamDesk/NasuTek M3 Source Code! Each Directory Tree contains different code that involves
the StreamDesk project.

Directory Tree
==============
* NasuTek-M3: Contains the .NET NasuTek M3 Media Library which is used by StreamDesk for its core features like for reading and using the M3 database.
* StreamDesk-Winforms: The WinForms version of StreamDesk.
* StreamDesk-Cocoa: The Mac Version of StreamDesk.
* Installers: Where installer source code is provided.
* Tests: Contains tests for either StreamDesk or NasuTek M3. Release maintainers it is required to run these test suites before releasing.

Licensing
=========
All files are licensed under Apache License 2.0. For more information on licensed information read LICENSE and NOTICE
for more information.

Building StreamDesk
===================

WinForms StreamDesk (Windows)
-----------------------------
To build WinForms StreamDesk do the following

1. You can either build StreamDesk with the Solution File in "StreamDesk-WinForms" or running the Build_Debug.cmd or Build_Release.cmd files (In the Visual Studio Command Line if "msbuild" isn't in your PATH).

There are no dependencies required to build WinForms StreamDesk, any dependency is included in the source code tree.

NOTE: This UI will be discontinued on the release of StreamDesk 3.1. For more information please view http://redmine.nasutek.com/issues/25

NasuTek M3 (Media Library)
--------------------------
When you build StreamDesk this is automatically built for you. But if you just want to use StreamDesk's Media Library (NasuTek M3) you may build this separately.

To build M3 by itself do the following

1. You can either build M3 with the Solution File in "NasuTek-M3" or running the Build_Debug.cmd or Build_Release.cmd files (In the Visual Studio Command Line if "msbuild" isn't in your PATH).

There are no dependencies required to build M3, any dependency is included in the source code tree.

Cocoa StreamDesk (Mac OS X)
---------------------------
To build Cocoa StreamDesk do the following

1. You can either build StreamDesk with the Solution File in "StreamDesk-Cocoa" or running the Build_Debug.sh or Build_Release.sh files.

You do need the MonoMac Addin installed in MonoDevelop before you can build Cocoa StreamDesk as this is needed for the Cocoa Bindings.