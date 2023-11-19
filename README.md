# OpenAI Text-to-Speech Integration for Unity

## Overview
This project integrates OpenAI's Text-to-Speech API into a Unity application, allowing users to convert and synthesize text to spoken audio within Unity via any AudioSource component.
Tested with Unity version 2022.3.4f1.

### Setup
1. Open the main Unity Scene.
2. Input your OpenAI API key in the `OpenAI` GameObject.
3. Configure TTS model and voice settings within the `TTSManager` of both entities within the inspector.

### Running the Application
Enter text into the UI's input field and click the Talk-button to hear the spoken audio. The text is sent to OpenAI's TTS API, processed, and the resulting audio is played through an `AudioSource` in Unity.

### Disclaimer
This project is a prototype and serves as a basic example of integrating OpenAI's TTS API with Unity. Feel free to create a PR.
