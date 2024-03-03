using UnityEditor;
using UnityEngine;

public class CaptureScreenshot : Editor {
	/// <summary>
	/// キャプチャを撮る
	/// 
	/// GameViewにフォーカスがあること
	/// </summary>
	/// <remarks>
	/// Edit > CaptureScreenshot に追加。
	/// HotKeyは Shift + F12。
	/// </remarks>
	/// %	Ctrl	command (mac)
	/// #	Shift	shift
	/// &	Alt	option
	[MenuItem( "Editor/CaptureScreenshot %&s" )]
	private static void DoCaptureScreenshot() {
		var filename = "Screenshot/" + System.DateTime.Now.ToString( "yyyyMMdd-HHmmss" ) + ".png";
		ScreenCapture.CaptureScreenshot( filename );
		var assembly = typeof( UnityEditor.EditorWindow ).Assembly;
		var type = assembly.GetType( "UnityEditor.GameView" );
		var gameview = EditorWindow.GetWindow( type );
		gameview.Repaint();

		Debug.Log( "ScreenShot: " + filename );
	}
}