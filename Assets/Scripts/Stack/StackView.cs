using System.Collections;
using UnityEngine;

namespace Nineva
{
	public class StackView : MonoBehaviour
	{
		public GameObject guy;

		public VisualStack<int> stack;

		void Start()
		{
			stack.onPush.AddListener(OnPush);
			stack.onPop.AddListener(OnPop);

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
			var gameObj = stack.Pop().Item2;
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
			stack.Push(item);
			yield return waitFor;
		}

		void OnPop(GameObject go, int val)
		{
			print("pop: " + val);
		}

		void OnPush(GameObject go, int val)
		{
			print("push: " + val);
		}
	}
}