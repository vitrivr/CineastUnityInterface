# Org.Vitrivr.CineastApi.Api.MiscApi

All URIs are relative to *http://localhost:4567*

Method | HTTP request | Description
------------- | ------------- | -------------
[**FindDistinctElementsByColumn**](MiscApi.md#finddistinctelementsbycolumn) | **POST** /api/v1/find/boolean/column/distinct | Find all distinct elements of a given column



## FindDistinctElementsByColumn

> DistinctElementsResult FindDistinctElementsByColumn (ColumnSpecification columnSpecification = null)

Find all distinct elements of a given column

Find all distinct elements of a given column. Please note that this operation does cache results.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.Vitrivr.CineastApi.Api;
using Org.Vitrivr.CineastApi.Client;
using Org.Vitrivr.CineastApi.Model;

namespace Example
{
    public class FindDistinctElementsByColumnExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://localhost:4567";
            var apiInstance = new MiscApi(Configuration.Default);
            var columnSpecification = new ColumnSpecification(); // ColumnSpecification |  (optional) 

            try
            {
                // Find all distinct elements of a given column
                DistinctElementsResult result = apiInstance.FindDistinctElementsByColumn(columnSpecification);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling MiscApi.FindDistinctElementsByColumn: " + e.Message );
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
 **columnSpecification** | [**ColumnSpecification**](ColumnSpecification.md)|  | [optional] 

### Return type

[**DistinctElementsResult**](DistinctElementsResult.md)

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

