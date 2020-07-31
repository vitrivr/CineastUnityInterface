# Org.Vitrivr.CineastApi.Api.DefaultApi

All URIs are relative to *http://10.34.58.145:1900*

Method | HTTP request | Description
------------- | ------------- | -------------
[**GetObjectsWithId**](DefaultApi.md#getobjectswithid) | **GET** /objects/{id} | Get objects with id
[**GetThumbnailsWithId**](DefaultApi.md#getthumbnailswithid) | **GET** /thumbnails/{id} | Get thumbnails with id



## GetObjectsWithId

> void GetObjectsWithId (string id)

Get objects with id

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.Vitrivr.CineastApi.Api;
using Org.Vitrivr.CineastApi.Client;
using Org.Vitrivr.CineastApi.Model;

namespace Example
{
    public class GetObjectsWithIdExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://10.34.58.145:1900";
            var apiInstance = new DefaultApi(Configuration.Default);
            var id = id_example;  // string | 

            try
            {
                // Get objects with id
                apiInstance.GetObjectsWithId(id);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.GetObjectsWithId: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **string**|  | 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Default response |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetThumbnailsWithId

> void GetThumbnailsWithId (string id)

Get thumbnails with id

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.Vitrivr.CineastApi.Api;
using Org.Vitrivr.CineastApi.Client;
using Org.Vitrivr.CineastApi.Model;

namespace Example
{
    public class GetThumbnailsWithIdExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://10.34.58.145:1900";
            var apiInstance = new DefaultApi(Configuration.Default);
            var id = id_example;  // string | 

            try
            {
                // Get thumbnails with id
                apiInstance.GetThumbnailsWithId(id);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.GetThumbnailsWithId: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **string**|  | 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Default response |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)

