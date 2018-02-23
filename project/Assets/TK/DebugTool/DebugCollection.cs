using UnityEngine;
using UnityEngine.Events;

using System.Collections;

namespace TK.DebugTool
{

	public abstract class DebugCollection<T> : DebugItem, IDebugGroup where T : DebugCollection<T>
	{
		public UnityAction<T> OnAccess = null;
		public UnityAction<T> OnExit = null;
		private GUIContent headContent = new GUIContent ();

		public bool IsAccess { get; set; }

		// Get or set parent group that convers this debug item
		// The parent must be the group
		public IDebugGroup Parent { get; set; }

		public override string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
				headContent.text = "[+] " + Name;
			}
		}

		public DebugCollection(string name) : base(name)
		{
		}

		protected override GUIContent Content
		{
			get
			{
				return headContent;
			}
		}

		public void InvokeOnAccess()
		{
			IsAccess = true;
			if (OnAccess != null)
			{
				OnAccess((T)this);
			}
		}

		public void InvokeOnExit()
		{
			IsAccess = false;
			if (OnExit != null)
			{
				OnExit((T)this);
			}
		}

		/// <summary>
		/// Draws the main button of the group.
		/// </summary>
		protected void DrawMainButton()
		{
			if (GUILayout.Button(Content, ButtonStyle))
			{
				InvokeOnAccess();
			}
		}

		public override void Draw()
		{
			if (IsAccess)
			{
				DrawContent();
			}
			else
			{
				DrawMainButton();
			}
		}

		public abstract IDebugGroup Add(IDebugItem item);
		public abstract void Remove (string name);
		public abstract void Close();
		public abstract void Back();
		public abstract void Clear();
		protected abstract void DrawContent();
	}

}
