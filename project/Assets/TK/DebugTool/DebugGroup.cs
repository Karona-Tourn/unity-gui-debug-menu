// Unity name space
using UnityEngine;
using UnityEngine.Events;

// Microsoft name space
using System.Collections.Generic;

namespace TK.DebugTool
{

	/// <summary>
	/// Debug group to list all debug items.
	/// </summary>
	public class DebugGroup : DebugCollection<DebugGroup>
	{
		private const int MAX_DISPLAY_COUNT = 17;
		private readonly float MAX_ELEMENT_HEIGHT	= 26.5f;
		private const string LABEL_BACK = "Back";
		private const string LABEL_CLOSE = "Close";
		private Vector2 scrollPos;
		private GUIStyle scrollStyle = null;
		protected bool lowOpacity = false;

		// List to store all items
		private List<IDebugItem> items = new List<IDebugItem> ();

		public DebugGroup (string name) : base (name)
		{
		}

		private bool IsDuplicated (IDebugItem item)
		{
			return items.Exists (e => e.Name == item.Name);
		}

		/// <summary>
		/// Add the specified debug item.
		/// </summary>
		/// <param name="item">New item.</param>
		public override IDebugGroup Add (IDebugItem item)
		{
            if (item == null)
            {
                UnityEngine.Debug.LogWarning("Debug item cannot be null.");
                return this;
            }

			if (IsDuplicated (item))
			{
                UnityEngine.Debug.LogWarningFormat("Debug item {0} already exists.", item.Name);
                return this;
			}

			if (item is IDebugGroup)
			{
				((IDebugGroup)item).Parent = this;
			}

			item.Menu = Menu;
			items.Add (item);

			return this;
		}

		/// <summary>
		/// Remove debug items by the specified name.
		/// </summary>
		/// <param name="name">Name of item to be deleted. Name can be a single name or path separated by dot (.).</param>
		public override void Remove (string name)
		{
			string deletedName = name;
			int index = name.IndexOf ('.');

			if (index >= 0)
			{
				deletedName = name.Substring (0, index);

				IDebugGroup group = (IDebugGroup)items.Find (item => item.Name.Equals (deletedName));
				if (group != null)
				{
					group.Remove (name.Substring (index + 1));
				}
			}
			else
			{
				items.RemoveAll (item => item.Name == deletedName);
			}

		}

		public override void Clear ()
		{
			items.Clear ();
		}

		public override void Close ()
		{
			if (Menu.CurrentItem == this)
			{
				//InvokeOnExit ();
				Back ();
				if (Parent != null && Parent is IDebugGroup)
				{
					((IDebugGroup)Parent).Close ();
				}
			}
			else
			{
				Menu.CurrentItem.Close ();
			}
		}

		public override void Back ()
		{
			InvokeOnExit ();
			if (Parent != null && Parent is IDebugGroup)
			{
				Parent.InvokeOnAccess ();
				Menu.CurrentItem = Parent;
			}
		}

		/// <summary>
		/// Draws the item list.
		/// </summary>
		protected override void DrawContent ()
		{
			GUILayout.BeginVertical (lowOpacity ? Menu.Style.LowOpacityBoxStyle : Menu.Style.BoxStyle);

			GUILayout.Label (Content);

			GUILayout.BeginHorizontal ();

			// TODO: Draw button back
			if (GUILayout.Button (LABEL_BACK))
			{
				Back ();
			}

			// TODO: Draw button close
			if (GUILayout.Button (LABEL_CLOSE))
			{
				Close ();
			}

			GUILayout.EndHorizontal ();

			if (scrollStyle == null)
			{
				scrollStyle = new GUIStyle (GUI.skin.scrollView);

				// Set padding right for 20 px to avoid overlapping
				// the right scrollbar over the elements
				scrollStyle.padding.right = 20;
			}

			if (items.Count > 2)
				scrollPos = GUILayout.BeginScrollView (scrollPos, scrollStyle,
					GUILayout.Height (Mathf.Clamp (items.Count, 0, MAX_DISPLAY_COUNT) * MAX_ELEMENT_HEIGHT));

			// Draw to list all debug items
			for (int i = 0; i < items.Count; i++)
			{
				IDebugItem item = items [i];
				item.Draw ();
				if (item is IDebugGroup)
				{
					IDebugGroup group = (IDebugGroup)item;
					if (group.IsAccess)
					{
						InvokeOnExit ();
						Menu.CurrentItem = group;
						break;
					}
				}
			}

			if (items.Count > 2)
				GUILayout.EndScrollView ();

			GUILayout.EndVertical ();
		}

	}

}
