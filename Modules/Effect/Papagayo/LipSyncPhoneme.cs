using System;
using System.Collections.Generic;
using System.Text;

namespace VixenModules.Effect.LipSync
{
    public class LipSyncPhoneme
    {
        string m_Text = null;
        int m_startFrame = 0;
        int m_endFrame = 0;
        PhonemeType m_type = PhonemeType.Rest;

        public PhonemeType Type
        {
            get
            {
                return m_type;
            }
        }

        public string TypeName
        {
            get
            {
                return m_type.ToString();
            }
        }

        public int StartFrame
        {
            get
            {
                return m_startFrame;
            }
        }

        public int EndFrame
        {
            get
            {
                return m_endFrame;
            }

            set
            {
                m_endFrame = value;
            }
        }

        public bool isPhonemeType(string testVal)
        {
            return ((testVal != null) &&
                (this.m_Text.Equals(testVal.ToUpper())));
        }

        public LipSyncPhoneme(string pair, LipSyncPhoneme lastObj)
        {
            m_type = PhonemeType.Rest;
            m_Text = "Rest";
            if (pair != null)
            {
                string[] split = pair.Split(' ');
                if (split.Length == 2)
                {
                    m_startFrame = Convert.ToInt32(split[0].TrimStart(null));
                    m_Text = split[1].TrimStart(null);
                    try
                    {
                        m_type = (PhonemeType)Enum.Parse(typeof(PhonemeType), m_Text);
                    }
                    catch (Exception)
                    {
                        m_type = PhonemeType.Rest;
                    }

                    if (lastObj != null)
                    {
                        lastObj.m_endFrame = m_startFrame - 1;
                    }
                }
            }
        }
    }
}
