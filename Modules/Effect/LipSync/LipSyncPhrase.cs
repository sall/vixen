using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace VixenModules.Effect.LipSync
{
    class LipSyncPhrase
    {
        string m_text;
        int m_state = 0;
        int m_startFrame;
        int m_endFrame;
        int m_numWords;
        LipSyncWord[] m_words = null;

        public LipSyncPhrase()
        {
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
        }

        public void Load(StreamReader file, ref Dictionary<PhonemeType, List<LipSyncPhoneme>> phonemes)
        {
            string line;
            while (m_state < 4)
            {
                line = file.ReadLine();
                if (line == null)
                {
                    throw new IOException("Corrupt File Format");
                }

                //Trim leading whitespace on the read string. 
                line = line.TrimStart(null);

                switch (m_state)
                {
                    //Read the phrase text
                    case 0:
                        m_text = line;
                        m_state++;
                        break;

                    //Read the start frame
                    case 1:
                        m_startFrame = Convert.ToInt32(line);
                        m_state++;
                        break;

                    //Read the end frame
                    case 2:
                        m_endFrame = Convert.ToInt32(line);
                        m_state++;
                        break;

                    //Read the end frame
                    case 3:
                        m_numWords = Convert.ToInt32(line);
                        m_words = new LipSyncWord[m_numWords];

                        for (int j = 0; j < m_numWords; j++)
                        {
                            m_words[j] = new LipSyncWord(file, ref phonemes);
                        }
                        m_state++;
                        break;

                    default:
                        break;
                }
                Console.WriteLine(m_state.ToString() + " - " + line);
            }
        }

        public LipSyncPhoneme GetEventPhoneme(int eventNum)
        {
            foreach (LipSyncWord word in this.m_words)
            {
                if (eventNum >= word.StartFrame &&
                    eventNum <= word.EndFrame)
                {
                    return word.GetEventPhoneme(eventNum);
                }
            }
            return new LipSyncPhoneme(null, null);
        }
    }
}
