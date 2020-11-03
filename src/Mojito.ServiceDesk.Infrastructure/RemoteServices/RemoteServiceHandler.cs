using Mojito.ServiceDesk.Application.Common.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mojito.ServiceDesk.Infrastructure.RemoteServices
{
    public abstract class RemoteServiceHandler
    {
        protected async Task<T> EnterServiceGetRequest<T>(string url, object parameters, string serviceName = "خارجی", X509Certificate2Collection certificate2Collection = null)
        {
            return await Task.Run(() =>
            {
                string result = string.Empty;

                var param = parameters == null ? "" : toQueryString(parameters);

                Uri reqUri = new Uri(url, UriKind.Absolute);

                var request = (HttpWebRequest)WebRequest.Create(reqUri + param);

                if (certificate2Collection != null)
                    request.ClientCertificates = certificate2Collection;

                request.Timeout = 20000;
                request.Method = WebRequestMethods.Http.Get;

                request.Headers.Add("ServiceSecret", @";b@U{Hh#3;|[(%22-p?YF%<}2Ru(KHIZ#l,SwOAj*x3HKUnlm[s^7b,8}p:oo+l");

                try
                {
                    var httpResponse = (HttpWebResponse)request.GetResponse();

                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        result = streamReader.ReadToEnd();
                    }

                }
                catch (CustomException)
                {
                    throw;
                }
                catch (WebException e)
                {
                    throw new Exception("خطا در سرویس " + serviceName + " با کد " + e.Status);
                }
                catch (Exception ex)
                {
                    throw new Exception("خطا در سرویس " + serviceName);
                }
                finally
                {
                    request.Abort();
                }

                if (typeof(T) == typeof(string))
                {
                    //return result;
                }
                return JsonConvert.DeserializeObject<T>(result);
            });
        }


        protected async Task<string> EnterServiceGetRequest(string url, object parameters, string serviceName = "خارجی", X509Certificate2Collection certificate2Collection = null)
        {
            return await Task.Run(() =>
            {
                string result = string.Empty;

                var param = parameters == null ? "" : toQueryString(parameters);

                Uri reqUri = new Uri(url, UriKind.Absolute);

                var request = (HttpWebRequest)WebRequest.Create(reqUri + param);

                if (certificate2Collection != null)
                    request.ClientCertificates = certificate2Collection;

                request.Timeout = 20000;
                request.Method = WebRequestMethods.Http.Get;

                request.Headers.Add("ServiceSecret", @";b@U{Hh#3;|[(%22-p?YF%<}2Ru(KHIZ#l,SwOAj*x3HKUnlm[s^7b,8}p:oo+l");

                try
                {
                    var httpResponse = (HttpWebResponse)request.GetResponse();

                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        result = streamReader.ReadToEnd();
                    }

                }
                catch (CustomException)
                {
                    throw;
                }
                catch (WebException e)
                {
                    throw new Exception("خطا در سرویس " + serviceName + " با کد " + e.Status);
                }
                catch (Exception ex)
                {
                    throw new Exception("خطا در سرویس " + serviceName);
                }
                finally
                {
                    request.Abort();
                }

                return result;

            });
        }


        private string toQueryString(object obj)
        {
            var jObj = JObject.FromObject(obj);
            var query = "?" + String.Join("&",
                jObj.Children().Cast<JProperty>()
                .Select(jp => jp.Name + "=" + HttpUtility.UrlEncode(jp.Value.ToString())));
            return query;
        }


        private string toJSON(object obj)
        {
            string result = JsonConvert.SerializeObject(obj);
            return result;
        }


        private string SendPostRequest(string url, string jData, string serviceName, int timeout, string method = WebRequestMethods.Http.Post)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json;charset=utf-8";
            httpWebRequest.Method = method;
            httpWebRequest.Timeout = timeout;

            // Added in order to ignore invalid SSL certificate
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            string result = string.Empty;
            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(jData);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                        result = streamReader.ReadToEnd();
                }
            }
            catch (WebException e)
            {
                using (var streamReader = new StreamReader(((HttpWebResponse)e.Response).GetResponseStream()))
                {
                    var excRes = streamReader.ReadToEnd();

                    if (excRes.Contains("duplicate key value violates unique constraint"))
                    {
                        throw new CustomException("این نام کاربری در دسترس نیست.", (int)HttpStatusCode.Conflict);
                    }
                }

                throw new Exception("خطا در سرویس " + serviceName);
            }
            catch (Exception ex)
            {
                throw new Exception("خطا در سرویس " + serviceName);
            }
            finally
            {
                httpWebRequest.Abort();
            }


            return result;
        }


        private string SendDeleteRequest(string url, string serviceName, int timeout)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json;charset=utf-8";
            httpWebRequest.Method = "DELETE";
            httpWebRequest.Timeout = timeout;

            // Added in order to ignore invalid SSL certificate
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            string result = string.Empty;
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                        result = streamReader.ReadToEnd();
                }
            }
            catch (WebException e)
            {
                throw new Exception("خطا در سرویس " + serviceName);
            }
            catch (Exception ex)
            {
                throw new Exception("خطا در سرویس " + serviceName);
            }
            finally
            {
                httpWebRequest.Abort();
            }


            return result;
        }


        protected string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }
}
