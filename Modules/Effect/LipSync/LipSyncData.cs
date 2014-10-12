using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Vixen.Module;
using VixenModules.App.ColorGradients;
using VixenModules.App.Curves;
using System.Drawing;

namespace VixenModules.Effect.LipSync
{
    [DataContract]
    internal class LipSyncData : ModuleDataModelBase
    {
        private Curve _levelCurve = null;
        private Curve _maxStraightLine;

        [DataMember]
        public String StaticPhoneme { get; set; }

        [DataMember]
        public String PhonemeMapping { get; set; }

        [DataMember]
        public String LyricData { get; set; }

        [DataMember]
        public Curve LevelCurve 
        { 
            get
            {
                if  (_levelCurve == null)
                {
                    _levelCurve = new Curve(MaxStraightLine);
                }
                return _levelCurve;
            }

            set
            {
                _levelCurve = value;
            }
        }

        [DataMember]
        public ColorGradient GradientOverride { get; set; }

        private Curve MaxStraightLine
        {
            get
            {
                if (_maxStraightLine == null)
                {
                    _maxStraightLine = new Curve();
                    _maxStraightLine.Points.Clear();
                    _maxStraightLine.Points.Add(0, 100);
                    _maxStraightLine.Points.Add(100, 100);
                }

                return _maxStraightLine;
            }
                
        }
        public LipSyncData()
        {
            StaticPhoneme = "REST";
            PhonemeMapping = "";
            LyricData = "";
        }

        public override IModuleDataModel Clone()
        {
            LipSyncData result = new LipSyncData();
            result.StaticPhoneme = StaticPhoneme;
            result.PhonemeMapping = PhonemeMapping;
            result.LyricData = LyricData;
            result.LevelCurve = LevelCurve;
            return result;
        }

        public bool IsDefaultCurve()
        {
            Curve tempCurve = LevelCurve;
            Curve tempMax = MaxStraightLine;
            return (tempCurve != null) &&
                    (tempCurve.Points.Count == 2) &&
                    (tempCurve.Points[0].Equals(tempMax.Points[0])) &&
                    (tempCurve.Points[1].Equals(tempMax.Points[1]));
        }
    }
}
