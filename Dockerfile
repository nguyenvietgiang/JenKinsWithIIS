# Stage 1: Build ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy file csproj và restore packages
COPY *.csproj ./
RUN dotnet restore

# Copy toàn bộ source và build
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app

# Thiết lập biến môi trường để app lắng nghe mọi IP ở port 80
ENV ASPNETCORE_URLS=http://+:80

# Copy từ stage build sang runtime
COPY --from=build /app/publish .

# Mở port cho container
EXPOSE 80
EXPOSE 443

# Chạy ứng dụng
ENTRYPOINT ["dotnet", "JenKinsWithIIS.dll"]
