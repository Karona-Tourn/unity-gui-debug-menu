using UnityEngine;

namespace TK.DebugTool
{

	public class DebugAccessorBool : DebugBool
	{

		public DebugAccessorBool(string name, DebugFieldAccessor<bool> accessor) : base(name, accessor.Value, (value) => accessor.Value = value)
		{
		}

	}

	public class DebugAccessorString : DebugItem
	{
		private DebugFieldAccessor<string> accessor;

		public DebugAccessorString(string name, DebugFieldAccessor<string> accessor) : base(name)
		{
			this.accessor = accessor;
		}

		public override void Draw()
		{
			GUILayout.BeginVertical();
			GUILayout.Label(Name);
			accessor.Value = GUILayout.TextField(accessor.Value);
			GUILayout.EndVertical();
		}

	}

	public class DebugAccessorSlider : DebugItem
	{
		private DebugFieldAccessor<float> accessor;
		private float left;
		private float right;

		public DebugAccessorSlider(string name, DebugFieldAccessor<float> accessor, float left, float right) : base(name)
		{
			this.accessor = accessor;
			this.left = left;
			this.right = right;
		}

		public override void Draw()
		{
			GUILayout.BeginVertical(GUILayout.Width(300));
			GUILayout.Label(Content);
			accessor.Value = GUILayout.HorizontalSlider(accessor.Value, left, right);
			GUILayout.EndVertical();
		}
	}

	public class DebugAccessorFloat : DebugItem
	{
		private DebugFieldAccessor<float> accessor;
		private float changeValue;

		public DebugAccessorFloat(string name, DebugFieldAccessor<float> accessor, float changeValue) : base(name)
		{
			this.accessor = accessor;
			this.changeValue = changeValue;
		}

		public override void Draw()
		{
			GUILayout.BeginHorizontal();
			GUILayout.Label(Name);
			if (GUILayout.Button(" - ", GUILayout.Width(50)))
			{
				accessor.Value -= changeValue;
			}
			GUILayout.Label(accessor.Value.ToString(), GUILayout.Width(100));
			if (GUILayout.Button(" + ", GUILayout.Width(50)))
			{
				accessor.Value += changeValue;
			}
			GUILayout.EndHorizontal();
		}
	}

}
