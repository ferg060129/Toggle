using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class DiaryPlatform : Platform
    {

        public DiaryPlatform(int xLocation, int yLocation)
            : base(xLocation, yLocation)
        {
            itemType = new DiaryI().GetType();
            goodGraphic = Textures.textures["diaryPlatformG"];
            badGraphic = Textures.textures["diaryPlatformB"];
        }
        public override void changePlatformGraphic(bool b)
        {
            if (b)
            {
                goodGraphic = Textures.textures["diaryPlatformCompleteG"];
                badGraphic = Textures.textures["diaryPlatformCompleteB"];
            }
            else
            {
                goodGraphic = Textures.textures["diaryPlatformG"];
                badGraphic = Textures.textures["diaryPlatformB"];
            }
        }
    }
}
