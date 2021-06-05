using System.Collections;
using DefaultNamespace;
using DG.Tweening;
using Nineva;
using UnityEngine;
using UnityEngine.Assertions;

public class SortStackUsingTwoStacksProblem : MonoBehaviour
{
	[SerializeField] GameObject tmpLocation;
	[SerializeField] VisualStackInt s1;
	[SerializeField] VisualStackInt s2;

	void Start()
	{
		// StartCoroutine(Run(s1));
		// StartCoroutine(Run(s2));

		// Sort(s1);
	}

	public void OnRunButtonClick()
	{
		StartCoroutine(Sort(s1));
	}

	IEnumerator Run(VisualStackInt stack)
	{
		yield return PushAnimated(1, new WaitForSeconds(0.5f), stack);
		yield return PushAnimated(2, new WaitForSeconds(0.5f), stack);
		yield return PushAnimated(3, new WaitForSeconds(0.5f), stack);
		yield return PushAnimated(4, new WaitForSeconds(0.5f), stack);
		yield return PushAnimated(5, new WaitForSeconds(0.5f), stack);

		yield return PopAnimated(new WaitForSeconds(0.5f), stack);
		yield return PopAnimated(new WaitForSeconds(0.5f), stack);
		yield return PopAnimated(new WaitForSeconds(0.5f), stack);
		yield return PopAnimated(new WaitForSeconds(0.5f), stack);
		yield return PopAnimated(new WaitForSeconds(0.5f), stack);
	}

	IEnumerator PopAnimated(WaitForSeconds waitForSeconds, VisualStackInt stack)
	{
		yield return waitForSeconds;
		var gameObj = stack.Pop().go;
		yield return SmoothLerp(2f, gameObj);
		Destroy(gameObj);
	}

	// just to demo animation
	static IEnumerator SmoothLerp(float time, GameObject target)
	{
		var position = target.transform.position;
		var startingPos = position;
		var finalPos = position + target.transform.right * 5;
		float elapsedTime = 0;

		while (elapsedTime < time)
		{
			target.transform.position = Vector3.Lerp(startingPos, finalPos, elapsedTime / time);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
	}

	IEnumerator PushAnimated(int item, WaitForSeconds waitFor, VisualStackInt stack)
	{
		stack.Push(item);
		yield return waitFor;
	}

	IEnumerator Sort(VisualStackInt s)
	{
		var r = s2;
		Assert.IsTrue(r.IsEmpty, "Second stack must be empty");

		while (!s.IsEmpty)
		{
			var tmp = s.Pop();
			yield return tmp.MoveTo(tmpLocation);
			while (!r.IsEmpty && r.Peek().val > tmp.val)
			{
				yield return r.PopAndPushTo(s);
			}

			yield return tmp.PushToStack(r);
		}
	}
}