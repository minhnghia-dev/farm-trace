const hre = require("hardhat");
const { ethers } = hre;

async function main() {
    // Địa chỉ hợp đồng
    const contractAddress = "0x585FEb9D566331e11ec668411206B2A395eC357D"; // Địa chỉ mới
    const contractABI = require("../artifacts/contracts/AgriTrace.sol/AgriTrace.json").abi;

    // Kết nối với blockchain Sepolia
    const provider = ethers.provider;
    const contract = new ethers.Contract(contractAddress, contractABI, provider);

    // Kiểm tra các mã sản phẩm
    const productCodes = ["TEST002", "CAFE001", "CAFE002", "GAO001"];

    console.log("=== Kiểm tra toàn diện dữ liệu trên Blockchain ===\n");
    console.log("Địa chỉ hợp đồng:", contractAddress);
    console.log("Provider:", provider._network?.name || "unknown");

    // Kiểm tra xem hợp đồng có tồn tại không
    const code = await ethers.provider.getCode(contractAddress);
    if (code === "0x") {
        console.error("❌ Hợp đồng không tồn tại tại địa chỉ:", contractAddress);
        return;
    }
    console.log("✓ Hợp đồng tồn tại tại địa chỉ:", contractAddress);

    // Kiểm tra từng sản phẩm
    for (const productCode of productCodes) {
        try {
            const product = await contract.getProduct(productCode);
            console.log(`\n📦 Sản phẩm: ${productCode}`);
            console.log(`   Tên: "${product[0]}"`);
            console.log(`   Nông trại: "${product[1]}"`);
            console.log(`   Ngày thu hoạch: ${product[2].toString()}`);
            console.log(`   Nhà vận chuyển: "${product[3]}"`);

            if (product[0] === "" && product[1] === "" && product[2].toString() === "0") {
                console.log("   ⚠️  Không tìm thấy dữ liệu cho sản phẩm này");
            } else {
                console.log("   ✓ Dữ liệu hợp lệ");
            }
        } catch (error) {
            console.error(`   ❌ Lỗi khi gọi hàm getProduct: ${error.message}`);
        }
    }

    // Kiểm tra các sự kiện ProductDetails
    console.log("\n\n=== Kiểm tra sự kiện ProductDetails ===\n");
    try {
        const filter = contract.filters.ProductDetails();
        const events = await contract.queryFilter(filter, -100000); // Kiểm tra 100,000 block gần đây
        
        if (events.length === 0) {
            console.log("⚠️  Không tìm thấy sự kiện ProductDetails nào");
        } else {
            console.log(`✓ Tìm thấy ${events.length} sự kiện ProductDetails:`);
            events.slice(-5).forEach((event, index) => {
                console.log(`\n  Sự kiện ${index + 1}:`);
                console.log(`    Code: ${event.args.code}`);
                console.log(`    Name: ${event.args.name}`);
                console.log(`    Farm: ${event.args.farm}`);
                console.log(`    Date: ${event.args.harvestDate.toString()}`);
                console.log(`    Transporter: ${event.args.transporter}`);
            });
        }
    } catch (error) {
        console.error(`❌ Lỗi khi truy vấn sự kiện: ${error.message}`);
    }

    // Kiểm tra giao dịch cụ thể
    console.log("\n\n=== Kiểm tra giao dịch cụ thể ===\n");
    const txHash = "0xeb2f4c3228715bf65925c9930902801d5fad7264fedd2fa819a5ad8159f2f95b";
    try {
        const tx = await ethers.provider.getTransaction(txHash);
        const receipt = await ethers.provider.getTransactionReceipt(txHash);
        
        console.log(`✓ Giao dịch: ${txHash}`);
        console.log(`  Status: ${receipt.status === 1 ? "Thành công" : "Thất bại"}`);
        console.log(`  From: ${tx.from}`);
        console.log(`  To: ${tx.to}`);
        console.log(`  Gas Used: ${receipt.gasUsed.toString()}`);
    } catch (error) {
        console.error(`❌ Lỗi khi kiểm tra giao dịch: ${error.message}`);
    }
}

main().catch((error) => {
    console.error(error);
    process.exitCode = 1;
});