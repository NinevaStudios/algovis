using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VisualStackItem : MonoBehaviour
{
	[SerializeField] TextMeshPro _textMesh;

	public string Text
	{
		set => _textMesh.text = value;
	}
}
