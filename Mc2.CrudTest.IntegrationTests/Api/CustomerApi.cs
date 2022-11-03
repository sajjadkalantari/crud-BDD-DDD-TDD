
using Mc2.CrudTest.Presentation.Application.Commands;
using Mc2.CrudTest.Presentation.Application.Dtos;
using Mc2.CrudTest.Shared.Exceptions;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mc2.CrudTest.IntegrationTests.Api
{
    public class CustomerApi
    {
        private readonly string _baseUrl = "api/v1/Customers";
        private readonly RestClient _client;

        public CustomerApi()
        {
            _client = new RestClient("https://localhost:5001");
            _client.AddDefaultHeader("content-type", "application/json");
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => true;
        }

        public async Task<CustomerDTO> CreateCustomerAsync(CreateCustomerCommand model)
        {
            var request = new RestRequest(_baseUrl).AddBody(model);

            var response = await _client.ExecutePostAsync<CustomerDTO>(request);

            if (!response.IsSuccessful)
            {
                var result = JsonSerializer.Deserialize<ResponseDTO>(response.Content);
                throw new CustomSystemException(result.Code, result.Description);
            }

            return response.Data;
        }

        public async Task<CustomerDTO> UpdateCustomerAsync(UpdateCustomerCommand model)
        {
            var request = new RestRequest($"{_baseUrl}/{model.Id}").AddBody(model);
            var response = await _client.PutAsync<CustomerDTO>(request);
            return response;
        }

        public async Task<List<CustomerDTO>> GetCustomersAsync()
        {
            var request = new RestRequest(_baseUrl);
            var response = await _client.GetAsync<List<CustomerDTO>>(request);
            return response;
        }

        public async Task<int> DeleteCustomersAsync(int id)
        {
            var request = new RestRequest($"{_baseUrl}/{id}");
            var response = await _client.DeleteAsync<int>(request);
            return response;
        }
    }
}
