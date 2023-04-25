## Requirements
Unity 2021.1 ou later. The project uses IObjectPool to create an AudioSources pooler: https://docs.unity3d.com/2021.1/Documentation/ScriptReference/Pool.IObjectPool_1.html
## Instaled Packages
1 - **Naughtyattributes** - Used to customize the inspector.
https://assetstore.unity.com/packages/tools/utilities/naughtyattributes-129996

2 - **Dotween** - Used to make volume fade.
https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676

## Architecture

The general structure of the project is done using ScriptablesObjects and Service Locator pattern. 
More about ServiceLocator: https://gameprogrammingpatterns.com/service-locator.html

**SoundBankData** - Used to store information such as: sounds, effects, states, etc. This architecture allows us to create several Sound Banks for different themes or parts of a game. For example. We can have a Sound Bank for each level of the game, with that we have the benefit of not having to load all the sounds of the game in the memory at once, in addition to the organization of the project itself.

## How To use
