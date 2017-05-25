using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ExtensionsLibrary.Extensions;
using NetLibrary.Extensions;
using ObjectAnalysisProject.Extensions;
using RestExportApp.Model;
using WindowsFormsLibrary.Extensions;

namespace RestExportApp.RestApi {
	public static partial class YahooApi {
		#region メソッド

		/// <summary>
		/// 降水量を取得する REST API です。
		/// </summary>
		/// <param name="appId">アプリケーションID</param>
		/// <param name="lat">緯度</param>
		/// <param name="lon">経度</param>
		/// <returns>降水量情報のテーブルを返します。</returns>
		public static async Task<DataTable> GetPrecipitationAsync(string appId, double lat, double lon) {
			var url = $@"https://map.yahooapis.jp/weather/V1/place";
			var para = $@"coordinates={lat},{lon}&output=json&appid={appId}";
			var uri = new Uri($@"{url}?{para}");

			var json = await uri.GetJsonAsync<PrecipitationModel>();
			var weathers = json.Feature.FirstOrDefault()?.Property?.WeatherList?.Weather;
			var tbl = weathers.ToDataTable();
			return tbl;
		}

		/// <summary>
		/// 商品検索を取得する REST API です。
		/// </summary>
		/// <param name="appId">アプリケーションID</param>
		/// <param name="category_id">カテゴリーID</param>
		/// <param name="sort">ソート方法を表す文字列</param>
		/// <returns>商品検索情報のテーブルを返します。</returns>
		public static async Task<DataTable> GetItemSearchAsync(string appId, int category_id, string sort) {
			var url = $@"https://shopping.yahooapis.jp/ShoppingWebService/V1/json/itemSearch";
			var para = $@"category_id={category_id}&sort={sort}&appid={appId}";
			var uri = new Uri($@"{url}?{para}");

			var jobj = await uri.GetObjectAsync();
			var jstr = jobj.ToString();

			var result = jobj["ResultSet"]["0"]["Result"];

			var cs = result.Select(c => c.First()).Where(c => c.HasValues).Select(c => new {
				Name = c["Name"]?.ToString(),
				Url = c["Url"]?.ToString(),
			}).Where(c => c.Name != null).ToList();
			var tbl = cs.ToDataTable();
			return tbl;
		}

		/// <summary>
		/// カテゴリーを取得する REST API です。
		/// </summary>
		/// <param name="appId">アプリケーションID</param>
		/// <param name="category_id">カテゴリーID</param>
		/// <returns>カテゴリー情報のテーブルを返します。</returns>
		public static async Task<DataTable> GetCategorySearchAsync(string appId, int category_id) {
			var url = $@"https://shopping.yahooapis.jp/ShoppingWebService/V1/json/categorySearch";
			var para = $@"category_id={category_id}&appid={appId}";
			var uri = new Uri($@"{url}?{para}");

			var jobj = await uri.GetObjectAsync();
			var jstr = jobj.ToString();

			var result = jobj["ResultSet"]["0"]["Result"];
			var children = result["Categories"]["Children"];

			var cs = children.Select(c => c.First()).Where(c => c.HasValues).Select(c => new {
				Order = c["_attributes"]["sortOrder"].ToString(),
				Id = c["Id"].ToString(),
				Url = c["Url"].ToString(),
				Title = c["Title"]["Long"].ToString(),
			}).ToList();
			var tbl = cs.ToDataTable();
			return tbl;
		}

		#endregion
	}
}
