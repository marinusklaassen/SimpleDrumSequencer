# Simple Drum Sequencer - [SDS-101]

I created SDS-101 as proof of concept to learn Xamarin Forms, cross platform (mobile) app development, & MVVM more in depth.

One major drawback is that the audio part is problematic due to performance and latency issues of the standard OS audio API's. I use a 3rd party wrapper https://www.nuget.org/packages/Xam.Plugin.SimpleAudioPlayer/ to play audio samples. Maybe when I find the time I build cut my own audio engine for realtime audio processsing.  

Anyway at least the interface looks quite slick :-)... Maybe in the future I will rebuild the app in SuperCollider with some actual realtime synthesis.

For now I got it running for UWP on Windows 10. 

![Alt text](SDS-101-Screenshot.png?raw=true "SDS-101 (screenshot)")

Next up: 
  * More async event handlers and commands
  * Calling generic on load methods in the viewmodels
  * Drumpad page, about pages & navigation
  * Gitflow branching strategy
  * Azure build pipelines (CI&CD) 
  * Android & IOS support 
  * Reformatting & reusability
