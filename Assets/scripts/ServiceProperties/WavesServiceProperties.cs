using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


public class WavesServiceProperties
{
    public readonly string baseUrl = "https://localhost:7075/api/";
    public string Wave { get; set; }
    public string StartWavePos { get; set; }
    public string ApiKey { get; set; }

    public WavesServiceProperties()
    {
        Wave = "Wave/";
        StartWavePos = "WavePosition/";
        ApiKey = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJzdSIsImV4cCI6MTc0MTMzMzAwMSwicm9sZXMiOlsiUExBWUVSIiwiTUVOVE9SIiwiR0FNRV9ESVoiLCJNRVRIT0RJU1QiLCJBRE1JTklTVFJBVE9SIl0sInVzZXJJZCI6IjQxZWI1MWI2LTQ5YTktNGZiMi04Nzg1LWNkNzc0ZDA3ZDYyMiJ9.9nVZ7M2sPx1VdltUDAZ2ebi9WGATwWUIZloZSD8SI5s";
    }
}
