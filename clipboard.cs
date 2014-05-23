using UnityEngine;
using System.Collections;
using System;
using System.Reflection;
using System.Runtime.InteropServices;


public class clipboardtest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	static string text;

	void OnGUI()
	{
		if (GUI.Button(new Rect(0,0, 100, 100), "test")){
			text = ImportString();
		}

		if (GUI.Button(new Rect(100,0, 100, 100), "to clipboard")){
			ExportString("from unity test");
		}
		
		GUI.Label(new Rect(0, 120, 300, 300), text);
	}

	public void SetText(string _text)
	{
		text = _text;
	}


	[DllImport("__Internal")]
	
	private static extern string _importString();
	
	
	
	//retrieves the clipboard contents from xcode
	
	public static string ImportString()
		
	{
		
		//Debug.Log ("import: "+_importString());
		
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			
						return _importString ();
		} else if (Application.platform == RuntimePlatform.Android) {

			AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(runOnUiThread));
			return "";
			

		}
		else
		{
			
			return "";
			
		}

		
	}

	static void runOnUiThread() {
		AndroidJavaClass jc = new AndroidJavaClass("com.pp.test.Clipboard");
		jc.CallStatic("GetCopyBufferString");
		Debug.Log("I'm running on the Java UI thread!");
	}



[DllImport("__Internal")]
	
	private static extern void _exportString(string exportData);
	
	
	
	//passes string to xcode and xcode copies to clipboard
	
	public static void ExportString(string exportData)
		
	{
		
		// Debug.Log ("export: "+exportData);
		
		if( Application.platform == RuntimePlatform.IPhonePlayer )
			
		{
			
			_exportString(exportData);
			
		
	} else if (Application.platform == RuntimePlatform.Android) {
		AndroidJavaClass jc = new AndroidJavaClass("com.pp.test.Clipboard");
			jc.CallStatic("SetCopyBufferString", exportData);
	}
	
}

}
