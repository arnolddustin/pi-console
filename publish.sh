cd pi
dotnet publish -r linux-arm
rsync -ru bin/Debug/netcoreapp2.0/linux-arm/publish/* pi@raspberrypi:~/pi

