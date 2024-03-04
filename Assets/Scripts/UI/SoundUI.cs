using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundUI : MonoBehaviour
{
    public static SoundUI Instance { get; private set; }
    private AudioSource _audio;
    private int priority;

    [SerializeField] private AudioClip ErrorClip;
    [SerializeField] private AudioClip Win;
    [SerializeField] private AudioClip Lose;
    [SerializeField] private AudioClip Click;
    [SerializeField] private AudioClip positiveSoundClip;
    [SerializeField] private AudioClip Tick;
    [SerializeField] private AudioClip Pop;
    [SerializeField] private AudioClip BeepTick;
    [SerializeField] private AudioClip BeepOut;
    [SerializeField] private AudioClip Success;
    [SerializeField] private AudioClip Success2;
    [SerializeField] private AudioClip Success3;
    [SerializeField] private AudioClip Cash;
    [SerializeField] private AudioClip BlowCells;
    [SerializeField] private AudioClip FruitBlow;
    [SerializeField] private AudioClip BerryBlow;
    [SerializeField] private AudioClip SmallBlow;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        _audio = GetComponent<AudioSource>();
    }

    public void PlayUISound(SoundsUI _type)
    {
        PlayUISound(_type, 1f);                
    }

    private void Update()
    {
        if (priority > 0 && !_audio.isPlaying)
        {
            priority = 0;
        }
    }

    public AudioClip GetAudioClip(SoundsUI _type)
    {
        switch(_type)
        {
            case SoundsUI.fruit_blow:
                return FruitBlow;

            case SoundsUI.berry_blow:
                return BerryBlow;
        }

        return null;
    }

    public void PlayUISound(SoundsUI _type, float volume)
    {
        _audio.volume = volume;
        _audio.pitch = 1;

        switch (_type)
        {
            case SoundsUI.tick:
                if (priority > 0) break;
                _audio.Stop();
                _audio.clip = Tick;
                _audio.Play();
                break;

            case SoundsUI.error:
                if (priority > 0) break;
                _audio.Stop();
                _audio.clip = ErrorClip;
                _audio.Play();
                break;

            case SoundsUI.pop:
                if (priority > 0) break;
                _audio.Stop();
                _audio.clip = Pop;
                _audio.Play();
                break;

            case SoundsUI.click:
                if (priority > 0) break;
                _audio.Stop();
                _audio.clip = Click;
                _audio.Play();
                break;

            case SoundsUI.positive:
                if (priority > 0) break;
                _audio.Stop();
                _audio.clip = positiveSoundClip;
                _audio.Play();
                break;

            case SoundsUI.win:
                priority = 2;
                _audio.Stop();
                _audio.clip = Win;
                _audio.Play();
                break;

            case SoundsUI.lose:
                priority = 2;
                _audio.Stop();
                _audio.clip = Lose;
                _audio.Play();
                break;

            case SoundsUI.beep_tick:
                if (priority > 0) break;
                _audio.Stop();   
                _audio.pitch = 1.5f;
                _audio.clip = BeepTick;
                _audio.Play();
                break;

            case SoundsUI.beep_out:
                if (priority > 0) break;
                _audio.Stop();
                _audio.pitch = 0.8f;
                _audio.clip = BeepOut;
                _audio.Play();
                break;

            case SoundsUI.success:
                priority = 2;
                _audio.Stop();
                _audio.pitch = 1f;
                _audio.clip = Success;
                _audio.Play();
                break;

            case SoundsUI.success2:
                priority = 2;
                _audio.Stop();
                _audio.pitch = 1f;
                _audio.clip = Success2;
                _audio.Play();
                break;

            case SoundsUI.success3:
                priority = 2;
                _audio.Stop();
                _audio.pitch = 1f;
                _audio.clip = Success3;
                _audio.Play();
                break;

            case SoundsUI.cash:
                if (priority > 0) break;
                _audio.Stop();
                _audio.pitch = 1f;
                _audio.clip = Cash;
                _audio.Play();
                break;

            case SoundsUI.blow_cells:
                if (priority > 1) break;
                priority = 1;
                _audio.Stop();
                _audio.pitch = 1f;
                _audio.clip = BlowCells;
                _audio.Play();
                break;

            case SoundsUI.fruit_blow:
                if (priority > 0) break;
                _audio.Stop();
                _audio.pitch = 1f;
                _audio.clip = FruitBlow;
                _audio.Play();
                break;

            case SoundsUI.berry_blow:
                if (priority > 0) break;
                _audio.Stop();
                _audio.pitch = 1f;
                _audio.clip = BerryBlow;
                _audio.Play();
                break;

            case SoundsUI.small_blow:
                if (priority > 1) break;
                priority = 1;
                _audio.Stop();
                _audio.pitch = 1f;
                _audio.clip = SmallBlow;
                _audio.Play();
                break;


        }
    }
}

public enum SoundsUI
{
    none,
    error,
    positive, 
    tick,
    pop,
    click,
    win,
    lose,
    beep_tick,
    beep_out, 
    success,
    cash,
    success2,
    success3,
    blow_cells,
    fruit_blow,
    berry_blow,
    small_blow
}
