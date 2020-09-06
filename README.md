# ListingReportsCreator

Language: C#
<br>

Type of project: Console App (.Net Core). This project can run on .NET Core on Windows, Linux and MacOS.
<br>

This project reads two csv files which are in the folder ("../ListingReportsCreator-master/ListingReportsCreator/bin/Debug/netcoreapp3.1") and creates a command line output with 
the four reports requested. These reports accomplish all acceptance criteria. It also contains a project with unit tests for every report.

Note: Prices are formatted correctly (€ #,-) but command lines do not print correctly the euro symbol.


<b>How to run the code using Windows:</b>

1- Download the project. It doesn't matter where you download it. It uses relative paths.

2- Download .NET Core Runtime from <a>https://dotnet.microsoft.com/download</a>

3- Open a Command Prompt.

4- Move to ../ListingReportsCreator-master/ListingReportsCreator/bin/Debug/netcoreapp3.1

5- Use the command "dotnet  ListingReportsCreator.dll".

<br>


<b>How to run the tests using Windows:</b>

1- Download the project. It doesn't matter where you download it. It uses relative paths.

2- Download .NET Core SDK from <a>https://dotnet.microsoft.com/download</a>

3- Open a Command Prompt.

4- Move to ../ListingReportsCreator-master/ListingReportsCreator.UnitTests/bin/Debug/netcoreapp3.1

5- Use the command "dotnet vstest ListingReportsCreator.UnitTests.dll".

