using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace TK.DebugTool
{

	public class DebugInput : DebugItem
	{
		private string value = "";
		private UnityAction<DebugInput, string> Action;

		public DebugInput(string name, UnityAction<DebugInput, string> action) : base(name)
		{
			this.Action = action;
		}

		public override void Draw()
		{
			GUILayout.BeginHorizontal(GUILayout.Width(Screen.width * 0.3f));
			value = GUILayout.TextField(value);
			if (GUILayout.Button(Content, GUILayout.Width(GUIStyle.none.CalcSize(Content).x)))
			{
				if (Action != null) { Action(this, value); }
			}
			GUILayout.EndHorizontal();
		}
	}

}
