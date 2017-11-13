# pi-console
.NET Core 2.0 console application that uses the [pi-dotnetcore](https://www.github.com/arnolddustin/pi-dotnetcore) library to interact with the Raspberry Pi.

## getting started
* clone this repostory: `git clone https://github.com/arnolddustin/pi-console.git`
* configure your Raspberry Pi for running dotnet.  full instructions can be found in the [pi-dotnet core README](https://github.com/arnolddustin/pi-dotnetcore#getting-started-guide).
* build the console application and deploy it to the Raspberry Pi by running the publish script: `./publish.sh`. The script builds the dotnetcore 2.0 app for the **linux-arm platform** then uses `rsync` to copy the build output to a Pi listening at at the login/host `pi@raspberrypi`.

## running on the pi
* connect to the pi over `ssh`.
* make the `pi` command available from anywhere by creating a symbolic link to it: `sudo ln -s ~/pi/pi /usr/local/bin`
* start the console app by running `sudo pi`.  this will display the set of commands available.  running with `sudo` is required because elevated privileges are required to write to the GPIO system on the Raspberry Pi

## wiringPi
Commands in the `pi.wiringPi.commands` namespace use .NET PInvoke to call the [wiringPi library](http://www.wiringPi.com) on the RaspberryPi.  To install support for these commands:
* Follow the wiringPi [installation instructions](http://wiringpi.com/download-and-install/) to install wiringPi
* Verify that wiringPi is installed correctly by running `gpio readall`
* Open the folder where you cloned the wiringPi git repository and run `./build`
* Create shared libraries that will be used by the wiringPi wrapper by running the following commands:
  * `cc -shared wiringPi.o -o libwiringPi.so`
  * `cc -shared wiringPiI2C.o -o libwiringPiI2C.so`
  * `cc -shared wiringPiSPI.o -o libwiringPiSPI.so`

## References
* The wiringPi wrapper is from [WiringPi.NET](https://github.com/danriches/WiringPi.Net). The original library has some sample code and full Visual Studio instructions.