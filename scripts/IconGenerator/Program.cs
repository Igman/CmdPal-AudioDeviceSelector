using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using Svg;

namespace IconGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Try to find the solution root by looking for .sln file up the tree
            string currentDir = Directory.GetCurrentDirectory();
            string projectRoot = currentDir;

            // Simple heuristic to find project root if running from bin folder
            while (!File.Exists(Path.Combine(projectRoot, "AudioDeviceSelector.sln")) && Directory.GetParent(projectRoot) != null)
            {
                projectRoot = Directory.GetParent(projectRoot).FullName;
            }

            string svgPath = Path.Combine(projectRoot, "AudioDeviceSelector", "Assets", "NewAppIcon.svg");
            string assetsDir = Path.Combine(projectRoot, "AudioDeviceSelector", "Assets");

            if (!File.Exists(svgPath))
            {
                Console.WriteLine($"Error: SVG file not found at {svgPath}");
                return;
            }

            Console.WriteLine($"Found SVG at: {svgPath}");

            var targets = new List<(string Filename, int Width, int Height)>
            {
                ("StoreLogo.png", 50, 50),
                ("Square150x150Logo.png", 150, 150),
                ("Square44x44Logo.png", 44, 44),
                ("Wide310x150Logo.png", 310, 150),
                ("SplashScreen.png", 620, 300),
                ("Square150x150Logo.scale-200.png", 300, 300),
                ("Square44x44Logo.scale-200.png", 88, 88),
                ("Wide310x150Logo.scale-200.png", 620, 300),
                ("SplashScreen.scale-200.png", 1240, 600),
            };

            var svgDocument = SvgDocument.Open(svgPath);

            foreach (var target in targets)
            {
                string outputPath = Path.Combine(assetsDir, target.Filename);
                Console.WriteLine($"Generating {target.Filename} ({target.Width}x{target.Height})...");

                try
                {
                    // For non-square images (Wide310x150, etc.), we center the square icon.
                    // For square images, we just resize.
                    
                    int iconSize;
                    int x = 0;
                    int y = 0;

                    if (target.Width != target.Height)
                    {
                        // Landscape/Wide
                        // Fit by height with some padding usually, but here let's fit height fully
                        iconSize = Math.Min(target.Width, target.Height);
                        x = (target.Width - iconSize) / 2;
                        y = (target.Height - iconSize) / 2;
                    }
                    else
                    {
                        iconSize = target.Width;
                    }

                    // Create the final bitmap
                    using (var bitmap = new Bitmap(target.Width, target.Height))
                    using (var graphics = Graphics.FromImage(bitmap))
                    {
                        // Clear background - Transparent
                        graphics.Clear(Color.Transparent);
                        graphics.SmoothingMode = SmoothingMode.HighQuality;
                        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                        // Resize the SVG document temporarily
                        svgDocument.Width = new SvgUnit(SvgUnitType.Pixel, iconSize);
                        svgDocument.Height = new SvgUnit(SvgUnitType.Pixel, iconSize);

                        // Draw the SVG at the calculated position
                        // We need to translate the graphics context to the center
                        graphics.TranslateTransform(x, y);
                        
                        // SvgDocument.Draw() draws at 0,0 relative to the graphics transform
                        svgDocument.Draw(graphics);

                        bitmap.Save(outputPath, ImageFormat.Png);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to generate {target.Filename}: {ex.Message}");
                }
            }
            Console.WriteLine("Done!");
        }
    }
}
