# Org.Vitrivr.CineastApi.Api.MetadataApi

All URIs are relative to *http://localhost:4567*

Method | HTTP request | Description
------------- | ------------- | -------------
[**FindMetaById**](MetadataApi.md#findmetabyid) | **GET** /api/v1/find/metadata/by/id/{id} | Find metadata for the given object id
[**FindMetaFullyQualified**](MetadataApi.md#findmetafullyqualified) | **GET** /api/v1/find/metadata/of/{id}/in/{domain}/with/{key} | Find metadata for specific object id in given domain with given key
[**FindMetadataByDomain**](MetadataApi.md#findmetadatabydomain) | **GET** /api/v1/find/metadata/in/{domain}/by/id/{domain} | Find metadata for specific object id in given domain
[**FindMetadataByDomainBatched**](MetadataApi.md#findmetadatabydomainbatched) | **POST** /api/v1/find/metadata/in/{domain} | Find metadata in the specified domain for all the given ids
[**FindMetadataByKey**](MetadataApi.md#findmetadatabykey) | **GET** /api/v1/find/metadata/with/{key}/by/id/{id} | Find metadata for a given object id with specified key
[**FindMetadataByKeyBatched**](MetadataApi.md#findmetadatabykeybatched) | **POST** /api/v1/find/metadata/with/{key} | Find metadata for a given object id with specified key
[**FindMetadataForObjectIdBatched**](MetadataApi.md#findmetadataforobjectidbatched) | **POST** /api/v1/find/metadata/by/id | Finds metadata for the given list of object ids



## FindMetaById

> MediaObjectMetadataQueryResult FindMetaById (string id)

Find metadata for the given object id

Find metadata by the given object id

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.Vitrivr.CineastApi.Api;
using Org.Vitrivr.CineastApi.Client;
using Org.Vitrivr.CineastApi.Model;

namespace Example
{
    public class FindMetaByIdExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://localhost:4567";
            var apiInstance = new MetadataApi(Configuration.Default);
            var id = id_example;  // string | The object id to find metadata of

            try
            {
                // Find metadata for the given object id
                MediaObjectMetadataQueryResult result = apiInstance.FindMetaById(id);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling MetadataApi.FindMetaById: " + e.Message );
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
 **id** | **string**| The object id to find metadata of | 

### Return type

[**MediaObjectMetadataQueryResult**](MediaObjectMetadataQueryResult.md)

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


## FindMetaFullyQualified

> MediaObjectMetadataQueryResult FindMetaFullyQualified (string id, string domain, string key)

Find metadata for specific object id in given domain with given key

The description

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.Vitrivr.CineastApi.Api;
using Org.Vitrivr.CineastApi.Client;
using Org.Vitrivr.CineastApi.Model;

namespace Example
{
    public class FindMetaFullyQualifiedExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://localhost:4567";
            var apiInstance = new MetadataApi(Configuration.Default);
            var id = id_example;  // string | The object id
            var domain = domain_example;  // string | The domain name
            var key = key_example;  // string | Metadata key

            try
            {
                // Find metadata for specific object id in given domain with given key
                MediaObjectMetadataQueryResult result = apiInstance.FindMetaFullyQualified(id, domain, key);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling MetadataApi.FindMetaFullyQualified: " + e.Message );
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
 **id** | **string**| The object id | 
 **domain** | **string**| The domain name | 
 **key** | **string**| Metadata key | 

### Return type

[**MediaObjectMetadataQueryResult**](MediaObjectMetadataQueryResult.md)

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


## FindMetadataByDomain

> MediaObjectMetadataQueryResult FindMetadataByDomain (string domain, string id)

Find metadata for specific object id in given domain

Find metadata for specific object id in given domain

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.Vitrivr.CineastApi.Api;
using Org.Vitrivr.CineastApi.Client;
using Org.Vitrivr.CineastApi.Model;

namespace Example
{
    public class FindMetadataByDomainExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://localhost:4567";
            var apiInstance = new MetadataApi(Configuration.Default);
            var domain = domain_example;  // string | The domain of the metadata to find
            var id = id_example;  // string | The object id of the multimedia object to find metadata for

            try
            {
                // Find metadata for specific object id in given domain
                MediaObjectMetadataQueryResult result = apiInstance.FindMetadataByDomain(domain, id);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling MetadataApi.FindMetadataByDomain: " + e.Message );
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
 **domain** | **string**| The domain of the metadata to find | 
 **id** | **string**| The object id of the multimedia object to find metadata for | 

### Return type

[**MediaObjectMetadataQueryResult**](MediaObjectMetadataQueryResult.md)

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


## FindMetadataByDomainBatched

> MediaObjectMetadataQueryResult FindMetadataByDomainBatched (string domain, IdList idList = null)

Find metadata in the specified domain for all the given ids

Find metadata in the specified domain for all the given ids

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.Vitrivr.CineastApi.Api;
using Org.Vitrivr.CineastApi.Client;
using Org.Vitrivr.CineastApi.Model;

namespace Example
{
    public class FindMetadataByDomainBatchedExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://localhost:4567";
            var apiInstance = new MetadataApi(Configuration.Default);
            var domain = domain_example;  // string | The domain of the metadata to find
            var idList = new IdList(); // IdList |  (optional) 

            try
            {
                // Find metadata in the specified domain for all the given ids
                MediaObjectMetadataQueryResult result = apiInstance.FindMetadataByDomainBatched(domain, idList);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling MetadataApi.FindMetadataByDomainBatched: " + e.Message );
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
 **domain** | **string**| The domain of the metadata to find | 
 **idList** | [**IdList**](IdList.md)|  | [optional] 

### Return type

[**MediaObjectMetadataQueryResult**](MediaObjectMetadataQueryResult.md)

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


## FindMetadataByKey

> MediaObjectMetadataQueryResult FindMetadataByKey (string key, string id)

Find metadata for a given object id with specified key

Find metadata for a given object id with specified key

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.Vitrivr.CineastApi.Api;
using Org.Vitrivr.CineastApi.Client;
using Org.Vitrivr.CineastApi.Model;

namespace Example
{
    public class FindMetadataByKeyExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://localhost:4567";
            var apiInstance = new MetadataApi(Configuration.Default);
            var key = key_example;  // string | The key of the metadata to find
            var id = id_example;  // string | The object id of for which the metadata should be find

            try
            {
                // Find metadata for a given object id with specified key
                MediaObjectMetadataQueryResult result = apiInstance.FindMetadataByKey(key, id);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling MetadataApi.FindMetadataByKey: " + e.Message );
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
 **key** | **string**| The key of the metadata to find | 
 **id** | **string**| The object id of for which the metadata should be find | 

### Return type

[**MediaObjectMetadataQueryResult**](MediaObjectMetadataQueryResult.md)

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


## FindMetadataByKeyBatched

> MediaObjectMetadataQueryResult FindMetadataByKeyBatched (string key, IdList idList = null)

Find metadata for a given object id with specified key

Find metadata with a the speicifed key from the path across all domains and for the provided ids

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.Vitrivr.CineastApi.Api;
using Org.Vitrivr.CineastApi.Client;
using Org.Vitrivr.CineastApi.Model;

namespace Example
{
    public class FindMetadataByKeyBatchedExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://localhost:4567";
            var apiInstance = new MetadataApi(Configuration.Default);
            var key = key_example;  // string | The key of the metadata to find
            var idList = new IdList(); // IdList |  (optional) 

            try
            {
                // Find metadata for a given object id with specified key
                MediaObjectMetadataQueryResult result = apiInstance.FindMetadataByKeyBatched(key, idList);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling MetadataApi.FindMetadataByKeyBatched: " + e.Message );
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
 **key** | **string**| The key of the metadata to find | 
 **idList** | [**IdList**](IdList.md)|  | [optional] 

### Return type

[**MediaObjectMetadataQueryResult**](MediaObjectMetadataQueryResult.md)

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


## FindMetadataForObjectIdBatched

> MediaObjectMetadataQueryResult FindMetadataForObjectIdBatched (OptionallyFilteredIdList optionallyFilteredIdList = null)

Finds metadata for the given list of object ids

Finds metadata for the given list of object ids

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.Vitrivr.CineastApi.Api;
using Org.Vitrivr.CineastApi.Client;
using Org.Vitrivr.CineastApi.Model;

namespace Example
{
    public class FindMetadataForObjectIdBatchedExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://localhost:4567";
            var apiInstance = new MetadataApi(Configuration.Default);
            var optionallyFilteredIdList = new OptionallyFilteredIdList(); // OptionallyFilteredIdList |  (optional) 

            try
            {
                // Finds metadata for the given list of object ids
                MediaObjectMetadataQueryResult result = apiInstance.FindMetadataForObjectIdBatched(optionallyFilteredIdList);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling MetadataApi.FindMetadataForObjectIdBatched: " + e.Message );
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
 **optionallyFilteredIdList** | [**OptionallyFilteredIdList**](OptionallyFilteredIdList.md)|  | [optional] 

### Return type

[**MediaObjectMetadataQueryResult**](MediaObjectMetadataQueryResult.md)

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

