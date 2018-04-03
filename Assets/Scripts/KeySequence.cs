using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeySequence {
    private List<KeyCode> sequence;
    private int index;

    /// <summary>
    /// Updates the sequence index and checks if it reached the end, returns true if it does
    /// </summary>
    /// <returns>True if sequence reached end</returns>
    public bool HasFinished() {
        if (sequence != null) {
            if (!Input.anyKeyDown) return false;

            if (Input.GetKeyDown(sequence[index])) {
                index++;
            } else if (Input.GetKeyDown(sequence[0])) {
                index = 1;
            } else {
                index = 0;
            }

            if (index >= sequence.Count) {
                index = 0;
                return true;
            }
        } else {
            Debug.LogError("Null sequence, did you forget to initialise with a sequence?");
        }
        return false;
    }

    public void SetSequence(IEnumerable<KeyCode> sequence) {
        this.sequence = sequence.ToList();
    }

    /// <summary>
    /// Creates a handler for a key sequence
    /// </summary>
    /// <param name="sequence"></param>
    public KeySequence(IEnumerable<KeyCode> sequence) {
        this.sequence = sequence.ToList();
        this.index = 0;
    }

    public KeySequence(params KeyCode[] key) : this(key.ToList()) {
    }

    /// <summary>
    /// [Debug only] Creates a default key sequence with the sequence A, B, C
    /// </summary>
    public KeySequence() : this(new KeyCode[] { KeyCode.A, KeyCode.B, KeyCode.C }) {
    }
}
