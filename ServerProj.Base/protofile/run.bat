for /f "delims=" %%i in ('dir /b proto "proto/*.proto"') do protoc -I=proto/ --csharp_out=cs/ proto/%%i --grpc_out grpc/ --plugin=protoc-gen-grpc=./grpc_csharp_plugin.exe
pause



