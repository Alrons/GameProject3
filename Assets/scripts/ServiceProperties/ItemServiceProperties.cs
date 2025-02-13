using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;

public class ItemServiceProperties
{
    public readonly string baseUrl = "https://localhost:7075/api/";

    public string AddedItems { get; set; }
    public string Items { get; set; }
    public string Tables { get; set; }
    public int UserId { get; set; }
    public string ApiKey { get; set; }

    public ItemServiceProperties()
    {
        AddedItems = "AddedItems/";
        Items = "Items/";
        Tables = "Tables/";
        UserId = 1;
        ApiKey = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJzdSIsImV4cCI6MTc0MTMzMzAwMSwicm9sZXMiOlsiUExBWUVSIiwiTUVOVE9SIiwiR0FNRV9ESVoiLCJNRVRIT0RJU1QiLCJBRE1JTklTVFJBVE9SIl0sInVzZXJJZCI6IjQxZWI1MWI2LTQ5YTktNGZiMi04Nzg1LWNkNzc0ZDA3ZDYyMiJ9.9nVZ7M2sPx1VdltUDAZ2ebi9WGATwWUIZloZSD8SI5s";
    }
}
