using UnityEngine;

[CreateAssetMenu(fileName = "GameSounds", menuName = "Breakout/Game Sounds")]
public class GameSounds : ScriptableObject
{
    public AudioClip launchSound; 
    public AudioClip bounceSound; 
    public AudioClip brickBreakSound; 
    public AudioClip ballLostSound; 
    public AudioClip buffApplySound;
}
