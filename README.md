# Sparse Affinity Propagation C# 
Implementation of the affinitypropagation a clustering algorithm by passing messages between data points. Optimizedfor sparse similarity matrix.

Compile Debug mode: dotnet build<br />
Compile Release mode: dotnet build --configuration Release<br />

Expected output from toy.csv<br />
2 2 2 2 2 2 6 6 6 6 2 6 2 6 6 19 19 19 19 19 19 2 19 19 6<br />

**Csv files can have two formats**<br />

**1)similarity file of the form**<br />

**int,int,float**<br />

example:<br />

1,3,-3.32424<br />
3,5,2.41241<br />
5,1,-3.42423<br />
.....<br />
.....<br />
Note: File should not include the main diagonal values<br />

**2)2D points file of the form**<br />

**float,float,float**<br />

example:<br />
-2.3415,3.6968<br />
-1.1092,3.1117<br />
-1.5669,1.8351<br />
-2.6585,0.6649<br />
-4.0317,2.8457<br />
-3.081,2.1011<br />
.....<br />
.....<br />
