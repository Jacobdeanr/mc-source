# Mc-Source

Mc-Source is a tool capable of converting Minecraft structures to Source-compatible maps.

## Usage

> Mc-Source is currently in beta and does not produce stable results. Therefore, no releases are published. The steps listed below are to be seen as templates for the future releases.

1. Download Mc-Source from the [Releases](https://github.com/h45h74x/mc-source/releases) section. Start it once to generate a default config.

2. Download the materials. Custom materials are supported (e.g. for mods), just make sure your configuration file is set up correctly.

3. Run Mc-Source

The output `.vmf`-file simply needs to be opened in Hammer Editor, where the map can be compiled by pressing <F9>.

### Supported formats

Below you can find information about supported source files, including the programs capable of generating the files and extracting structures from the minecraft world. For older minecraft versions, changing block IDs in the `config.yml` configuration file might be required. 

| MC Version | Format | Extension | Compatible Programs |
|-|-|-|-|
| 1.15.+ (tested) | [Sponge Schematic Specification v3](https://github.com/SpongePowered/Schematic-Specification) | `.schem` | WorldEdit (AsyncWorldEdit, FAWE, ...), McEdit, Schematica, BuilderTools |

### Options

Mc-Source can be run with multiple command line arguments:

| Argument | Default | Description | Example |
|-|-|-|-|
|||||

### Configuration

Information on the setup of the `config.yml` file can be found in the [Configuration.md](./Configuration.md) documentation file.

## Contributing

Pull requests are greatly appreciated. Also, feel free to open an issue for discussion of bugs or features.
