# Org.Vitrivr.CineastApi.Api.SessionApi

All URIs are relative to *http://localhost:4567*

Method | HTTP request | Description
------------- | ------------- | -------------
[**EndExtraction**](SessionApi.md#endextraction) | **POST** /api/v1/session/extract/end | End the active extraction session
[**EndSession**](SessionApi.md#endsession) | **GET** /api/v1/session/end/{id} | End the session for given id
[**ExtractItem**](SessionApi.md#extractitem) | **POST** /api/v1/session/extract/new | Extract new item
[**StartExtraction**](SessionApi.md#startextraction) | **POST** /api/v1/session/extract/start | Start extraction session
[**StartSession**](SessionApi.md#startsession) | **POST** /api/v1/session/start | Start new session for given credentials
[**ValidateSession**](SessionApi.md#validatesession) | **GET** /api/v1/session/validate/{id} | Validates the session with given id



## EndExtraction

> SessionState EndExtraction ()

End the active extraction session

CAUTION. Untested

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.Vitrivr.CineastApi.Api;
using Org.Vitrivr.CineastApi.Client;
using Org.Vitrivr.CineastApi.Model;

namespace Example
{
    public class EndExtractionExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://localhost:4567";
            var apiInstance = new SessionApi(Configuration.Default);

            try
            {
                // End the active extraction session
                SessionState result = apiInstance.EndExtraction();
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling SessionApi.EndExtraction: " + e.Message );
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

[**SessionState**](SessionState.md)

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


## EndSession

> SessionState EndSession (string id)

End the session for given id

Ends the session for the given id

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.Vitrivr.CineastApi.Api;
using Org.Vitrivr.CineastApi.Client;
using Org.Vitrivr.CineastApi.Model;

namespace Example
{
    public class EndSessionExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://localhost:4567";
            var apiInstance = new SessionApi(Configuration.Default);
            var id = id_example;  // string | The id of the session to end

            try
            {
                // End the session for given id
                SessionState result = apiInstance.EndSession(id);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling SessionApi.EndSession: " + e.Message );
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
 **id** | **string**| The id of the session to end | 

### Return type

[**SessionState**](SessionState.md)

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


## ExtractItem

> SessionState ExtractItem (ExtractionContainerMessage extractionContainerMessage = null)

Extract new item

TODO

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.Vitrivr.CineastApi.Api;
using Org.Vitrivr.CineastApi.Client;
using Org.Vitrivr.CineastApi.Model;

namespace Example
{
    public class ExtractItemExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://localhost:4567";
            var apiInstance = new SessionApi(Configuration.Default);
            var extractionContainerMessage = new ExtractionContainerMessage(); // ExtractionContainerMessage |  (optional) 

            try
            {
                // Extract new item
                SessionState result = apiInstance.ExtractItem(extractionContainerMessage);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling SessionApi.ExtractItem: " + e.Message );
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
 **extractionContainerMessage** | [**ExtractionContainerMessage**](ExtractionContainerMessage.md)|  | [optional] 

### Return type

[**SessionState**](SessionState.md)

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


## StartExtraction

> SessionState StartExtraction ()

Start extraction session

Changes the session's state to extraction

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.Vitrivr.CineastApi.Api;
using Org.Vitrivr.CineastApi.Client;
using Org.Vitrivr.CineastApi.Model;

namespace Example
{
    public class StartExtractionExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://localhost:4567";
            var apiInstance = new SessionApi(Configuration.Default);

            try
            {
                // Start extraction session
                SessionState result = apiInstance.StartExtraction();
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling SessionApi.StartExtraction: " + e.Message );
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

[**SessionState**](SessionState.md)

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


## StartSession

> SessionState StartSession (StartSessionMessage startSessionMessage = null)

Start new session for given credentials

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.Vitrivr.CineastApi.Api;
using Org.Vitrivr.CineastApi.Client;
using Org.Vitrivr.CineastApi.Model;

namespace Example
{
    public class StartSessionExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://localhost:4567";
            var apiInstance = new SessionApi(Configuration.Default);
            var startSessionMessage = new StartSessionMessage(); // StartSessionMessage |  (optional) 

            try
            {
                // Start new session for given credentials
                SessionState result = apiInstance.StartSession(startSessionMessage);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling SessionApi.StartSession: " + e.Message );
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
 **startSessionMessage** | [**StartSessionMessage**](StartSessionMessage.md)|  | [optional] 

### Return type

[**SessionState**](SessionState.md)

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


## ValidateSession

> SessionState ValidateSession (string id)

Validates the session with given id

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.Vitrivr.CineastApi.Api;
using Org.Vitrivr.CineastApi.Client;
using Org.Vitrivr.CineastApi.Model;

namespace Example
{
    public class ValidateSessionExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://localhost:4567";
            var apiInstance = new SessionApi(Configuration.Default);
            var id = id_example;  // string | The id to validate the session of

            try
            {
                // Validates the session with given id
                SessionState result = apiInstance.ValidateSession(id);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling SessionApi.ValidateSession: " + e.Message );
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
 **id** | **string**| The id to validate the session of | 

### Return type

[**SessionState**](SessionState.md)

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

