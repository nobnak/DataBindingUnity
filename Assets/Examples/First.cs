using UnityEngine;
using System.Collections;

public class First : MonoBehaviour {
	public Data data;

	[System.Serializable]
	public class Data {
		public int intfield;
		public string stringfield;
		public bool boolfield;
		public float floatfield;
	}

	void Start () {
	}
	
	void Update () {
	}
}
