# Org.Vitrivr.CineastApi.Api.ObjectApi

All URIs are relative to *http://localhost:4567*

Method | HTTP request | Description
------------- | ------------- | -------------
[**FindObjectsAll**](ObjectApi.md#findobjectsall) | **GET** /api/v1/find/objects/all | Find all objects for a certain type
[**FindObjectsByAttribute**](ObjectApi.md#findobjectsbyattribute) | **GET** /api/v1/find/object/by/{attribute}/{value} | Find object by specified attribute value. I.e by id, name or path
[**FindObjectsByIdBatched**](ObjectApi.md#findobjectsbyidbatched) | **POST** /api/v1/find/object/by/id | Find objects by id



## FindObjectsAll

> MediaObjectQueryResult FindObjectsAll (string type)

Find all objects for a certain type

Find all objects for a certain type

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.Vitrivr.CineastApi.Api;
using Org.Vitrivr.CineastApi.Client;
using Org.Vitrivr.CineastApi.Model;

namespace Example
{
    public class FindObjectsAllExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://localhost:4567";
            var apiInstance = new ObjectApi(Configuration.Default);
            var type = type_example;  // string | The type the objects should have

            try
            {
                // Find all objects for a certain type
                MediaObjectQueryResult result = apiInstance.FindObjectsAll(type);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ObjectApi.FindObjectsAll: " + e.Message );
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
 **type** | **string**| The type the objects should have | 

### Return type

[**MediaObjectQueryResult**](MediaObjectQueryResult.md)

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


## FindObjectsByAttribute

> MediaObjectQueryResult FindObjectsByAttribute (string attribute, string value)

Find object by specified attribute value. I.e by id, name or path

Find object by specified attribute value. I.e by id, name or path

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.Vitrivr.CineastApi.Api;
using Org.Vitrivr.CineastApi.Client;
using Org.Vitrivr.CineastApi.Model;

namespace Example
{
    public class FindObjectsByAttributeExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://localhost:4567";
            var apiInstance = new ObjectApi(Configuration.Default);
            var attribute = attribute_example;  // string | The attribute type of the value. One of: id, name, path
            var value = value_example;  // string | 

            try
            {
                // Find object by specified attribute value. I.e by id, name or path
                MediaObjectQueryResult result = apiInstance.FindObjectsByAttribute(attribute, value);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ObjectApi.FindObjectsByAttribute: " + e.Message );
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
 **attribute** | **string**| The attribute type of the value. One of: id, name, path | 
 **value** | **string**|  | 

### Return type

[**MediaObjectQueryResult**](MediaObjectQueryResult.md)

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


## FindObjectsByIdBatched

> MediaObjectQueryResult FindObjectsByIdBatched (IdList idList = null)

Find objects by id

Find objects by id

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.Vitrivr.CineastApi.Api;
using Org.Vitrivr.CineastApi.Client;
using Org.Vitrivr.CineastApi.Model;

namespace Example
{
    public class FindObjectsByIdBatchedExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://localhost:4567";
            var apiInstance = new ObjectApi(Configuration.Default);
            var idList = new IdList(); // IdList |  (optional) 

            try
            {
                // Find objects by id
                MediaObjectQueryResult result = apiInstance.FindObjectsByIdBatched(idList);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ObjectApi.FindObjectsByIdBatched: " + e.Message );
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

[**MediaObjectQueryResult**](MediaObjectQueryResult.md)

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

