FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Bước 1: Copy file cấu hình dự án để tải thư viện (Restore)
# Lệnh COPY này chỉ lấy đúng file .csproj
COPY ["AgriTrace.API.csproj", "./"]
RUN dotnet restore "AgriTrace.API.csproj"

COPY . . 

# Bước 3: Biên dịch code thành file chạy (.dll)
RUN dotnet publish "AgriTrace.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Copy kết quả đã build từ Giai đoạn 1 sang
COPY --from=build /app/publish .

# Cấu hình để Render nhận diện được Port 10000
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

# Chạy file API của bạn
ENTRYPOINT ["dotnet", "AgriTrace.API.dll"]