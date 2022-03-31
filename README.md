# Cineast Unity Interface

A Unity Package Manager package for the Cineast OpenAPI RESTful API.
See [Cineast](https://github.com/vitrivr/cineast) for further API informations

## Prerequisites

This package provides a Unity endpoint for vitrivr's Cineast RESTful API.
Thus, a running Cineast instance is a requirement for this package to work.

For a guide on how to setup cineast, please see [Cineast's Github page](https://github.com/vitrivr/cineast).

## Usage -- Unity

To use this package in Unity, the `manifest.json` has to be expanded by the following entry:

```json
"com.vitrivr.unityinterface.cineastapi": "https://github.com/vitrivr/CineastUnityInterface.git"
```

The Unity Package Manager (_upm_) will take care of downloading and linking this package.

### Versions

This package uses [semver](https://semver.org). It is recommended to manually specify a version in the packages manifest entry.
The package was tested with Unity 2020.3 LTS.

## Usage -- Developer

This is a Unity3d project setup to easily develop (and test) the Unity3d Cineast Interface.
We strongly recommend cloning this repository into the `Packages` directory of a Unity shell project to make development and generation of `.meta` files easier.

1. If necessary, update the OpenApi Specs (OAS) of vitrivr. Reade more in [Generate OpenApi Dependencies](#generate-openapi-dependencies).
2. To test your changes and generate the **required** `.meta` files for any files you may have added, import the root directory of this repository into a Unity project as a local package with the `file:` method.
3. Do not forget to create a PR of your work to include it in the main branch.

### Generate OpenAPI Dependencies

Follow these steps to generate / update the cineast OAS. We provide the latest generation in this repo for convenience.

For unix systems, the build requires an installation of the [.NET Core SDK](https://dotnet.microsoft.com/), [wget](https://www.gnu.org/software/wget/) and [mono](https://www.mono-project.com). Mono and wget are available through [homebrew](https://brew.sh) on mac.

1. Issue the following `gradle` command:
   ```
   $> ./gradlew clean deploy tidy
   ```
2. (Android Only) Delete the file `Runtime/Libs/Newtonsoft.Json.dll`.
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
 * Credits go to @SamuelBoerlin, his [first steps](https://github.com/SamuelBoerlin/Cineast-OpenAPI-Implementation) with Cineast and OpenAPI helped a lot.
