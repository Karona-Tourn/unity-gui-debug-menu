using UnityEngine;
using UnityEngine.Events;

namespace TK.DebugTool
{

	/// <summary>
	/// Debug boolean value
	/// </summary>
	public class DebugBool : DebugItem
	{

		public UnityAction<bool> OnValueChanged;
		private bool currentValue;

		public DebugBool(string name, bool currentValue, UnityAction<bool> onValueChanged) : base(name)
		{
			this.currentValue = currentValue;
			OnValueChanged = onValueChanged;
		}

		public override void Draw()
		{
			bool newValue = GUILayout.Toggle (currentValue, Content);
			if (newValue != currentValue)
			{
				currentValue = newValue;
				if (OnValueChanged != null)
				{
					OnValueChanged(currentValue);
				}
			}
		}

	}

}
