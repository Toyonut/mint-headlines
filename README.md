# Mint-Headlines

This is a simple scraper to read the Linux Mint headlines off the RSS feed. Pretty basic, but it mostly works.

## TODO:
* ~add configurable amount of headlines to print.~
* see if I can work out how to fix the random unicode in the console output properly.
* better error handling.
* ~build properly to make Linux and Windows tools instead of running from dotnet run.~ Kind Of...
* script/dotfile to run the docker container from a single command and maybe clean up old containers.
* build script to run the build steps and package it into a nice container.

I couldn't be bothered properly packaging it, so I dockerized it instead.

## building:
1. run `dotnet clean`
2. run `dotnet publish` to restore and build the app.
3. run `docker build -t mint-headlines:1.0 .` to build the docker container.

## Running:
1. run `docker run mint-headlines:1.0` to get all the headlines.
2. run `docker run mint-headlines:1.0 3` to pass in the parameter for the top three headlines.

Seems to work OK