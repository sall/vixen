using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace VixenModules.Effect.LipSync
{
    class LipSyncWord
    {
        string line = null;
        string m_wordText = null;
        int m_startFrame = 0;
        int m_endFrame = 0;
        int m_numPhoneme = 0;
        LipSyncPhoneme[] m_phoneme = null;

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
        }

        public LipSyncWord(StreamReader file, ref Dictionary<PhonemeType, List<LipSyncPhoneme>> phonemes)
        {
            List<LipSyncPhoneme> tmpList = null;
            line = file.ReadLine();
            if (line == null)
            {
                throw new IOException("Corrupt File Format");
            }

            line = line.TrimStart(null);
            string[] split = line.Split(' ');
            if (split.Length == 4)
            {
                m_wordText = split[0].TrimStart(null);
                m_startFrame = Convert.ToInt32(split[1].TrimStart(null));
                m_endFrame = Convert.ToInt32(split[2].TrimStart(null));
                m_numPhoneme = Convert.ToInt32(split[3].TrimStart(null));
                m_phoneme = new LipSyncPhoneme[m_numPhoneme];

                LipSyncPhoneme lastObj = null;
                for (int j = 0; j < m_numPhoneme; j++)
                {
                    if ((line = file.ReadLine()) != null)
                    {
                        line = line.TrimStart(null);
                        m_phoneme[j] = new LipSyncPhoneme(line, lastObj);
                        lastObj = m_phoneme[j];

                        if (phonemes.ContainsKey(m_phoneme[j].Type) == false)
                        {
                            phonemes.Add(m_phoneme[j].Type, new List<LipSyncPhoneme>());
                        }
                        tmpList = phonemes[m_phoneme[j].Type];
                        tmpList.Add(m_phoneme[j]);
                    }
                }

                if (lastObj != null)
                {
                    lastObj.EndFrame = m_endFrame;
                }
            }
        }

        public LipSyncPhoneme GetEventPhoneme(int eventNum)
        {
            foreach (LipSyncPhoneme phoneme in m_phoneme)
            {
                if (eventNum >= phoneme.StartFrame &&
                    eventNum <= phoneme.EndFrame)
                {
                    return phoneme;
                }
            }
            return new LipSyncPhoneme(null, null);
        }
    }
}
