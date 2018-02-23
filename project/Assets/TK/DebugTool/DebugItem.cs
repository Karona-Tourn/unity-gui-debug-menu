using UnityEngine;

namespace TK.DebugTool
{

	public abstract class DebugItem : IDebugItem
	{
		private string name = "";
		private GUIStyle style = null;
		private GUIContent content = new GUIContent ();

		public virtual string Name
		{
			get { return name; }
			set { name = value; }
		}

		public object Tag { get; set; }

		public DebugMenu Menu { get; set; }

		public GUIStyle ButtonStyle
		{
			get
			{
				if (style == null)
				{
					style = new GUIStyle (GUI.skin.button);
					style.alignment = TextAnchor.MiddleLeft;
				}
				return style;
			}
		}

		protected virtual GUIContent Content
		{
			get
			{
				if (content.text.Equals (name) == false)
					content.text = name;
				return content;
			}
		}

		public DebugItem (string name)
		{
			Name = name;
		}

		public abstract void Draw ();
	}

}
