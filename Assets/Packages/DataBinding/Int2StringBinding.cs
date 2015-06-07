using UnityEngine;
using System.Collections;

namespace DataBinding {

	public class Int2StringBinding : Binding<int, string> {

		protected override void Start() {
			base.Start();
		}

		#region implemented abstract members of Binding
		public override bool Convert (int srcValue, out string dstValue) {
			dstValue = srcValue.ToString();
			return true;
		}

		public override bool ConvertBack (string dstValue, out int srcValue) {
			return int.TryParse(dstValue, out srcValue);
		}
		#endregion
	}
}