
FROM microsoft/dotnet:nanoserver


ARG source=.
WORKDIR /app
COPY $source .

RUN dotnet restore
ENTRYPOINT ["dotnet", "run"]
#ENTRYPOINT ["dotnet", "CustomerTrackingService.dll"]