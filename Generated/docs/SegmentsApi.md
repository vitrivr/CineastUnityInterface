# Org.Vitrivr.CineastApi.Api.SegmentsApi

All URIs are relative to *http://localhost:4567*

Method | HTTP request | Description
------------- | ------------- | -------------
[**FindSegmentSimilar**](SegmentsApi.md#findsegmentsimilar) | **POST** /api/v1/find/segments/similar | Find similar segments based on the given query



## FindSegmentSimilar

> SimilarityQueryResultBatch FindSegmentSimilar (SimilarityQuery similarityQuery = null)

Find similar segments based on the given query

Performs a similarity search based on the formulated query

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.Vitrivr.CineastApi.Api;
using Org.Vitrivr.CineastApi.Client;
using Org.Vitrivr.CineastApi.Model;

namespace Example
{
    public class FindSegmentSimilarExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://localhost:4567";
            var apiInstance = new SegmentsApi(Configuration.Default);
            var similarityQuery = new SimilarityQuery(); // SimilarityQuery |  (optional) 

            try
            {
                // Find similar segments based on the given query
                SimilarityQueryResultBatch result = apiInstance.FindSegmentSimilar(similarityQuery);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling SegmentsApi.FindSegmentSimilar: " + e.Message );
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
 **similarityQuery** | [**SimilarityQuery**](SimilarityQuery.md)|  | [optional] 

### Return type

[**SimilarityQueryResultBatch**](SimilarityQueryResultBatch.md)

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

