using UnityEngine;
using UnityEngine.UI;
using TK.DebugTool;

public class HpBar : MonoBehaviour
{
	public Text textHp = null;
	public int maxHp = 10000;
	private int hp = 0;
	private Scrollbar bar = null;

	public int Hp
	{
		get { return hp; }
		set
		{
			hp = Mathf.Clamp ( value, 0, maxHp );
			bar.size = (float)hp / maxHp;

			textHp.text = string.Format ( "{0}/{1}", hp, maxHp );
		}
	}

	// Use this for initialization
	void Start ()
	{
		bar = GetComponent<Scrollbar> ();
		Hp = 0;

		var root = DebugManager.Instance.Root;
		var group = new DebugGroup("Cheat HP");
		root.Add ( group );
		group.Add ( new DebugAction ( "+100 HP", () => Hp += 100 ) );
		group.Add ( new DebugAction ( "+200 HP", () => Hp += 200 ) );
		group.Add ( new DebugAction ( "+300 HP", () => Hp += 300 ) );
		group.Add ( new DebugAction ( "+1000 HP", () => Hp += 1000 ) );
		group.Add ( new DebugAction ( "-100 HP", () => Hp -= 100 ) );
		group.Add ( new DebugAction ( "-400 HP", () => Hp -= 400 ) );
	}
}
