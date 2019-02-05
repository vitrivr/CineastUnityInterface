# README

This is in very early development. Do not use in production.

**Warning: There are missing dependencies in the current project setup**.

## Features

This is a Unity3d endpoint for vitrivr's Cineast RESTful API.

See [Cineast](https://github.com/vitrivr/cineast) for further API informations

## Usage

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

Assuming you have the `CineastApi` MonoBehaviour attachted to a gameobject:

Create Queries via the `QueryFactory` and store it as `query`.
Then, to actually query Cineast:

```
val api = cineastApiGameObject.GetComponent<CineastApi>();
api.RequestSimilarAndThen(query, handler);
```

The `handler` is an `Action<List<MultimediaObject>>` variable which will
receive the response, as soon Cineast answers.

## Development Status

Why this is an early development version:

 * There are some missing dependencies in the crrent project setup.
 * There should be an Interface instead of the plain Action for handling responses

