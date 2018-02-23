using UnityEngine;

using System.Collections.Generic;

namespace TK.DebugTool
{

	public class DebugBoolArray : DebugGroup
	{

		private bool[] array;

		public DebugBoolArray(string name, bool[] array) : base(name)
		{
			this.array = array;
			OnAccess = OnAccessCallback;
			OnExit = OnExitCallback;
		}

		private void OnAccessCallback(DebugGroup group)
		{
			int length = array.Length;
			for (int i = 0; i < length; i++)
			{
				int index = i;
				Add(new DebugBool(index.ToString(), array[index], (value) => array[index] = value));
			}
		}

		private void OnExitCallback(DebugGroup group)
		{
			Clear();
		}

	}

	public class DebugBoolList : DebugGroup
	{

		private List<bool> list;

		public DebugBoolList(string name, List<bool> list) : base(name)
		{
			this.list = list;
			OnAccess = OnAccessCallback;
			OnExit = OnExitCallback;
		}

		private void OnAccessCallback(DebugGroup group)
		{
			int length = list.Count;
			for (int i = 0; i < length; i++)
			{
				int index = i;
				Add(new DebugBool(index.ToString(), list[index], (value) => list[index] = value));
			}
		}

		private void OnExitCallback(DebugGroup group)
		{
			Clear();
		}

	}

}
