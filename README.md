<h1 align="center">OpenAI Text-to-Speech Integration for Unity</h1>

<div align="center">
  <img src="https://github.com/mapluisch/OpenAI-Text-To-Speech-for-Unity/assets/31780571/ae08c0ba-cb60-4b6f-a904-bcdcc059893c" width="70%"/>
  <p><em>Implementation of OpenAI's Text-To-Speech in Unity - synthesize any text and play it via any AudioSource.</em></p>
</div>

---

## Overview
This project integrates OpenAI's Text-to-Speech API into any Unity project, allowing users to convert and synthesize text to spoken audio via any AudioSource component within seconds.

It *does not use third-party libraries*, making it super lightweight and easy to use cross-platform.

### What's new in v.1.1.
- Added a new custom editor script for easily setting up TTS within your Unity project (see demo videos below)

## Demos

Once you've installed this project, setting up OpenAI's TTS within Unity takes seconds. 

Integrate this project (either in your existing project, or in a fresh one), then open up the `TTS Setup` Prefab and click through the steps, as shown in this demo:

https://github.com/mapluisch/OpenAI-Text-To-Speech-for-Unity/assets/31780571/e6dc69b9-d0b5-4af5-9844-5d336551cf10

I've also added a quick UI example scene, so you can tinker around with some TTS settings easily:

https://github.com/mapluisch/OpenAI-Text-To-Speech-for-Unity/assets/31780571/aadf35dc-453e-4f55-8930-f9169e3b185e

### Setup
0. Download the latest release `.unitypackage`.
1. Import it into your own project, e.g. via `Assets > Import Package`.
2. Either open the `OpenAI-TTS-Example` scene, or open up the `TTS Setup` Prefab and click through the installation steps.
- Optional: Change the `TTSManager` Prefab settings to your liking (useful if you want to have different entities with predefined voices, speeds, etc.)
3. Reference the `TTSManager` and call `TTSManager.SynthesizeAndPlay` via script.

### Using OpenAI TTS in your own scene
0. Open up the `TTS Setup` Prefab.
1. Add your OpenAI API key first, then click through the installation steps.
- Optional: Change the `TTSManager` Prefab settings to your liking (useful if you want to have different entities with predefined voices, speeds, etc.)
2. Reference a `TTSManager` and call `TTSManager.SynthesizeAndPlay` via script.

### Disclaimer
This project is a prototype and serves as a basic example of integrating OpenAI's TTS API with Unity. Feel free to create a PR 😊
