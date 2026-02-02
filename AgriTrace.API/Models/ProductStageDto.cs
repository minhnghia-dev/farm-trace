namespace AgriTrace.API.Models
{
    public class ProductionStageDto
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductLocation { get; set; }
        public long ProductionStartDate { get; set; }
        public string Producer { get; set; }
    }

    public class HarvestStageDto
    {
        public string ProductCode { get; set; }
        public long HarvestDate { get; set; }
        public long Quantity { get; set; }
        public string ProcessingMethod { get; set; }
        public long PackingDate { get; set; }
        public string ProcessingUnit { get; set; }
    }

    public class DistributionStageDto
    {
        public string ProductCode { get; set; }
        public string DistributionUnit { get; set; }
        public long WarehouseExitDate { get; set; }
        public string SalePoint { get; set; }
        public string ProductStatus { get; set; }
        public long DistributionCompletedDate { get; set; }
    }

    public class ProductTraceDto
    {
        public ProductionStageDto ProductionStage { get; set; }
        public HarvestStageDto HarvestStage { get; set; }
        public DistributionStageDto DistributionStage { get; set; }
        public string QrCode { get; set; }
    }
}
