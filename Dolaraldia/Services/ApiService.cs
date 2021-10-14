using Dolaraldia.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Dolaraldia.Services
{
    public class ApiService
    {
		public async Task<bool> CheckConnectionAsync(string url)
		{
			if (!CrossConnectivity.Current.IsConnected)
			{
				return false;
			}
			return await CrossConnectivity.Current.IsRemoteReachable(url);
		}
		public async Task<Response> GetData<T>(
			string urlBase)
		{
			try
			{
				var model = new VarModel
				{
					AccessKeyConfirm = App.Current.Resources["AccessApiKey"].ToString()
				};
				var request = JsonConvert.SerializeObject(model);
				var content = new StringContent(
					request, Encoding.UTF8,
					"application/json");
				var client = new HttpClient();
				client.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue(App.Current.Resources["AccessServer"].ToString(), App.Current.Resources["AccessKeyServer"].ToString());
				client.BaseAddress = new Uri(urlBase);
				var url = string.Format(
					"?function={0}&access_key={1}", "Home", App.Current.Resources["AccessApiKey"].ToString());
				var response = await client.PostAsync(url, content);

				if (!response.IsSuccessStatusCode)
				{
					return new Response
					{
						IsSuccess = false,
						Message = response.StatusCode.ToString(),
					};
				}

				var result = await response.Content.ReadAsStringAsync();
				var list = JsonConvert.DeserializeObject<T>(result);
				return new Response
				{
					IsSuccess = true,
					Message = "Ok",
					Result = list,
				};
			}
			catch (Exception ex)
			{
				return new Response
				{
					IsSuccess = false,
					Message = ex.Message,
				};
			}
		}
	}
}
