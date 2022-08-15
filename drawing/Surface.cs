
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace learnmonogame.drawing {
    public static class Surface {
        public static Texture2D DrawRect(ref SpriteBatch graphics,int width, int height, Color col) {

            Texture2D rect = new Texture2D(graphics.GraphicsDevice, width, height);

            Color[] data = new Color[width * height];
        for (int i = 0; i < data.Length; i++) data[i] = col;
        rect.SetData(data);

        return rect ; 
            
        }

      
    }
}