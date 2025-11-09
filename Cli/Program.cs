using System;
using System.Diagnostics;
using Sylais.TTS;
using SoundFlow.Backends.MiniAudio;
using Sylais.AudioUtilities;
using Sylais.STT;

// test to run kokoro
//
// KokoroTester.RunSample();

// test to capture and playback audio device
//
// var path = Path.Combine(Directory.GetCurrentDirectory(), "output.wav");
//
// using var audioEngine = new MiniAudioEngine();
// using var audioUtil = new AudioUtility(audioEngine);
//
// audioUtil.CaptureAudio();
// audioUtil.PlayAudio(path);


var path = Path.Combine(Directory.GetCurrentDirectory(), "output.wav");

using var audioEngine = new MiniAudioEngine();
using var audioUtil = new AudioUtility(audioEngine);

audioUtil.CaptureAudio();
audioUtil.PlayAudio(path);

// test to run whisper net 
// var whisperNet = new WhisperNet();
//
// var text = await whisperNet.RunTest();
// Console.WriteLine(text);
// await KokoroTester.Speak(text);

// text = await whisperNet.RunTest("LJ001-0002.wav");
// KokoroTester.Speak(text);
