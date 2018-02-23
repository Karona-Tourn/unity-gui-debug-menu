using UnityEngine;
using UnityEngine.Events;

namespace TK.DebugTool
{

	// Draw action button debug
	public class DebugAction : DebugItem
	{
		private UnityAction action;
		private UnityAction<DebugAction> paramAction;

		public DebugAction(string name, UnityAction action) : base(name)
		{
			this.action = action;
		}

		public DebugAction(string name, UnityAction<DebugAction> paramAction) : base(name)
		{
			this.paramAction = paramAction;
		}

		public override void Draw()
		{
			if (GUILayout.Button(Content, ButtonStyle))
			{
				if (action != null) { action(); }
				if (paramAction != null) { paramAction(this); }
			}
		}
	}

}
