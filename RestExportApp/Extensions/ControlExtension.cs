using System.Linq;
using System.Windows.Forms;

namespace RestExportApp.Extensions {
	public static class ControlExtension {
		public static RadioButton SelectedRadioButton(this Control @this) {
			return @this.Controls.OfType<RadioButton>().SingleOrDefault(r => r.Checked);
		}
	}
}
