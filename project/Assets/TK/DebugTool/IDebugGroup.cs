using UnityEngine;

namespace TK.DebugTool
{

	public interface IDebugGroup : IDebugItem
	{
		// Debug parent of this debug
		IDebugGroup Parent { get; set; }

		bool IsAccess { get; set; }

		// Add new debug item
		IDebugGroup Add(IDebugItem item);

		// Remove debug item by name
		void Remove(string name);

		void InvokeOnAccess();

		void InvokeOnExit();

		// Clear all debug items from list
		void Clear();
		void Back();
		void Close();

	}

}
