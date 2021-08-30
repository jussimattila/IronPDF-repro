# .NET 3.1 Single-file publishing to Windows EXE fails when run on Linux

On Linux, verify you are running dotnet 3.1:
```
> dotnet --version
3.1.412
```
Clone this repository and run this command to package the program in single file executable:
```
> dotnet publish /property:PublishProfile=FolderProfile /bl
```
You will get this error:
```
Microsoft (R) Build Engine version 16.7.2+b60ddb6f4 for .NET
Copyright (C) Microsoft Corporation. All rights reserved.

/usr/share/dotnet/sdk/3.1.412/MSBuild.dll -distributedlogger:Microsoft.DotNet.Tools.MSBuild.MSBuildLogger,/usr/share/dotnet/sdk/3.1.412/dotnet.dll*Microsoft.DotNet.Tools.MSBuild.MSBuildForwardingLogger,/usr/share/dotnet/sdk/3.1.412/dotnet.dll -maxcpucount -restore -target:Publish -verbosity:m /bl /property:PublishProfile=FolderProfile ./IronPDF-bug-repro.sln
  Determining projects to restore...
  All projects are up-to-date for restore.
  IronPDF-bug-repro -> /mnt/c/Users/JussiMattila/source/repos/IronPDF-bug-repro/bin/Debug/netcoreapp3.1/win-x86/IronPDF-bug-repro.dll
/usr/share/dotnet/sdk/3.1.412/Sdks/Microsoft.NET.Sdk/targets/Microsoft.NET.Publish.targets(881,5): error MSB4018: The "GenerateBundle" task failed unexpectedly. [/mnt/c/Users/JussiMattila/source/repos/IronPDF-bug-repro/IronPDF-bug-repro.csproj]
/usr/share/dotnet/sdk/3.1.412/Sdks/Microsoft.NET.Sdk/targets/Microsoft.NET.Publish.targets(881,5): error MSB4018: System.ArgumentException: Invalid input specification: Found multiple entries with the same BundleRelativePath [/mnt/c/Users/JussiMattila/source/repos/IronPDF-bug-repro/IronPDF-bug-repro.csproj]
/usr/share/dotnet/sdk/3.1.412/Sdks/Microsoft.NET.Sdk/targets/Microsoft.NET.Publish.targets(881,5): error MSB4018:    at Microsoft.NET.HostModel.Bundle.Bundler.GenerateBundle(IReadOnlyList`1 fileSpecs) [/mnt/c/Users/JussiMattila/source/repos/IronPDF-bug-repro/IronPDF-bug-repro.csproj]
/usr/share/dotnet/sdk/3.1.412/Sdks/Microsoft.NET.Sdk/targets/Microsoft.NET.Publish.targets(881,5): error MSB4018:    at Microsoft.NET.Build.Tasks.GenerateBundle.ExecuteCore() [/mnt/c/Users/JussiMattila/source/repos/IronPDF-bug-repro/IronPDF-bug-repro.csproj]
/usr/share/dotnet/sdk/3.1.412/Sdks/Microsoft.NET.Sdk/targets/Microsoft.NET.Publish.targets(881,5): error MSB4018:    at Microsoft.NET.Build.Tasks.TaskBase.Execute() [/mnt/c/Users/JussiMattila/source/repos/IronPDF-bug-repro/IronPDF-bug-repro.csproj]
/usr/share/dotnet/sdk/3.1.412/Sdks/Microsoft.NET.Sdk/targets/Microsoft.NET.Publish.targets(881,5): error MSB4018:    at Microsoft.Build.BackEnd.TaskExecutionHost.Microsoft.Build.BackEnd.ITaskExecutionHost.Execute() [/mnt/c/Users/JussiMattila/source/repos/IronPDF-bug-repro/IronPDF-bug-repro.csproj]
/usr/share/dotnet/sdk/3.1.412/Sdks/Microsoft.NET.Sdk/targets/Microsoft.NET.Publish.targets(881,5): error MSB4018:    at Microsoft.Build.BackEnd.TaskBuilder.ExecuteInstantiatedTask(ITaskExecutionHost taskExecutionHost, TaskLoggingContext taskLoggingContext, TaskHost taskHost, ItemBucket bucket, TaskExecutionMode howToExecuteTask) [/mnt/c/Users/JussiMattila/source/repos/IronPDF-bug-repro/IronPDF-bug-repro.csproj]
```
The error is caused because IronPDF causes two files with the exact same RelativePath to be included(see screenshot below), preventing the publishing process from completing successfully. Here's some information about this: https://github.com/dotnet/sdk/issues/3465#issuecomment-859455073. Log file `msbuild.binlog` generated during publish process can be read with https://msbuildlog.com/.

![IronPdf single file publish bug](https://user-images.githubusercontent.com/8625258/131328586-da7b8dba-2aaa-417b-85d0-7351988b64b6.png)
