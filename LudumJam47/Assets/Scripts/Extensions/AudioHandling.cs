using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioExtensions
{
    public static class AudioHandling
    {
        // Summary:
        // Stops any playing clips on source and plays new clip
        public static void PlayAudioClipOnSource(AudioSource source, AudioClip clip)
        {
            if (!clip) return;
            if (source.isPlaying) source.Stop();

            source.clip = clip;
            source.Play();
        }

        public static void PlayClipWithRandomPitch(AudioSource source, AudioClip clip, float minPitch, float maxPitch)
        {
            if (!clip) return;
            if (source.isPlaying) source.Stop();

            source.clip = clip;
            source.pitch = Random.Range(minPitch, maxPitch);
            source.Play();
        }
    }
}