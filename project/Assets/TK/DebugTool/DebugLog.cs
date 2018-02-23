using UnityEngine;

namespace TK.DebugTool
{
	public class DebugLogContent : DebugItem
	{
		private string log = "";
		private Vector2 scroll = Vector2.zero;
		private bool showStackTrace = false;
		private bool minimize = false;
		private GUIStyle style = null;

		public DebugLogContent (string name, bool showStackTrace, string predefinedLog = "") : base (name)
		{
            log = predefinedLog;
		}

		public void Prepare ()
		{
			Application.logMessageReceived += Application_logMessageReceived;
		}

		public void Release ()
		{
			Application.logMessageReceived -= Application_logMessageReceived;
		}

		private void Application_logMessageReceived (string condition, string stackTrace, LogType type)
		{
			if (log.Length > 0)
			{
				log += "\n";
			}
			if (type == LogType.Error || type == LogType.Assert)
				log += "<color=red>";
			else if (type == LogType.Warning)
				log += "<color=yellow>";
			else
				log += "<color=white>";
			log += System.DateTime.Now.ToShortDateString () + " - " + System.DateTime.Now.ToShortTimeString ();
			log += "\n====================";
			log += "\n" + condition;
			if (showStackTrace)
			{
				log += "\n" + stackTrace;
			}
			log += "</color>";
		}

		public override void Draw ()
		{
			if (style == null)
			{
				style = new GUIStyle (GUI.skin.textArea);
				style.richText = true;
			}

			if (minimize)
				GUILayout.BeginVertical (GUILayout.Width (300));
			else
				GUILayout.BeginVertical (GUILayout.Width (300), GUILayout.Height (250));
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("Clear"))
			{
				log = "";
			}
			if (GUILayout.Button (minimize ? "Maximize" : "Minimize"))
			{
				minimize = !minimize;
			}
			GUILayout.EndHorizontal ();

			if (!minimize)
			{
				scroll = GUILayout.BeginScrollView (scroll, GUILayout.ExpandHeight (true), GUILayout.ExpandWidth (true));
				log = GUILayout.TextArea (log, style, GUILayout.ExpandHeight (true), GUILayout.ExpandWidth (true));
				GUILayout.EndScrollView ();
			}
			GUILayout.EndVertical ();
		}
	}

	public class DebugLog : DebugGroup
	{
        public DebugLog (string name, bool showStackTrace, string predefinedLog = "") : base (name)
		{
			lowOpacity = true;
            var item = new DebugLogContent ("Log", showStackTrace, predefinedLog);
			Add (item);
			OnAccess = d =>
			{
				item.Prepare ();
			};

			OnExit = d =>
			{
				item.Release ();
			};
		}
	}
}
