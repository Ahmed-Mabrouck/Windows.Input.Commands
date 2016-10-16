# Windows.Input.Commands
A very rich implementation for ICommand pattern with 24 types of commands that meet all the requirements.

# Installation: 
You can install it through NuGet: [Windows Commands NuGet](https://www.nuget.org/packages/Windows.Input.Commands/)

# Usage:
Windows.Input.Commands provides 24 different implementations of ICommand pattern:

1. Basic Commands [CanExecuteChanged must be triggered explicitly]:
  1. Single Handler Command [Calls only single delegate on execution]
    1. Object Paramtered.
    2. Generic Paramtered
    3. Parameterless.
    4. Asynchronous:
      1. Object Paramtered.
      2. Generic Paramtered
      3. Parameterless.
  2. Blended Handlers Commands [Call array of delegates on execution]
    1. Object Paramtered.
    2. Generic Paramtered
    3. Parameterless.
    4. Asynchronous:
      1. Object Paramtered.
      2. Generic Paramtered
      3. Parameterless.
2. Observable Commands [CanExecuteChanged is triggered implicicty, but canExecute must be an expressions which returns an observable property]:
  1. Single Handler Command [Calls only single delegate on execution]
    1. Object Paramtered.
    2. Generic Paramtered
    3. Parameterless.
    4. Asynchronous:
      1. Object Paramtered.
      2. Generic Paramtered
      3. Parameterless.
  2. Blended Handlers Commands [Call array of delegates on execution]
    1. Object Paramtered.
    2. Generic Paramtered
    3. Parameterless.
    4. Asynchronous:
      1. Object Paramtered.
      2. Generic Paramtered
      3. Parameterless.
      
For better details, you can follow this code map:
![Windows.Input.Commands Code Map](Code-Map/Code Map.png?raw=true "Windows.Input.Commands Code Map")

For better quality, attached in Code-Map folder a higher resolution xls file for the code map.
