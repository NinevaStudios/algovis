using System.Collections;
using DG.Tweening;
using Nineva;
using UnityEngine;

namespace DefaultNamespace
{
	public static class StackUtil
	{
		public static IEnumerator PopFromXPushToY<T>(VisualStack<T> x, VisualStack<T> y)
		{
			var item = x.Pop();
			var tween = item.go.transform
				.DOMove(y.GetNextItemPos(), 1f)
				.OnComplete(() => { GameObject.Destroy(item.go); });
			yield return tween.WaitForCompletion();

			y.Push(item.val);
		}

		public static IEnumerator PushToStack<T>(VisualStack<T>.StackItem item, VisualStack<T> stack)
		{
			yield return item.go.transform.DOMove(stack.GetNextItemPos(), 1f).WaitForCompletion();
			Object.Destroy(item.go);
			stack.Push(item.val);
		}
	}
}