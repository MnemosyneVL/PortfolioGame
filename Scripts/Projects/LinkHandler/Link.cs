using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class Link : MonoBehaviour 
{
    public string link;
	public void OpenLinkJSPlugin()
	{
		#if !UNITY_EDITOR
		openWindow(link);
		#endif
	}

	[DllImport("__Internal")]
	private static extern void openWindow(string url);

}