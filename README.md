# pi-console
.NET Core 2.0 console application that uses the [pi-dotnetcore](https://www.github.com/arnolddustin/pi-dotnetcore) library to interact with the Raspberry Pi.

## getting started
* clone this repostory: `git clone https://github.com/arnolddustin/pi-console.git`
* configure your Raspberry Pi for running dotnet.  full instructions [can be found here](https://github.com/arnolddustin/pi-dotnetcore#getting-started-guide).
* build the console application and deploy it to the Raspberry Pi by running the publish script: `./publish.sh`. The script builds the dotnetcore 2.0 app for the **linux-arm platform** then uses `rsync` to copy the build output to a Pi listening at at the login/host `pi@raspberrypi`.

## running on the pi
* connect to the pi over `ssh`.
* change to the `pi-console` directory
* start the console app by running `sudo ./pi-console`.  this will display the set of commands available

## available commands
* `list` - lists all active GPIO pins
* `init <pinnumber> [input|output]` - initializes the specified GPIO pin for input or output. not specifying `input` or `output` will default to `output`.
* `deinit <pinnumber>` - deinitializes the specified GPIO pin
* `status <pinnumber>` - displays the current state of the specified GPIO pin (pin must be activated first)
* `on <pinnumber>` - turns on the specified GPIO pin (pin must be activated first)
* `off <pinnumber>` - turns off the specified GPIO pin (pin must be activated first)
