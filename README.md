# Pixel Wall-E  
A program that allows **interpreting code** and **drawing on a canvas** in a pixel art style. The interface is developed with **Godot 4.4 Mono**, and all scripts are written in **C#**.  
---
## 🚀 Features  
- **Code interpretation:** Executes instructions to generate pixel art drawings  
- **Interactive canvas:** Real-time drawing on a customizable canvas  
- **Pixel art tools:** Designed for low-resolution art creation  
- **Intuitive interface:** Godot-based with full C# support  
- **Customizable settings:** Adjust brush size, colors, and canvas dimensions  
---
## 🧰 Requirements  
- [Godot 4.4 Mono](https://godotengine.org/download) (with C# support)  
- [.NET SDK 9.0+](https://dotnet.microsoft.com/en-us/download)  
- Compatible OS: Windows, macOS, or Linux  
- Recommended code editors:  
  - Visual Studio  
  - JetBrains Rider  
  - Visual Studio Code  
---
## 🛠️ Installation  
1. **Clone the repository:**  
   ```bash  
   git clone https://github.com/Abner-Abreu/Pixel-Wall-E.git  
   ```  
2. **Open in Godot:**  
   - Launch Godot 4.4 Mono  
   - Import project → Select `Pixel Wall-E` folder  
3. **Configure C# environment:**  
   - Wait for initial script compilation  
   - Verify .NET SDK installation  
---
## ▶️ Execution  
### From Godot Editor  
1. Open `MainWindow.tscn`  
2. Click **Play** button ▶️  
### Command Line (Optional)  
# Navigate to project folder  
```bash  
cd path/to/Pixel-Wall-E
```
# Run via Godot executable (adjust path as needed)  
```bash  
godot4.4-mono --path ./  
```  
### Building Executables  
1. Configure export templates in Godot  
2. Use `Project → Export` to create platform-specific builds  
---
## 🎨 Workflow  
1. Write drawing instructions in the code editor  
2. Execute code to generate pixel art  
3. Use interactive tools for manual editing  
4. Adjust canvas/brush settings in real-time  
5. Export creations as PNG images  
> **Note**: First launch may take longer while Godot initializes the C# environment  
