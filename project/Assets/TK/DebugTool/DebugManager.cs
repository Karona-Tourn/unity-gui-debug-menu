using UnityEngine;

namespace TK.DebugTool
{

	public class DebugManager : MonoBehaviour
	{
		private const float originalWidth = 480f;  // define here the original resolution
		private const float originalHeight = 854f; // you used to create the GUI contents 
		private static DebugManager instance = null;

		private Matrix4x4 newMatrix = new Matrix4x4();

		public Matrix4x4 GetMatrix ()
		{
			return newMatrix;
		}

		private DebugMenu menu = new DebugMenu();

		private void Awake()
		{
			var w = Screen.width;
			var h = (originalHeight / originalWidth) * w;
			var guiscale = new Vector3 (w / originalWidth, h / originalHeight, 1);
			newMatrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, guiscale * 1.5f);
		}

		public IDebugGroup Root
		{
			get { return menu.Root; }
		}

		public bool IsDebugOpening
		{
			get { return menu.IsDebugOpening; }
		}

		public void Close()
		{
			menu.Close();
		}

		private void OnGUI()
		{
			var svMat = GUI.matrix; // save current matrix
									// substitute matrix - only scale is altered from standard
			GUI.matrix = newMatrix;

			// Draw debug menu
			menu.Draw();

			// restore matrix before returning
			GUI.matrix = svMat; // restore matrix
		}

		public static DebugManager Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new GameObject("_DebugManager").AddComponent<DebugManager>();
					DontDestroyOnLoad(instance.gameObject);
				}
				return instance;
			}
		}
	}

}
