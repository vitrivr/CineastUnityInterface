# README

This is in very early development. Do not use in production.

**Warning: There are missing dependencies in the current project setup**.

## Features

This is a Unity3d endpoint for vitrivr's Cineast RESTful API.

See [Cineast](https://github.com/vitrivr/cineast) for further API informations

## Usage

### Unity Package Manager

To use this package with upm, add the following dependency to your `manifest.json`:

```
"com.vitrivr.unityinterface.cineastapi": "https://github.com/vitrivr/UnityInterface.git#release"
```

### Fresh Unity Project

If you have not yet started a unity project, this one is for you:

 1. Clone this repository
 2. Open this repository's root folder as Unity project
 3. Start developing your project.

You will have to _attach_ the `CineastApi` MonoBehaviour to a gameobject
in order to get the API working.

### Adding to existing Unity Project

If you already have a unity project and want to add the API to that one:

 1. Clone this repository
 2. Open this repostiory's root folder as Unity project
 3. Right click to `CineastUnityInterface` and export this as `.unitypackag`
    (Do not tick 'Include dependencies', as there shouldn't be any dependencies for this API
 4. Switch to your main unity project
 5. Import the previously exportet package: Assets > Import Unity Package...
    and select the one from the step before

You wil have to _attach_ the `CineastApi` MonoBehaviour to a gameobject
in order to get the API working.

### API

_coming soon_

## Contribution

One can contribute to this project by sumbitting a PR.

### Publishing

This API is published as a Unity Package Manager (_upm_) ready package.
To make it available to _upm_, one has to issue the `publish.py` script.

This requires [Git Subtree](https://github.com/mwitkow/git-subtree) to be installed.


---

Credits go to @neogeek, his [Tutorial](https://github.com/neogeek/unity-package-example) lead to the transformation to a proper _upm_ package.
