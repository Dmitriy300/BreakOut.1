using UnityEngine;

[CreateAssetMenu(fileName = "GameSounds", menuName = "Breakout/Game Sounds")]
public class GameSounds : ScriptableObject
{
    public AudioClip launchSound; // «вук запуска м€ча
    public AudioClip bounceSound; // «вук отскока м€ча
    public AudioClip brickBreakSound; // «вук разрушени€ кирпича
    public AudioClip ballLostSound; // «вук потери м€ча
    public AudioClip buffApplySound;
}
