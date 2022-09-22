# Mc-Source

Mc-Source is a tool capable of converting Minecraft structures to Source-compatible maps.


## :joystick: Usage

> Mc-Source is currently in beta and does not produce stable results. Therefore, no releases are published. The steps listed below are to be seen as templates for the future releases.

1. Download Mc-Source from the [Releases](https://github.com/h45h74x/mc-source/releases) section. Start it once to generate a default config.

2. Download the materials. Custom materials are supported (e.g. for mods), just make sure your configuration file is set up correctly.

3. Run Mc-Source

4. Open the output `.vmf`-file in Hammer Editor, where the map can be compiled by pressing &lt;F9&gt;.

Mc-Source has support for custom textures and adding new blocks, even for mods. More information can be found [here](./Configuration.md). 

## :hammer: Options

Mc-Source can be run with multiple command line arguments:

| Argument | Default | Description | Example |
|-|-|-|-|
|||||

Persistent configuration, specifically regarding block textures, can be found in the [Configuration.md](./Configuration.md) file.


## :bookmark: Supported formats

Below you can find information about supported source files, including the programs capable of generating the files and extracting structures from the minecraft world. For older minecraft versions, changing block IDs in the `config.yml` configuration file might be required. 

| MC Version | Format | Extension | Compatible Programs |
|-|-|-|-|
| 1.15.+ (tested) | [Sponge Schematic Specification v3](https://github.com/SpongePowered/Schematic-Specification) | `.schem` | WorldEdit (AsyncWorldEdit, FAWE, ...), McEdit, Schematica, BuilderTools |


## :magic_wand: Contributing

Pull requests are greatly appreciated. Also, feel free to open an issue for discussing bugs or features.
