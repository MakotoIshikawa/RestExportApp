using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExtensionsLibrary.Extensions;
using NetLibrary.Extensions;
using ObjectAnalysisProject.Extensions;
using RestExportApp.Model;
using WindowsFormsLibrary.Extensions;
using OfficeOpenXml;
using System.IO;
using OfficeOpenXml.Table;

namespace RestExportApp {
	public partial class FormMain : Form {
		#region フィールド

		private string _appid = "dj0zaiZpPTAxaDBCNkxtd2hVTSZzPWNvbnN1bWVyc2VjcmV0Jng9ZTM-";

		#endregion

		#region コンストラクタ

		public FormMain() {
			this.InitializeComponent();
		}

		#endregion

		#region イベントハンドラ

		private async void btnGet_Click(object sender, EventArgs e) {
			var rdo = this.group01.Controls.OfType<RadioButton>().SingleOrDefault(r => r.Checked);
			var st = rdo?.Text;
			this.grid01.Tag = st;

			switch (st) {
			case "降水量":
				await this.Precipitation();
				break;
			case "商品検索":
				await this.ItemSearch();
				break;
			case "カテゴリー":
				await this.CategorySearch();
				break;
			}
		}

		private async void btnExport_Click(object sender, EventArgs e) {
			try {
				var dlg = new SaveFileDialog {
					FilterIndex = 1,
					Title = "エクスポート",
					Filter = "Excelファイル (*.xlsx, *.xlsm)|*.xlsx;*.xlsm",
				};

				if (dlg.ShowDialog() != DialogResult.OK) {
					return;
				}

				var table = this.grid01.ToDataTable();
				var name = $"{this.grid01.Tag}";
				table.TableName = name;
				await Task.Run(() => {
					var file = new FileInfo(dlg.FileName);
					using (var xlsx = new ExcelPackage(file)) {
						var sheet = xlsx.Workbook.Worksheets.Add(name);
						sheet.Cells["A1"].LoadFromDataTable(table, true, TableStyles.Light6);

						xlsx.Save();
					}
				});
			} catch (Exception ex) {
				this.ShowErrorMessage(ex, true);
			}
		}

		#endregion

		#region メソッド

		private async Task Precipitation() {
			var lat = 139.732293;
			var lon = 35.663613;

			this.ShowMessageBox($"降水量を調べます。緯度 : {lat}, 経度 : {lon}");
			var url = $@"https://map.yahooapis.jp/weather/V1/place";
			var para = $@"coordinates={lat},{lon}&output=json&appid={_appid}";
			var uri = new Uri($@"{url}?{para}");

			var json = await uri.GetJsonAsync<PrecipitationModel>();
			var weathers = json.Feature.FirstOrDefault()?.Property?.WeatherList?.Weather;
			this.grid01.DataSource = weathers.ToDataTable();
		}

		private async Task CategorySearch() {
			this.ShowMessageBox($"カテゴリーを調べます。");
			var url = $@"https://shopping.yahooapis.jp/ShoppingWebService/V1/json/categorySearch";
			var category_id = 1;
			var para = $@"category_id={category_id}&appid={_appid}";
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
			this.grid01.DataSource = cs.ToDataTable();
		}

		private async Task ItemSearch() {
			this.ShowMessageBox($"商品検索を調べます。");
			var url = $@"https://shopping.yahooapis.jp/ShoppingWebService/V1/json/itemSearch";
			var category_id = 635;
			var sort = "-sold";
			var para = $@"category_id={category_id}&sort={sort}&appid={_appid}";
			var uri = new Uri($@"{url}?{para}");

			var jobj = await uri.GetObjectAsync();
			var jstr = jobj.ToString();

			var result = jobj["ResultSet"]["0"]["Result"];

			var cs = result.Select(c => c.First()).Where(c => c.HasValues).Select(c => new {
				Name = c["Name"]?.ToString(),
				Url = c["Url"]?.ToString(),
			}).Where(c => c.Name != null).ToList();
			this.grid01.DataSource = cs.ToDataTable();
		}

		#endregion
	}
}
