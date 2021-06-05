using System.Collections;
using DG.Tweening;
using Nineva;
using UnityEngine;

namespace DefaultNamespace
{
	public static class StackUtil
	{
		public static IEnumerator PopAndPushTo<T>(this VisualStack<T> x, VisualStack<T> y, float duration = 1f)
		{
			var item = x.Pop();
			var tween = item.go.transform
				.DOMove(y.GetNextItemPos(), duration)
				.OnComplete(() => { Object.Destroy(item.go); });
			yield return tween.WaitForCompletion();

			y.Push(item.val);
		}

		public static IEnumerator PushToStack<T>(this VisualStack<T>.StackItem item, VisualStack<T> stack, float duration = 1f)
		{
			yield return item.go.transform.DOMove(stack.GetNextItemPos(), duration).WaitForCompletion();
			Object.Destroy(item.go);
			stack.Push(item.val);
		}

		public static IEnumerator MoveTo(this VisualStack<int>.StackItem item, GameObject target, float duration = 1f)
		{
			yield return item.go.transform.DOMove(target.transform.position, duration).WaitForCompletion();
		}
	}
}