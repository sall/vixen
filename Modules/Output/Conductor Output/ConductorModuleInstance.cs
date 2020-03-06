using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Vixen;
using Vixen.Execution;
using Vixen.Module.Controller;
using Vixen.Commands;
using Vixen.Sys;


namespace VixenModules.Output.ConductorOutput
{

    //  Authored by Charles Strang on 10-29-2013
    //  Additional contributions by Tony Eberle
    //  code can be used and distributed freely
    //  Please give the coders their proper acknowledgement.

	class ConductorModuleInstance : ControllerModuleInstanceBase
	{
        public ConductorModuleData _myconductordata;

        private bool _sequenceStarted = false;

		byte[] bytes = new byte[16384];
		int[] mybuffer = new int[16384];

        int intervalcount = 0;
//        int nextheal = 0;
        long curmill,modmill;
        long selfheal = 0, moddelay = 50;

        string mypath, myfilename;

        string[] intervaldata = new string[32768];

		Stopwatch sw = new Stopwatch();

		BinaryWriter bw;

		public ConductorModuleInstance()
		{
			DataPolicyFactory = new ConductorOutputDataPolicyFactory();
			
			VixenSystem.Contexts.ContextCreated += Contexts_ContextCreated;
			VixenSystem.Contexts.ContextReleased += Contexts_ContextReleased;


		}
		
		void Contexts_ContextCreated(object sender, ContextEventArgs e)
		{
			IContext sequenceContext = e.Context as IContext;

			if (sequenceContext != null)
			{
				sequenceContext.ContextStarted += sequenceContext_ContextStarted;
				sequenceContext.ContextEnded += sequenceContext_ContextEnded;

			}
		}


		void Contexts_ContextReleased(object sender, ContextEventArgs e)
		{

			IContext sequenceContext = e.Context as IContext;
			if (sequenceContext != null)
			{
				sequenceContext.ContextStarted -= sequenceContext_ContextStarted;
				sequenceContext.ContextEnded -= sequenceContext_ContextEnded;

			}

		}

		void sequenceContext_ContextEnded(object sender, EventArgs e)
		{
			_sequenceStarted = false;
			
///// close files (possibly write all data from memory array)

			//Close file and stop timer
			sw.Stop();
            if (_myconductordata.savedata)
            {
				bw.Close();
				if (_myconductordata.OutputDebug)
				{
					if (File.Exists(string.Format("{0}{1}.debug.txt", mypath, myfilename)))
					{
						File.Move(string.Format("{0}{1}.debug.txt", mypath, myfilename), string.Format("{0}{1}_{2:MMdd-hhmmss}.debug.txt", mypath, myfilename, DateTime.Now));
					}

					System.IO.File.WriteAllLines(string.Format("{0}{1}.debug.txt", mypath, myfilename), intervaldata); // write debug array
				}
            }

		}

		void sequenceContext_ContextStarted(object sender, EventArgs e)
		{

    		Vixen.Execution.Context.ISequenceContext sequenceContext = (Vixen.Execution.Context.ISequenceContext)sender;

			//start timer and open output file

			sw.Start();
			
			// need to make a form to allow the user to choose location and name of file.  maybe make the default
			//  to the sequence directory and name.
			
			//string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\vixen";
            if (_myconductordata.savedata)
            {
                
                mypath = sequenceContext.Sequence.FilePath.Substring(0, sequenceContext.Sequence.FilePath.LastIndexOf("\\")) +"\\";
                myfilename = sequenceContext.Sequence.Name;

                if (File.Exists(string.Format("{0}{1}.seq", mypath, myfilename)))
                {
                    File.Move(string.Format("{0}{1}.seq", mypath, myfilename), string.Format("{0}{1}_{2:MMdd-hhmmss}.seq", mypath, myfilename, DateTime.Now));
                }

                bw = new BinaryWriter(new FileStream(string.Format("{0}{1}.seq", mypath, myfilename), FileMode.Create));
                intervalcount = 0;
				selfheal = 0;
            }
			_sequenceStarted = true;

		}


		public override void UpdateState(int chainIndex, Vixen.Commands.ICommand[] outputStates)
		{

            
			if (_sequenceStarted & sw.ElapsedMilliseconds >=25 & _myconductordata.savedata)
			{
                modmill = sw.ElapsedMilliseconds;

                if (modmill >= 50)
                {
                    selfheal = selfheal + (50 - modmill);
                    moddelay = 50;
                }
                else if (selfheal < 0)
                {
                    if ((modmill - 50) < selfheal)
                    {
                        moddelay = 50 + selfheal;
                        selfheal = 0;
                    }
                    else
                    {
                        moddelay = 50 + (modmill - 50);
                        selfheal = selfheal - (modmill - 50);
                    }
                }
                else
                {
                    moddelay = 50;
                }


                while ((sw.ElapsedMilliseconds <  moddelay) & (modmill < 50))
                {
                         
                }

                curmill = sw.ElapsedMilliseconds;
                if ((curmill > 50) & (modmill <= 50))
                {
                    selfheal = selfheal + (50 - curmill);
                }

                sw.Reset();
				sw.Start();

				// zero out array (there has to be a better way of doing this)

				for (int i = 0; i < 16384; i++)
				{
					mybuffer[i] = 0;
				}

				int ii = 0;

				// get the channel intensities from the system and put them in the mybuffer array
				//  we do not put them directly into the bytes array, since we have to do some post
				//  processing to get them in the correct order for the Conductor file

				foreach (ICommand command in outputStates)
				{
					if (command == null)
					{
					}
					else if (command is _8BitCommand)
					{
						mybuffer[ii] = (command as _8BitCommand).CommandValue;
					}
                        
					ii++;
				}
				
				// put the buffered data into the correct sequence and in the right type (type of byte) 
				// so we can write the binary file.
				
				for(int i=0; i < 4096 ;i++)
     			{
					bytes[(i*4)] = Convert.ToByte(mybuffer[i]);
					bytes[(i*4) + 1] = Convert.ToByte(mybuffer[i + 4096]);
					bytes[(i*4) + 2] = Convert.ToByte(mybuffer[i + 8192]);
					bytes[(i*4) + 3] = Convert.ToByte(mybuffer[i + 12288]);
				}
				
				// write the 16384 bytes to the binary file.  The entire song could be kept in the array 
				// until the sequence is done and then write the whole array at once.  As it stands, the
				// write finishes in less than a millisecond, so speed is not of the essence, but should be
				// considered, since older computers may not be able to keep up.
				
				// each second of sequence will consume 327680 bytes or 320KB.  Therefore a minute will consume
				// 19660800 bytes or 18.75MB of memory per minute of sequence.  The average song is less than 5 
				// minutes, so a 5 minute sequence will consume 93.75MB of memory.  This is within the 
				// constraints of modern computers.
				
                if (curmill == 50)
                {
                    intervaldata[intervalcount] = intervalcount.ToString() + " , " + modmill.ToString() + " , " + curmill.ToString() + " , " + selfheal.ToString() + " , " + moddelay.ToString();
                }
                else
                {
                    intervaldata[intervalcount] = intervalcount.ToString() + " , " + modmill.ToString() + " , " + curmill.ToString() + " , " + selfheal.ToString() + " , " + moddelay.ToString() + "<---*-*-*-*-*-*-*-*-*-*-*-*-*";   
                }

                intervalcount += 1;


				bw.Write(bytes);
			}
		}

		public override bool HasSetup
		{
			get { return true; }
		}

		public override bool Setup()
		{

            ConductorSetupForm dialog = new ConductorSetupForm(_myconductordata);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this._myconductordata.savedata = dialog.SaveData;
				this._myconductordata.OutputDebug = dialog.OutputDebug;
			}
            return true;

		}

        public override Vixen.Module.IModuleDataModel ModuleData
        {
            get
            {
                return _myconductordata;
            }
            set
            {
                _myconductordata = value as ConductorModuleData;
            }
        }

		public override void Start()
		{
			base.Start();

		}

		public override void Stop()
		{
			base.Stop();

		}
	}
}
