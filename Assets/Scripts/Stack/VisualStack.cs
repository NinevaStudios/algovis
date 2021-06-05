using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace Nineva
{
	[Serializable]
	public class VisualStack<T> : MonoBehaviour
	{
		public class StackItem
		{
			public T val;
			public GameObject go;


		}

		VisualStackItem _stackItemPrefab;

		readonly Stack<StackItem> _stack = new Stack<StackItem>();

		public List<T> initialValues;

		public OnPeekEvent onPeek = new OnPeekEvent();
		public OnPopEvent onPop = new OnPopEvent();
		public OnPushEvent onPush = new OnPushEvent();

		void Awake()
		{
			_stackItemPrefab = Resources.Load<VisualStackItem>("Prefabs/pref_StackItem");
			initialValues.ForEach(x => Push(x));
		}

		public StackItem Peek()
		{
			var item = _stack.Peek();
			onPeek.Invoke(item.go, item.val);
			return item;
		}

		public StackItem Pop()
		{
			var item = _stack.Pop();
			onPop.Invoke(item.go, item.val);
			return item;
		}

		public GameObject Push(T item)
		{
			var go = CreateGameObject(item);
			onPush.Invoke(go, item);
			_stack.Push(new StackItem {go = go, val = item});
			return go;
		}

		public bool IsEmpty => _stack.Count == 0;

		GameObject CreateGameObject(T item)
		{
			var go = Instantiate(_stackItemPrefab, GetNextItemPos(), Quaternion.identity);
			go.name = "Stack item " + item;
			go.Text = item.ToString();
			return go.gameObject;
		}

		public Vector3 GetNextItemPos()
		{
			var stackPos = gameObject.transform.position;
			return new Vector3(stackPos.x, _stack.Count, stackPos.z);
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
	}
}