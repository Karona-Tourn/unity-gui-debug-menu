using UnityEngine;

namespace TK.DebugTool
{
	public class DebugStyle
	{
		private GUIStyle boxStyle = null;
		private GUIStyle buttonStyle = null;
		private GUIStyle lowOpacityBoxStyle = null;

		public GUIStyle BoxStyle
		{
			get
			{
				if (boxStyle == null)
				{
					boxStyle = new GUIStyle (GUI.skin.box);
					boxStyle.normal.background = GenerateStyleTexture (new Color (54f / 255f, 54f / 255f, 54f / 255f));
				}
				return boxStyle;
			}
		}

		public GUIStyle LowOpacityBoxStyle
		{
			get
			{
				if (lowOpacityBoxStyle == null)
				{
					lowOpacityBoxStyle = new GUIStyle (GUI.skin.box);
				}
				return lowOpacityBoxStyle;
			}
		}

		public GUIStyle ButtonStyle
		{
			get
			{
				if (buttonStyle == null)
				{
					Color textColor = Color.black;

					buttonStyle = new GUIStyle (GUI.skin.button);
					buttonStyle.normal.background = GenerateStyleTexture (new Color (237 / 255f, 28f / 255f, 36f / 255f));
					buttonStyle.hover.background = GenerateStyleTexture (new Color (250 / 255f, 35f / 255f, 46f / 255f));
					buttonStyle.active.background = GenerateStyleTexture (new Color (200 / 255f, 18f / 255f, 26f / 255f));

					buttonStyle.normal.textColor = textColor;
					buttonStyle.hover.textColor = textColor;
					buttonStyle.active.textColor = textColor;

					buttonStyle.alignment = TextAnchor.MiddleLeft;
				}
				return buttonStyle;
			}
		}

		public static Texture2D GenerateStyleTexture(Color color)
		{
			// Generate background texture for the box style of the debug group
			int pixels = 16;
			Texture2D texture = new Texture2D (pixels, pixels);
			for (int y = 0; y < texture.height; y++)
			{
				for (int x = 0; x < texture.width; x++)
				{
					if (x == 0 || y == 0 || (x == texture.width - 1) || (y == texture.height - 1))
					{
						texture.SetPixel (x, y, Color.black);
					}
					else
					{
						texture.SetPixel (x, y, color);
					}
				}
			}
			texture.Apply ();
			return texture;
		}
	}

	public class DebugMenu
	{
		private IDebugGroup rootItem;
		private IDebugGroup curItem;

		public IDebugGroup Root
		{
			get { return rootItem; }
		}

		public IDebugGroup CurrentItem
		{
			get { return curItem; }
			set { curItem = value; }
		}

		public bool IsDebugOpening
		{
			get { return curItem.IsAccess; }
		}

		public DebugStyle Style { get; private set; }

		public DebugMenu()
		{
			rootItem = new DebugGroup("Debug");
			rootItem.Menu = this;
			curItem = rootItem;

			Style = new DebugStyle ();
		}

		public void Close()
		{
			IDebugGroup group = curItem;
			group.Close();
		}

		// Update is called once per frame
		public void Draw()
		{
			curItem.Draw();
		}
	}

}
