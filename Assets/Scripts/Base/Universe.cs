using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Universe : MonoBehaviour
{
    public static List<Attractor> attractors = new List<Attractor>();
    public static List<Attractable> attractables = new List<Attractable>();

    public const float G = 100f;
}
