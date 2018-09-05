# NameSorter project

## Purpose
---
This project is an assessment project (the project) requested by GlobalX to demonstrate a set of practices employed in the solution for a simple problem domain.

The problem domain is to sort a set of names by Last name, and up to 3 given names. Instead of the problem domain, the assessment focuses on the practices employed in the design and coding, with the intention of future maintainability and extensibility by developers other than the creator.

The problem doamin has been simplified to show employed design and coding practices, so some obvious features such as allowing the name list sorted descending are left out.

The project is to target .NET Core as a console application that accepts a file as input.

## Design
---
S.O.L.I.D is the OOD principles to be applied in the project. General design patterns are used when they are necessary.

The project is divided into 3 major components in gerneral, Source, Algorithm and Desitination. Source provides targets on which the Algorithm is applied, the result of the Algorithm is to be output to the Destinations. This design enforces the principle of Single Responsibility and Interface Segregation by dividing features into small units.

An INameSorter interface is used to present the name sorting service. The interface facilitates the Dependent Injection (DI) principle by declaring the dependencies to the above Source, Algorithm and Destination. 

As the dependencies can be swapped with other implementations according to Liskov substitution principle, Open and Close principle is facilitated.

## Build
---
The project is developed with .NET Core 2.1 and targeting both netcoreapp2.0.  

First you need to checkout the source by giving command on a command prompt:
```
$ git clone https://github.com/yzhu228/NameSorter.git
```

The command will checkout the source into a folder named `NameSorter`. Now issue the following commands:
```
$ cd NameSorter
$ dotnet build
```

## Test
---
The project is developed with testing in mind. There are unit tests exist to stress test the modules in the solution. To run test:
```
$ dotnet test ./NameSorter.Test/
```

## Run
---
To acquire help on running the name-sorter application, issue command as below:
```
$ dotnet run --project ./NameSorter.App/ -- --help
```
To run the name-sorter with the shipped sample name list `name-list.txt`, issue command below"
```
$ dotnet run --project ./NameSorter.App/ -- ./NameSorter.App/name-list.txt
```
This command generates a file named `sorted-names-list.txt` in the current working directory as well as output the list on the console window.

If you want a name other than the default, use `-o` option to specify the output file name:
```
$ dotnet run --project ./NameSorter.App/ -- ./NameSorter.App/name-list.txt -o names-sorted.txt
```

## Improvment and Refactor
---
There are many places upon which this solution can be improved. Here I just list few:

* Sort descenting
* Provide INameSorter factory to reduce couple between client the library.
* Possible employment of IoC for DI.
* Asynchronous sorting 



    
  

