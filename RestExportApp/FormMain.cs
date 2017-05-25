using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExtensionsLibrary.Extensions;
using ObjectAnalysisProject.Extensions;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using RestExportApp.Properties;
using WindowsFormsLibrary.Extensions;
using static RestExportApp.RestApi.YahooApi;

namespace RestExportApp {
	public partial class FormMain : Form {
		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormMain() {
			this.InitializeComponent();

			this.AppId = Settings.Default.AppId;
		}

		#endregion

		#region プロパティ

		/// <summary>
		/// アプリケーションID
		/// </summary>
		protected string AppId { get; set; }

		#endregion

		#region イベントハンドラ

		private async void btnGet_Click(object sender, EventArgs e) {
			try {
				await GetAsync();
			} catch (Exception ex) {
				this.ShowErrorMessage(ex, true);
			}
		}

		private async void btnExport_Click(object sender, EventArgs e) {
			try {
				await ExportAsync();
			} catch (Exception ex) {
				this.ShowErrorMessage(ex, true);
			}
		}

		#endregion

		#region メソッド

		#region GetAsync

		/// <summary>
		/// GET 処理
		/// </summary>
		protected virtual async Task GetAsync() {
			var rdo = this.group01.Controls.OfType<RadioButton>().SingleOrDefault(r => r.Checked);
			var st = rdo?.Text;
			this.grid01.Tag = st;

			switch (st) {
			case "降水量":
				this.ShowMessageBox($"降水量を調べます。");
				this.grid01.DataSource = await GetPrecipitationAsync(this.AppId, 139.732293, 35.663613);
				break;
			case "商品検索":
				this.ShowMessageBox($"商品検索を調べます。");
				this.grid01.DataSource = await GetItemSearchAsync(this.AppId, 635, "-sold");
				break;
			case "カテゴリー":
				this.ShowMessageBox($"カテゴリーを調べます。");
				this.grid01.DataSource = await GetCategorySearchAsync(this.AppId, 1);
				break;
			}
		}

		#endregion

		#region ExportAsync

		/// <summary>
		/// エクスポート処理
		/// </summary>
		protected virtual async Task ExportAsync() {
			var name = $"{this.grid01.Tag}";

			var dlg = new SaveFileDialog {
				// はじめのファイル名を指定する
				FileName = $"{name}.xlsx",

				// はじめに表示されるフォルダを指定する
				InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),

				// [ファイルの種類]に表示される選択肢を指定する
				Filter = "Excelファイル (*.xlsx, *.xlsm)|*.xlsx;*.xlsm|すべてのファイル (*.*)|*.*",

				// [ファイルの種類]ではじめに選択されるものを指定する
				FilterIndex = 1,

				// タイトルを設定する
				Title = "エクスポートのファイルを選択してください",
			};

			if (dlg.ShowDialog() != DialogResult.OK) {
				return;
			}

			var table = this.grid01.ToDataTable();
			table.TableName = name;

			await Task.Run(() => {
				var file = new FileInfo(dlg.FileName);
				if (file.Exists) {
					file.Delete();
				}

				using (var xlsx = new ExcelPackage(file)) {
					var sheet = xlsx.Workbook.Worksheets.Add(name);
					sheet.Cells["A1"].LoadFromDataTable(table, true, TableStyles.Light6);

					xlsx.Save();
				}
			});
		}

		#endregion

		#endregion
	}
}
