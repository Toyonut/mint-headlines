from microsoft/dotnet:2.1-runtime

copy bin/release/netcoreapp2.1/ubuntu.16.04-x64/* /usr/mint-headlines/
copy mint-headlines.sh /usr/mint-headlines/mint-headlines.sh

workdir /usr/mint-headlines

entrypoint ["./mint-headlines.sh"]
cmd ["0"]