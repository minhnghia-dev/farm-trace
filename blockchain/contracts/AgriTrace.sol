// SPDX-License-Identifier: MIT
pragma solidity ^0.8.20;

contract AgriTrace {

    // Giai đoạn 1: Sản xuất
    struct ProductionStage {
        string productCode;
        string productName;
        string productLocation;
        uint256 productionStartDate;
        string producer;
        bool completed;
    }

    // Giai đoạn 2: Thu hoạch & xử lý
    struct HarvestStage {
        uint256 harvestDate;
        uint256 quantity;
        string processingMethod;
        uint256 packingDate;
        string processingUnit;
        bool completed;
    }

    // Giai đoạn 3: Phân phối & tiêu thụ
    struct DistributionStage {
        string distributionUnit;
        uint256 warehouseExitDate;
        string salePoint;
        string productStatus;
        uint256 distributionCompletedDate;
        bool completed;
    }

    // Lưu trữ toàn bộ dữ liệu sản phẩm
    struct ProductRecord {
        ProductionStage production;
        HarvestStage harvest;
        DistributionStage distribution;
    }

    mapping(string => ProductRecord) private productRecords;

    event ProductionStageAdded(string code, string productName, string producer);
    event HarvestStageAdded(string code, uint256 harvestDate, uint256 quantity);
    event DistributionStageAdded(string code, string distributionUnit, string salePoint);

    // Giai đoạn 1: Thêm thông tin sản xuất
    function addProductionStage(
        string memory _code,
        string memory _productName,
        string memory _productLocation,
        uint256 _productionStartDate,
        string memory _producer
    ) public {
        productRecords[_code].production = ProductionStage(
            _code,
            _productName,
            _productLocation,
            _productionStartDate,
            _producer,
            true
        );
        emit ProductionStageAdded(_code, _productName, _producer);
    }

    // Giai đoạn 2: Thêm thông tin thu hoạch & xử lý
    function addHarvestStage(
        string memory _code,
        uint256 _harvestDate,
        uint256 _quantity,
        string memory _processingMethod,
        uint256 _packingDate,
        string memory _processingUnit
    ) public {
        productRecords[_code].harvest = HarvestStage(
            _harvestDate,
            _quantity,
            _processingMethod,
            _packingDate,
            _processingUnit,
            true
        );
        emit HarvestStageAdded(_code, _harvestDate, _quantity);
    }

    // Giai đoạn 3: Thêm thông tin phân phối & tiêu thụ
    function addDistributionStage(
        string memory _code,
        string memory _distributionUnit,
        uint256 _warehouseExitDate,
        string memory _salePoint,
        string memory _productStatus,
        uint256 _distributionCompletedDate
    ) public {
        productRecords[_code].distribution = DistributionStage(
            _distributionUnit,
            _warehouseExitDate,
            _salePoint,
            _productStatus,
            _distributionCompletedDate,
            true
        );
        emit DistributionStageAdded(_code, _distributionUnit, _salePoint);
    }

    // Lấy toàn bộ dữ liệu sản phẩm (tất cả 3 giai đoạn)
    function getProductRecord(string memory _code)
        public
        view
        returns (
            ProductionStage memory,
            HarvestStage memory,
            DistributionStage memory
        )
    {
        ProductRecord memory record = productRecords[_code];
        return (
            record.production,
            record.harvest,
            record.distribution
        );
    }
}
