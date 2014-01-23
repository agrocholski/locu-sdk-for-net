if (!(test-path "c:\code\nuget")){[IO.Directory]::CreateDirectory("c:\code\nuget")}

nuget pack Locu.VenueSearch.csproj -OutputDirectory "c:\code\nuget" -Build