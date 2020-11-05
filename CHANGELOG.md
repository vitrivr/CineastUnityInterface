# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).



## [Unreleased]

### Added

- Proper Cineast API support, based on OpenApi generated client
- For Devs: Automation regarding OpenApi code generation
- Lazy loading of results

### Changed

- Full rework of entire API
- Cleanup of interface for users of this API
- Result processing / handling
- Changed to proper async behaviour
- Legacy Cineast Api is now marked as such

### Deprecated

- Entire CineastApi is now legacy and deprecated.


## [1.0.0-SNAPSHOT]

### Added

- Full support of Cineast API, based on OpenApi generated client

## [0.0.2]

### Added

- Method to store the cineast config (i.e. for when a UI enables changes of the host)
- RESTful interface for JSON communication over HTTP using UnityWebRequest. GET and POST supported as for now
- Configuration options to map cineast categories to your custom ones.

## [0.0.1]

### Added

- Everything: Initial setup. Feature list coming soon