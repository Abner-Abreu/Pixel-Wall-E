# Pixel Wall-E

![Godot](https://img.shields.io/badge/Godot-4.4_Mono-478cbf?logo=godot-engine)
![.NET](https://img.shields.io/badge/.NET-9.0-512bd4?logo=.net)
![C#](https://img.shields.io/badge/C%23-12.0-239120?logo=csharp)
![License](https://img.shields.io/badge/license-MIT-green.svg)

A code-driven pixel art creation tool with an interactive canvas. Write code to generate pixel art or use the visual editor. Built with **Godot 4.4 Mono** and **C#**.

---

## 🚀 Features

- **Code Interpreter** - Execute drawing instructions to create pixel art programmatically
- **Interactive Canvas** - Real-time drawing with visual feedback
- **Pixel Art Tools** - Optimized for low-resolution art creation
- **Customizable Settings** - Adjust brush size, colors, and canvas dimensions
- **Cross-Platform** - Works on Windows, macOS, and Linux

---

## 🧰 Requirements

- [Godot 4.4 Mono](https://godotengine.org/download) (with C# support)
- [.NET SDK 9.0+](https://dotnet.microsoft.com/download)
- Compatible OS: Windows, macOS, or Linux
- Recommended IDEs:
  - Visual Studio
  - JetBrains Rider
  - Visual Studio Code with C# extension

---

## 🛠️ Installation

1. **Clone the repository:**
   ```bash
   git clone https://github.com/Abner-Abreu/Pixel-Wall-E.git
   cd Pixel-Wall-E
   ```

2. **Open in Godot:**
   - Launch Godot 4.4 Mono
   - Click **Import**
   - Select the `Pixel-Wall-E` folder
   - Click **Import & Edit**

3. **Build C# solution:**
   - Wait for initial script compilation
   - Godot will automatically generate `.csproj` files
   - Verify .NET SDK is properly configured

> [!NOTE]
> First launch may take longer while Godot initializes the C# environment.

---

## ▶️ Running the Project

### From Godot Editor

1. Open `MainWindow.tscn` (or main scene)
2. Click the **Play** button ▶️ (or press `F5`)

### From Command Line

```bash
# Navigate to project folder
cd path/to/Pixel-Wall-E

# Run via Godot executable (adjust path as needed)
godot4.4-mono --path .
```

### Building Executables

1. In Godot, go to **Project → Export**
2. Configure export templates for your target platform(s)
3. Click **Export Project** to create standalone builds

---

## 🎨 Workflow

1. **Write Code** - Use the built-in code editor to write drawing instructions
2. **Execute** - Run the code to generate pixel art on the canvas
3. **Edit Manually** - Use interactive tools to refine your creation
4. **Adjust Settings** - Modify canvas size, brush settings, colors in real-time
5. **Export** - Save your artwork as PNG images

---

## 🎯 Use Cases

- **Generative Art** - Create procedural pixel art through code
- **Game Asset Creation** - Generate sprites and textures
- **Learning Tool** - Teach programming concepts through visual feedback
- **Prototyping** - Quickly mock up pixel art ideas
- **Art Experimentation** - Combine code and manual drawing techniques

---

## 🗂️ Project Structure

```
Pixel-Wall-E/
├── src/                    # C# scripts
│   ├── Core/              # Core functionality
│   ├── Drawing/           # Drawing system
│   ├── Interpreter/       # Code interpreter
│   └── UI/                # User interface
├── scenes/                # Godot scenes
├── assets/                # Images, fonts, etc.
├── .gitignore
├── .gitattributes
├── project.godot          # Godot project file
├── LICENSE
└── README.md
```

---

## 🛠️ Technical Details

### Architecture

- **Interpreter Engine** - Parses and executes drawing commands
- **Canvas System** - Manages pixel buffer and rendering
- **Command Pattern** - Implements drawing operations as reusable commands
- **Event System** - Handles user input and tool interactions

### Technologies

- **Engine**: Godot 4.4 Mono
- **Language**: C# 9.0
- **Framework**: .NET 9.0
- **Rendering**: Godot's 2D rendering pipeline

---

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 🔗 Links

- [Godot Engine](https://godotengine.org/)
- [Godot C# Documentation](https://docs.godotengine.org/en/stable/tutorials/scripting/c_sharp/)
- [.NET Documentation](https://docs.microsoft.com/dotnet/)

---

**Create pixel art through code!** 🎨✨
