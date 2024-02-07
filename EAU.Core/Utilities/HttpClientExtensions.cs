using CNSys.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Utilities
{
    public struct GetWithIfNoneMatchResult<T>
    {
        public GetWithIfNoneMatchResult(T data, DateTime? modifiedDate)
        {
            Data = data;
            ModifiedDate = modifiedDate;
        }

        public readonly T Data;
        public readonly DateTime? ModifiedDate;
    }

    public struct PagedResult<T>
    {
        public PagedResult(T data, int? count)
        {
            Data = data;
            Count = count;
        }

        public readonly T Data;
        public readonly int? Count;
    }

    public static class HttpClientExtensions
    {
        public static MediaTypeFormatterCollection DefaultMediaTypeFormatterCollection { get; private set; }

        static HttpClientExtensions()
        {
            if (DefaultMediaTypeFormatterCollection == null)
            {
                DefaultMediaTypeFormatterCollection = new MediaTypeFormatterCollection();

                /*Използваме XmlSerializer инфраструктурата*/
                DefaultMediaTypeFormatterCollection.XmlFormatter.UseXmlSerializer = true;
                DefaultMediaTypeFormatterCollection.JsonFormatter.SerializerSettings.Converters.Add(new IsoTimeSpanConverter());
            }
        }

        public static async Task<GetWithIfNoneMatchResult<T>> GetWithIfNoneMatchAsync<T>(this HttpClient client, string requestUri, DateTime? ifNoneMatch, CancellationToken cancellationToken)
        {
            string ifNoneMatchStr = ifNoneMatch.HasValue ? ifNoneMatch.Value.FormatForETag() : null;

            using (HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get, new Uri(requestUri, UriKind.RelativeOrAbsolute)))
            {
                msg.Headers.IfNoneMatch.Add(EntityTagHeaderValue.Parse(string.Format("\"{0}\"", ifNoneMatchStr)));

                using (var response = await client.SendAsync(msg, cancellationToken))
                {
                    response.EnsureStatusCodes(HttpStatusCode.OK, HttpStatusCode.NotModified);

                    var data = await response.Content.ReadAsAsync<T>(DefaultMediaTypeFormatterCollection, cancellationToken);

                    string etag = response.Headers.ETag?.Tag;

                    var modifiedDate = string.IsNullOrEmpty(etag) ? (DateTime?)null : DateTime.Parse(etag.Replace("\"", string.Empty));

                    return new GetWithIfNoneMatchResult<T>(data, modifiedDate);
                }
            }
        }

        public static async Task<PagedResult<T>> GetPagedAsync<T>(this HttpClient client, string requestUri, CancellationToken cancellationToken)
        {
            using (var response = await client.GetAsync(requestUri, cancellationToken))
            {
                response.EnsureSuccessStatusCode2();

                var data = await response.Content.ReadAsAsync<T>(DefaultMediaTypeFormatterCollection, cancellationToken);

                int count = 0;
                if (response.Headers.TryGetValues("count", out var values))
                {
                    int.TryParse(values.First(), out count);
                }

                return new PagedResult<T>(data, count);
            }
        }

        public static Task<PagedResult<T>> GetPagedAsync<T>(this HttpClient client, string requestUri, object requestObj, CancellationToken cancellationToken)
        {
            return client.GetPagedAsync<T>(PrepareRequestUri(requestUri, requestObj), cancellationToken);
        }

        public static Task<T> GetAsync<T>(this HttpClient client, string requestUri, object requestObj, CancellationToken cancellationToken)
        {
            return client.GetAsync<T>(PrepareRequestUri(requestUri, requestObj), cancellationToken);
        }

        public static async Task<T> GetAsync<T>(this HttpClient client, string requestUri, CancellationToken cancellationToken)
        {
            using (var result = await client.GetAsync(requestUri, cancellationToken))
            {
                result.EnsureSuccessStatusCode2();

                return await result.Content.ReadAsAsync<T>(DefaultMediaTypeFormatterCollection, cancellationToken);
            }
        }

        public static async Task<T> GetAsXmlAsync<T>(this HttpClient client, string requestUri, CancellationToken cancellationToken)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri(requestUri, UriKind.RelativeOrAbsolute)))
            {
                request.Headers
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/xml"));

                using (var result = await client.SendAsync(request, cancellationToken))
                {
                    result.EnsureSuccessStatusCode2();

                    return await result.Content.ReadAsAsync<T>(DefaultMediaTypeFormatterCollection, cancellationToken);
                }
            }
        }

        public static async Task<Stream> GetStreamAsync(this HttpClient client, string requestUri, CancellationToken cancellationToken)
        {
            bool disposeResult = false;
            HttpResponseMessage result = null;
            /*При наличие на грешка, освобождаваме result - a */
            try
            {
                result = await client.GetAsync(requestUri, cancellationToken);

                result.EnsureSuccessStatusCode2();

                return new DisposingStream(await result.Content.ReadAsStreamAsync(), () =>
                {
                    result.Dispose();
                });
            }
            catch
            {
                disposeResult = true;
                throw;
            }
            finally
            {
                if (disposeResult && result != null)
                    result.Dispose();
            }
        }

        public static async Task<T> DeleteAsync<T>(this HttpClient client, string requestUri, CancellationToken cancellationToken)
        {
            using (var result = await client.DeleteAsync(requestUri, cancellationToken))
            {
                result.EnsureSuccessStatusCode2();

                return await result.Content.ReadAsAsync<T>(DefaultMediaTypeFormatterCollection, cancellationToken);
            }
        }

        public static Task<T> PostAsync<T>(this HttpClient client, string requestUri, object requestBodyObject, CancellationToken cancellationToken)
        {
            return client.PostAsync<T>(requestUri, new ObjectContent(requestBodyObject.GetType(), requestBodyObject, DefaultMediaTypeFormatterCollection.JsonFormatter), cancellationToken);
        }

        public static async Task<T> PostAsync<T>(this HttpClient client, string requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            using (var result = await client.PostAsync(requestUri, content, cancellationToken))
            {
                result.EnsureSuccessStatusCode2();

                return await result.Content.ReadAsAsync<T>(DefaultMediaTypeFormatterCollection, cancellationToken);
            }
        }

        public static Task<HttpResponseMessage> PostAsync(this HttpClient client, string requestUri, object requestBodyObject, List<(string Name, string Value)> headers, CancellationToken cancellationToken)
        {
            var content = new ObjectContent(requestBodyObject.GetType(), requestBodyObject, DefaultMediaTypeFormatterCollection.JsonFormatter);

            if (headers != null)
            {
                foreach (var haeder in headers)
                {
                    content.Headers.Add(haeder.Name, haeder.Value);
                }
            }

            return client.PostAsync(requestUri, content, cancellationToken);
        }

        public static Task<HttpResponseMessage> PostAsync(this HttpClient client, string requestUri, object requestBodyObject, CancellationToken cancellationToken)
        {
            return client.PostAsync(requestUri, new ObjectContent(requestBodyObject.GetType(), requestBodyObject, DefaultMediaTypeFormatterCollection.JsonFormatter), cancellationToken);
        }

        public static Task<T> PutAsync<T>(this HttpClient client, string requestUri, object requestBodyObject, CancellationToken cancellationToken)
        {
            return client.PutAsync<T>(requestUri, new ObjectContent(requestBodyObject.GetType(), requestBodyObject, DefaultMediaTypeFormatterCollection.JsonFormatter), cancellationToken);
        }

        public static async Task<T> PutAsync<T>(this HttpClient client, string requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            using (var result = await client.PutAsync(requestUri, content, cancellationToken))
            {
                result.EnsureSuccessStatusCode2();

                return await result.Content.ReadAsAsync<T>(DefaultMediaTypeFormatterCollection, cancellationToken);
            }
        }

        public static Task<HttpResponseMessage> PutAsync(this HttpClient client, string requestUri, object requestBodyObject, CancellationToken cancellationToken)
        {
            return client.PutAsync(requestUri, new ObjectContent(requestBodyObject.GetType(), requestBodyObject, DefaultMediaTypeFormatterCollection.JsonFormatter), cancellationToken);
        }

        #region Helpers

        private static string PrepareRequestUri(string requestUri, object requestObj)
        {
            if (requestObj != null)
            {
                var objQuary = GetObjectAsQuarryString(requestObj);

                if (!string.IsNullOrEmpty(objQuary))
                {
                    if (requestUri.Contains("?"))
                    {
                        requestUri += "&" + objQuary;
                    }
                    else
                    {
                        requestUri += "?" + objQuary;
                    }
                }
            }

            return requestUri;
        }

        private static string GetObjectAsQuarryString(object obj, string parentPropName = null)
        {
            var quaryString = "";

            if (obj != null)
            {
                foreach (var prop in obj.GetType().GetProperties())
                {
                    var propValue = prop.GetValue(obj);

                    if (propValue != null)
                    {
                        var propName = string.IsNullOrEmpty(parentPropName) ? prop.Name : parentPropName + "." + prop.Name;

                        if (propValue is DateTime date)
                        {
                            quaryString += string.Format("&{0}={1}", propName, date.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"));
                        }
                        else if (propValue is string propStr)
                        {
                            if (!string.IsNullOrWhiteSpace(propStr))
                            {
                                quaryString += string.Format("&{0}={1}", propName, propStr);
                            }
                        }
                        else if (propValue is IEnumerable)
                        {
                            var i = 0;

                            foreach (var propElemValue in (IEnumerable)propValue)
                            {
                                quaryString += string.Format("&{0}[{1}]={2}", propName, i, propElemValue);
                                i++;
                            }
                        }
                        else if (propValue.GetType().IsClass)
                        {
                            quaryString += GetObjectAsQuarryString(propValue, propName);
                        }
                        else
                        {
                            quaryString += string.Format("&{0}={1}", propName, propValue);
                        }
                    }
                }

                if (quaryString.Length > 0)
                {
                    quaryString = quaryString.Substring(1);
                }
            }

            return quaryString;
        }

        #endregion
    }

    public static class HttpResponseMessageExtensions
    {
        public static void EnsureStatusCodes(this HttpResponseMessage message, params System.Net.HttpStatusCode[] statusCodes)
        {
            var statusCode = message.StatusCode;

            /*Ако поне един код е сред позволените, то излизаме*/
            if (statusCodes.Any((item) => { return item == statusCode; }))
                return;

            throw new HttpRequestException(string.Format("Статус кода {0} в отговор на заявка на адрес {1} не е измежду очакваните кодове: {2}.",
                statusCode,
                message.RequestMessage.RequestUri,
                string.Join(",", statusCodes)
                ));
        }

        public static void EnsureSuccessStatusCode2(this HttpResponseMessage message)
        {
            if (message.IsSuccessStatusCode)
                return;

            throw new HttpRequestException(string.Format("Статус кода {0} в отговор на заявка на адрес {1} не е измежду очакваните кодове: {2}.",
                message.StatusCode,
                message.RequestMessage.RequestUri,
                "[200-299]"
                ));
        }
    }

}
