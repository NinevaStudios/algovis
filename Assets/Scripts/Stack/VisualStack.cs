using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace Nineva
{
	[Serializable]
	public class VisualStack<T> : MonoBehaviour
	{
		VisualStackItem _stackItemPrefab;

		void Awake()
		{
			_stackItemPrefab = Resources.Load<VisualStackItem>("Prefabs/pref_StackItem");
		}

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
			var pos = _stack.Count == 0 ? Vector3.zero : GetNextItemPos();
			var go = Instantiate(_stackItemPrefab, pos, Quaternion.identity);
			go.name = "Stack item " + item;
			go.Text = item.ToString();
			// TODO make better object size + color
			return go.gameObject;
		}

		Vector3 GetNextItemPos()
		{
			var stackPos = gameObject.transform.position;
			return new Vector3(stackPos.x, _stack.Count, stackPos.z);
		}
	}
}