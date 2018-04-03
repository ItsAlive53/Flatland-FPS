using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEggsManager : MonoBehaviour {
    private Dictionary<Action, KeySequence> sequences;

    private void Start() {
        sequences = new Dictionary<Action, KeySequence>();
    }

    private void Update() {
        foreach (var s in sequences) {
            if (s.Value.HasFinished()) {
                if (s.Key != null) {
                    s.Key();
                }
            }
        }
    }
}
