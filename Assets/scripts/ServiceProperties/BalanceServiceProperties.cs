
using System.Net.Http;
using System;

public class BalanceServiceProperties
{
    // Use a constant for the base URL
    public readonly string baseUrl = "http://10.1.0.213:8080/bank-service/api/v1/balance";

    public HttpClient HttpClient { get; set; }

    public string ApiKey { get; set; }

    public string Balance { get; set; }

    public BalanceServiceProperties()
    {
        // Initialize the HTTP client
        HttpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };

        ApiKey = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJzdSIsImV4cCI6MTc0MTMzMzAwMSwicm9sZXMiOlsiUExBWUVSIiwiTUVOVE9SIiwiR0FNRV9ESVoiLCJNRVRIT0RJU1QiLCJBRE1JTklTVFJBVE9SIl0sInVzZXJJZCI6IjQxZWI1MWI2LTQ5YTktNGZiMi04Nzg1LWNkNzc0ZDA3ZDYyMiJ9.9nVZ7M2sPx1VdltUDAZ2ebi9WGATwWUIZloZSD8SI5s";
    }
}

