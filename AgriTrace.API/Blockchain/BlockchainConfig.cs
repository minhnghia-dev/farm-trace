namespace AgriTrace.API.Blockchain
{
    public static class BlockchainConfig
    {
        // Địa chỉ Smart Contract bạn đã triển khai trên mạng Sepolia
        // Lưu ý: Nếu bạn deploy lại contract mới, hãy cập nhật địa chỉ này
        public const string ContractAddress = "0xf00A30ca670526B1903286ac6B756187BaEbE4cB";

        // ABI (Application Binary Interface): Định nghĩa các hàm để C# gọi được vào Smart Contract
        public const string ContractABI = @"[
            {
                ""anonymous"": false,
                ""inputs"": [
                    { ""indexed"": false, ""internalType"": ""string"", ""name"": ""code"", ""type"": ""string"" },
                    { ""indexed"": false, ""internalType"": ""string"", ""name"": ""productName"", ""type"": ""string"" },
                    { ""indexed"": false, ""internalType"": ""string"", ""name"": ""producer"", ""type"": ""string"" }
                ],
                ""name"": ""ProductionStageAdded"",
                ""type"": ""event""
            },
            {
                ""anonymous"": false,
                ""inputs"": [
                    { ""indexed"": false, ""internalType"": ""string"", ""name"": ""code"", ""type"": ""string"" },
                    { ""indexed"": false, ""internalType"": ""uint256"", ""name"": ""harvestDate"", ""type"": ""uint256"" },
                    { ""indexed"": false, ""internalType"": ""uint256"", ""name"": ""quantity"", ""type"": ""uint256"" }
                ],
                ""name"": ""HarvestStageAdded"",
                ""type"": ""event""
            },
            {
                ""anonymous"": false,
                ""inputs"": [
                    { ""indexed"": false, ""internalType"": ""string"", ""name"": ""code"", ""type"": ""string"" },
                    { ""indexed"": false, ""internalType"": ""string"", ""name"": ""distributionUnit"", ""type"": ""string"" },
                    { ""indexed"": false, ""internalType"": ""string"", ""name"": ""salePoint"", ""type"": ""string"" }
                ],
                ""name"": ""DistributionStageAdded"",
                ""type"": ""event""
            },
            {
                ""inputs"": [
                    { ""internalType"": ""string"", ""name"": ""_code"", ""type"": ""string"" },
                    { ""internalType"": ""string"", ""name"": ""_productName"", ""type"": ""string"" },
                    { ""internalType"": ""string"", ""name"": ""_productLocation"", ""type"": ""string"" },
                    { ""internalType"": ""uint256"", ""name"": ""_productionStartDate"", ""type"": ""uint256"" },
                    { ""internalType"": ""string"", ""name"": ""_producer"", ""type"": ""string"" }
                ],
                ""name"": ""addProductionStage"",
                ""outputs"": [],
                ""stateMutability"": ""nonpayable"",
                ""type"": ""function""
            },
            {
                ""inputs"": [
                    { ""internalType"": ""string"", ""name"": ""_code"", ""type"": ""string"" },
                    { ""internalType"": ""uint256"", ""name"": ""_harvestDate"", ""type"": ""uint256"" },
                    { ""internalType"": ""uint256"", ""name"": ""_quantity"", ""type"": ""uint256"" },
                    { ""internalType"": ""string"", ""name"": ""_processingMethod"", ""type"": ""string"" },
                    { ""internalType"": ""uint256"", ""name"": ""_packingDate"", ""type"": ""uint256"" },
                    { ""internalType"": ""string"", ""name"": ""_processingUnit"", ""type"": ""string"" }
                ],
                ""name"": ""addHarvestStage"",
                ""outputs"": [],
                ""stateMutability"": ""nonpayable"",
                ""type"": ""function""
            },
            {
                ""inputs"": [
                    { ""internalType"": ""string"", ""name"": ""_code"", ""type"": ""string"" },
                    { ""internalType"": ""string"", ""name"": ""_distributionUnit"", ""type"": ""string"" },
                    { ""internalType"": ""uint256"", ""name"": ""_warehouseExitDate"", ""type"": ""uint256"" },
                    { ""internalType"": ""string"", ""name"": ""_salePoint"", ""type"": ""string"" },
                    { ""internalType"": ""string"", ""name"": ""_productStatus"", ""type"": ""string"" },
                    { ""internalType"": ""uint256"", ""name"": ""_distributionCompletedDate"", ""type"": ""uint256"" }
                ],
                ""name"": ""addDistributionStage"",
                ""outputs"": [],
                ""stateMutability"": ""nonpayable"",
                ""type"": ""function""
            },
            {
                ""inputs"": [
                    { ""internalType"": ""string"", ""name"": ""_code"", ""type"": ""string"" }
                ],
                ""name"": ""getProductRecord"",
                ""outputs"": [
                    {
                        ""components"": [
                            { ""internalType"": ""string"", ""name"": ""productCode"", ""type"": ""string"" },
                            { ""internalType"": ""string"", ""name"": ""productName"", ""type"": ""string"" },
                            { ""internalType"": ""string"", ""name"": ""productLocation"", ""type"": ""string"" },
                            { ""internalType"": ""uint256"", ""name"": ""productionStartDate"", ""type"": ""uint256"" },
                            { ""internalType"": ""string"", ""name"": ""producer"", ""type"": ""string"" },
                            { ""internalType"": ""bool"", ""name"": ""completed"", ""type"": ""bool"" }
                        ],
                        ""internalType"": ""struct AgriTrace.ProductionStage"",
                        ""name"": """",
                        ""type"": ""tuple""
                    },
                    {
                        ""components"": [
                            { ""internalType"": ""uint256"", ""name"": ""harvestDate"", ""type"": ""uint256"" },
                            { ""internalType"": ""uint256"", ""name"": ""quantity"", ""type"": ""uint256"" },
                            { ""internalType"": ""string"", ""name"": ""processingMethod"", ""type"": ""string"" },
                            { ""internalType"": ""uint256"", ""name"": ""packingDate"", ""type"": ""uint256"" },
                            { ""internalType"": ""string"", ""name"": ""processingUnit"", ""type"": ""string"" },
                            { ""internalType"": ""bool"", ""name"": ""completed"", ""type"": ""bool"" }
                        ],
                        ""internalType"": ""struct AgriTrace.HarvestStage"",
                        ""name"": """",
                        ""type"": ""tuple""
                    },
                    {
                        ""components"": [
                            { ""internalType"": ""string"", ""name"": ""distributionUnit"", ""type"": ""string"" },
                            { ""internalType"": ""uint256"", ""name"": ""warehouseExitDate"", ""type"": ""uint256"" },
                            { ""internalType"": ""string"", ""name"": ""salePoint"", ""type"": ""string"" },
                            { ""internalType"": ""string"", ""name"": ""productStatus"", ""type"": ""string"" },
                            { ""internalType"": ""uint256"", ""name"": ""distributionCompletedDate"", ""type"": ""uint256"" },
                            { ""internalType"": ""bool"", ""name"": ""completed"", ""type"": ""bool"" }
                        ],
                        ""internalType"": ""struct AgriTrace.DistributionStage"",
                        ""name"": """",
                        ""type"": ""tuple""
                    }
                ],
                ""stateMutability"": ""view"",
                ""type"": ""function""
            }
        ]";
    }
}