using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApp2.DataAccessLayer.DTOModel;
using WebApp2.Model.Entity;
using WebApp2.Model.RequestModel;
using WebApp2.Model.ResponseModel;

namespace WebApp2.DataAccessLayer.HttpService
{
    public class ApiClientService : IApiClientService
    {
        private readonly HttpClient _httpClient;

        public ApiClientService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        public async Task<AgentDetailsResponseDTO> AddPolicyAsync(AddPolicyRequestDTO addPolicyRequestDTO, CancellationToken cancellationToken)
        {
            // Serialize the request model to JSON
            string jsonRequest = JsonSerializer.Serialize(addPolicyRequestDTO);

            // Create the HTTP content with JSON
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await httpContent.ReadAsStringAsync();
            Console.WriteLine(response);
            //var agent = JsonSerializer.Deserialize<List<AgentDetails>>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var response1 = "{\"AgentId\":1,\"StartDate\":\"2024-02-10T08:00:00\",\"EndDate\":\"2024-02-20T18:00:00\",\"PolicyId\":123,\"PolicyTenure\":30,\"PolicyName\":\"SamplePolicy\"}";
            Console.WriteLine(response1);
            // Deserialize the string into a strongly-typed object
            var agentDetails = JsonSerializer.Deserialize<AgentDetailsResponseDTO>(response1);

           
            return agentDetails;
        }
        public Task<AgentResponseDTO> GetAll(CancellationToken cancellationToken)
        {
            var _response = "{\"AgentDetails\":[{\"AgentId\":1,\"StartDate\":\"2024-02-10T08:00:00\",\"EndDate\":\"2024-02-20T18:00:00\",\"PolicyId\":121,\"PolicyTenure\":30,\"PolicyName\":\"SamplePolicy\"},{\"AgentId\":2,\"StartDate\":\"2024-02-10T08:00:00\",\"EndDate\":\"2024-02-20T18:00:00\",\"PolicyId\":121,\"PolicyTenure\":40,\"PolicyName\":\"SamplePolicy\"},{\"AgentId\":3,\"StartDate\":\"2024-02-10T08:00:00\",\"EndDate\":\"2024-02-20T18:00:00\",\"PolicyId\":123,\"PolicyTenure\":50,\"PolicyName\":\"SamplePolicy\"}]}";

            // Deserialize the JSON response into AgentResponseDTO
            var _agentDetailsResponse = JsonSerializer.Deserialize<AgentResponseDTO>(_response);

            // Return the AgentResponseDTO as a Task
            return Task.FromResult(_agentDetailsResponse);
        }
    }
}

