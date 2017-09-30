# pi-console
.NET Core 2.0 console application that uses the library to interact with the Raspberry Pi.

# publishing
The `publish.sh` script builds the console app and uses `scp` to copy the built output to a pie with hostname `raspberrypi` and user `pi`.

# running on the pi
* run `publish.sh` to publish to the pi
* `ssh` to the pi
* change to the `pi-console` directory
* start the console app by running `./pi-console`
