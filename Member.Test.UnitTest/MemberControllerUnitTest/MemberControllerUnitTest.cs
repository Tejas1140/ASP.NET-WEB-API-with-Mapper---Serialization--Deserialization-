using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WebApp2.ApplicationLayer.Controllers.V1;
using WebApp2.BusinessLayer.Services.Interface;
using WebApp2.Model.Entity;
using WebApp2.Model.RequestModel;
using WebApp2.Model.ResponseFormat;
using WebApp2.Model.ResponseModel;

namespace Member.Test.UnitTest.MemberControllerUnitTest
{
    public class MemberControllerUnitTest
    {
        private readonly Mock<IMemberService> _memberServiceMock;
        private readonly Mock<ILogger<MemberController>> _loggerMock;
        private readonly MemberController _memberController;

        public MemberControllerUnitTest()
        {
            _memberServiceMock = new Mock<IMemberService>();
            _loggerMock = new Mock<ILogger<MemberController>>();
            _memberController = new MemberController(_memberServiceMock.Object,_loggerMock.Object);
        }

        //FirstName
        [Fact]
        public async void FirstNameShouldNotNull()
        {
            //Arrange
            var obj = new AddMemberRequestModel()
            {
                Address = "Virar",
                FirstName = "",
                Age = 20,
                Gender = "Male",
                LastName = "Vaidya",
                Phone = "9075778727",
            };

            //Act
            IActionResult _result = await  _memberController.AddMember(obj, new CancellationToken());
            var _okResult = _result as ObjectResult;

            //Assert 
            Assert.NotNull(_result);
            Assert.Equal(StatusCodes.Status400BadRequest, _okResult.StatusCode);
        }

        //LastName
        [Fact]
        public async void LastNameShouldNotNull()
        {
            //Arrange
            var obj = new AddMemberRequestModel()
            {
                Address = "Virar",
                FirstName = "Tejas",
                Age = 20,
                Gender = "Male",
                LastName = "",
                Phone = "9075778727",
            };

            //Act
            IActionResult _result = await _memberController.AddMember(obj, new CancellationToken());
            var _okResult = _result as ObjectResult;

            //Assert 
            Assert.NotNull(_result);
            Assert.Equal(StatusCodes.Status400BadRequest, _okResult.StatusCode);
        }

        //Phone
        [Fact]
        public async void PhoneShouldIndianFormat()
        {
            //Arrange
            var obj = new AddMemberRequestModel()
            {
                Address = "Virar",
                FirstName = "Tejas",
                Age = 20,
                Gender = "Male",
                LastName = "Vaidya",
                Phone = "1234567890",
            };

            //Act
            IActionResult _result = await _memberController.AddMember(obj, new CancellationToken());
            var _okResult = _result as ObjectResult;

            //Assert 
            Assert.NotNull(_result);
            Assert.Equal(StatusCodes.Status400BadRequest, _okResult.StatusCode);
        }

        //Address
        [Fact]
        public async void AddressShouldNotNull()
        {
            //Arrange
            var obj = new AddMemberRequestModel()
            {
                Address = "",
                FirstName = "Tejas",
                Age = 20,
                Gender = "Male",
                LastName = "Vaidya",
                Phone = "9075778727",
            };

            //Act
            IActionResult _result = await _memberController.AddMember(obj, new CancellationToken());
            var _okResult = _result as ObjectResult;

            //Assert 
            Assert.NotNull(_result);
            Assert.Equal(StatusCodes.Status400BadRequest, _okResult.StatusCode);
        }

        //Gender
        [Fact]
        public async void GenderShouldNotNull()
        {
            //Arrange
            var obj = new AddMemberRequestModel()
            {
                Address = "Virar",
                FirstName = "Tejas",
                Age = 20,
                Gender = "",
                LastName = "Vaidya",
                Phone = "9075778727",
            };

            //Act
            IActionResult _result = await _memberController.AddMember(obj, new CancellationToken());
            var _okResult = _result as ObjectResult;

            //Assert 
            Assert.NotNull(_result);
            Assert.Equal(StatusCodes.Status400BadRequest, _okResult.StatusCode);
        }

        //Age
        [Fact]
        public async void AgeNotNull()
        {
            //Arrange
            var obj = new AddMemberRequestModel()
            {
                Address = "Virar",
                FirstName = "Tejas",
                Age = 0,
                Gender = "Male",
                LastName = "Vaidya",
                Phone = "9075778727",
            };

            //Act
            IActionResult _result = await _memberController.AddMember(obj, new CancellationToken());
            var _okResult = _result as ObjectResult;

            //Assert 
            Assert.NotNull(_result);
            Assert.Equal(StatusCodes.Status400BadRequest, _okResult.StatusCode);
        }  
        
        //ValidCase
        [Fact]
        public async Task ValidateTestCase()
        {
            //Arrange
            var obj = new AddMemberRequestModel()
            {
                Address = "Virar",
                FirstName = "Tejas",
                Age = 20,
                Gender = "Male",
                LastName = "Vaidya",
                Phone = "9075778727",
            };
            var response = new CommonStatusResponse<MemberDetailsResponseModel>()
            {
                statusCode = 1, statusMsg = "Member Added Successfully", Data = new MemberDetailsResponseModel()
                {
                    MemberId=23,
                    Address = "Virar",
                    FirstName = "Tejas",
                    Age = 20,
                    Gender = "Male",
                    LastName = "Vaidya",
                    Phone = "9075778727",
                }
            };
            _memberServiceMock.Setup(x=>x.AddMember(It.IsAny<AddMemberRequestModel>(),new CancellationToken())).ReturnsAsync(response);
            //Act
            var _result = await _memberController.AddMember(obj, new CancellationToken());
            var _okResult = _result as ObjectResult;
            var res = _okResult.Value;
            //Assert 
            Assert.NotNull(_result);
            Assert.Equal(StatusCodes.Status200OK, _okResult.StatusCode);
            Assert.NotNull(_okResult.Value.ToString());
        }

        //Valid Data with Null Response

        [Fact]
        public async Task InValidateTestCase()
        {
            //Arrange
            var obj = new AddMemberRequestModel()
            {
                Address = "Virar",
                FirstName = "Tejas",
                Age = 20,
                Gender = "Male",
                LastName = "Vaidya",
                Phone = "9075778727",
            };
            var response = new CommonStatusResponse<MemberDetailsResponseModel>()
            {
                statusCode = 0,
                statusMsg = "Some error occured while member adding. Please try again.",
                Data = null,
            };
            _memberServiceMock.Setup(x => x.AddMember(It.IsAny<AddMemberRequestModel>(), new CancellationToken())).ReturnsAsync(response);
            //Act
            var _result = await _memberController.AddMember(obj, new CancellationToken());
            var _okResult = _result as ObjectResult;
            var res = _okResult.Value;
            //Assert 
            Assert.NotNull(_result);
            Assert.Equal(StatusCodes.Status500InternalServerError, _okResult.StatusCode);
        }

        //GetAll
        [Fact]
        public async void ValidateGetAllMembers()
        {
            // Arrange
            var response = new CommonStatusResponse<List<MemberDetailsEntity>>()
            {
                Data = new List<MemberDetailsEntity>(),
                statusCode = 1,
                statusMsg = "All Members Details Retrived"
            };

            _memberServiceMock.Setup(x => x.GetAllMembers(It.IsAny<CancellationToken>()))
                              .ReturnsAsync(new CommonStatusResponse<IEnumerable<MemberDetailsEntity>>
                              {
                                  Data = response.Data,
                                  statusCode = response.statusCode,
                                  statusMsg = response.statusMsg
                              });

            // Act
            IActionResult _result = await _memberController.GetAllMembers(new CancellationToken());
            var _okResult = _result as ObjectResult;

            // Assert
            Assert.Equal(StatusCodes.Status200OK, _okResult.StatusCode);
            Assert.NotNull(_okResult);
        }

        //Invalidte test for getallmembers
        [Fact]
        public async void ValidateGetAllMembersExceptionHandling()
        {
            // Arrange
            _memberServiceMock.Setup(x => x.GetAllMembers(It.IsAny<CancellationToken>()))
                              .ThrowsAsync(new Exception("Simulated exception"));

            // Act
            IActionResult _result = await _memberController.GetAllMembers(new CancellationToken());
            var _objectResult = _result as ObjectResult;

            //// Assert
            //Assert.Equal(StatusCodes.Status500InternalServerError, _objectResult.StatusCode);
            //Assert.NotNull(_objectResult);

            var resultValue = (Result<string>)_objectResult.Value;
            Assert.Equal(StatusCodes.Status500InternalServerError, resultValue.statusCode);
            Assert.Equal("Failed to retrieve members", resultValue.message);
        }


        //GetAllAbove60
        [Fact]
        public async void ValidateGetAllMembersAbove60_Success()
        {
            // Arrange
            var response = new CommonStatusResponse<IEnumerable<MemberDetailsEntity>>()
            {
                Data = new List<MemberDetailsEntity>
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
                },
                statusCode = 1,
                statusMsg = "Members above 60 retrieved successfully"
            };

            _memberServiceMock.Setup(x => x.GetAllMembersAbove60(It.IsAny<CancellationToken>()))
                              .ReturnsAsync(response);

            // Act
            IActionResult result = await _memberController.GetAllMembersAbove60(new CancellationToken());
            var objectResult = result as ObjectResult;

            // Assert

            var resultValue = (Result<IEnumerable<MemberDetailsEntity>>)objectResult.Value;
            Assert.Equal(StatusCodes.Status200OK, resultValue.statusCode);
            Assert.Equal("All Members Details Whose Age is above 60 is Retrived", resultValue.message);
            Assert.NotNull(resultValue.data);
        }

        //Invalidte test for getallmembers60
        [Fact]
        public async void ValidateGetAllMembersAbove60_ExceptionHandling()
        {
            // Arrange
            _memberServiceMock.Setup(x => x.GetAllMembersAbove60(It.IsAny<CancellationToken>()))
                              .ThrowsAsync(new Exception("Simulated exception"));

            // Act
            IActionResult result = await _memberController.GetAllMembersAbove60(new CancellationToken());
            var objectResult = result as ObjectResult;

            // Assert
            Assert.NotNull(objectResult);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

            var resultValue = (Result<string>)objectResult.Value;
            Assert.Equal(StatusCodes.Status500InternalServerError, resultValue.statusCode);
            Assert.Equal("Failed to retrieve members", resultValue.message);
        }

        //ValidCase for UpdateMemberAddress
        [Fact]
        public async Task ValidateTestCaseForUpdateMemberAddress()
        {
            int memberId = 23;
            string newAddress = "Andheri";
            //Arrange
            var response = new CommonStatusResponse<MemberDetailsEntity>()
            {
                statusCode = 1,
                statusMsg = "Member Updated Successfully",
                Data = new MemberDetailsEntity()
                {
                    MemberId = 23,
                    Address = "Andheri",
                    FirstName = "Tejas",
                    Age = 20,
                    Gender = "Male",
                    LastName = "Vaidya",
                    Phone = "9075778727",
                }
            };
            _memberServiceMock.Setup(x => x.UpdateMemberAddress(memberId, newAddress, new CancellationToken())).ReturnsAsync(response);
            //Act
            var _result = await _memberController.UpdateMemberAddress(memberId, newAddress, new CancellationToken());
            var _okResult = _result as ObjectResult;
            var res = _okResult.Value;
            //Assert 
            Assert.NotNull(_result);
            Assert.Equal(StatusCodes.Status200OK, _okResult.StatusCode);
            Assert.NotNull(_okResult.Value.ToString());
        }

        //InValidCase for UpdateMemberAddress
        [Fact]
        public async Task InValidateTestCaseForUpdateMemberAddress()
        {
            int memberId = 200;
            string newAddress = "Virar";
            //Arrange
            var response = new CommonStatusResponse<MemberDetailsEntity>()
            {
                statusCode = 0,
                statusMsg = "Member Not Found",
                Data = null
            };
            _memberServiceMock.Setup(x => x.UpdateMemberAddress(memberId, newAddress, new CancellationToken())).ReturnsAsync(response);
            //Act
            var _result = await _memberController.UpdateMemberAddress(memberId, newAddress, new CancellationToken());
            var _okResult = _result as ObjectResult;
            var res = _okResult.Value;
            //Assert 
            Assert.NotNull(_result);
            Assert.Equal(StatusCodes.Status404NotFound, _okResult.StatusCode);
            Assert.Null(_okResult.Value.ToString());
        }

        //ValidCase for DeleteMemberById
        [Fact]
        public async Task ValidateTestCaseForDeleteMemberById()
        {
            int memberId = 20;
            //Arrange
            var response = new CommonStatusResponse<MemberDetailsEntity>()
            {
                statusCode = 1,
                statusMsg = "Member deleted successfully",
                Data = null
            };
            _memberServiceMock.Setup(x => x.DeleteMemberById(memberId, new CancellationToken())).ReturnsAsync(response);
            //Act
            var _result = await _memberController.DeleteMemberById(memberId, new CancellationToken());
            var _okResult = _result as ObjectResult;
            var res = _okResult.Value;
            //Assert 
            Assert.NotNull(_result);
            Assert.Equal(StatusCodes.Status200OK, _okResult.StatusCode);
        }

        //InValidCase for DeleteMemberById
        [Fact]
        public async Task InValidateTestCaseForDeleteMemberById()
        {
            int memberId = 200;
            //Arrange
            var response = new CommonStatusResponse<MemberDetailsEntity>()
            {
                statusCode = 0,
                statusMsg = "Member not found for deletion",
                Data = null
            };
            _memberServiceMock.Setup(x => x.DeleteMemberById(memberId, new CancellationToken())).ReturnsAsync(response);
            //Act
            var _result = await _memberController.DeleteMemberById(memberId, new CancellationToken());
            var _okResult = _result as ObjectResult;
            var res = _okResult.Value;
            //Assert 
            Assert.NotNull(_result);
            Assert.Equal(StatusCodes.Status404NotFound, _okResult.StatusCode);
        }

    }
}
