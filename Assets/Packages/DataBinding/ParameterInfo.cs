using UnityEngine;
using System.Collections;
using System.Reflection;

namespace DataBinding {

	public abstract class ParameterInfo<T> {
		public readonly static char[] SEP = new char[]{ '.' };

		public abstract T Value { get; set; }

		public static ParameterInfo<T> Generate(object obj, string path) {
			if (obj == null || path == null || path.Length == 0)
				return null;
			var srcNestedFieldNames = path.Split (SEP);
			object field = obj;
			FieldInfo fieldInfo = null;
			PropertyInfo propInfo = null;
			foreach (var fieldname in srcNestedFieldNames) {
				obj = field;
				if (obj == null || fieldname.Length == 0)
					return null;
				var fieldType = obj.GetType ();
				fieldInfo = fieldType.GetField (fieldname);
				if (fieldInfo != null) {
					field = fieldInfo.GetValue (obj);
					continue;
				}
				propInfo = fieldType.GetProperty (fieldname);
				if (propInfo != null) {
					field = propInfo.GetValue (obj, null);
					continue;
				}

				Debug.Log(string.Format("Member not found : {0}", fieldname));
				return null;
			}

			if (fieldInfo != null && fieldInfo.FieldType == typeof(T))
				return new FieldParameterInfo<T>(obj, fieldInfo);
			if (propInfo != null && propInfo.PropertyType == typeof(T))
				return new PropertyParameterInfo<T>(obj, propInfo);
			return null;
		}
	}

	public class FieldParameterInfo<T> : ParameterInfo<T> {
		object _obj;
		FieldInfo _info;

		public FieldParameterInfo(object obj, FieldInfo info) {
			this._obj = obj;
			this._info = info;
		}

		#region implemented abstract members of ParameterInfo 
		public override T Value {
			get { return (T)_info.GetValue(_obj); }
			set { _info.SetValue(_obj, value); }
		}
		#endregion
	}

	public class PropertyParameterInfo<T> : ParameterInfo<T> {
		object _obj;
		PropertyInfo _info;

		public PropertyParameterInfo(object obj, PropertyInfo info) {
			this._obj = obj;
			this._info = info;
		}

		#region implemented abstract members of ParameterInfo
		public override T Value {
			get { return (T)_info.GetValue(_obj, null); }
			set { _info.SetValue(_obj, value, null); }
		}
		#endregion
	}
}