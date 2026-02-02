# 🚀 AgriTrace 3.0 - Bắt Đầu Nhanh (Quick Start)

## ⚡ Khởi Động 30 Giây

### Bước 1: Mở 2 Terminal

**Terminal 1 - Backend:**
```bash
cd d:\NewFolder\farm-trace\AgriTraceBlockchain\AgriTrace.API
dotnet run --configuration Debug
```

**Terminal 2 - Frontend:**
```bash
cd d:\NewFolder\farm-trace\AgriTraceBlockchain\AgriTrace.Frontend
python -m http.server 8000
```

### Bước 2: Truy cập
- **Frontend**: http://localhost:8000
- **Backend API**: http://localhost:5255

---

## 🎯 Workflow Nhanh (5 phút)

### Scenario: Test Toàn bộ 3 Giai đoạn

**Bước 1: Nhà sản xuất - Ghi Giai đoạn 1**

1. Vào `http://localhost:8000`
2. Click tab **"Nhà sản xuất"**
3. Chọn tab **"Sản xuất"**
4. Điền form:
   ```
   Mã sản phẩm:          LUA_001
   Tên nông sản:         Lúa Thái Bình
   Địa điểm sản xuất:    Yên Lý, Hà Nội
   Ngày bắt đầu:         01/01/2024
   Người sản xuất:       Công ty ABC
   ```
5. Click **"GHI LÊN BLOCKCHAIN - GIAI ĐOẠN 1"**
6. ✅ Chờ transaction hash hiển thị

**Bước 2: Ghi Giai đoạn 2 (Tự động chuyển tab)**

1. Tab **"Thu hoạch"** sẽ tự hiển thị
2. Điền form:
   ```
   Mã sản phẩm:          LUA_001  (phải giống!)
   Ngày thu hoạch:       15/01/2024
   Sản lượng:            5000
   Hình thức xử lý:      Phơi nắng
   Ngày đóng gói:        20/01/2024
   Đơn vị thực hiện:     Công ty DEF
   ```
3. Click **"GHI LÊN BLOCKCHAIN - GIAI ĐOẠN 2"**
4. ✅ Chờ transaction hash

**Bước 3: Ghi Giai đoạn 3 (Tự động chuyển tab)**

1. Tab **"Phân phối"** sẽ tự hiển thị
2. Điền form:
   ```
   Mã sản phẩm:          LUA_001  (phải giống!)
   Đơn vị phân phối:     Logistics XYZ
   Ngày xuất kho:        01/02/2024
   Điểm bán:             Siêu thị ABC, HN
   Trạng thái:           Tốt
   Ngày hoàn tất:        10/02/2024
   ```
3. Click **"GHI LÊN BLOCKCHAIN - GIAI ĐOẠN 3"**
4. ✅ Chờ transaction hash
5. 🎉 Tự động quay về tab **"Người tiêu dùng"**

**Bước 4: Kiểm tra từ phía Tiêu dùng**

1. Tab **"Người tiêu dùng"** (mặc định)
2. Nhập: `LUA_001`
3. Click biểu tượng **tìm kiếm**
4. ✅ Hiển thị 3 thẻ:
   - 🌱 Giai đoạn Sản xuất
   - 🌾 Giai đoạn Thu hoạch
   - 🚚 Giai đoạn Phân phối

---

## 📱 Giao Diện Chi Tiết

### Tab Nhà sản xuất

```
┌─────────────────────────────────────────┐
│  [Sản xuất] [Thu hoạch] [Phân phối]    │
├─────────────────────────────────────────┤
│                                         │
│  Đăng ký Giai đoạn Sản xuất            │
│  Thông tin này sẽ được lưu trên BChain │
│                                         │
│  Mã sản phẩm (Code) *                  │
│  [________________]                    │
│                                         │
│  Tên nông sản *                        │
│  [________________]                    │
│                                         │
│  Địa điểm sản xuất *                   │
│  [________________]                    │
│                                         │
│  Ngày bắt đầu sản xuất *                │
│  [________________]                    │
│                                         │
│  Người sản xuất *                      │
│  [________________]                    │
│                                         │
│  [GHI LÊN BLOCKCHAIN - GIAI ĐOẠN 1]   │
│                                         │
└─────────────────────────────────────────┘
```

### Tab Người tiêu dùng

```
┌─────────────────────────────────────────┐
│  Truy xuất nguồn gốc 3 giai đoạn       │
│  Nhập mã hoặc quét QR để xác thực      │
│                                         │
│  [Nhập mã sản phẩm...] [🔍]            │
│                                         │
├─────────────────────────────────────────┤
│                                         │
│  ┌──────────────────────────────────┐  │
│  │ Lúa Hương Thơm - Phiên bản 3.0  │  │
│  │ Mã: #LUA_001                    │  │
│  └──────────────────────────────────┘  │
│                                         │
│  ┌──────────┐  ┌──────────┐  ┌────────┐│
│  │ 🌱 Sản   │  │ 🌾 Thu   │  │ 🚚 Phân││
│  │ Địa điểm │  │ Ngày     │  │ Đơn vị ││
│  │ Người SX │  │ Sản lượng │  │ Điểm   ││
│  │ Ngày BĐ  │  │ Hình thức │  │ Trạng  ││
│  └──────────┘  └──────────┘  └────────┘│
│                                         │
│              ┌──────────┐              │
│              │          │              │
│              │ QR Code  │              │
│              │          │              │
│              └──────────┘              │
│                                         │
│              [In tem QR]               │
│                                         │
└─────────────────────────────────────────┘
```

---

## 🔗 API Endpoints

### Thêm Giai đoạn 1: Sản Xuất

```http
POST /api/products/production
Content-Type: application/json

{
  "productCode": "LUA_001",
  "productName": "Lúa Thái Bình",
  "productLocation": "Yên Lý, Hà Nội",
  "productionStartDate": 1699000000,
  "producer": "Công ty ABC"
}

Response (200):
{
  "transactionHash": "0x1a2b3c...",
  "message": "Giai đoạn Sản xuất đã được ghi lên Blockchain!"
}
```

### Thêm Giai đoạn 2: Thu Hoạch

```http
POST /api/products/harvest
Content-Type: application/json

{
  "productCode": "LUA_001",
  "harvestDate": 1699100000,
  "quantity": 5000,
  "processingMethod": "Phơi nắng",
  "packingDate": 1699150000,
  "processingUnit": "Công ty DEF"
}

Response (200):
{
  "transactionHash": "0x1a2b3c...",
  "message": "Giai đoạn Thu hoạch đã được ghi lên Blockchain!"
}
```

### Thêm Giai đoạn 3: Phân Phối

```http
POST /api/products/distribution
Content-Type: application/json

{
  "productCode": "LUA_001",
  "distributionUnit": "Logistics XYZ",
  "warehouseExitDate": 1699200000,
  "salePoint": "Siêu thị ABC",
  "productStatus": "Tốt",
  "distributionCompletedDate": 1699250000
}

Response (200):
{
  "transactionHash": "0x1a2b3c...",
  "message": "Giai đoạn Phân phối đã được ghi lên Blockchain!"
}
```

### Truy Xuất Thông Tin

```http
GET /api/products/LUA_001

Response (200):
{
  "productionStage": {
    "productCode": "LUA_001",
    "productName": "Lúa Thái Bình",
    "productLocation": "Yên Lý, Hà Nội",
    "productionStartDate": 1699000000,
    "producer": "Công ty ABC"
  },
  "harvestStage": {
    "harvestDate": 1699100000,
    "quantity": 5000,
    "processingMethod": "Phơi nắng",
    "packingDate": 1699150000,
    "processingUnit": "Công ty DEF"
  },
  "distributionStage": {
    "distributionUnit": "Logistics XYZ",
    "warehouseExitDate": 1699200000,
    "salePoint": "Siêu thị ABC",
    "productStatus": "Tốt",
    "distributionCompletedDate": 1699250000
  },
  "qrCode": "data:image/png;base64,iVBORw0KGgo..."
}
```

---

## 🧪 Testing với cURL (PowerShell)

### Test Giai đoạn 1

```powershell
$body = @{
    productCode = "CURL_TEST_001"
    productName = "Lúa"
    productLocation = "HN"
    productionStartDate = 1699000000
    producer = "ABC"
} | ConvertTo-Json

Invoke-RestMethod -Method POST `
  -Uri "http://localhost:5255/api/products/production" `
  -ContentType "application/json" `
  -Body $body
```

### Test Giai đoạn 2

```powershell
$body = @{
    productCode = "CURL_TEST_001"
    harvestDate = 1699100000
    quantity = 5000
    processingMethod = "Phơi"
    packingDate = 1699150000
    processingUnit = "DEF"
} | ConvertTo-Json

Invoke-RestMethod -Method POST `
  -Uri "http://localhost:5255/api/products/harvest" `
  -ContentType "application/json" `
  -Body $body
```

### Test Giai đoạn 3

```powershell
$body = @{
    productCode = "CURL_TEST_001"
    distributionUnit = "XYZ"
    warehouseExitDate = 1699200000
    salePoint = "Shop ABC"
    productStatus = "Tốt"
    distributionCompletedDate = 1699250000
} | ConvertTo-Json

Invoke-RestMethod -Method POST `
  -Uri "http://localhost:5255/api/products/distribution" `
  -ContentType "application/json" `
  -Body $body
```

### Truy Xuất

```powershell
Invoke-RestMethod -Method GET `
  -Uri "http://localhost:5255/api/products/CURL_TEST_001" | ConvertTo-Json
```

---

## 🔍 Kiểm Tra Trên Blockchain

### Xem transaction trên Sepolia

```
https://sepolia.etherscan.io/address/0xf00A30ca670526B1903286ac6B756187BaEbE4cB
```

### Kiểm tra trong Hardhat Console

```bash
cd blockchain
npx hardhat console --network sepolia
```

```javascript
// Trong console
const contract = await ethers.getContractAt("AgriTrace", "0xf00A30ca670526B1903286ac6B756187BaEbE4cB");
const record = await contract.getProductRecord("LUA_001");
console.log(record);
```

---

## ⚠️ Ghi Chú Quan Trọng

1. **Mã sản phẩm phải giống nhau** khi ghi các giai đoạn khác nhau
2. **Chỉ ghi một lần duy nhất** - Blockchain immutable, không thể sửa
3. **Chờ transaction xác nhận** - Sepolia mất 1-2 phút để xác nhận
4. **Account cần ETH trên Sepolia** - Lấy từ: https://sepoliafaucet.com
5. **Private Key giữ bí mật** - Không commit vào Git

---

## 🆘 Troubleshooting

| Vấn đề | Giải pháp |
|--------|----------|
| Backend không chạy | Kiểm tra `dotnet run` |
| Frontend không load | Kiểm tra Python HTTP server |
| API không kết nối | Kiểm tra port 5255 open |
| Transaction thất bại | Kiểm tra Account ETH trên Sepolia |
| Dữ liệu không hiển thị | Chờ transaction xác nhận |

---

## 📚 Tài liệu Đầy Đủ

Để biết thêm chi tiết, xem:

- 📖 [README_3STAGE_COMPLETE.md](README_3STAGE_COMPLETE.md) - Tổng quan hoàn tất
- 📖 [TESTING_3STAGE_SYSTEM.md](TESTING_3STAGE_SYSTEM.md) - Hướng dẫn test chi tiết
- 📖 [CHANGELOG_3STAGE.md](CHANGELOG_3STAGE.md) - Danh sách thay đổi
- 📖 [POWERSHELL_TESTING.md](POWERSHELL_TESTING.md) - PowerShell scripts

---

## ✅ Checklist

- [ ] Backend running on 5255
- [ ] Frontend running on 8000
- [ ] Có thể submit Stage 1 ✅
- [ ] Có thể submit Stage 2 ✅
- [ ] Có thể submit Stage 3 ✅
- [ ] Có thể truy xuất 3 stage ✅
- [ ] QR code hiển thị ✅

---

**🎉 Đã sẵn sàng sử dụng AgriTrace 3.0!**
