# Affinity-Propagation-Csharp-dotnet-core
Affinity propagation algorithm in C#

Compile Debug mode: dotnet build
Compile Release mode: dotnet build --configuration Release

Expected output from toy.csv
2 2 2 2 2 2 6 6 6 6 2 6 2 6 6 19 19 19 19 19 19 2 19 19 6

**Csv files can have two formats**

**1)similarity file of the form**

**int,int,float**

example:

1,3,-3.32424
3,5,2.41241
5,1,-3.42423
.....
.....
Note: File should not include the main diagonal values

**2)2D points file of the form**

**float,float,float**

example:
-2.3415,3.6968
-1.1092,3.1117
-1.5669,1.8351
-2.6585,0.6649
-4.0317,2.8457
-3.081,2.1011
.....
.....
.....
