Thank you for checking out the StreamDesk Source Code! Each Directory Tree contains diffrent code that involves
StreamDesk.

= Directory Tree =
* Managed: Contains the .NET Released StreamDesk Library for reading and using the StreamDesk database in .NET enabled languages.
* libstreamdesk: Contains the C++ StreamDesk Library for reading and using the StreamDesk database in native code like C++/Obj-C++.
* StreamDesk-Winforms: The WinForms version of StreamDesk.
* StreamDesk-Qt: The Qt version of StreamDesk.
* StreamDesk-Cocoa: The Mac Version of StreamDesk.
* Installers: Where installer source code is provided.
* protos: Contains Source Code and Libraries involving Google Protocol Buffers and .proto files for StreamDesk Protocol Buffer databases.

= Licensing =
All files are licensed under Apache License 2.0. For more information on licensed information read LICENSE and NOTICE
for more information.

= Building StreamDesk =

Managed StreamDesk (C#)
-----------------------
To build Managed StreamDesk do the following

1. Before building the project, you must generate the Protocol Buffer files, this can be done by running Build_Protos.cmd in the "protos" directory (In the Visual Studio Command Line if "nmake" isn't in your PATH).
2. You can either build StreamDesk with the Solution File in "StreamDesk-WinForms" or running the Build_Debug.cmd or Build_Release.cmd files (In the Visual Studio Command Line if "msbuild" isn't in your PATH). The same goes if you just want to build the Managed StreamDesk Library in "Managed".

There are no dependencies required to build the Managed StreamDesk, any dependency is included in the source code tree.

libstreamdesk (C++ StreamDesk Library)
--------------------------------------
At the moment Qt StreamDesk has not been written yet. But when it is, the build instructions will be here.

Qt StreamDesk (C++ GUI)
-----------------------
At the moment Qt StreamDesk has not been written yet. But when it is, the build instructions will be here.

Cocoa StreamDesk (Mac OS X)
---------------------------
At the moment Cocoa StreamDesk has not been written yet. But when it is, the build instructions will be here.