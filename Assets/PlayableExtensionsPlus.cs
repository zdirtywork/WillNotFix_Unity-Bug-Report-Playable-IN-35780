using UnityEngine.Animations;
using UnityEngine.Playables;

public static class PlayableExtensionsPlus
{
    public static void Pause(this AnimationClipPlayable playable)
    {
        PlayableExtensions.Pause(playable);
        playable.SetTime(playable.GetTime());
    }
}