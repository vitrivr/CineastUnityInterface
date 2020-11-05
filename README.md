# README

**This is in very early development. Do not use in production.**

## Features

This is a Unity3d endpoint for vitrivr's Cineast RESTful API.

See [Cineast](https://github.com/vitrivr/cineast) for further API informations

## Prerequisites

This package provides a Unity3d endpoint for vitrivr's Cineast RESTful API.
Thus, a running cineast instance is a requirement for this package to work.

For a guide on how to setup cineast, please see [Cineast's Github page](https://github.com/vitrivr/cineast).

## Usage -- Unity3d

To use this package in unity, the `manifest.json` has to be expanded by the following entry:

```json
"com.vitrivr.unityinterface.cineastapi": "https://github.com/vitrivr/UnityInterface.git#release"
```

The Unity Package Manager ( _upm_ ) will take care of downloading and linking this package.

### Versions

Besides using [semver]() to specify the package's version, there are two _upm_ packages available:

* `#release` -- The latest released version of the package (semver version Major.Minor.Path)
* `#latest` -- Development versions of the package (semver version Major.Minor.Path-SNAPSHOT)

In most of the cases you want to have the `#release` version. In case you would like to have the development version,
use this `manifest.json` entry:

```json
"com.vitrivr.unityinterface.cineastapi": "https://github.com/vitrivr/UnityInterface.git#latest"
```

The package was tested with Unity 2018.4 LTS.

### Example

_TODO_

### Documentation

For the underlying API documentation, please refer to `Generated/README.md` and `Generated/docs/`.

## Usage -- Developer

This is a Unity3d project setup to easily develop (and test) the Unity3d Cineast Interface.
We strongly recommend cloning this repository into the `Assets` directory of a Unity shell project to make development and generation of `.meta` files easier.
To allow the shell project to function without compile errors, it is currently necessary to include the `jillejr.newtonsoft.json-for-unity` package as a workaround. Add the following to the root object in `manifest.json` on the same level as `dependencies`:
```json
"scopedRegistries": [
  {
    "name": "Packages from jillejr",
    "url": "https://npm.cloudsmith.io/jillejr/newtonsoft-json-for-unity/",
    "scopes": ["jillejr"]
  }
]
```
And add the following line to the dependencies: `"jillejr.newtonsoft.json-for-unity": "12.0.201"`
To actively develop this package, follow these steps:

1. If necessary, update the OpenApi Specs (OAS) of vitrivr. Reade more in [Generate OpenApi Dependencies](#generate-openapi-dependencies).
2. To test your changes and generate the **required** `.meta` files for any files you may have added, import the root directory of this repository into a Unity project as a local package with the `file:` method.
3. Do not forget to create a PR of your work to include it in the main branch.

### Generate OpenApi Dependencies

Follow these steps to generate / update the cineast OAS. We provide the latest generation in this repo for convenience.

For unix systems, the build requires an installation of the [.NET Core SDK](https://dotnet.microsoft.com/), [wget](https://www.gnu.org/software/wget/) and [mono](https://www.mono-project.com). Mono and wget are available through [homebrew](https://brew.sh) on mac.

1. Have an updated, running cineast api.
2. Issue the following `gradle` command:
   ```
   $> ./gradlew clean deploy -Poas="http://cineast-host:port/openapi-specs tidy"
   ```
   Replace `cineast-host:port` with your running cineast api. Most propably this will be the default (`localhost:4567`), in this case you can ommit the argument.
3. Generate the **required** `.meta` files for generated files by importing the root directory of this repository into a Unity project as a local package with the `file:` method.

> _Notice_
> These steps were tested under Windows 10 64bit, using OpenJDK 11 and Gradle 6.1.1. The publish script was tested with python 3.8.2.
> While these scripts are written with platform independence in mind, they might not work as expected.

## Contribution

One can contribute to this project by submitting a PR.

## Contribtors

 * Loris Sauter <loris.sauter@unibas.ch>
 * Florian Spiess <florian.spiess@unibas.ch>

## License

This work is licensed under the MIT open source license. Please refer to the LICENSE file.

## Credits

 * Credits go to @neogeek, his [Tutorial](https://github.com/neogeek/unity-package-example) lead to the transformation to a proper _upm_ package.
 * Credits go to @SamuelBoerlin, his [first steps](https://github.com/SamuelBoerlin/Cineast-OpenAPI-Implementation) with Cienast and OpenApi helped a lot.
