# AgriTrace 3.0 - PowerShell Testing Commands

## 🚀 Khởi động Hệ thống

### Khởi động Backend

```powershell
# Terminal 1: Khởi động Backend trên port 5255
cd "d:\NewFolder\farm-trace\AgriTraceBlockchain\AgriTrace.API"
dotnet run --configuration Debug
```

### Khởi động Frontend

```powershell
# Terminal 2: Khởi động Frontend trên port 8000
cd "d:\NewFolder\farm-trace\AgriTraceBlockchain\AgriTrace.Frontend"
python -m http.server 8000

# Truy cập: http://localhost:8000
```

---

## 📝 Testing Commands

Chạy các lệnh này từ terminal thứ 3 khi Backend & Frontend đã chạy

### 1️⃣ TEST GIAI ĐOẠN 1: SẢN XUẤT

#### Thêm sản phẩm Stage 1 (Sản xuất)

```powershell
$body = @{
    productCode = "LUA_001"
    productName = "Lúa Hương Thơm"
    productLocation = "Thôn Hạ, Yên Lý, Hà Nội"
    productionStartDate = [System.Math]::Floor((Get-Date).ToUniversalTime().AddDays(-30).Subtract([System.DateTime]::UnixEpoch).TotalSeconds)
    producer = "Công ty nông nghiệp Hà Nội ABC"
} | ConvertTo-Json

$result = Invoke-RestMethod -Method POST `
  -Uri "http://localhost:5255/api/products/production" `
  -ContentType "application/json" `
  -Body $body

Write-Host "✅ Giai đoạn 1 thành công!" -ForegroundColor Green
Write-Host "Transaction Hash: $($result.transactionHash)" -ForegroundColor Cyan
$result | ConvertTo-Json | Write-Host
```

#### Thêm sản phẩm Stage 1 - Phiên bản đơn giản

```powershell
$body = @{
    productCode = "TEST_001"
    productName = "Lúa"
    productLocation = "Hà Nội"
    productionStartDate = 1699000000
    producer = "ABC Corp"
} | ConvertTo-Json

Invoke-RestMethod -Method POST -Uri "http://localhost:5255/api/products/production" `
  -ContentType "application/json" -Body $body
```

---

### 2️⃣ TEST GIAI ĐOẠN 2: THU HOẠCH

#### Thêm sản phẩm Stage 2 (Thu hoạch)

```powershell
$body = @{
    productCode = "LUA_001"  # Phải giống Stage 1!
    harvestDate = [System.Math]::Floor((Get-Date).ToUniversalTime().AddDays(-10).Subtract([System.DateTime]::UnixEpoch).TotalSeconds)
    quantity = 5000
    processingMethod = "Phơi nắng tự nhiên"
    packingDate = [System.Math]::Floor((Get-Date).ToUniversalTime().AddDays(-5).Subtract([System.DateTime]::UnixEpoch).TotalSeconds)
    processingUnit = "Công ty chế biến XYZ"
} | ConvertTo-Json

$result = Invoke-RestMethod -Method POST `
  -Uri "http://localhost:5255/api/products/harvest" `
  -ContentType "application/json" `
  -Body $body

Write-Host "✅ Giai đoạn 2 thành công!" -ForegroundColor Green
Write-Host "Transaction Hash: $($result.transactionHash)" -ForegroundColor Cyan
```

#### Thêm sản phẩm Stage 2 - Phiên bản đơn giản

```powershell
$body = @{
    productCode = "TEST_001"  # Phải giống Stage 1!
    harvestDate = 1699100000
    quantity = 5000
    processingMethod = "Phơi nắng"
    packingDate = 1699150000
    processingUnit = "Công ty chế biến DEF"
} | ConvertTo-Json

Invoke-RestMethod -Method POST -Uri "http://localhost:5255/api/products/harvest" `
  -ContentType "application/json" -Body $body
```

---

### 3️⃣ TEST GIAI ĐOẠN 3: PHÂN PHỐI

#### Thêm sản phẩm Stage 3 (Phân phối)

```powershell
$body = @{
    productCode = "LUA_001"  # Phải giống Stage 1 & 2!
    distributionUnit = "Công ty Logistics Hà Nội"
    warehouseExitDate = [System.Math]::Floor((Get-Date).ToUniversalTime().Subtract([System.DateTime]::UnixEpoch).TotalSeconds)
    salePoint = "Siêu thị ABC, 123 Đường Đinh Tiên Hoàng, Hà Nội"
    productStatus = "Tốt"
    distributionCompletedDate = [System.Math]::Floor((Get-Date).ToUniversalTime().AddDays(7).Subtract([System.DateTime]::UnixEpoch).TotalSeconds)
} | ConvertTo-Json

$result = Invoke-RestMethod -Method POST `
  -Uri "http://localhost:5255/api/products/distribution" `
  -ContentType "application/json" `
  -Body $body

Write-Host "✅ Giai đoạn 3 thành công!" -ForegroundColor Green
Write-Host "Transaction Hash: $($result.transactionHash)" -ForegroundColor Cyan
```

#### Thêm sản phẩm Stage 3 - Phiên bản đơn giản

```powershell
$body = @{
    productCode = "TEST_001"  # Phải giống Stage 1 & 2!
    distributionUnit = "Logistics ABC"
    warehouseExitDate = 1699200000
    salePoint = "Siêu thị XYZ"
    productStatus = "Tốt"
    distributionCompletedDate = 1699250000
} | ConvertTo-Json

Invoke-RestMethod -Method POST -Uri "http://localhost:5255/api/products/distribution" `
  -ContentType "application/json" -Body $body
```

---

### 4️⃣ TRUY XUẤT THÔNG TIN SẢN PHẨM

#### Lấy thông tin 3 giai đoạn

```powershell
# Truy xuất sản phẩm LUA_001
$result = Invoke-RestMethod -Method GET -Uri "http://localhost:5255/api/products/LUA_001"

Write-Host "=== THÔNG TIN SẢN PHẨM ===" -ForegroundColor Cyan
Write-Host "`n📍 GIAI ĐOẠN SẢN XUẤT:" -ForegroundColor Yellow
$result.productionStage | Format-Table -AutoSize

Write-Host "`n📍 GIAI ĐOẠN THU HOẠCH:" -ForegroundColor Yellow
$result.harvestStage | Format-Table -AutoSize

Write-Host "`n📍 GIAI ĐOẠN PHÂN PHỐI:" -ForegroundColor Yellow
$result.distributionStage | Format-Table -AutoSize

Write-Host "`n✅ Lấy thông tin thành công!" -ForegroundColor Green

# Xem toàn bộ JSON
Write-Host "`nJSON đầy đủ:" -ForegroundColor Green
$result | ConvertTo-Json | Write-Host
```

#### Truy xuất và lưu vào file

```powershell
$result = Invoke-RestMethod -Method GET -Uri "http://localhost:5255/api/products/LUA_001"
$result | ConvertTo-Json | Out-File -FilePath "product_LUA_001.json" -Encoding UTF8

Write-Host "✅ Dữ liệu đã lưu vào: product_LUA_001.json" -ForegroundColor Green
Invoke-Item "product_LUA_001.json"  # Mở file
```

---

## 🔄 FULL WORKFLOW TEST (Tất cả 3 giai đoạn)

Chạy script này để test toàn bộ workflow một lần:

```powershell
Write-Host "==== AGRITRACE 3.0 - FULL WORKFLOW TEST ====" -ForegroundColor Cyan

$productCode = "FULL_TEST_$(Get-Random -Minimum 1000 -Maximum 9999)"
Write-Host "`n📦 Mã sản phẩm: $productCode" -ForegroundColor Yellow

# ========== STAGE 1: PRODUCTION ==========
Write-Host "`n1️⃣ THÊM GIAI ĐOẠN SẢN XUẤT..." -ForegroundColor Green
$body1 = @{
    productCode = $productCode
    productName = "Lúa Hương Thơm Séc"
    productLocation = "Thôn Đồng Sao, Yên Phong, Bắc Ninh"
    productionStartDate = [System.Math]::Floor((Get-Date).ToUniversalTime().AddDays(-60).Subtract([System.DateTime]::UnixEpoch).TotalSeconds)
    producer = "Trang trại Huyền Anh"
} | ConvertTo-Json

try {
    $result1 = Invoke-RestMethod -Method POST `
      -Uri "http://localhost:5255/api/products/production" `
      -ContentType "application/json" `
      -Body $body1
    Write-Host "✅ Stage 1 thành công!" -ForegroundColor Green
    Write-Host "Transaction: $($result1.transactionHash)" -ForegroundColor Cyan
}
catch {
    Write-Host "❌ Lỗi Stage 1: $_" -ForegroundColor Red
    exit
}

# Chờ 2 giây
Start-Sleep -Seconds 2

# ========== STAGE 2: HARVEST ==========
Write-Host "`n2️⃣ THÊM GIAI ĐOẠN THU HOẠCH..." -ForegroundColor Green
$body2 = @{
    productCode = $productCode
    harvestDate = [System.Math]::Floor((Get-Date).ToUniversalTime().AddDays(-30).Subtract([System.DateTime]::UnixEpoch).TotalSeconds)
    quantity = 7500
    processingMethod = "Phơi nắng 15 ngày"
    packingDate = [System.Math]::Floor((Get-Date).ToUniversalTime().AddDays(-15).Subtract([System.DateTime]::UnixEpoch).TotalSeconds)
    processingUnit = "Nhà máy Đức Long"
} | ConvertTo-Json

try {
    $result2 = Invoke-RestMethod -Method POST `
      -Uri "http://localhost:5255/api/products/harvest" `
      -ContentType "application/json" `
      -Body $body2
    Write-Host "✅ Stage 2 thành công!" -ForegroundColor Green
    Write-Host "Transaction: $($result2.transactionHash)" -ForegroundColor Cyan
}
catch {
    Write-Host "❌ Lỗi Stage 2: $_" -ForegroundColor Red
    exit
}

# Chờ 2 giây
Start-Sleep -Seconds 2

# ========== STAGE 3: DISTRIBUTION ==========
Write-Host "`n3️⃣ THÊM GIAI ĐOẠN PHÂN PHỐI..." -ForegroundColor Green
$body3 = @{
    productCode = $productCode
    distributionUnit = "Giao vận Tây Hồ"
    warehouseExitDate = [System.Math]::Floor((Get-Date).ToUniversalTime().AddDays(-5).Subtract([System.DateTime]::UnixEpoch).TotalSeconds)
    salePoint = "Siêu thị Aeon Hà Nội - Tầng 2"
    productStatus = "Tốt"
    distributionCompletedDate = [System.Math]::Floor((Get-Date).ToUniversalTime().Subtract([System.DateTime]::UnixEpoch).TotalSeconds)
} | ConvertTo-Json

try {
    $result3 = Invoke-RestMethod -Method POST `
      -Uri "http://localhost:5255/api/products/distribution" `
      -ContentType "application/json" `
      -Body $body3
    Write-Host "✅ Stage 3 thành công!" -ForegroundColor Green
    Write-Host "Transaction: $($result3.transactionHash)" -ForegroundColor Cyan
}
catch {
    Write-Host "❌ Lỗi Stage 3: $_" -ForegroundColor Red
    exit
}

# Chờ 3 giây để Blockchain xác nhận
Write-Host "`n⏳ Chờ Blockchain xác nhận (3 giây)..." -ForegroundColor Yellow
Start-Sleep -Seconds 3

# ========== RETRIEVE ALL STAGES ==========
Write-Host "`n4️⃣ TRỰ XUẤT THÔNG TIN SẢN PHẨM..." -ForegroundColor Green
try {
    $resultFinal = Invoke-RestMethod -Method GET `
      -Uri "http://localhost:5255/api/products/$productCode"
    
    Write-Host "`n✅ TRUY XUẤT THÀNH CÔNG!" -ForegroundColor Green
    Write-Host "`n📊 TÓẮT TẮT DỮ LIỆU:" -ForegroundColor Cyan
    Write-Host "Sản phẩm: $($resultFinal.productionStage.productName)" -ForegroundColor White
    Write-Host "Mã: $($resultFinal.productionStage.productCode)" -ForegroundColor White
    Write-Host "Sản xuất tại: $($resultFinal.productionStage.productLocation)" -ForegroundColor White
    Write-Host "Thu hoạch: $($resultFinal.harvestStage.quantity) tấn" -ForegroundColor White
    Write-Host "Bán tại: $($resultFinal.distributionStage.salePoint)" -ForegroundColor White
    
    Write-Host "`n📝 JSON ĐẦY ĐỦ:" -ForegroundColor Cyan
    $resultFinal | ConvertTo-Json | Write-Host
}
catch {
    Write-Host "❌ Lỗi truy xuất: $_" -ForegroundColor Red
    exit
}

Write-Host "`n🎉 TEST HOÀN TẤT THÀNH CÔNG!" -ForegroundColor Green
Write-Host "════════════════════════════════════════════" -ForegroundColor Cyan
```

---

## 🔍 KIỂM TRA BACKEND

### Kiểm tra Backend đang chạy

```powershell
$response = Invoke-WebRequest -Uri "http://localhost:5255/health" -ErrorAction SilentlyContinue

if ($response.StatusCode -eq 200) {
    Write-Host "✅ Backend ĐANG CHẠY" -ForegroundColor Green
} else {
    Write-Host "❌ Backend KHÔNG CHẠY" -ForegroundColor Red
}
```

### Kiểm tra toàn bộ hệ thống

```powershell
Write-Host "🔍 KIỂM TRA HỆ THỐNG" -ForegroundColor Cyan

# Check Backend
try {
    $backendCheck = Invoke-WebRequest -Uri "http://localhost:5255/api/products/TEST" -ErrorAction SilentlyContinue
    Write-Host "✅ Backend: http://localhost:5255 - OK" -ForegroundColor Green
}
catch {
    Write-Host "❌ Backend: http://localhost:5255 - ERROR" -ForegroundColor Red
}

# Check Frontend
try {
    $frontendCheck = Invoke-WebRequest -Uri "http://localhost:8000" -ErrorAction SilentlyContinue
    Write-Host "✅ Frontend: http://localhost:8000 - OK" -ForegroundColor Green
}
catch {
    Write-Host "❌ Frontend: http://localhost:8000 - ERROR" -ForegroundColor Red
}

# Check Sepolia Network
try {
    $sepoliaCheck = Invoke-WebRequest -Uri "https://sepolia.etherscan.io" -ErrorAction SilentlyContinue
    Write-Host "✅ Sepolia Network: Online - OK" -ForegroundColor Green
}
catch {
    Write-Host "❌ Sepolia Network: Offline - ERROR" -ForegroundColor Red
}
```

---

## 🧹 CLEANUP

### Xóa tất cả test data

```powershell
# Lưu ý: Blockchain là immutable, không thể xóa data!
# Nhưng bạn có thể xóa local files

Remove-Item -Path "product_*.json" -Force -ErrorAction SilentlyContinue
Write-Host "✅ Xóa file test thành công" -ForegroundColor Green
```

---

## 📋 Troubleshooting

### Lỗi: Connection refused

```powershell
# Kiểm tra Backend có chạy?
Get-Process dotnet -ErrorAction SilentlyContinue

# Nếu không có, khởi động lại
cd "d:\NewFolder\farm-trace\AgriTraceBlockchain\AgriTrace.API"
dotnet run --configuration Debug
```

### Lỗi: "Contract address not found"

```powershell
# Kiểm tra appsettings.json
cat "d:\NewFolder\farm-trace\AgriTraceBlockchain\AgriTrace.API\appsettings.json"

# Đảm bảo contract address: 0xf00A30ca670526B1903286ac6B756187BaEbE4cB
```

### Lỗi: "Transaction failed"

```powershell
# Kiểm tra Private Key
cat "d:\NewFolder\farm-trace\AgriTraceBlockchain\AgriTrace.API\appsettings.Development.json"

# Đảm bảo account có đủ ETH trên Sepolia (từ faucet)
```

---

## 🎯 Quick Links

- **Frontend**: http://localhost:8000
- **Backend**: http://localhost:5255
- **Sepolia Etherscan**: https://sepolia.etherscan.io/address/0xf00A30ca670526B1903286ac6B756187BaEbE4cB
- **Contract Address**: `0xf00A30ca670526B1903286ac6B756187BaEbE4cB`

---

**Ghi chú**: Sao chép các lệnh này vào PowerShell để chạy test!
