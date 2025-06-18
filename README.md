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
