using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace Nineva
{
	[Serializable]
	public class VisualStack<T>
	{
		[Serializable]
		public class OnPeekEvent : UnityEvent<GameObject, T>
		{
		}

		[Serializable]
		public class OnPopEvent : UnityEvent<GameObject, T>
		{
		}

		[Serializable]
		public class OnPushEvent : UnityEvent<GameObject, T>
		{
		}

		readonly Stack<Tuple<T, GameObject>> _stack = new Stack<Tuple<T, GameObject>>();
		public OnPeekEvent onPeek = new OnPeekEvent();
		public OnPopEvent onPop = new OnPopEvent();
		public OnPushEvent onPush = new OnPushEvent();

		public Tuple<T, GameObject> Peek()
		{
			var val = _stack.Peek();
			onPeek.Invoke(val.Item2, val.Item1);
			return val;
		}

		public Tuple<T, GameObject> Pop()
		{
			var val = _stack.Pop();
			onPop.Invoke(val.Item2, val.Item1);
			return val;
		}

		public GameObject Push(T item)
		{
			var go = CreateGameObject(item);
			onPush.Invoke(go, item);
			_stack.Push(new Tuple<T, GameObject>(item, go));
			return go;
		}

		GameObject CreateGameObject(T item)
		{
			var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
			go.name = "Stack item " + item;
			// TODO make better object size + color
			go.transform.position = _stack.Count == 0 ? Vector3.zero : GetNextItemPos();
			return go;
		}

		Vector3 GetNextItemPos()
		{
			var result = _stack.Peek().Item2.transform.position;
			return new Vector3(result.x, result.y + 1, result.z);
		}
	}
}