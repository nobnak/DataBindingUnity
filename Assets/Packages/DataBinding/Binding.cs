using UnityEngine;
using System.Collections;
using System.Reflection;

namespace DataBinding {

	public abstract class Binding<SRC, DST> : MonoBehaviour {
		public Behaviour src;
		public string srcPath;
		public Behaviour dst;
		public string dstPath;

		[SerializeField]
		public UnityEngine.Events.UnityEvent OnSrcChanged;
		[SerializeField]
		public UnityEngine.Events.UnityEvent OnDstChanged;

		ParameterInfo<SRC> _srcInfo;
		ParameterInfo<DST> _dstInfo;

		protected virtual void Start() {
			_srcInfo = ParameterInfo<SRC>.Generate(src, srcPath);
			_dstInfo = ParameterInfo<DST>.Generate(dst, dstPath);

			if (_srcInfo == null) {
				Debug.LogError(string.Format("Binding src is invalid : {0}", srcPath));
				enabled = false;
				return;
			} else if (_dstInfo == null) {
				Debug.LogError(string.Format("Binding dst is invalid : {0}", dstPath));
				enabled = false;
				return;
			}

			Load ();
		}

		public abstract bool Convert(SRC srcValue, out DST dstValue);
		public abstract bool ConvertBack(DST dstValue, out SRC srcValue);

		public virtual void Load() {
			DST tmp;
			if (Convert(_srcInfo.Value, out tmp)) {
				_dstInfo.Value = tmp;
				OnDstChanged.Invoke();
			}
		}
		public virtual void Save() {
			SRC tmp;
			if (ConvertBack(_dstInfo.Value, out tmp)) {
				_srcInfo.Value = tmp;
				OnSrcChanged.Invoke();
			}
		}
		
	}

	public class SimpleBinding<T> : Binding<T, T> {
		protected override void Start() {
			base.Start();
		}

		#region implemented abstract members of Binding
		public override bool Convert (T srcValue, out T dstValue) {
			dstValue = srcValue;
			return true;
		}

		public override bool ConvertBack (T dstValue, out T srcValue) {
			srcValue = dstValue;
			return true;
		}

		#endregion


	}
}
