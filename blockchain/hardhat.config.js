require("@nomicfoundation/hardhat-toolbox");

const SEPOLIA_PRIVATE_KEY = "a03f93962fd40c09e1ed111ff4e4560003875a3c31f38070ea55ad23e23ddf3c"; 

module.exports = {
  solidity: "0.8.28", 
  networks: {
    sepolia: {
      url: "https://eth-sepolia.g.alchemy.com/v2/lIZev7ByZECS5qwumKkpx",
      accounts: [SEPOLIA_PRIVATE_KEY] 
    }
  }
};

const ABI = [
    {
        "inputs": [
            { "internalType": "string", "name": "_code", "type": "string" }
        ],
        "name": "getProduct",
        "outputs": [
            { "internalType": "string", "name": "", "type": "string" },
            { "internalType": "string", "name": "", "type": "string" },
            { "internalType": "uint256", "name": "", "type": "uint256" },
            { "internalType": "string", "name": "", "type": "string" }
        ],
        "stateMutability": "view",
        "type": "function"
    }
];
const contract = new ethers.Contract("0x8D30de71F97173387e2F17d6a5fc5A5FfF29E82A", ABI, ethers.provider);
const product = await contract.getProduct("CAFE001");
console.log("Sản phẩm:", product);