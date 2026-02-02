using Microsoft.AspNetCore.Mvc;
using AgriTrace.API.Services;
using AgriTrace.API.Models;

namespace AgriTrace.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly BlockchainService _blockchainService;
        private readonly QRCodeService _qrCodeService;

        public ProductController(BlockchainService blockchainService, QRCodeService qrCodeService)
        {
            _blockchainService = blockchainService;
            _qrCodeService = qrCodeService;
        }

        /// <summary>
        /// Thêm Giai đoạn Sản xuất
        /// </summary>
        [HttpPost("production")]
        public async Task<IActionResult> AddProductionStage([FromBody] ProductionStageDto dto)
        {
            try
            {
                var txHash = await _blockchainService.AddProductionStageAsync(dto);
                return Ok(new { transactionHash = txHash, message = "Giai đoạn Sản xuất đã được ghi lên Blockchain!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Lỗi khi thêm Giai đoạn Sản xuất: {ex.Message}" });
            }
        }

        /// <summary>
        /// Thêm Giai đoạn Thu hoạch
        /// </summary>
        [HttpPost("harvest")]
        public async Task<IActionResult> AddHarvestStage([FromBody] HarvestStageDto dto)
        {
            try
            {
                var txHash = await _blockchainService.AddHarvestStageAsync(dto);
                return Ok(new { transactionHash = txHash, message = "Giai đoạn Thu hoạch đã được ghi lên Blockchain!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Lỗi khi thêm Giai đoạn Thu hoạch: {ex.Message}" });
            }
        }

        /// <summary>
        /// Thêm Giai đoạn Phân phối
        /// </summary>
        [HttpPost("distribution")]
        public async Task<IActionResult> AddDistributionStage([FromBody] DistributionStageDto dto)
        {
            try
            {
                var txHash = await _blockchainService.AddDistributionStageAsync(dto);
                return Ok(new { transactionHash = txHash, message = "Giai đoạn Phân phối đã được ghi lên Blockchain!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Lỗi khi thêm Giai đoạn Phân phối: {ex.Message}" });
            }
        }

        /// <summary>
        /// Lấy toàn bộ thông tin sản phẩm (3 giai đoạn) từ Blockchain
        /// </summary>
        [HttpGet("{code}")]
        public async Task<IActionResult> GetProductRecord(string code)
        {
            try
            {
                var result = await _blockchainService.GetProductRecordAsync(code);
                
                if (result == null || result.ProductionStage == null)
                {
                    return NotFound(new { message = "Không tìm thấy thông tin sản phẩm trên Blockchain!" });
                }

                // SỬA LỖI TẠI ĐÂY: Sử dụng biến 'code' từ tham số và tạo 'webUrl' đúng tên miền Netlify
                string webUrl = $"https://zesty-pithivier-ca08ff.netlify.app?code={code}";
                string qrCodeBase64 = _qrCodeService.GenerateQRCode(webUrl);

                var response = new
                {
                    productionStage = new
                    {
                        productCode = result.ProductionStage.ProductCode,
                        productName = result.ProductionStage.ProductName,
                        productLocation = result.ProductionStage.ProductLocation,
                        productionStartDate = result.ProductionStage.ProductionStartDate,
                        producer = result.ProductionStage.Producer
                    },
                    harvestStage = new
                    {
                        harvestDate = result.HarvestStage.HarvestDate,
                        quantity = result.HarvestStage.Quantity,
                        processingMethod = result.HarvestStage.ProcessingMethod,
                        packingDate = result.HarvestStage.PackingDate,
                        processingUnit = result.HarvestStage.ProcessingUnit
                    },
                    distributionStage = new
                    {
                        distributionUnit = result.DistributionStage.DistributionUnit,
                        warehouseExitDate = result.DistributionStage.WarehouseExitDate,
                        salePoint = result.DistributionStage.SalePoint,
                        productStatus = result.DistributionStage.ProductStatus,
                        distributionCompletedDate = result.DistributionStage.DistributionCompletedDate
                    },
                    qrCode = qrCodeBase64
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Lỗi khi lấy thông tin sản phẩm: {ex.Message}" });
            }
        }
    }
}
