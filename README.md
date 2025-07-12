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
 
```shell
D:\project\personalProject\sylais\App\BinaryDependencies\whisper.cpp\build\bin\Release>whisper-cli.exe -f jfk.mp3 -m ..\..\..\models\ggml-base.en.bin
whisper_init_from_file_with_params_no_state: loading model from '..\..\..\models\ggml-base.en.bin'
whisper_init_with_params_no_state: use gpu    = 1
whisper_init_with_params_no_state: flash attn = 0
whisper_init_with_params_no_state: gpu_device = 0
whisper_init_with_params_no_state: dtw        = 0
whisper_init_with_params_no_state: devices    = 1
whisper_init_with_params_no_state: backends   = 1
whisper_model_load: loading model
whisper_model_load: n_vocab       = 51864
whisper_model_load: n_audio_ctx   = 1500
whisper_model_load: n_audio_state = 512
whisper_model_load: n_audio_head  = 8
whisper_model_load: n_audio_layer = 6
whisper_model_load: n_text_ctx    = 448
whisper_model_load: n_text_state  = 512
whisper_model_load: n_text_head   = 8
whisper_model_load: n_text_layer  = 6
whisper_model_load: n_mels        = 80
whisper_model_load: ftype         = 1
whisper_model_load: qntvr         = 0
whisper_model_load: type          = 2 (base)
whisper_model_load: adding 1607 extra tokens
whisper_model_load: n_langs       = 99
whisper_model_load:          CPU total size =   147.37 MB
whisper_model_load: model size    =  147.37 MB
whisper_backend_init_gpu: no GPU found
whisper_init_state: kv self size  =    6.29 MB
whisper_init_state: kv cross size =   18.87 MB
whisper_init_state: kv pad  size  =    3.15 MB
whisper_init_state: compute buffer (conv)   =   16.26 MB
whisper_init_state: compute buffer (encode) =   85.86 MB
whisper_init_state: compute buffer (cross)  =    4.65 MB
whisper_init_state: compute buffer (decode) =   96.35 MB

system_info: n_threads = 4 / 8 | WHISPER : COREML = 0 | OPENVINO = 0 | CPU : SSE3 = 1 | SSSE3 = 1 | AVX = 1 | AVX2 = 1 | F16C = 1 | FMA = 1 | OPENMP = 1 | REPACK = 1 |

main: processing 'jfk.mp3' (177984 samples, 11.1 sec), 4 threads, 1 processors, 5 beams + best of 5, lang = en, task = transcribe, timestamps = 1 ...


[00:00:00.000 --> 00:00:11.000]   And so, my fellow Americans, ask not what your country can do for you, ask what you can do for your country.

whisper_print_timings:     load time =   227.52 ms
whisper_print_timings:     fallbacks =   0 p /   0 h
whisper_print_timings:      mel time =    16.69 ms
whisper_print_timings:   sample time =   149.74 ms /   137 runs (     1.09 ms per run)
whisper_print_timings:   encode time =  2096.47 ms /     1 runs (  2096.47 ms per run)
whisper_print_timings:   decode time =    13.31 ms /     2 runs (     6.66 ms per run)
whisper_print_timings:   batchd time =   386.26 ms /   131 runs (     2.95 ms per run)
whisper_print_timings:   prompt time =     0.00 ms /     1 runs (     0.00 ms per run)
whisper_print_timings:    total time =  2926.72 ms
```
  
</details>


## Llama.cpp POC Loc 

## Piper POC Log

<details>
  <summary>Expand....</summary>

- to use piper, you only need compiled binary (relased under their github), and simple echo command

```shell
D:\project\personalProject\piper>echo 'hello piper' | piper -m en_GB-jenny_dioco-medium.onnx -c en_GB-jenny_dioco-medium.onnx.json
[2025-06-20 07:41:52.944] [piper] [info] Loaded voice in 0.3411954 second(s)
[2025-06-20 07:41:52.965] [piper] [info] Initialized piper
[2025-06-20 07:41:52.965] [piper] [info] Output directory: D:\project\personalProject\piper
D:\project\personalProject\piper\1750380112966465700.wav
[2025-06-20 07:41:53.068] [piper] [info] Real-time factor: 0.086504291015625 (infer=0.0703019 sec, audio=0.8126984126984127 sec)
[2025-06-20 07:41:53.069] [piper] [info] Terminated piper
```
  
</details>


## Logs 

- 20 juni 2025 ⏰ 06:14, able to build whisper and run example (also my own recorded audio file), and its just work even on low quality audio (low quality microphone), here is the output, as you can see its only take about 3 seconds for 17 second recording.
- 10 juli ⏰ 23:10, able to run with `spectre.console` and running transcribe, audio record is using SoundFlow

```shell
Recording... Press any key to stop.
Recording stopped. Saved to D:\project\personalProject\sylais\App\./AudioSample\output.wav
Playing Recorded Audio
Playing...

[00:00:00.000 --> 00:00:03.240]   Hello, who are you? I'm Tim, thank you.
```
- 12 Juli ⏰ 20:20, make update to project, suddently piper is moved to use python and not cpp compiled binary.
- 13 juli ⏰ 05:36, usefull note
  - https://huggingface.co/rhasspy/piper-voices/tree/main/en/en_US/hfc_female/medium
  - https://github.com/ggml-org/whisper.cpp/releases/tag/v1.7.6
  - https://github.com/OHF-Voice/piper1-gpl/blob/main/docs/API_HTTP.md


## Flow

- SoundFlow 
- record wav 
- whisper (with http or cli) 
- get audio 
- llama.cpp 
- get text 
- piper 
- get wav 
- play with SoundFlow

## Reference 

- [whisper.cpp](https://github.com/ggml-org/whisper.cpp)
- [llama.cpp](https://github.com/ggml-org/llama.cpp)
- [piper](https://github.com/rhasspy/piper)
- [SoundFlow](https://github.com/LSXPrime/SoundFlow)

<sub>Work In Progress, Made with ♥️ by AH...</sub>
