# Org.Vitrivr.CineastApi.Api.StatusApi

All URIs are relative to *http://localhost:4567*

Method | HTTP request | Description
------------- | ------------- | -------------
[**Status**](StatusApi.md#status) | **GET** /api/v1/status | Get the status of the server



## Status

> Ping Status ()

Get the status of the server

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.Vitrivr.CineastApi.Api;
using Org.Vitrivr.CineastApi.Client;
using Org.Vitrivr.CineastApi.Model;

namespace Example
{
    public class StatusExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://localhost:4567";
            var apiInstance = new StatusApi(Configuration.Default);

            try
            {
                // Get the status of the server
                Ping result = apiInstance.Status();
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling StatusApi.Status: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

This endpoint does not need any parameter.

### Return type

[**Ping**](Ping.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)

