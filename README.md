# [Avoidable][Won't Fix] Unity-Bug-Report-Playable-IN-35780

**Unity has stated that they will not fix this bug.**

> RESOLUTION NOTE:
Thank you for bringing this issue to our attention. Unfortunately, after careful consideration we will not be addressing your issue at this time, as we are currently committed to resolving other higher-priority issues, as well as delivering the new animation system. Our priority levels are determined by factors such as the severity and frequency of an issue and the number of users affected by it. However we know each case is different, so please continue to log any issues you find, as well as provide any general feedback on our roadmap page to help us prioritize.

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
