using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using UnityEngine.UI;

// About this issue:
// 
// GameObject does not stop moving when being moved by an `AnimationClipPlayable` and the `AnimationClipPlayable` is paused.
// 
// This issue can be temporarily fixed by calling `animationClipPlayable.SetTime(animationClipPlayable.GetTime())`
// after `animationClipPlayable.Pause()` has been called.
//
// How to reproduce:
// 1. Open the "SampleScene".
// 2. Enter play mode, and you will see the character walking in the Game view.
// 3. Click the "Toggle Playable" button, and you will see the character's animation paused,
//    but the character's transform **unexpectedly** keep changing.
// 4. [Temporary fix] Click the "Toggle Playable" button again, and you will see the character's animation resume.
// 5. [Temporary fix] Click the "Toggle Playable and SetTime" button, and you will see the character's animation resume,
//    and the character's transform remains the same as expected.

[RequireComponent(typeof(Animator))]
public class AnimPlayablePauseTest : MonoBehaviour
{
    public Text playableText;
    public Text positionText;
    public Text rotationText;
    public AnimationClip clip;

    private Animator _animator;
    private PlayableGraph _graph;
    private Playable _clipPlayable;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _graph = PlayableGraph.Create("Anim Playable Pause Test");
        _graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);

        // _clipPlayable -> output
        _clipPlayable = AnimationClipPlayable.Create(_graph, clip);
        var output = AnimationPlayableOutput.Create(_graph, "Anim Output", _animator);
        output.SetSourcePlayable(_clipPlayable);

        _graph.Play();
    }

    private void LateUpdate()
    {
        playableText.text = $"PlayState: {_clipPlayable.GetPlayState()}";
        positionText.text = $"Position: {transform.position}";
        rotationText.text = $"Rotation: {transform.rotation.eulerAngles}";
    }

    private void OnDestroy()
    {
        _graph.Destroy();
    }

    public void TogglePlayable(bool temporarilyFix)
    {
        if (_clipPlayable.GetPlayState() == PlayState.Playing) // Pause
        {
            if (temporarilyFix)
            {
                ((AnimationClipPlayable)_clipPlayable).Pause();
            }
            else
            {
                _clipPlayable.Pause();
            }
        }
        else // Play
        {
            _clipPlayable.Play();
        }
    }
}