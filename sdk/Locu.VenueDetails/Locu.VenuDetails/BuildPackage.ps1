if (!(test-path "c:\code\nuget")){[IO.Directory]::CreateDirectory("c:\code\nuget")}

nuget pack Locu.VenueDetails.csproj -OutputDirectory "c:\code\nuget" -Build