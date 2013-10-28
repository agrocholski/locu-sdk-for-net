Locu SDK for .NET
================

The Locu SDK for .NET allows you to build .NET applications for the Microsoft platform (including ASP.NET, Windows 8, and Windows Phone 8) that leverage Locu.

## Requirements

In order to use the Locu SDK for .NET, you will need a Locu API key. An API key can be obtained [here](https://dev.locu.com)

## Download & Install

### Via Git

To get the source code of the Locu SDK for .NET via git just type:

```bash
git clone https://github.com/agrocholski/locu-sdk-for-net
cd locu-sdk-for-net
```

### Via NuGet

To get the binaries associated with this project you can also have them installed by the .NET package manager [NuGet](http://www.nuget.org)

#### Venue Search binaries
```bash
PM> Install-Package Locu.VenueSearch
```

## Code Samples

### Searching for a Venue

First, include the classes you need.

```csharp
using Locu.VenuSearch;
```

Next, define the parameters of your search using a VenueSearchObject. The following example shows how to search for venues with menus in Minneapolis, MN.

```csharp
var request = new VenueSearchRequest(apiKey);
request.Locality = "Minneapolis";
request.Region = "MN";
parameters.HasMenu = true;
```

Finally, create an instance of the VenueSearchClient class and call the SendAsync method with your request object.

```csharp
var search = new VenueSearchClient();
var result = await search.SendAsync(request);
```

![Powered by Locu](/Images/Locu/poweredby-color.png)