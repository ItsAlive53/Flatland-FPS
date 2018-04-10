using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EasterEggsManager : MonoBehaviour {
    private Dictionary<Action, KeySequence> sequences;

    private void Awake() {
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

    public void AddSequence(Action resultFunction, IEnumerable<KeyCode> sequence) {
        sequences.Add(resultFunction, new KeySequence(sequence));
    }

    public void AddSequence(Action resultFunction, params KeyCode[] sequence) {
        var tmp = new List<KeyCode>();
        if (sequence == null) return;

        foreach (var k in sequence) {
            tmp.Add(k);
        }
        AddSequence(resultFunction, tmp);
    }
}
