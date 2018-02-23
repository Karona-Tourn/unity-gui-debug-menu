using UnityEngine;
using System.Collections;

namespace TK.DebugTool
{
	
	public interface IDebugItem
	{
		string Name { get; }

		object Tag { get; set; }

		// Menu to draw this debug
		DebugMenu Menu { get; set; }

		void Draw();
	}

}
