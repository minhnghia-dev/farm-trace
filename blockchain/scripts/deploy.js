const { ethers } = require("hardhat");

async function main() {
  // 1. Lấy thông tin tài khoản người deploy (ví MetaMask của Nghĩa)
  const [deployer] = await ethers.getSigners();
  console.log("----------------------------------------------------------");
  console.log("Đang triển khai Contract bằng tài khoản:", deployer.address);

  // 2. Kiểm tra số dư ví (Giúp Nghĩa biết mình có đủ 0.05 ETH không)
  const balance = await ethers.provider.getBalance(deployer.address);
  console.log("Số dư tài khoản hiện tại:", ethers.formatEther(balance), "ETH");

  // 3. Kết nối với Smart Contract có tên là "AgriTrace"
  // Lưu ý: Tên trong ngoặc phải trùng khớp hoàn toàn với tên class trong file .sol
  const AgriTrace = await ethers.getContractFactory("AgriTrace");
  
  console.log("Đang gửi yêu cầu triển khai AgriTrace...");

  // 4. Thực hiện lệnh Deploy
  const agriTrace = await AgriTrace.deploy();

  // 5. Đợi mạng lưới Sepolia xác nhận giao dịch thành công
  await agriTrace.waitForDeployment();

  // 6. In ra địa chỉ Contract cuối cùng
  const contractAddress = await agriTrace.getAddress();
  console.log("----------------------------------------------------------");
  console.log("CHÚC MỪNG! Contract đã được triển khai thành công.");
  console.log("Địa chỉ Contract của Nghĩa là:", contractAddress);
  console.log("----------------------------------------------------------");
  console.log("Hãy copy địa chỉ trên dán vào: https://sepolia.etherscan.io/");
}

// Thực thi hàm main và xử lý lỗi nếu có
main()
  .then(() => process.exit(0))
  .catch((error) => {
    console.error("Lỗi trong quá trình Deploy:", error);
    process.exit(1);
  });