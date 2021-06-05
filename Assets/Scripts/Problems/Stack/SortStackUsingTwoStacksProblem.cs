using System.Collections;
using System.Collections.Generic;
using Nineva;
using UnityEngine;

public class SortStackUsingTwoStacksProblem : MonoBehaviour
{
    [SerializeField] VisualStackInt s1;
    [SerializeField] VisualStackInt s2;
    
		void Start()
		{
			StartCoroutine(Run());
		}

		IEnumerator Run()
		{
			yield return PushAnimated(1, new WaitForSeconds(0.5f));
			yield return PushAnimated(2, new WaitForSeconds(0.5f));
			yield return PushAnimated(3, new WaitForSeconds(0.5f));
			yield return PushAnimated(4, new WaitForSeconds(0.5f));
			yield return PushAnimated(5, new WaitForSeconds(0.5f));

			yield return PopAnimated(new WaitForSeconds(0.5f));
			yield return PopAnimated(new WaitForSeconds(0.5f));
			yield return PopAnimated(new WaitForSeconds(0.5f));
			yield return PopAnimated(new WaitForSeconds(0.5f));
			yield return PopAnimated(new WaitForSeconds(0.5f));
		}

		IEnumerator PopAnimated(WaitForSeconds waitForSeconds)
		{
			yield return waitForSeconds;
			var gameObj = s1.Pop().Item2;
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

		IEnumerator PushAnimated(int item, WaitForSeconds waitFor)
		{
			s1.Push(item);
			yield return waitFor;
		}
}
