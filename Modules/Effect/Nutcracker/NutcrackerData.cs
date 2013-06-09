﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Drawing;
using Vixen.Module;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Vixen.Data.Value;
using Vixen.Intent;
using Vixen.Sys;
using Vixen.Module.Effect;
using Vixen.Sys.Attribute;

namespace VixenModules.Effect.Nutcracker
{
    [DataContract]
    [KnownType(typeof(SerializableFont)),
    KnownType(typeof(System.Drawing.FontStyle)),
    KnownType(typeof(System.Drawing.GraphicsUnit))]
    public class NutcrackerData
    {
        public NutcrackerData()
        {
            Text_Font = new SerializableFont(new Font("Arial", 8));
        }

        public ElementNode[] TargetNodes;

        [DataMember]
        public NutcrackerEffects.PreviewType PreviewType = NutcrackerEffects.PreviewType.Tree180;

        [DataMember]
        public NutcrackerEffects.Effects CurrentEffect = NutcrackerEffects.Effects.Bars;
        [DataMember]
        public int Speed = 5;

        [DataMember]
        public Palette Palette = new Palette();

        // Bars
        [DataMember]
        public int Bars_PaletteRepeat = 1;
        [DataMember]
        public int Bars_Direction = 1;
        [DataMember]
        public bool Bars_Highlight = false;
        [DataMember]
        public bool Bars_3D = false;

        // Butterfly
        [DataMember]
        public int Butterfly_Colors = 1;
        [DataMember]
        public int Butterfly_Style = 1;
        [DataMember]
        public int Butterfly_BkgrdChunks = 1;
        [DataMember]
        public int Butterfly_BkgrdSkip = 2;

        // ColorWash
        [DataMember]
        public int ColorWash_Count = 1;
        [DataMember]
        public bool ColorWash_FadeHorizontal = false;
        [DataMember]
        public bool ColorWash_FadeVertical = false;

        // Fire
        [DataMember]
        public int Fire_Height = 50;

        // Garlands
        [DataMember]
        public int Garland_Type = 1;
        [DataMember]
        public int Garland_Spacing = 1;

        // Life
        [DataMember]
        public int Life_CellsToStart = 50;
        [DataMember]
        public int Life_Type = 1;

        // Meteors
        [DataMember]
        public int Meteor_Colors = 1;
        [DataMember]
        public int Meteor_Count = 10;
        [DataMember]
        public int Meteor_TrailLength = 8;

        // Fireworks
        [DataMember]
        public int Fireworks_Explosions = 5;
        [DataMember]
        public int Fireworks_Particles = 20;
        [DataMember]
        public int Fireworks_Velocity = 50;
        [DataMember]
        public int Fireworks_Fade = 50;

        // Snowflakes
        [DataMember]
        public int Snowflakes_Max;
        [DataMember]
        public int Snowflakes_Type;

        // Snowstorm
        [DataMember]
        public int Snowstorm_MaxFlakes = 10;
        [DataMember]
        public int Snowstorm_TrailLength = 10;

        // Spirals
        [DataMember]
        public int Spirals_PaletteRepeat = 1;
        [DataMember]
        public int Spirals_Direction = 0;
        [DataMember]
        public int Spirals_Rotation = 1;
        [DataMember]
        public int Spirals_Thickness = 5;
        [DataMember]
        public bool Spirals_Blend = false;
        [DataMember]
        public bool Spirals_3D = false;

        // Twinkles
        [DataMember]
        public int Twinkles_Count = 50;

        // Text
        [DataMember]
        public int Text_Top = 5;
        [DataMember]
        public int Text_Left = 5;
        [DataMember]
        public string Text_Line1 = "";
        [DataMember]
        public string Text_Line2 = "";
        [DataMember]
        public int Text_Direction = 0;
        [DataMember]
        public SerializableFont Text_Font { get; set; }
        [DataMember]
        public int Text_TextRotation = 0;

        // Picture
        [DataMember]
        public string Picture_FileName = "";
        [DataMember]
        public int Picture_Direction = 0;
        [DataMember]
        public int Picture_GifSpeed = 1;

        [OnDeserialized]
        void OnDeserialized(StreamingContext context)
        {
            if (Palette == null)
                Palette = new Palette();
            if (Fire_Height < 1)
                Fire_Height = 50;
            if (Meteor_Colors < 1)
                Meteor_Colors = 1;
            if (Meteor_Count < 1)
                Meteor_Count = 10;
            if (Meteor_TrailLength < 1)
                Meteor_TrailLength = 8;

            if (Spirals_PaletteRepeat == 0)
                Spirals_PaletteRepeat = 1;

            if (Twinkles_Count < 2)
                Twinkles_Count = 10;

            if (Text_Line1 == null)
                Text_Line1 = "";
            if (Text_Line2 == null)
                Text_Line2 = "";

            if (Text_Font == null)
            {
                Text_Font = new SerializableFont(new Font("Arial", 8));
            }

            if (Picture_FileName == null)
                Picture_FileName = "";
            if (Picture_GifSpeed < 1)
                Picture_GifSpeed = 1;
        }
    }
}
