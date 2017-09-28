FROM microsoft/dotnet:2.0-sdk
WORKDIR /app
COPY . .
WORKDIR /app/pi-console
RUN dotnet restore
RUN dotnet publish -c Release -o out

ENTRYPOINT ["dotnet", "out/pi-console.dll"]