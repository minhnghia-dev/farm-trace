# AgriTrace 3.0 - Hệ thống Truy xuất 3 Giai đoạn (Testing Guide)

## 📋 Tổng Quát

Hệ thống đã được nâng cấp từ 1 giai đoạn sang **3 giai đoạn** với đầu vào chi tiết cho mỗi giai đoạn:

1. **Giai đoạn Sản xuất** (Production Stage)
   - Mã sản phẩm, tên nông sản, địa điểm, ngày bắt đầu, người sản xuất

2. **Giai đoạn Thu hoạch** (Harvest Stage)
   - Ngày thu hoạch, sản lượng, hình thức xử lý, ngày đóng gói, đơn vị thực hiện

3. **Giai đoạn Phân phối** (Distribution Stage)
   - Đơn vị phân phối, ngày xuất kho, điểm bán, trạng thái, ngày hoàn tất

## 🔧 Kiến trúc Hệ thống

### Backend (.NET 9.0)
- **Địa chỉ**: http://localhost:5255
- **Cơ sở dữ liệu**: Blockchain Sepolia testnet
- **Smart Contract**: `0xf00A30ca670526B1903286ac6B756187BaEbE4cB`

### API Endpoints

#### 1. Thêm Giai đoạn Sản xuất
```http
POST /api/products/production
Content-Type: application/json

{
  "productCode": "LUA_001",
  "productName": "Lúa giống",
  "productLocation": "Thôn Hạ, Yên Lý",
  "productionStartDate": 1699000000,
  "producer": "Công ty nông nghiệp ABC"
}
```

**Response (200 OK)**:
```json
{
  "transactionHash": "0x1234...5678",
  "message": "Giai đoạn Sản xuất đã được ghi lên Blockchain!"
}
```

#### 2. Thêm Giai đoạn Thu hoạch
```http
POST /api/products/harvest
Content-Type: application/json

{
  "productCode": "LUA_001",
  "harvestDate": 1699100000,
  "quantity": 5000,
  "processingMethod": "Phơi nắng",
  "packingDate": 1699150000,
  "processingUnit": "Công ty chế biến XYZ"
}
```

**Response (200 OK)**:
```json
{
  "transactionHash": "0x1234...5678",
  "message": "Giai đoạn Thu hoạch đã được ghi lên Blockchain!"
}
```

#### 3. Thêm Giai đoạn Phân phối
```http
POST /api/products/distribution
Content-Type: application/json

{
  "productCode": "LUA_001",
  "distributionUnit": "Công ty logistics ABC",
  "warehouseExitDate": 1699200000,
  "salePoint": "Siêu thị ABC, Hà Nội",
  "productStatus": "Tốt",
  "distributionCompletedDate": 1699250000
}
```

**Response (200 OK)**:
```json
{
  "transactionHash": "0x1234...5678",
  "message": "Giai đoạn Phân phối đã được ghi lên Blockchain!"
}
```

#### 4. Lấy Toàn bộ Thông tin Sản phẩm
```http
GET /api/products/{productCode}
```

**Response (200 OK)**:
```json
{
  "productionStage": {
    "productCode": "LUA_001",
    "productName": "Lúa giống",
    "productLocation": "Thôn Hạ, Yên Lý",
    "productionStartDate": 1699000000,
    "producer": "Công ty nông nghiệp ABC"
  },
  "harvestStage": {
    "harvestDate": 1699100000,
    "quantity": 5000,
    "processingMethod": "Phơi nắng",
    "packingDate": 1699150000,
    "processingUnit": "Công ty chế biến XYZ"
  },
  "distributionStage": {
    "distributionUnit": "Công ty logistics ABC",
    "warehouseExitDate": 1699200000,
    "salePoint": "Siêu thị ABC, Hà Nội",
    "productStatus": "Tốt",
    "distributionCompletedDate": 1699250000
  },
  "qrCode": "data:image/png;base64,iVBORw0KGgo..."
}
```

## 🚀 Cách Chạy Hệ thống

### 1. Khởi động Backend

```bash
cd d:\NewFolder\farm-trace\AgriTraceBlockchain\AgriTrace.API
dotnet run --configuration Debug
```

Backend sẽ lắng nghe trên **http://localhost:5255**

### 2. Khởi động Frontend

Mở một terminal mới tại thư mục Frontend:

```bash
cd d:\NewFolder\farm-trace\AgriTraceBlockchain\AgriTrace.Frontend
python -m http.server 8000
```

Truy cập: **http://localhost:8000**

## 🧪 Hướng dẫn Testing

### Scenario 1: Đăng ký Sản phẩm Đầy đủ 3 Giai đoạn

**Bước 1**: Truy cập tab "Nhà sản xuất"

**Bước 2**: Điền Giai đoạn Sản xuất

```
Mã sản phẩm: LUA_001
Tên nông sản: Lúa Thái Bình
Địa điểm: Thôn Hạ, Yên Lý, Hà Nội
Ngày bắt đầu: 01/01/2024
Người sản xuất: Công ty nông nghiệp ABC
```

Click: "GHI LÊN BLOCKCHAIN - GIAI ĐOẠN 1"

✅ Chờ transaction hash trả về

**Bước 3**: Chuyển sang Giai đoạn Thu hoạch (tự động chuyển)

```
Mã sản phẩm: LUA_001 (phải giống Stage 1)
Ngày thu hoạch: 20/01/2024
Sản lượng: 5000
Hình thức xử lý: Phơi nắng
Ngày đóng gói: 25/01/2024
Đơn vị thực hiện: Công ty chế biến XYZ
```

Click: "GHI LÊN BLOCKCHAIN - GIAI ĐOẠN 2"

✅ Chờ transaction hash trả về

**Bước 4**: Chuyển sang Giai đoạn Phân phối

```
Mã sản phẩm: LUA_001 (phải giống Stage 1)
Đơn vị phân phối: Công ty logistics ABC
Ngày xuất kho: 01/02/2024
Điểm bán: Siêu thị ABC, Hà Nội
Trạng thái: Tốt
Ngày hoàn tất: 10/02/2024
```

Click: "GHI LÊN BLOCKCHAIN - GIAI ĐOẠN 3"

✅ Chờ transaction hash trả về

### Scenario 2: Truy xuất Thông tin Sản phẩm

**Bước 1**: Truy cập tab "Người tiêu dùng"

**Bước 2**: Nhập mã sản phẩm: `LUA_001`

**Bước 3**: Click nút tìm kiếm

✅ Kết quả sẽ hiển thị 3 giai đoạn:

- **Giai đoạn Sản xuất** (xanh - Production)
- **Giai đoạn Thu hoạch** (vàng - Harvest)
- **Giai đoạn Phân phối** (cam - Distribution)

**Bước 4**: In QR code (click "In tem QR")

## 🔗 Testing với cURL

### Test Giai đoạn 1: Sản xuất

```bash
$body = @{
    productCode = "LUA_002"
    productName = "Lúa Hương Thơm"
    productLocation = "Hà Tây"
    productionStartDate = 1699000000
    producer = "Công ty DEF"
} | ConvertTo-Json

Invoke-RestMethod -Method POST -Uri "http://localhost:5255/api/products/production" `
  -ContentType "application/json" -Body $body
```

### Test Giai đoạn 2: Thu hoạch

```bash
$body = @{
    productCode = "LUA_002"
    harvestDate = 1699100000
    quantity = 6000
    processingMethod = "Sấy lò"
    packingDate = 1699150000
    processingUnit = "Công ty chế biến DEF"
} | ConvertTo-Json

Invoke-RestMethod -Method POST -Uri "http://localhost:5255/api/products/harvest" `
  -ContentType "application/json" -Body $body
```

### Test Giai đoạn 3: Phân phối

```bash
$body = @{
    productCode = "LUA_002"
    distributionUnit = "Công ty logistics DEF"
    warehouseExitDate = 1699200000
    salePoint = "Hàng tạp hóa Minh"
    productStatus = "Tốt"
    distributionCompletedDate = 1699250000
} | ConvertTo-Json

Invoke-RestMethod -Method POST -Uri "http://localhost:5255/api/products/distribution" `
  -ContentType "application/json" -Body $body
```

### Test Truy xuất

```bash
Invoke-RestMethod -Method GET -Uri "http://localhost:5255/api/products/LUA_002"
```

## 📊 Cấu trúc Dữ liệu Trên Blockchain

Smart Contract lưu trữ 3 struct chính:

```solidity
struct ProductionStage {
    string productCode;
    string productName;
    string productLocation;
    uint256 productionStartDate;
    string producer;
    bool completed;
}

struct HarvestStage {
    uint256 harvestDate;
    uint256 quantity;
    string processingMethod;
    uint256 packingDate;
    string processingUnit;
    bool completed;
}

struct DistributionStage {
    string distributionUnit;
    uint256 warehouseExitDate;
    string salePoint;
    string productStatus;
    uint256 distributionCompletedDate;
    bool completed;
}
```

## ⚠️ Ghi Chú Quan Trọng

1. **Mã sản phẩm phải giống nhau** khi ghi các giai đoạn khác nhau
2. **Timestamp** là số giây từ epoch (Unix timestamp)
3. **Frontend tự động chuyển tab** sau khi ghi thành công
4. **Tất cả dữ liệu được mã hóa trên Blockchain** - không thể thay đổi
5. **Sepolia testnet** - dùng ETH giả để test (free từ faucet)

## 🔗 Liên kết Hữu ích

- **Sepolia Testnet**: https://sepolia.etherscan.io
- **Smart Contract**: https://sepolia.etherscan.io/address/0xf00A30ca670526B1903286ac6B756187BaEbE4cB
- **Hardhat Documentation**: https://hardhat.org

## ✅ Checklist Testing

- [ ] Backend chạy trên port 5255
- [ ] Frontend chạy trên port 8000
- [ ] Có thể submit Giai đoạn 1
- [ ] Có thể submit Giai đoạn 2
- [ ] Có thể submit Giai đoạn 3
- [ ] Có thể truy xuất 3 giai đoạn từ Blockchain
- [ ] QR code hiển thị chính xác
- [ ] Tab tự động chuyển sau submit

## 📞 Troubleshooting

### Lỗi: "POST http://localhost:5255/api/products/production net::ERR_CONNECTION_REFUSED"

**Giải pháp**: Kiểm tra backend đã khởi động chưa

```bash
cd d:\NewFolder\farm-trace\AgriTraceBlockchain\AgriTrace.API
dotnet run --configuration Debug
```

### Lỗi: "Không tìm thấy thông tin sản phẩm trên Blockchain"

**Giải pháp**: 
- Kiểm tra mã sản phẩm nhập vào có chính xác không
- Đợi transaction được xác nhận (có thể mất vài giây)
- Kiểm tra địa chỉ contract có đúng không

### Lỗi: "Transaction failed with error"

**Giải pháp**:
- Kiểm tra Private Key trong appsettings.json
- Kiểm tra RPC endpoint (ChainUrl) có hoạt động không
- Kiểm tra tài khoản có đủ ETH để ghi dữ liệu không

---

**Phiên bản**: AgriTrace 3.0  
**Cập nhật**: Tháng 12 2024  
**Hỗ trợ**: Blockchain Sepolia testnet
