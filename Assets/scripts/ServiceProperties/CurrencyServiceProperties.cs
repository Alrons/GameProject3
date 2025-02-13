using System.Net.Http;
using System;

public class CurrencyServiceProperties
{
    // Use a constant for the base URL
    public readonly string baseUrl = "http://10.1.0.213:8080/reward-service/api/v1/currency";

    public string ApiKey { get; set; }

    public CurrencyServiceProperties()
    {

        ApiKey = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJzdSIsImV4cCI6MTc0MTMzMzAwMSwicm9sZXMiOlsiUExBWUVSIiwiTUVOVE9SIiwiR0FNRV9ESVoiLCJNRVRIT0RJU1QiLCJBRE1JTklTVFJBVE9SIl0sInVzZXJJZCI6IjQxZWI1MWI2LTQ5YTktNGZiMi04Nzg1LWNkNzc0ZDA3ZDYyMiJ9.9nVZ7M2sPx1VdltUDAZ2ebi9WGATwWUIZloZSD8SI5s";
    }
}   