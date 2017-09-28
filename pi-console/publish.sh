dotnet publish -r linux-arm
scp -r bin/Debug/netcoreapp2.0/linux-arm/publish pi@raspberrypi:~/publish
