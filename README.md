# NameSorter project

## Purpose
---
This project is an assessment project (the project) requested by GlobalX to demonstrate a set of practices employeed in a simple problem domain solution.

The problem domain is to sort a set of names by Last name, and up to 3 given names. Instead of the problem domain, the assessment focuses on the practices employeed in the design and coding, with the intention of future maintainability and extensibility by developers other than the creator.

The project is to target .NET Core as a console application that accepts a file as input.

## Design
---
S.O.L.I.D is the OOD principles to be applied in the project. General design patterns are used when they are necessary.

The project is to be divided into 3 major components in gerneral, Source, Algorithm and Desitination. Source provides targets on which the Algorithm it to apply, the result of the Algorithm is to be output to the Destination.

## Build
---
The project is developed with .NET Core 2.1 and targeting both netcoreapp2.0 and net452. To build the project you will need to have .NET Core 2.1 and .NET Framework 4.5.2+ SDK installed on your development envrionment. 

First you can checkout the source by issuing command on a command prompt:
```
$ git clone https://github.com/yzhu228/NameSorter.git
```

The command will checkout the source into a folder named `NameSorter`. Now issue the following commands:
```
$ cd NameSorter
$ dotnet build -c Release
```
If you want to build an executable version for particular runtime, for example Windows x64:
```
$ dotnet build -c Release -r win-x64 ./NameSorter/NameSorter.csproj
```