# AgriTrace 3.0 – Hệ thống Truy xuất Nguồn gốc Nông sản Đa Giai đoạn

AgriTrace 3.0 là hệ thống truy xuất nguồn gốc nông sản dựa trên **Blockchain**, cho phép theo dõi **toàn bộ vòng đời của sản phẩm** từ lúc sản xuất cho đến khi đến tay người tiêu dùng.

Phiên bản 3.0 đánh dấu bước chuyển quan trọng từ mô hình **ghi nhận tĩnh** sang **theo dõi đa giai đoạn (Multi-stage Tracking)**, đảm bảo **minh bạch – bất biến – có thể kiểm chứng** trong chuỗi cung ứng nông nghiệp.

---

## Mục tiêu hệ thống

- Minh bạch hóa chuỗi cung ứng nông sản Việt
- Ngăn chặn gian lận nguồn gốc, làm giả xuất xứ
- Tăng niềm tin của người tiêu dùng
- Hỗ trợ nông dân, doanh nghiệp và đơn vị vận chuyển ghi nhận dữ liệu đáng tin cậy

---

## Demo triển khai

- **Frontend**: Netlify (Static Web) - Link: https://farm-trace.netlify.app
- **Backend API**: Render (Swagger API v3.0)
- **Blockchain**: EVM-compatible (Hardhat / Local / Testnet)

Người dùng quét **QR Code** để xem **timeline đầy đủ của sản phẩm**.

---

## Công nghệ sử dụng

| Thành phần | Công nghệ |
|---------|----------|
| Smart Contract | Solidity ^0.8.0 |
| Blockchain Tooling | Hardhat |
| Backend API | .NET 9 |
| Blockchain SDK | Nethereum |
| Frontend | HTML5, Tailwind CSS, Vanilla JS |
| Deploy Backend | Render (Docker) |
| Deploy Frontend | Netlify |

---

## Thiết kế Smart Contract

Smart Contract của AgriTrace 3.0 được thiết kế nhằm đảm bảo **tính bất biến, minh bạch và khả năng mở rộng** cho việc truy xuất nguồn gốc nông sản theo mô hình **đa giai đoạn (Multi-stage Tracking)**.

### Mục tiêu thiết kế

- Lưu trữ **toàn bộ vòng đời sản phẩm** trên Blockchain
- Mỗi giai đoạn trong chuỗi cung ứng là **một bản ghi bất biến**
- Chỉ cho phép **ghi thêm (append-only)**, không cho chỉnh sửa hoặc xóa
- Dễ dàng mở rộng thêm giai đoạn mới trong tương lai

---

## Ghi chú Triển khai

- **Render**  
  Đã cấu hình `Dockerfile` để tự động build và chạy môi trường **.NET 9** cho Backend API.

- **Netlify**  
  Triển khai Frontend bằng cách kéo thả thư mục `AgriTrace.Frontend`.  
  Đảm bảo biến môi trường `API_URL` được thiết lập chính xác để kết nối Backend.

---

© 2025 **AgriTrace Team**  
*Minh bạch nông sản Việt bằng sức mạnh Blockchain*
