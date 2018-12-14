using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains only a dictionary to save the number of stars gotten in each level
/// </summary>
[System.Serializable]
public class Save {
	public Dictionary<int, int> stars = new Dictionary<int, int>();
}