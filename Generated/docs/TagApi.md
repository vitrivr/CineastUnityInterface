# Org.Vitrivr.CineastApi.Api.TagApi

All URIs are relative to *http://10.34.58.145:1900*

Method | HTTP request | Description
------------- | ------------- | -------------
[**FindAllTags**](TagApi.md#findalltags) | **GET** /api/v1/find/tags/all | Find all tags
[**FindTagsBy**](TagApi.md#findtagsby) | **GET** /api/v1/find/tags/by/{attribute}/{value} | Find all tags specified by attribute value
[**FindTagsById**](TagApi.md#findtagsbyid) | **POST** /api/v1/tags/by/id | Find all tags by ids



## FindAllTags

> TagsQueryResult FindAllTags ()

Find all tags

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.Vitrivr.CineastApi.Api;
using Org.Vitrivr.CineastApi.Client;
using Org.Vitrivr.CineastApi.Model;

namespace Example
{
    public class FindAllTagsExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://10.34.58.145:1900";
            var apiInstance = new TagApi(Configuration.Default);

            try
            {
                // Find all tags
                TagsQueryResult result = apiInstance.FindAllTags();
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling TagApi.FindAllTags: " + e.Message );
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

[**TagsQueryResult**](TagsQueryResult.md)

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


## FindTagsBy

> TagsQueryResult FindTagsBy (string attribute, string value)

Find all tags specified by attribute value

Find all tags by attributes id, name or matchingname and filter value

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.Vitrivr.CineastApi.Api;
using Org.Vitrivr.CineastApi.Client;
using Org.Vitrivr.CineastApi.Model;

namespace Example
{
    public class FindTagsByExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://10.34.58.145:1900";
            var apiInstance = new TagApi(Configuration.Default);
            var attribute = attribute_example;  // string | The attribute to filter on. One of: id, name, matchingname
            var value = value_example;  // string | The value of the attribute to filter

            try
            {
                // Find all tags specified by attribute value
                TagsQueryResult result = apiInstance.FindTagsBy(attribute, value);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling TagApi.FindTagsBy: " + e.Message );
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
 **attribute** | **string**| The attribute to filter on. One of: id, name, matchingname | 
 **value** | **string**| The value of the attribute to filter | 

### Return type

[**TagsQueryResult**](TagsQueryResult.md)

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


## FindTagsById

> TagsQueryResult FindTagsById (IdList idList = null)

Find all tags by ids

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.Vitrivr.CineastApi.Api;
using Org.Vitrivr.CineastApi.Client;
using Org.Vitrivr.CineastApi.Model;

namespace Example
{
    public class FindTagsByIdExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://10.34.58.145:1900";
            var apiInstance = new TagApi(Configuration.Default);
            var idList = new IdList(); // IdList |  (optional) 

            try
            {
                // Find all tags by ids
                TagsQueryResult result = apiInstance.FindTagsById(idList);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling TagApi.FindTagsById: " + e.Message );
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

[**TagsQueryResult**](TagsQueryResult.md)

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

