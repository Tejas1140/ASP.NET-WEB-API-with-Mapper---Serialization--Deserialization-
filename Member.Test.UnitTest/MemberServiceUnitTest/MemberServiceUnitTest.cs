using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp2.BusinessLayer.Services.Implementation;
using WebApp2.DataAccessLayer.Interface;
using WebApp2.Model.Entity;
using WebApp2.Model.RequestModel;
using WebApp2.Model.ResponseModel;

namespace Member.Test.UnitTest.MemberServiceUnitTest
{
    public class MemberServiceUnitTest
    {
        private readonly Mock<IUnitOfWork> _repoMock;
        private readonly MemberService _memberService;

        public MemberServiceUnitTest()
        {
            _repoMock = new Mock<IUnitOfWork>();
            _memberService = new MemberService(_repoMock.Object);
        }

        //ValidateAddMember_Success 

        [Fact]
        public async void ValidateAddMember_Success()
        {
            // Arrange
            var requestModel = new AddMemberRequestModel()
            {
                Address = "Virar",
                FirstName = "Tejas",
                Age = 20,
                Gender = "Male",
                LastName = "Vaidya",
                Phone = "9075778727",
            };

            var memberEntity = new MemberDetailsEntity
            {
                MemberId = 23,
                Address = "Virar",
                FirstName = "Tejas",
                Age = 20,
                Gender = "Male",
                LastName = "Vaidya",
                Phone = "9075778727",
            };

            _repoMock.Setup(repo => repo.MemberRepository.AddAsync(It.IsAny<MemberDetailsEntity>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(memberEntity);

            _repoMock.Setup(repo => repo.save(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask); 

            // Act
            var result = await _memberService.AddMember(requestModel, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.statusCode);
            Assert.Equal("Success", result.statusMsg);
            Assert.NotNull(result.Data);
        }

        //ValidateAddMember_Failure 

        [Fact]
        public async Task ValidateAddMember_Failure()
        {
            // Arrange
            var requestModel = new AddMemberRequestModel()
            {
                Address = "Virar",
                FirstName = "Tejas",
                Age = 20,
                Gender = "Male",
                LastName = "Vaidya",
                Phone = "9075778727",
            };

            var memberEntity = new MemberDetailsEntity
            {
                MemberId = 23,
                Address = "Virar",
                FirstName = "Tejas",
                Age = 20,
                Gender = "Male",
                LastName = "Vaidya",
                Phone = "9075778727",
            };

            _repoMock.Setup(repo => repo.MemberRepository.AddAsync(It.IsAny<MemberDetailsEntity>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync((MemberDetailsEntity)null);

            // Act
            var result = await _memberService.AddMember(requestModel, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.statusCode);
            Assert.Equal("Failed", result.statusMsg);
            Assert.Null(result.Data);
        }

        //ValidateGetAllMembers_Success
        [Fact]
        public async Task ValidateGetAllMembers_Success()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var members = new List<MemberDetailsEntity>
            {
                    new MemberDetailsEntity
                    {
                        Address = "Virar",
                        FirstName = "Tejas",
                        Age = 64,
                        Gender = "Male",
                        LastName = "Vaidya",
                        Phone = "9075778727",
                    },
                    new MemberDetailsEntity
                    {
                        Address = "Virar",
                        FirstName = "Saili",
                        Age = 60,
                        Gender = "Female",
                        LastName = "Vaidya",
                        Phone = "9075778727",
                    }

            };

            // Mock the repository to return the sample list of members
            _repoMock.Setup(repo => repo.MemberRepository.GetAll(It.IsAny<CancellationToken>()))
                           .ReturnsAsync(members);

            // Act
            var result = await _memberService.GetAllMembers(cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.statusCode);
            Assert.Equal("Success", result.statusMsg);
            Assert.NotNull(result.Data);
            Assert.Equal(members, result.Data);
        }

        //ValidateGetAllMembers_Exception
        [Fact]
        public async Task ValidateGetAllMembers_Failure()
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            // Simulate repository returning null
            _repoMock.Setup(repo => repo.MemberRepository.GetAll(cancellationToken))
                     .ReturnsAsync((IEnumerable<MemberDetailsEntity>)null);

            // Act
            var result = await _memberService.GetAllMembers(cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.statusCode);
            Assert.Equal("Failed", result.statusMsg);
            Assert.Null(result.Data);
        }
    }
}
