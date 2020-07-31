# Org.Vitrivr.CineastApi.Api.SegmentApi

All URIs are relative to *http://10.34.58.145:1900*

Method | HTTP request | Description
------------- | ------------- | -------------
[**FindSegmentById**](SegmentApi.md#findsegmentbyid) | **GET** /api/v1/find/segments/by/id/{id} | Finds segments for specified id
[**FindSegmentByIdBatched**](SegmentApi.md#findsegmentbyidbatched) | **POST** /api/v1/find/segments/by/id | Finds segments for specified ids
[**FindSegmentByObjectId**](SegmentApi.md#findsegmentbyobjectid) | **GET** /api/v1/find/segments/all/object/{id} | Find segments by their media object&#39;s id



## FindSegmentById

> MediaSegmentQueryResult FindSegmentById (string id)

Finds segments for specified id

Finds segments for specified id

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.Vitrivr.CineastApi.Api;
using Org.Vitrivr.CineastApi.Client;
using Org.Vitrivr.CineastApi.Model;

namespace Example
{
    public class FindSegmentByIdExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://10.34.58.145:1900";
            var apiInstance = new SegmentApi(Configuration.Default);
            var id = id_example;  // string | The id of the segments

            try
            {
                // Finds segments for specified id
                MediaSegmentQueryResult result = apiInstance.FindSegmentById(id);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling SegmentApi.FindSegmentById: " + e.Message );
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
 **id** | **string**| The id of the segments | 

### Return type

[**MediaSegmentQueryResult**](MediaSegmentQueryResult.md)

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


## FindSegmentByIdBatched

> MediaSegmentQueryResult FindSegmentByIdBatched (IdList idList = null)

Finds segments for specified ids

Finds segments for specified ids

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.Vitrivr.CineastApi.Api;
using Org.Vitrivr.CineastApi.Client;
using Org.Vitrivr.CineastApi.Model;

namespace Example
{
    public class FindSegmentByIdBatchedExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://10.34.58.145:1900";
            var apiInstance = new SegmentApi(Configuration.Default);
            var idList = new IdList(); // IdList |  (optional) 

            try
            {
                // Finds segments for specified ids
                MediaSegmentQueryResult result = apiInstance.FindSegmentByIdBatched(idList);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling SegmentApi.FindSegmentByIdBatched: " + e.Message );
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
 **idList** | [**IdList**](IdList.md)|  | [optional] 

### Return type

[**MediaSegmentQueryResult**](MediaSegmentQueryResult.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: application/json
- **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## FindSegmentByObjectId

> MediaSegmentQueryResult FindSegmentByObjectId (string id)

Find segments by their media object's id

Find segments by their media object's id

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.Vitrivr.CineastApi.Api;
using Org.Vitrivr.CineastApi.Client;
using Org.Vitrivr.CineastApi.Model;

namespace Example
{
    public class FindSegmentByObjectIdExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://10.34.58.145:1900";
            var apiInstance = new SegmentApi(Configuration.Default);
            var id = id_example;  // string | The id of the media object to find segments fo

            try
            {
                // Find segments by their media object's id
                MediaSegmentQueryResult result = apiInstance.FindSegmentByObjectId(id);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling SegmentApi.FindSegmentByObjectId: " + e.Message );
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
 **id** | **string**| The id of the media object to find segments fo | 

### Return type

[**MediaSegmentQueryResult**](MediaSegmentQueryResult.md)

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

