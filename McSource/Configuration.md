# Map markers

Map markers can be made by placing signs in the minecraft world. The first line must always contain `[mcsource]`, whilst
the second line contains the name of the entity to place.

## Examples

### Random weapon (TTT)

```
> [mcsource]
> ttt_random_weapon
> 
> 
```

### Random ammunition pack (TTT)

```
> [mcsource]
> ttt_random_ammo
> 
> 
```

### Spawnpoint (TTT)

```
> [mcsource]
> info_player_start
> 
> 
```

# The config file

The McSource `config.yml` contains different sections for configuring McSource.

## MaterialsPath

The **MaterialsPath** value determines the root path of all material folders.
> **Note:** Please make sure to escape backslashes ('\\')!

```yml
materialsPath: "C:\\Program Files (x86)\\Steam\\steamapps\\common\\GarrysMod\\garrysmod\\materials"
```

## Textures

The **Textures** section maps allows for mapping materials and specific attributes to blocks. The **Textures** section
contains all namespaces. McSource will search through all **Namespaces** section in descending order.

```yml
# ...
textures:
```

### Namespaces:

A **Namespace** can contain **Blocks**

```yml
# ...
textures:
  minecraft:
  # ...
  my_mod:
  # ...
```

### Blocks:

Blocks can be assigned a texture, a type and a `translucent` flag, determining whether the texture is see-through

###### Possible block types:

| Type       | Description                                                                         |
|------------|-------------------------------------------------------------------------------------|
| block      | A solid block                                                                       |
| slab       | A slab-shaped block that can be placed on the upper or lower half of an empty space |
| stairs     | A stair-shaped block that can connect to nearby stairs                              |
| pane       | A pane-shaped block that can extend to solid blocks nearby                          |
| fence      | A fence-shaped block that can extend to solid blocks nearby                         |
| fence-gate | A fence-gate-shaped block which can be opened or closed                             |
| door       | A door-shaped block which can be opened or closed                                   |
| trapdoor   | A trapdoor-shaped block which can be opened or closed                               |
| plant      | A plant-shaped block                                                                |
| ladder     | A ladder-shaped block to climb on                                                   |
| flat       | A flat block like snow or carpet, can be stackable                                  |
| fire       | Burning fire                                                                        |
| torch      | A torch-shaped block                                                                |
| sign       | A sign-shaped block, maybe containing text                                          |
| rod        | A rod-shaped block                                                                  |
| ignored    | Do not process                                                                      |

### Texture

A **Texture** defines material paths for different block materials for each side, optionally depending on different
block states, stages:

#### Possible values:

###### String

The material path to use for all sides at once
> **Note:** Please make sure to escape backslashes ('\\')!

```yml
texture: "minecraft\\stone"
```

###### Sides

The paths to use for each block side. The sides _'front'_, _'back'_, _'left'_ and _'right'_ take precedence over _'
sides'_, which takes precedence over _'default'_:

```yml
texture:
  sides:
    default: "minecraft\\melon" # All sides that are not specified
    # sides:                   # Optional
    top: "minecraft\\melon_top" # Optional
    # bottom:                  # Optional
    # front:                   # Optional
    # back:                    # Optional
    # left:                    # Optional
    # right:                   # Optional
```

###### Stages

Defines the material to use for each block stage by either using a **String** or a **Sides** value.

```yml
texture:
stages:
  - "minecraft\\wheat_stage_0"
  - "minecraft\\wheat_stage_1"
  - "minecraft\\wheat_stage_2"
  - sides:
      default: "minecraft\\wheat_stage_3"
```

###### States

Defines the texture to use for each block state by either using a **String**, a **Sides** or a **Stages** value.

```yml
texture:
states:
  on: "minecraft\\torch_on"
  off:
    sides:
      default: "minecraft\\torch_off"
    stages:
      - "minecraft\\a"
      - "minecraft\\b"
      - "minecraft\\c"
```