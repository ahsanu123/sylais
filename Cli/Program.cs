using System;
using System.Diagnostics;
using Sylais.Kokoro;
using SoundFlow.Backends.MiniAudio;
using Sylais.AudioUtilities;

// KokoroTester.RunSample();

var path = Path.Combine(Directory.GetCurrentDirectory(), "output.wav");

using var audioEngine = new MiniAudioEngine();
using var audioUtil = new AudioUtility(audioEngine);

audioUtil.CaptureAudio();
audioUtil.PlayAudio(path);
