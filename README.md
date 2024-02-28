# OpenAI Text-to-Speech Integration for Unity

<p align="center">
  <img src="https://github.com/mapluisch/OpenAI-Text-To-Speech-for-Unity/assets/31780571/1f1ccb2f-f780-4f18-b347-d05ac25b6875" width="70%"/>
</p>


---

## Overview
This project integrates OpenAI's Text-to-Speech API into a Unity application, allowing users to convert and synthesize text to spoken audio within Unity via any AudioSource component.
Tested with Unity version 2022.3.13f1.

## Demo Video
https://github.com/mapluisch/OpenAI-Text-To-Speech-for-Unity/assets/31780571/29c19c4f-8d7a-40e2-bc4e-c7b7337520e2

### Setup
1. Download the latest release `.unitypackage`.
2. Import it into your own project, e.g. via `Assets > Import Package`.
3. Either open the `OpenAI-TTS-Example` scene, or add the necessary Prefabs to your own scene:

### Using OpenAI TTS in your own scene
1. Add `OpenAI` and `TTSManager` Prefabs to your scene.
2. Add your OpenAI API key to the OpenAI-Prefab.
- Optional: Change the `TTSManager` Prefab settings to your liking (useful if you want to have different entities with predefined voices, speeds, etc.)
3. Call `TTSManager.SynthesizeAndPlay` of your `TTSManager` object.

### Disclaimer
This project is a prototype and serves as a basic example of integrating OpenAI's TTS API with Unity. Feel free to create a PR.
