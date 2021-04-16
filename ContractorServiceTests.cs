using ApexaContractorAPI.Models;
using ApexaContractorAPI.Repository.Interfaces;
using ApexaContractorAPI.Service.Implementation;
using ApexaContractorAPI.Service.Interfaces;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ApexaContractorAPITest
{
    public class ContractorServiceTests
    {
        private readonly IContractorService _service;
        private readonly Mock<IContractorRepository> _iRepo = new Mock<IContractorRepository>();
        public ContractorServiceTests()
        {
            _service = new ContractorService(_iRepo.Object);
        }
        [Fact]
        public void GetContractors_Returns_List_Of_Contractors()
        {
            var list = new List<ContractorModel>();
            list.Add( new ContractorModel()
            {
                Id = 1,
                Name = "Danny",
                Address = "Address1",
                PhoneNumber = "(111)220-1231",
                Type = "Advisor",
                HealthStatus = "Green"
            });

            list.Add(new ContractorModel()
            {
                Id = 2,
                Name = "Treyson",
                Address = "Address1",
                PhoneNumber = "(111)220-1231",
                Type = "Carrier",
                HealthStatus = "Yellow"
            });
            
            _iRepo.Setup(x =>  x.GetContractorList()).Returns(list);

            var contractorslist = _service.GetContractorList();

            Assert.Equal(2, contractorslist.Count);//should return 2 contractors
        }

        [Fact]
        public void ValidateSaveContract_Returns_False_When_Contractors_Are_The_Same()
        {
            var id1 = 1;
            var id2 = 1;

            _iRepo.Setup(x => x.ValidateSaveContract(id1,id2)).Returns(false);

            var result = _service.ValidateSaveContract(id1, id2);

            Assert.False(result);
        }

        [Fact]
        public void ValidateSaveContract_Returns_True_When_Contractors_Are_Not_The_Same()
        {
            var id1 = 1;
            var id2 = 2;

            _iRepo.Setup(x => x.ValidateSaveContract(id1, id2)).Returns(true);

            var result = _service.ValidateSaveContract(id1, id2);

            Assert.True(result);
        }

        [Fact]
        public void DeleteSaveContract_Returns_True_When_Contract_Is_Removed()
        {
            var id1 = 1;

            _iRepo.Setup(x => x.RemoveContract(id1)).Returns(true);

            var result = _service.RemoveContract(id1);

            Assert.True(result);
        }
    }
}
