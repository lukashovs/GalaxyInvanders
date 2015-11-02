using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GalaxyInvanders.Game_Objects
{
    class Frame
    {
        private int widthFrame;                 
        private int heightFrame;
        private int numberOfFrame;
        private float timePerFrame;
        private float totalElapsed;
        private Vector2 posotion; //позиция вывода спрайта
        protected Rectangle sprRec; //позиция в кадре
        protected int frames;   // количество кадров в фрейме 
        private const int framePerSec = 10; // скорость анимации 

        public Frame(int _frames, Texture2D _model)
        {
            frames = _frames;
            heightFrame = _model.Height;
            widthFrame = _model.Width / frames;
            numberOfFrame = 0;
            posotion = new Vector2(100, 100);
            timePerFrame = (float)1 / framePerSec;
            sprRec = new Rectangle(0, 0, widthFrame, heightFrame);
        }
        public void UpdateFrame(float elpT)
        {
            if (frames > 1)
            {
                totalElapsed += elpT;
                if (totalElapsed > timePerFrame)
                {
                    if (numberOfFrame == frames - 1)
                        numberOfFrame = 0;
                    else
                    {
                        numberOfFrame++;
                    }
                    sprRec = new Rectangle((int)(widthFrame) * numberOfFrame, 0, widthFrame, heightFrame);
                    totalElapsed = 0;
                }
            }
        }
        public void ChangeFrame(int _frames, Texture2D _newModel)
        {
            frames = _frames;
            heightFrame = _newModel.Height;
            widthFrame = _newModel.Width / frames;
            numberOfFrame = 0;
            posotion = new Vector2(100, 100);
            timePerFrame = (float)1 / framePerSec;
            sprRec = new Rectangle(0, 0, widthFrame, heightFrame);
        }
        public int Frames { get { return frames; } set { } }
        public int NumberOfFrame { get { return numberOfFrame; } set { } }
        public float TimePerFrame { get { return timePerFrame; } set { } }
        public Rectangle SprRec { get { return sprRec; } set { } }
    }
}
