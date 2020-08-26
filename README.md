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

The Untiy Package Manager ( _upm_ ) will take care of downloading and linking this package.

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
To actively develop this package, follow these steps:

1. Open the root folder of this repo as Unity project
2. If necessary, update the OpenApi Specs (OAS) of vitrivr. Reade more in [Generate OpenApi Dependencies](#generate-openapi-dependencies).
3. Switch back to unity
4. Publish your changes using the provided `publish.py` script. 
   You specify the package tag (to use in the `manifest.json` of your dependent unity project) using the `--release` argument:
   
   ```
   $> python publish.py --release="tag"
   ```
   
   However, there are two reserved tags: `release` and `latest`.
5. Navigate to your dependent unity project and adjust the tag based on the previous step.
6. Do not forget to create a PR of your work to include it in the main branch.

This requires [Git Subtree](https://github.com/mwitkow/git-subtree) to be installed.

### Generate OpenApi Dependencies

Follow these steps to generate / update the cineast OAS. We provide the latest generation in this repo for convenience.

For unix systems, the build requires an installation of the [.NET Core SDK](https://dotnet.microsoft.com/), [wget](https://www.gnu.org/software/wget/) and [mono](https://www.mono-project.com). Mono and wget are available through [homebrew](https://brew.sh) on mac.

1. Have an updated, running cineast api.
2. Issue the following `gradle` command:
   ```
   $> ./gradlew deploy -Poas="http://cineast-host:port/openapi-specs"
   ```
   Replace `cineast-host:port` with your running cineast api. Most propably this will be the default (`localhost:4567`), in this case you can ommit the argument.

> _Notice_
> These steps were tested under Windows 10 64bit, using OpenJDK 11 and Gradle 6.1.1. The publish script was tested with python 3.8.2.
> While these scripts are written with platform independence in mind, they might not work as expected.

## Contribution

One can contribute to this project by submitting a PR.

## Contribtors

 * Loris Sauter <loris.sauter@unibas.ch>

## License

This work is licensed under the MIT open source license. Please refer to the LICENSE file.

## Credits

 * Credits go to @neogeek, his [Tutorial](https://github.com/neogeek/unity-package-example) lead to the transformation to a proper _upm_ package.
 * Credits go to @SamuelBoerlin, his [first steps](https://github.com/SamuelBoerlin/Cineast-OpenAPI-Implementation) with Cienast and OpenApi helped a lot.
