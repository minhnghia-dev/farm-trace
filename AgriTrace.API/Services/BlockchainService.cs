using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using AgriTrace.API.Blockchain;
using System.Numerics;
using Nethereum.Contracts;
using Nethereum.ABI.FunctionEncoding.Attributes;
using AgriTrace.API.Models;

namespace AgriTrace.API.Services
{
    // Lớp để lưu trữ kết quả Giai đoạn Sản xuất từ smart contract
    [FunctionOutput]
    public class ProductionStageOutput
    {
        [Parameter("string", "productCode", 1)]
        public string ProductCode { get; set; }

        [Parameter("string", "productName", 2)]
        public string ProductName { get; set; }

        [Parameter("string", "productLocation", 3)]
        public string ProductLocation { get; set; }

        [Parameter("uint256", "productionStartDate", 4)]
        public BigInteger ProductionStartDate { get; set; }

        [Parameter("string", "producer", 5)]
        public string Producer { get; set; }

        [Parameter("bool", "completed", 6)]
        public bool Completed { get; set; }
    }

    // Lớp để lưu trữ kết quả Giai đoạn Thu hoạch từ smart contract
    [FunctionOutput]
    public class HarvestStageOutput
    {
        [Parameter("uint256", "harvestDate", 1)]
        public BigInteger HarvestDate { get; set; }

        [Parameter("uint256", "quantity", 2)]
        public BigInteger Quantity { get; set; }

        [Parameter("string", "processingMethod", 3)]
        public string ProcessingMethod { get; set; }

        [Parameter("uint256", "packingDate", 4)]
        public BigInteger PackingDate { get; set; }

        [Parameter("string", "processingUnit", 5)]
        public string ProcessingUnit { get; set; }

        [Parameter("bool", "completed", 6)]
        public bool Completed { get; set; }
    }

    // Lớp để lưu trữ kết quả Giai đoạn Phân phối từ smart contract
    [FunctionOutput]
    public class DistributionStageOutput
    {
        [Parameter("string", "distributionUnit", 1)]
        public string DistributionUnit { get; set; }

        [Parameter("uint256", "warehouseExitDate", 2)]
        public BigInteger WarehouseExitDate { get; set; }

        [Parameter("string", "salePoint", 3)]
        public string SalePoint { get; set; }

        [Parameter("string", "productStatus", 4)]
        public string ProductStatus { get; set; }

        [Parameter("uint256", "distributionCompletedDate", 5)]
        public BigInteger DistributionCompletedDate { get; set; }

        [Parameter("bool", "completed", 6)]
        public bool Completed { get; set; }
    }

    public class BlockchainService
    {
        private readonly Web3 _web3;
        private readonly ILogger<BlockchainService> _logger;

        public BlockchainService(IConfiguration configuration, ILogger<BlockchainService> logger)
        {
            _logger = logger;
            var privateKey = configuration["Blockchain:PrivateKey"] ?? throw new ArgumentNullException("PrivateKey missing");
            var chainUrl = configuration["Blockchain:ChainUrl"] ?? throw new ArgumentNullException("ChainUrl missing");
            
            // Thiết lập tài khoản từ Private Key
            var account = new Account(privateKey);
            _web3 = new Web3(account, chainUrl);
        }

        /// <summary>
        /// Thêm Giai đoạn Sản xuất lên Blockchain
        /// </summary>
        public async Task<string> AddProductionStageAsync(ProductionStageDto dto)
        {
            try 
            {
                var contract = _web3.Eth.GetContract(BlockchainConfig.ContractABI, BlockchainConfig.ContractAddress);
                var function = contract.GetFunction("addProductionStage");

                var gas = new Nethereum.Hex.HexTypes.HexBigInteger(600000);
                
                _logger.LogInformation($"Đang thêm Giai đoạn Sản xuất cho sản phẩm {dto.ProductCode}...");
                _logger.LogInformation($"Tham số: Code: {dto.ProductCode}, Name: {dto.ProductName}, Location: {dto.ProductLocation}, Date: {dto.ProductionStartDate}, Producer: {dto.Producer}");

                var txHash = await function.SendTransactionAsync(
                    _web3.TransactionManager.Account.Address,
                    gas,
                    new Nethereum.Hex.HexTypes.HexBigInteger(0),
                    dto.ProductCode, 
                    dto.ProductName, 
                    dto.ProductLocation, 
                    new BigInteger(dto.ProductionStartDate),
                    dto.Producer);

                _logger.LogInformation($"Thêm Giai đoạn Sản xuất thành công. Hash: {txHash}");
                return txHash;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi thêm Giai đoạn Sản xuất: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Thêm Giai đoạn Thu hoạch lên Blockchain
        /// </summary>
        public async Task<string> AddHarvestStageAsync(HarvestStageDto dto)
        {
            try 
            {
                var contract = _web3.Eth.GetContract(BlockchainConfig.ContractABI, BlockchainConfig.ContractAddress);
                var function = contract.GetFunction("addHarvestStage");

                var gas = new Nethereum.Hex.HexTypes.HexBigInteger(600000);
                
                _logger.LogInformation($"Đang thêm Giai đoạn Thu hoạch cho sản phẩm {dto.ProductCode}...");
                _logger.LogInformation($"Tham số: Code: {dto.ProductCode}, HarvestDate: {dto.HarvestDate}, Quantity: {dto.Quantity}, ProcessingMethod: {dto.ProcessingMethod}, PackingDate: {dto.PackingDate}, ProcessingUnit: {dto.ProcessingUnit}");

                var txHash = await function.SendTransactionAsync(
                    _web3.TransactionManager.Account.Address,
                    gas,
                    new Nethereum.Hex.HexTypes.HexBigInteger(0),
                    dto.ProductCode,
                    new BigInteger(dto.HarvestDate),
                    new BigInteger(dto.Quantity),
                    dto.ProcessingMethod,
                    new BigInteger(dto.PackingDate),
                    dto.ProcessingUnit);

                _logger.LogInformation($"Thêm Giai đoạn Thu hoạch thành công. Hash: {txHash}");
                return txHash;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi thêm Giai đoạn Thu hoạch: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Thêm Giai đoạn Phân phối lên Blockchain
        /// </summary>
        public async Task<string> AddDistributionStageAsync(DistributionStageDto dto)
        {
            try 
            {
                var contract = _web3.Eth.GetContract(BlockchainConfig.ContractABI, BlockchainConfig.ContractAddress);
                var function = contract.GetFunction("addDistributionStage");

                var gas = new Nethereum.Hex.HexTypes.HexBigInteger(600000);
                
                _logger.LogInformation($"Đang thêm Giai đoạn Phân phối cho sản phẩm {dto.ProductCode}...");
                _logger.LogInformation($"Tham số: Code: {dto.ProductCode}, DistributionUnit: {dto.DistributionUnit}, WarehouseExitDate: {dto.WarehouseExitDate}, SalePoint: {dto.SalePoint}, ProductStatus: {dto.ProductStatus}, CompletedDate: {dto.DistributionCompletedDate}");

                var txHash = await function.SendTransactionAsync(
                    _web3.TransactionManager.Account.Address,
                    gas,
                    new Nethereum.Hex.HexTypes.HexBigInteger(0),
                    dto.ProductCode,
                    dto.DistributionUnit,
                    new BigInteger(dto.WarehouseExitDate),
                    dto.SalePoint,
                    dto.ProductStatus,
                    new BigInteger(dto.DistributionCompletedDate));

                _logger.LogInformation($"Thêm Giai đoạn Phân phối thành công. Hash: {txHash}");
                return txHash;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi thêm Giai đoạn Phân phối: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Lấy toàn bộ thông tin sản phẩm (3 giai đoạn) từ Blockchain
        /// </summary>
        public async Task<ProductTraceDto> GetProductRecordAsync(string code)
        {
            try
            {
                var contract = _web3.Eth.GetContract(BlockchainConfig.ContractABI, BlockchainConfig.ContractAddress);
                var function = contract.GetFunction("getProductRecord");

                // Gọi hàm view để lấy 3 giai đoạn
                var result = await function.CallDeserializingToObjectAsync<ProductRecordOutput>(code);

                if (result != null)
                {
                    _logger.LogInformation($"Lấy thông tin sản phẩm {code} thành công từ Blockchain");

                    var productTrace = new ProductTraceDto
                    {
                        ProductionStage = new ProductionStageDto
                        {
                            ProductCode = result.ProductionStage.ProductCode ?? "",
                            ProductName = result.ProductionStage.ProductName ?? "",
                            ProductLocation = result.ProductionStage.ProductLocation ?? "",
                            ProductionStartDate = (long)result.ProductionStage.ProductionStartDate,
                            Producer = result.ProductionStage.Producer ?? "",
                        },
                        HarvestStage = new HarvestStageDto
                        {
                            ProductCode = code,
                            HarvestDate = (long)result.HarvestStage.HarvestDate,
                            Quantity = (long)result.HarvestStage.Quantity,
                            ProcessingMethod = result.HarvestStage.ProcessingMethod ?? "",
                            PackingDate = (long)result.HarvestStage.PackingDate,
                            ProcessingUnit = result.HarvestStage.ProcessingUnit ?? "",
                        },
                        DistributionStage = new DistributionStageDto
                        {
                            ProductCode = code,
                            DistributionUnit = result.DistributionStage.DistributionUnit ?? "",
                            WarehouseExitDate = (long)result.DistributionStage.WarehouseExitDate,
                            SalePoint = result.DistributionStage.SalePoint ?? "",
                            ProductStatus = result.DistributionStage.ProductStatus ?? "",
                            DistributionCompletedDate = (long)result.DistributionStage.DistributionCompletedDate,
                        }
                    };

                    return productTrace;
                }

                _logger.LogWarning($"Kết quả null cho mã sản phẩm {code}");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi đọc từ Blockchain: {ex.Message}");
                throw;
            }
        }
    }

    // Lớp để deserialize kết quả từ getProductRecord
    [FunctionOutput]
    public class ProductRecordOutput
    {
        [Parameter("tuple", "production", 1)]
        public ProductionStageOutput ProductionStage { get; set; }

        [Parameter("tuple", "harvest", 2)]
        public HarvestStageOutput HarvestStage { get; set; }

        [Parameter("tuple", "distribution", 3)]
        public DistributionStageOutput DistributionStage { get; set; }
    }
}