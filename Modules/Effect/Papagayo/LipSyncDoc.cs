using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Vixen;

namespace VixenModules.Effect.LipSync
{
    public enum PhonemeType { AI, E, O, U, FV, L, MBP, WQ, etc, Rest };

    public class LipSyncDoc
    {
        int m_state = 0;
        string m_soundPath;
        float m_fps;
        int m_duration;
        int m_numVoices = 0;
        Dictionary<string, LipSyncVoice> m_voices = null;
        XmlNode m_fileNameNode = null;
        XmlNode m_settingsNode = null;
        string m_fileNameStr = null;
        Dictionary<string, Dictionary<string, List<Int32>>> m_channelMap = null;
        bool m_isValid;

        public LipSyncDoc()
        {
            m_isValid = false;
            m_voices = new Dictionary<string, LipSyncVoice>();
        }


#if false
        private bool LoadXmlData()
        {
            bool retVal = true;
            int chIndex;
            string voiceStr = null;
            string phonemeStr = null;
            XmlNodeList voiceNodes = null;
            XmlNodeList phonemeNodes = null;
            XmlNodeList chNodes = null;
            XmlAttributeCollection attributes = null;

            try
            {
                XmlNode chMap = this.SettingsNode.SelectSingleNode("Settings/Channel_Map");
                if (chMap.HasChildNodes == true)
                {
                    voiceNodes = chMap.ChildNodes;
                    foreach (XmlNode voiceNode in voiceNodes)
                    {
                        attributes = voiceNode.Attributes;

                        if (attributes == null)
                        {
                            continue;
                        }

                        voiceStr = attributes["Name"].InnerText.Trim();

                        m_channelMap[voiceStr] = new Dictionary<string, List<int>>();

                        phonemeNodes = voiceNode.ChildNodes;
                        foreach (XmlNode phonemeNode in phonemeNodes)
                        {
                            attributes = phonemeNode.Attributes;
                            phonemeStr = attributes["Name"].InnerText;
                            m_channelMap[voiceStr][phonemeStr] = new List<int>();

                            chNodes = phonemeNode.ChildNodes;
                            foreach (XmlNode chNode in chNodes)
                            {
                                chIndex = Convert.ToInt32(chNode.InnerText);
                                m_channelMap[voiceStr][phonemeStr].Add(chIndex);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                m_channelMap.Clear();
                retVal = false;
            }
            return retVal;
        }
#endif
        public List<string> VoiceList
        {
            get
            {
                List<string> retval = new List<string>();
                foreach (KeyValuePair<string, LipSyncVoice> voice in m_voices)
                {
                    retval.Add(voice.Value.VoiceName);
                }

                return retval;
            }
        }



        public bool IsValid
        {
            get
            {
                return m_isValid;
            }
            set
            {
                m_isValid = value;
            }
        }

        public int SamplePeriod
        {
            get
            {
                return (m_isValid == true) ? (int)((1 / m_fps) * 1000) : -1;
            }
        }

        public int DurationMS
        {
            get
            {
                return (m_isValid == true) ? ((m_duration - 1) * 1000 / (int)m_fps) : -1;
            }
        }

        public void Clear()
        {
            m_state = 0;
            m_numVoices = 0;
            m_voices = null;

            m_isValid = false;

        }

        public void Load(string fileName)
        {
            string line;
            LipSyncVoice voice = null;

            m_isValid = false;
            m_state = 0;
            m_soundPath = null;
            m_fps = 0;
            m_duration = 0;
            m_numVoices = 0;

            m_voices.Clear();


            StreamReader file = new StreamReader(fileName);
            while ((line = file.ReadLine()) != null)
            {
                //Trim leading whitespace on the read string. 
                line = line.TrimStart(null);

                switch (m_state)
                {
                    //Papagayo File Header
                    case 0:
                        if ((line.Contains("lipsync") == false))
                        {
                            throw new IOException("Invalid File Format");
                        }
                        else
                        {
                            m_state++;
                        }
                        break;

                    //Read the sound path
                    case 1:
                        m_soundPath = line;
                        m_state++;
                        break;

                    //Decode FPS
                    case 2:
                        m_fps = Convert.ToInt32(line);
                        m_state++;
                        break;

                    //Read the sound duration
                    case 3:
                        m_duration = Convert.ToInt32(line);
                        m_state++;
                        break;

                    //Read the number of voices
                    case 4:
                        m_numVoices = Convert.ToInt32(line);
                        for (int j = 0; j < m_numVoices; j++)
                        {
                            voice = new LipSyncVoice();
                            voice.Load(file);
                            m_voices.Add(voice.VoiceName.Trim(), voice);
                        }

                        m_state++;
                        break;

                    default:
                        throw new IOException();
                }
            }

            m_fileNameStr = fileName;
            file.Close();
            m_isValid = true;
        }

        public LipSyncPhoneme GetEventPhoneme(string voiceStr, int eventNum)
        {
            LipSyncPhoneme retVal = null;
            try
            {
                LipSyncVoice voice = m_voices[voiceStr.Trim()];
                if (voice != null)
                {
                    retVal = voice.GetEventPhoneme(eventNum);
                }
            }
            catch (KeyNotFoundException) { }

            return retVal;
        }

        public List<LipSyncPhoneme> PhonemeList(string voiceStr)
        {
            List<LipSyncPhoneme> retVal = null;
            try
            {
                LipSyncVoice voice = m_voices[voiceStr.Trim()];
                if (voice != null)
                {
                    retVal = voice.PhonemeList;
                }
            }
            catch (KeyNotFoundException) { }

            return retVal;

        }
    }
}
