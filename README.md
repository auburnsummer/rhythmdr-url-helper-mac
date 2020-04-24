This is a helper application that registers a `rhythmdr://` URL handler on MacOS.

**note**: This is mostly useless at the moment, since it only works if RD is
closed! I'm still thinking about how to get it working when RD is already open.

If you really want to install it, you should clone this application and then run
`sh build.sh`. This will generate an application, which you can then run once.
After that, opening `rhythmdr://` links should open the links in the URL Helper.