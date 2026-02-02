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