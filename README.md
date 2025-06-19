<h1 align="center">🍉Sylais</h1>

<p align="center">
  <img src="./sylais.svg" />
  <p align="center"> 
     Offline AI English Speaking Practice.
  </p>
</p>

## Introduction

Sylais is program to integrate 3 component: whisper.cpp, llama.cpp, and piper. the purpose is simple, convert captured voice into text, then use text for ask to AI model, then capture the answer and convert it back into voice.
I dont want it online, i just want use locally to improve my english conversation, so the simpler the better.

## Whisper.cpp POC Log 

<details>
  <summary>Expand....</summary>

- 20 juni 2025 ⏰ 06:14, able to build whisper and run example (also my own recorded audio file), and its just work even on low quality audio (low quality microphone), here is the output, as you can see its only take about 3 seconds for 17 second recording.

```shell
main: processing 'Recording.mp3' (289537 samples, 18.1 sec), 4 threads, 1 processors, 5 beams + best of 5, lang = en, task = transcribe, timestamps = 1 ...


[00:00:00.000 --> 00:00:10.080]   So hi. My name is Aksano. I'm from Indonesia and I live in Indonesia. Right now, I'm just looking
[00:00:10.080 --> 00:00:17.440]   opportunity to get software developer at foreign country. That's it. Thank you.

whisper_print_timings:     load time =   224.99 ms
whisper_print_timings:     fallbacks =   0 p /   0 h
whisper_print_timings:      mel time =    23.13 ms
whisper_print_timings:   sample time =   257.79 ms /   236 runs (     1.09 ms per run)
whisper_print_timings:   encode time =  1847.78 ms /     1 runs (  1847.78 ms per run)
whisper_print_timings:   decode time =    14.93 ms /     2 runs (     7.47 ms per run)
whisper_print_timings:   batchd time =   659.61 ms /   230 runs (     2.87 ms per run)
whisper_print_timings:   prompt time =     0.00 ms /     1 runs (     0.00 ms per run)
whisper_print_timings:    total time =  3099.74 ms
```
  
</details>


## Llama.cpp POC Loc 

## Piper POC Log

## Flow

- NAudio 
- record wav 
- whisper (with http or cli) 
- get audio 
- llama.cpp 
- get text 
- piper 
- get wav 
- play with NAudio

## Reference 

- [whisper.cpp](https://github.com/ggml-org/whisper.cpp)
- [llama.cpp](https://github.com/ggml-org/llama.cpp)
- [piper](https://github.com/rhasspy/piper)

<sub>Work In Progress, Made with ♥️ by AH...</sub>
