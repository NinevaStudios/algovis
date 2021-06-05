using Nineva;
using UnityEngine;

public class Demo : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		var stack = new VisualStack<int>();
		stack.onPeek.AddListener((s, val) => { print(s + " " + val); });

		stack.Push(2);
		stack.Peek();
	}

	// Update is called once per frame
	void Update()
	{
	}
}