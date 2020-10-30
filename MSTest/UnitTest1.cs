using IndianCensus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MSTest
{
    [TestClass]
    public class UnitTest1
    {
        string stateCodePath = @"E:\C# projects\IndianCensus\IndianCensus\CSVFiles\IndiaStateCode.csv";
        string stateCensusPath = @"E:\C# projects\IndianCensus\IndianCensus\CSVFiles\IndiaStateCensusData.csv";
        string wrongCensusPath = @"E:\C# projects\IndianCensus\IndianCensus\CSVFiles\IndiaStateData.csv";
        string wrongStateCodePath = @"E:\C# projects\IndianCensus\IndianCensus\CSVFiles\IndiaCode.csv";
        string wrongTypeStateCodePath = @"E:\C# projects\IndianCensus\IndianCensus\CSVFiles\IndiaStateCode.txt";
        string wrongHeaderStateCodePath = @"E:\C# projects\IndianCensus\IndianCensus\CSVFiles\WrongIndiaStateCode.csv";
        string wrongHeaderStateCensusPath = @"E:\C# projects\IndianCensus\IndianCensus\CSVFiles\WrongIndiaStateCensusData.csv";
        string delimiterStateCodePath = @"E:\C# projects\IndianCensus\IndianCensus\CSVFiles\DelimiterIndiaStateCode.csv";
        string delimiterStateCensusPath = @"E:\C# projects\IndianCensus\IndianCensus\CSVFiles\DelimiterIndiaStateCensusData.csv";
        IndianCensus.CSVAdapterFactory csv = null;
        Dictionary<string, CensusDTO> totalRecords;
        Dictionary<string, CensusDTO> stateRecords;
        [TestInitialize]
        public void SetUp() {
            csv = new IndianCensus.CSVAdapterFactory();
            totalRecords = new Dictionary<string, CensusDTO>();
            stateRecords = new Dictionary<string, CensusDTO>();
        }
        /// TC 1.1
        /// Giving the correct path it should return the total count from the census
        [TestMethod]
        public void GivenStateCensusCSVShouldReturnRecords()
        {
            stateRecords = csv.LoadCsvData(IndianCensus.CensusAnalyser.Country.INDIA, stateCensusPath, "State,Population,AreaInSqKm,DensityPerSqKm");
            Assert.AreEqual(29, stateRecords.Count);
        }
        /// TC 1.2
        /// Giving incorrect path should return file not found custom exception
        [TestMethod]
        public void GivenIncorrectFileShouldThrowCustomException() {
            var censusCustomException = Assert.ThrowsException<CensusAnalyserException>(() => csv.LoadCsvData(CensusAnalyser.Country.INDIA, wrongCensusPath, "State,Population,AreaInSqKm,DensityPerSqKm"));
            Assert.AreEqual(censusCustomException.exception, CensusAnalyserException.Exception.FILE_NOT_FOUND);   
        }
        /// TC 1.3
        /// Giving wrong type of path should return invalid file type custom exception
        [TestMethod]
        public void GivenWrongTypeReturnsCustomException() {
            var customException = Assert.ThrowsException<CensusAnalyserException>(()=>csv.LoadCsvData(CensusAnalyser.Country.INDIA,wrongTypeStateCodePath, "SrNo,State Name,TIN,StateCode"));
            Assert.AreEqual(customException.exception, CensusAnalyserException.Exception.INVALID_FILE_TYPE);
        }
        /// TC 1.4
        /// Giving wrong delimiter should return incorrect delimiter custom exception
        [TestMethod]
        public void GivenWrongDelimeterReturnsCustomException() {
            
            var censusException = Assert.ThrowsException<CensusAnalyserException>(() => csv.LoadCsvData(CensusAnalyser.Country.INDIA, delimiterStateCensusPath, "State,Population,AreaInSqKm,DensityPerSqKm"));
            Assert.AreEqual(censusException.exception, CensusAnalyserException.Exception.INCOREECT_DELIMITER);
        }
        /// TC 1.5
        /// Giving wrong header type should return incorrect header type custom exception
        [TestMethod]
        public void GivenWrongHeaderReturnsCustomException() {
            
            var censusException = Assert.ThrowsException<CensusAnalyserException>(()=>csv.LoadCsvData(CensusAnalyser.Country.INDIA,wrongHeaderStateCensusPath, "State,Population,AreaInSqKm,DensityPerSqKm"));
            Assert.AreEqual(censusException.exception, CensusAnalyserException.Exception.INCORRECT_HEADER);
        }
        /// TC 2.1
        /// Giving the correct path it should return the total count from the census
        [TestMethod]
        public void GivenStateCodeReturnCount() {
            totalRecords = csv.LoadCsvData(CensusAnalyser.Country.INDIA, stateCodePath, "SrNo,State Name,TIN,StateCode");
            Assert.AreEqual(37, totalRecords.Count);
        }
        /// TC 2.2
        /// Giving incorrect path should return file not found custom exception
        [TestMethod]
        public void GivenIncorrectPathCodeCustomException() {
            var stateCustomException = Assert.ThrowsException<CensusAnalyserException>(() => csv.LoadCsvData(CensusAnalyser.Country.INDIA, wrongStateCodePath, "SrNo,State Name,TIN,StateCode"));
            Assert.AreEqual(stateCustomException.exception, CensusAnalyserException.Exception.FILE_NOT_FOUND);
        }
        /// TC 2.3
        /// Giving wrong type of path should return invalid file type custom exception
        [TestMethod]
        public void GivenIncorrectPathTypeCodeReturnException() {
            var customException = Assert.ThrowsException<CensusAnalyserException>(() => csv.LoadCsvData(CensusAnalyser.Country.INDIA, wrongTypeStateCodePath, "SrNo,State Name,TIN,StateCode"));
            Assert.AreEqual(customException.exception, CensusAnalyserException.Exception.INVALID_FILE_TYPE);
        }
        /// TC 2.4
        /// Giving wrong delimiter should return incorrect delimiter custom exception
        [TestMethod]
        public void GivenWrongHeaderStateCodeReturnCustomException() {
            var stateException = Assert.ThrowsException<CensusAnalyserException>(() => csv.LoadCsvData(CensusAnalyser.Country.INDIA, delimiterStateCodePath, "SrNo,State Name,TIN,StateCode"));
            Assert.AreEqual(stateException.exception, CensusAnalyserException.Exception.INCOREECT_DELIMITER);
        }
        /// TC 2.5
        /// Giving wrong header type should return incorrect header type custom exception
        [TestMethod]
        public void GivenWrongDelimiterCodeReturnCustomException() {
            var stateException = Assert.ThrowsException<CensusAnalyserException>(() => csv.LoadCsvData(CensusAnalyser.Country.INDIA, wrongHeaderStateCodePath, "SrNo,State Name,TIN,StateCode"));
            Assert.AreEqual(stateException.exception, CensusAnalyserException.Exception.INCORRECT_HEADER);
        }
    }
}
