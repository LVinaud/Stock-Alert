# Stock-Alert
A B3 stock alert will be sent to an email if its value is lower or higher than predefined ones.

The code is througly commented in portuguese. However, the code is simple to understand.

In order to run and use the code, simply download the src folder available in this repo and create a dotnet project using the terminal command $dotnet new console.

This code was developed in ubuntu, although it can be built into Windows or Mac OS.

The command $dotnet publish -c Release -r win-x64 --self-contained is able to create a .exe for windows use.

The command line to use it is "src.exe <STOCK-SYMBOL> <SELL-VALUE> <BUY-VALUE>".

The code can be changed to choose between frequency of stock updates and maximum time of analysis.

The smtp and email configurations are all explicited and should be changed in the config.json. A brapi token should also be generated and put into the code, which is free.
