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

#### Venu Details binaries
```bash
PM> Install-Package Locu.VenueDetails
```

## Dependencies

The current version of the Locu SDK for .NET has the following dependencies:

- [Microsoft.Net.Http](https://www.nuget.org/packages/Microsoft.Net.Http/)
- [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/)

These dependencies can be downloaded directly or referenced by your project through NuGet.

## Code Samples

### Searching for a venue

First, include the classes you need.

```csharp
using Locu.VenuSearch;
```

Next, define the parameters of your search using a VenueSearchRequest object. The following example shows how to search for venues with menus in Minneapolis, MN.

```csharp
var request = new VenueSearchRequest(apiKey);
request.Locality = "Minneapolis";
request.Region = "MN";
parameters.HasMenu = true;
```

Finally, create an instance of the VenueSearchClient class and call the SendAsync method with your request object.

```csharp
var client = new VenueSearchClient();
var result = await client.SendAsync(request);
```

### Getting venue details

First, include classes you need.

```csharp
using Locu.VenueDetails;
```

Next, create a VenueDetailsRequest object using your API key and the Locu venue ID of the venue you want to retrieve details for.

```csharp
var request = new VenueDetailsRequest(apiKey, venueId);
```

Last, create an instance of the VenueDetailsClient class and call the SendAsync method with your request object.

```csharp
var client = new VenueDetailsClient();
var response = await client.SendAsync(request);
```

## Tools

### Locu Venue Details Console

The source code also contains the Locu Venue Details Console. This tool allows you to save the details of a known Locu venue to a local json file. The following parameters can be passed in to the tool:

The call to the tool will look like this:

- _apiKey_: Your Locu API key
- _venueId_: The Locu Id of the venue you want save the details of
- _filePath_: The path to the file where you want to save the venue details

```bash
VenueDetails apiKey venueId filePath
```

If you do not provide all three parameters, the tool will prompt you for them.

![Powered by Locu](/docs/images/Locu/poweredby-color.png)