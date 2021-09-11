# Simple Drum Sequencer - [SDS-101]

I created SDS-101 as proof of concept to learn Xamarin Forms, cross platform (mobile) app development, & MVVM more in depth.

One major drawback is that the audio part is problematic due to performance and latency issues of the standard OS audio API's. I use a 3rd party wrapper https://www.nuget.org/packages/Xam.Plugin.SimpleAudioPlayer/ to play audio samples. Maybe when I find the time I build cut my own audio engine for realtime audio processsing.  

Anyway at least the interface looks quite slick :-)... Maybe in the future I will rebuild the app in SuperCollider with some actual realtime synthesis.

For now I got it running for UWP on Windows 10. 

![Alt text](SDS-101-Screenshot.png?raw=true "SDS-101 (screenshot)")

# Status 
The timing of recurring ticks is very problematic. I measured large time differences of around 15 ms. This means that another implementation (audio rate) is needed. Next experimentation will be with the JUCE framework without Xamarin or C#. 

## CI/CD Status

|Branch|Build Status|
|------ |:--------- |
|main   | [![](https://dev.azure.com/marinusklaassen0069/Xamarin%20Forms%20Cross-platform%20Apps/_apis/build/status/marinusklaassen.simpledrumsequencer?branchName=main)](https://dev.azure.com/marinusklaassen0069/Xamarin%20Forms%20Cross-platform%20Apps/_build/latest?definitionId=1&branchName=main) |
|feature/azure-pipelines | [![](https://dev.azure.com/marinusklaassen0069/Xamarin%20Forms%20Cross-platform%20Apps/_apis/build/status/marinusklaassen.simpledrumsequencer?branchName=feature%2Fazure-pipelines)](https://dev.azure.com/marinusklaassen0069/Xamarin%20Forms%20Cross-platform%20Apps/_build/latest?definitionId=1&branchName=feature%2Fazure-pipelines) |

