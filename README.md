# Unity-Bug-Report-Playable-IN-35780

## About this issue

GameObject does not stop moving when being moved by an `AnimationClipPlayable` and the `AnimationClipPlayable` is paused.

![Sample](./imgs~/img_sample.gif)

## How to reproduce

1. Open the "SampleScene".
2. Enter play mode, and you will see the character walking in the Game view.
3. Click the "Toggle Playable" button, and you will see the character's animation paused, but the character's transform **unexpectedly** keep changing.
4. [Temporary fix] Click the "Toggle Playable" button again, and you will see the character's animation resume.
5. [Temporary fix] Click the "Toggle Playable and SetTime" button, and you will see the character's animation resume, and the character's transform remains the same as expected.

## Solution

This issue can be temporarily fixed by calling `animationClipPlayable.SetTime(animationClipPlayable.GetTime())`
after `animationClipPlayable.Pause()` has been called.

```csharp
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
```
