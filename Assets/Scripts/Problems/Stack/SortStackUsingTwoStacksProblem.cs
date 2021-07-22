using System.Collections;
using DefaultNamespace;
using DefaultNamespace.Problems.Common;
using Nineva;
using UnityEngine;
using UnityEngine.Assertions;

public class SortStackUsingTwoStacksProblem : MonoBehaviour, IProblem
{
	[SerializeField] [Range(0.1f, 2f)] float _speed = 0.5f;

	[SerializeField] GameObject tmpLocation;
	[SerializeField] VisualStackInt s1;
	[SerializeField] VisualStackInt s2;

	IEnumerator Sort()
	{
		Assert.IsTrue(s2.IsEmpty, "Second stack must be empty");

		while (!s1.IsEmpty)
		{
			var tmp = s1.Pop();
			yield return tmp.MoveTo(tmpLocation, _speed);
			while (!s2.IsEmpty && s2.Peek().val > tmp.val)
			{
				yield return s2.PopAndPushTo(s1, _speed);
			}

			yield return tmp.PushToStack(s2, _speed);
		}

		while (!s2.IsEmpty)
		{
			yield return s2.PopAndPushTo(s1, _speed);
		}
	}

	public void Reset()
	{
		throw new System.NotImplementedException();
	}

	public void RunSimulation()
	{
		StartCoroutine(Sort());
	}
}