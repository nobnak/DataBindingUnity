using UnityEngine;
using System.Collections;

namespace DataBinding {

	[AddComponentMenu("DataBinding/Float->String")]
	public class Float2StringBinding : Binding<float, string> {
		public string format = "f2";
		
		protected override void Start() {
			base.Start();
		}
		
		#region implemented abstract members of Binding
		public override bool Convert (float srcValue, out string dstValue) {
			dstValue = srcValue.ToString(format);
			return true;
		}
		
		public override bool ConvertBack (string dstValue, out float srcValue) {
			return float.TryParse(dstValue, out srcValue);
		}
		#endregion
	}
}