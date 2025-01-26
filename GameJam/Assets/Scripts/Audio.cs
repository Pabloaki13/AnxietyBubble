using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Audio : MonoBehaviour {

    public static Audio instance;  // Externally refer to this & play themes with (GameObjectName).instance.(play function)
    [Header("Plz add AudioSource components, no need to assign them:")]
    [SerializeField] int totalSourcesMusic = 2;
    [SerializeField] int totalSourcesSFX = 4;
    AudioSource[] sourceMusic;
    AudioSource[] sourceSFX;
    Dictionary<string, AudioClip> clipsMusic = new Dictionary<string, AudioClip>();
    Dictionary<string, AudioClip> clipsSFX = new Dictionary<string, AudioClip>();

    private void Update() {
        if (Input.GetKeyDown(KeyCode.G)) {
            PlaySFX("babycry");
        }
        if (Input.GetKeyDown(KeyCode.H)) {
            PlaySFX("crowd");
        }
        if (Input.GetKeyDown(KeyCode.J)) {
            PlaySFX("doorbell");
        }
        if (Input.GetKeyDown(KeyCode.K)) {
            PlaySFX("steps");
        }
        if (Input.GetKeyDown(KeyCode.L)) {
            PlaySFX("shopCardPush");
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            PlayMusic("Shop");
        }
    }

    private void Awake() {
        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }
        DontDestroyOnLoad(gameObject);

        LoadClipsMusic();
        LoadClipsSFX();

        if (gameObject.GetComponent<AudioSource>() == null) {
            Debug.LogError("No AudioSources found on; " + gameObject.name);
        } else {
            AudioSource[] allAudioSources = gameObject.GetComponents<AudioSource>();
            if (allAudioSources.Length != totalSourcesMusic + totalSourcesSFX) {
                Debug.LogError("No exact AudioSources given; You Generated " + allAudioSources.Length + " instead of " + totalSourcesMusic + totalSourcesSFX);
            } else {
                sourceMusic = new AudioSource[totalSourcesMusic];
                sourceSFX = new AudioSource[totalSourcesSFX];
                for (int current = 0; current < totalSourcesMusic; current++) {
                    sourceMusic[current] = allAudioSources[current];
                }
                for (int current = 0; current < totalSourcesSFX; current++) {
                    sourceSFX[current] = allAudioSources[current + totalSourcesMusic];
                }
            }
        }
	    sourceMusic[0].loop = true;

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "SuperMArket") {
            StartCoroutine("ShoppingMusic");
        } else if (scene.name == "SuperMArket") {
            StartCoroutine("IntroMusic");
        }
        //StartCoroutine("music"); //Ex: Multiple music plays feature (if not waiting for scene load, all audio sources say ".isPlaying = true")
    }

    //if not waiting, all audio souces say '.isPlaying = true', no joke
    IEnumerator ShoppingMusic() { 
        yield return new WaitForSeconds(2);
        PlayMusic("shopAmbience");
        yield return new WaitForSeconds(2);
        PlayMusic("ShopTheme", false);
    }

    IEnumerator IntroMusic() {
        yield return new WaitForSecondsRealtime(2);
        PlayMusic("nightindoor");
    }

    private void LoadClipsMusic() {
        clipsMusic["shopAmbience"] = Resources.Load<AudioClip>("Music/supermarket-ambience-17419");
        clipsMusic["shopTheme"] = Resources.Load<AudioClip>("Music/shoppingthemehifi");

        clipsMusic["nightindoor"] = Resources.Load<AudioClip>("Music/ambience-night-rural-campground-new-zeala-17140");

        foreach (var clip in clipsMusic) {
            if (clip.Value == null) Debug.LogWarning("LoadClipsMusic; File of key " + clip.Key + " not detected");
        }
    }

    private void LoadClipsSFX() {
        clipsSFX["babycry"] = Resources.Load<AudioClip>("SFX/baby-crying-64996");
        clipsSFX["mercadona"] = Resources.Load<AudioClip>("SFX/supermercadoost");
        clipsSFX["steps"] = Resources.Load<AudioClip>("SFX/foot-steps-250627");
        clipsSFX["shopCardPush"] = Resources.Load<AudioClip>("SFX/pushing-grocery-cart-63821");

        clipsSFX["doorbell"] = Resources.Load<AudioClip>("SFX/doorbell");
        clipsSFX["bubblePop"] = Resources.Load<AudioClip>("SFX/bubble-pops-272427");
        clipsSFX["phoneCall"] = Resources.Load<AudioClip>("SFX/phone-call-71976");

        foreach (var clip in clipsSFX) {
            if (clip.Value == null) Debug.LogWarning("LoadClipsSFX; File of key " + clip.Key + " not detected");
        }
    }

    public void PlayMusic(string clipName, bool overridePlayingMusic = true, int overridePlayerNumber = 0) {
        if (clipsMusic.ContainsKey(clipName)) {
            if (overridePlayingMusic) {
                sourceMusic[overridePlayerNumber].clip = clipsMusic[clipName];
                sourceMusic[overridePlayerNumber].Play();
            } else {
                bool isMusicPlaying = false;
                foreach (AudioSource player in sourceMusic) {
                    if (player.isPlaying == false) {
                        player.clip = clipsMusic[clipName];
                        player.Play();
                        isMusicPlaying = true;
                        break;
                    }
                }
                if (isMusicPlaying == false) Debug.LogWarning("PlayMusic; No audio player was free");
            }
        } else Debug.LogWarning("clipsMusic; No key of name: " + clipName);
    }

    public void PlaySFX(string clipName) {
        if (clipsSFX.ContainsKey(clipName)) {
            bool isSFXPlaying = false;
            foreach (AudioSource player in sourceSFX) {
                if (player.isPlaying == false) {
                    player.clip = clipsSFX[clipName];
                    player.Play();
                    isSFXPlaying = true;
                    break;
                }
            }
            if (isSFXPlaying == false) Debug.LogWarning("PlaySFX; No audio player was free");
        } else Debug.LogWarning("clipsSFX; No key of name: " + clipName);
    }

}
