using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveReference : MonoBehaviour
{
    private List<List<KeyValuePair<int, int>>> _waves;
    private int _idx = 0;
    public int Current => _idx;

    public List<KeyValuePair<int, int>> Wave(int waveNumber)
    {
        // Check if Wave is available
        if (waveNumber < 0)
            throw new ArgumentOutOfRangeException(nameof(waveNumber), $"Wave must be a positive integer");
        if (_waves.Count <= waveNumber)
            throw new ArgumentOutOfRangeException(nameof(waveNumber), $"Wave {waveNumber} too high");

        // Return the requested Wave
        return _waves[waveNumber];
    }

    public List<KeyValuePair<int, int>> Next()
    {
        // Check to see if end of list
        if (_waves.Count <= _idx)
        {
            throw new IndexOutOfRangeException("There are no more waves to play");
        }

        // Return the wave
        return _waves[_idx++];
    }

    private void Awake()
    {
        // Create the main List
        _waves = new();

        // Add the Waves
        _waves.Add(new() { new(0, 02), new(1, 00), new(2, 0), new(3, 0) });
        _waves.Add(new() { new(0, 05), new(1, 00), new(2, 0), new(3, 0) });
        _waves.Add(new() { new(0, 10), new(1, 01), new(2, 0), new(3, 0) });
        _waves.Add(new() { new(0, 08), new(1, 03), new(2, 0), new(3, 0) });
        _waves.Add(new() { new(0, 05), new(1, 02), new(2, 1), new(3, 0) });
        _waves.Add(new() { new(0, 00), new(1, 01), new(2, 2), new(3, 0) });
        _waves.Add(new() { new(0, 05), new(1, 00), new(2, 2), new(3, 0) });
        _waves.Add(new() { new(0, 00), new(1, 00), new(2, 0), new(3, 1) });
        _waves.Add(new() { new(0, 00), new(1, 04), new(2, 2), new(3, 0) });
        _waves.Add(new() { new(0, 20), new(1, 10), new(2, 5), new(3, 3) });
    }
}