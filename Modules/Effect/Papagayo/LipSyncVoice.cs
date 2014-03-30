using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace VixenModules.Effect.Papagayo
{
    class LipSyncVoice
    {
        int m_state = 0;
        string m_voiceName = null;
        string m_voicePhrase = null;
        int m_numPhrases = 0;
        LipSyncPhrase[] m_phrases = null;
        Dictionary<PhonemeType, List<LipSyncPhoneme>> m_phonemes = null;

        public string VoiceName
        {
            get { return m_voiceName; }
        }

        public LipSyncVoice()
        {

        }

        public void Load(StreamReader file)
        {
            m_voiceName = null;
            m_voicePhrase = null;
            m_numPhrases = 0;

            string line;
            while (m_state < 3)
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
                    //Read the voice name
                    case 0:
                        m_voiceName = line;
                        m_state++;
                        break;

                    //Read the Voice Phrase Text
                    case 1:
                        m_voicePhrase = line.Replace('|', '\n');
                        m_state++;
                        break;

                    //Read the number of Voice Phrases
                    case 2:
                        m_numPhrases = Convert.ToInt32(line);
                        m_phrases = new LipSyncPhrase[m_numPhrases];
                        m_phonemes = new Dictionary<PhonemeType, List<LipSyncPhoneme>>();

                        for (int j = 0; j < m_numPhrases; j++)
                        {
                            m_phrases[j] = new LipSyncPhrase();
                            m_phrases[j].Load(file, ref m_phonemes);
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
            foreach (LipSyncPhrase phrase in m_phrases)
            {
                if (eventNum >= phrase.StartFrame &&
                    eventNum <= phrase.EndFrame)
                {
                    return phrase.GetEventPhoneme(eventNum);
                }
            }
            return new LipSyncPhoneme(null, null);
        }

        public List<LipSyncPhoneme> PhonemeList
        {
            get
            {
                List<LipSyncPhoneme> retVal = new List<LipSyncPhoneme>();
                retVal.Add(new LipSyncPhoneme(null, null));
                foreach (KeyValuePair<PhonemeType, List<LipSyncPhoneme>> phoneMe in m_phonemes)
                {
                    retVal.AddRange(phoneMe.Value);
                }
                return retVal;
            }
        }
    }
}
