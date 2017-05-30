using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ExtensionsLibrary.Extensions;
using NetLibrary.Extensions;
using Newtonsoft.Json.Linq;
using ObjectAnalysisProject.Extensions;
using RestExportApp.Models;
using WindowsFormsLibrary.Extensions;

namespace RestExportApp.RestApi {
	public static partial class YahooApi {
		#region メソッド

		#region API

		/// <summary>
		/// 降水量を取得する REST API です。
		/// </summary>
		/// <param name="appId">アプリケーションID</param>
		/// <param name="lat">緯度</param>
		/// <param name="lon">経度</param>
		/// <returns>降水量情報を返します。</returns>
		public static async Task<PrecipitationModel> GetPrecipitationInfoAsync(string appId, double lat, double lon) {
			var url = $@"https://map.yahooapis.jp/weather/V1/place";
			var para = $@"coordinates={lat},{lon}&output=json&appid={appId}";
			var uri = new Uri($@"{url}?{para}");

			return await uri.GetJsonAsync<PrecipitationModel>();
		}

		/// <summary>
		/// 商品検索を取得する REST API です。
		/// </summary>
		/// <param name="appId">アプリケーションID</param>
		/// <param name="category_id">カテゴリーID</param>
		/// <param name="sort">ソート方法を表す文字列</param>
		/// <returns>商品検索情報を返します。</returns>
		public static async Task<JObject> GetItemSearchInfoAsync(string appId, int category_id, string sort) {
			var url = $@"https://shopping.yahooapis.jp/ShoppingWebService/V1/json/itemSearch";
			var para = $@"category_id={category_id}&sort={sort}&appid={appId}";
			var uri = new Uri($@"{url}?{para}");

			return await uri.GetObjectAsync();
		}

		/// <summary>
		/// カテゴリーを取得する REST API です。
		/// </summary>
		/// <param name="appId">アプリケーションID</param>
		/// <param name="category_id">カテゴリーID</param>
		/// <returns>カテゴリー情報を返します。</returns>
		public static async Task<JObject> GetCategorySearchInfoAsync(string appId, int category_id) {
			var url = $@"https://shopping.yahooapis.jp/ShoppingWebService/V1/json/categorySearch";
			var para = $@"category_id={category_id}&appid={appId}";
			var uri = new Uri($@"{url}?{para}");

			return await uri.GetObjectAsync();
		}

		#endregion

		#region DB登録用列挙体取得

		/// <summary>
		/// 降水量を取得します。
		/// </summary>
		/// <param name="appId">アプリケーションID</param>
		/// <param name="lat">緯度</param>
		/// <param name="lon">経度</param>
		/// <returns>降水量情報を返します。</returns>
		public static async Task<IEnumerable<Weathers>> GetWeathersAsync(string appId, double lat, double lon) {
			var json = await GetPrecipitationInfoAsync(appId, lat, lon);

			var weathers = json.Feature.FirstOrDefault()?.Property?.WeatherList?.Weather;
			var items = weathers.Select(w => new Weathers {
				Type = w.Type,
				Date = w.Date,
				Rainfall = w.Rainfall,
			});
			return items;
		}

		/// <summary>
		/// 商品検索を取得します。
		/// </summary>
		/// <param name="appId">アプリケーションID</param>
		/// <param name="category_id">カテゴリーID</param>
		/// <param name="sort">ソート方法を表す文字列</param>
		/// <returns>商品検索情報を返します。</returns>
		public static async Task<IEnumerable<Products>> GetProductsAsync(string appId, int category_id, string sort) {
			var jobj = await GetItemSearchInfoAsync(appId, category_id, sort);

			var result = jobj["ResultSet"]["0"]["Result"];

			var cs = result.Select(c => c.First()).Where(c => c.HasValues).Select(c => new Products {
				Code = c["Code"]?.ToString(),
				Name = c["Name"]?.ToString(),
				Url = c["Url"]?.ToString(),
			}).Where(c => c.Name != null);

			return cs;
		}

		/// <summary>
		/// カテゴリーを取得します。
		/// </summary>
		/// <param name="appId">アプリケーションID</param>
		/// <param name="category_id">カテゴリーID</param>
		/// <returns>カテゴリー情報を返します。</returns>
		public static async Task<IEnumerable<Categories>> GetCategoriesAsync(string appId, int category_id) {
			var jobj = await GetCategorySearchInfoAsync(appId, category_id);

			var result = jobj["ResultSet"]["0"]["Result"];
			var children = result["Categories"]["Children"];

			var cs = children.Select(c => c.First()).Where(c => c.HasValues).Select(c => new Categories {
				CategoryId = Convert.ToInt32(c["Id"]),
				Order = Convert.ToInt32(c["_attributes"]["sortOrder"]),
				Url = c["Url"].ToString(),
				Title = c["Title"]["Long"].ToString(),
			});

			return cs;
		}

		#endregion

		#endregion
	}
}
