﻿using GalaSoft.MvvmLight.Messaging;
using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Xml.Serialization;
using vMixController.Classes;
using vMixController.PropertiesControls;

namespace vMixController.Widgets
{
    public class vMixControlMidiInterface : vMixControl
    {

        /// <summary>
        /// The <see cref="Midis" /> property's name.
        /// </summary>
        public const string MidisPropertyName = "Midis";

        private ObservableCollection<MidiInterfaceKey> _midis = new ObservableCollection<MidiInterfaceKey>();

        /// <summary>
        /// Sets and gets the Midis property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<MidiInterfaceKey> Midis
        {
            get
            {
                return _midis;
            }

            set
            {
                if (_midis == value)
                {
                    return;
                }

                _midis = value;
                RaisePropertyChanged(MidisPropertyName);
            }
        }

        //private static string[] _midiDevices = null;
        public static string[] MidiDevices
        {
            get
            {

                return Melanchall.DryWetMidi.Multimedia.InputDevice.GetAll().Select(x => x.Name).ToArray();
            }
        }

        [XmlIgnore]
        public Melanchall.DryWetMidi.Multimedia.InputDevice Device { get; set; }


        /// <summary>
        /// The <see cref="MaxMIDIValue" /> property's name.
        /// </summary>
        public const string MaxMIDIValuePropertyName = "MaxMIDIValue";

        private int _maxMIDIValue = 127;

        /// <summary>
        /// Sets and gets the MaxMIDIValue property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int MaxMIDIValue
        {
            get
            {
                return _maxMIDIValue;
            }

            set
            {
                if (_maxMIDIValue == value)
                {
                    return;
                }

                _maxMIDIValue = value;
                RaisePropertyChanged(MaxMIDIValuePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="DeviceCaps" /> property's name.
        /// </summary>
        public const string DeviceCapsPropertyName = "DeviceCaps";

        private string _deviceCaps = "";

        /// <summary>
        /// Sets and gets the DeviceCaps property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string DeviceCaps
        {
            get
            {
                return _deviceCaps;
            }

            set
            {
                if (_deviceCaps == value)
                {
                    return;
                }

                if (Device != null)
                {
                    //Device.ChannelMessageReceived -= Device_ChannelMessageReceived;
                    Device.EventReceived -= Device_EventReceived;
                    if (Device.IsListeningForEvents)
                        Device.StopEventsListening();
                    //Device.Reset();
                    Device.Dispose();
                    Device = null;
                }

                _deviceCaps = value;
                RaisePropertyChanged(DeviceCapsPropertyName);
            }
        }

        public override string Type
        {
            get
            {
                return "MIDI Device";
            }
        }

        string _midiDeviceName;
        public string MidiDeviceName
        {
            get { return _midiDeviceName; }
            set
            {
                if (_midiDeviceName == value) return;
                _midiDeviceName = value;
                if (Device != null)
                {
                    //Device.Close();
                    //Device.Reset();
                    Device.Dispose();
                    Device = null;
                }

                Device = CreateDeviceByName(_midiDeviceName);

                if (Device != null)
                {
                    //Device.Reset();
                    Device.EventReceived += Device_EventReceived;
                    Device.StartEventsListening();
                }
            }
        }

        private void Device_EventReceived(object sender, Melanchall.DryWetMidi.Multimedia.MidiEventReceivedEventArgs e)
        {

            Dispatcher.Invoke(() =>
            {
                foreach (var item in Midis)
                {
                    switch (e.Event.EventType)
                    {

                        case Melanchall.DryWetMidi.Core.MidiEventType.NoteOff:
                        case Melanchall.DryWetMidi.Core.MidiEventType.NoteOn:
                        case Melanchall.DryWetMidi.Core.MidiEventType.NoteAftertouch:
                            var note = (e.Event as Melanchall.DryWetMidi.Core.NoteEvent);
                            if (note.Channel == item.A && item.B == note.NoteNumber)
                                Messenger.Default.Send(new Pair<string, object>(item.C, (byte)note.Velocity));
                            break;
                        case Melanchall.DryWetMidi.Core.MidiEventType.ControlChange:
                            var cc = (e.Event as Melanchall.DryWetMidi.Core.ControlChangeEvent);
                            if (cc.Channel == item.A && item.B == cc.ControlNumber)
                                Messenger.Default.Send(new Pair<string, object>(item.C, (byte)cc.ControlValue));
                            break;
                        case Melanchall.DryWetMidi.Core.MidiEventType.ProgramChange:
                            var pc = (e.Event as Melanchall.DryWetMidi.Core.ProgramChangeEvent);
                            if (pc.Channel == item.A)
                                Messenger.Default.Send(new Pair<string, object>(item.C, (byte)pc.ProgramNumber));
                            break;
                        case Melanchall.DryWetMidi.Core.MidiEventType.PitchBend:
                            var pb = (e.Event as Melanchall.DryWetMidi.Core.PitchBendEvent);
                            if (pb.Channel == item.A)
                                Messenger.Default.Send(new Pair<string, object>(item.C, (byte)pb.PitchValue));
                            break;
                        default:
                            
                            break;
                    }
                }
            });

                /*if (e.Message.MidiChannel == item.A && e.Message.Data1 == item.B && e.Message.Command == item.D)
                    Messenger.Default.Send(new Pair<string, object>(item.C, e.Message.Data2));*/
        }

        private void Device_ChannelMessageReceived(object sender, ChannelMessageEventArgs e)
        {
            
            /*foreach (var item in Midis)
            {
                if (e.Message.MidiChannel == item.A && e.Message.Data1 == item.B && e.Message.Command == item.D)
                    Messenger.Default.Send(new Pair<string, object>(item.C, e.Message.Data2));
            }*/
        }

        private Melanchall.DryWetMidi.Multimedia.InputDevice CreateDeviceByName(string name)
        {
            try
            {
                var deviceNumber = MidiDevices.Select((obj, idx) => new { obj, idx }).Where(x => x.obj == name).FirstOrDefault();

                if (deviceNumber != null)
                    if (Device?.Name != name)
                    {
                        if (Device != null)
                        {
                            //Device.Reset();
                            Device.Dispose();
                        }

                        return Melanchall.DryWetMidi.Multimedia.InputDevice.GetByIndex(deviceNumber.idx);//new InputDevice(deviceNumber.idx);
                    }
                    else
                        return Device;
                return null;
            }
            catch (Exception)
            {
                return Device;
            }
        }

        public vMixControlMidiInterface()
        {

        }

        public override UserControl[] GetPropertiesControls()
        {

            if (Device != null)
            {
                //Device.Reset();
                Device.Dispose();
                Device = null;
            }

            Device = CreateDeviceByName(_midiDeviceName);
            if (Device != null)
            {
                //Device.Reset();
                if (!Device.IsListeningForEvents)
                    Device.StartEventsListening();//StartRecording();
                Device.EventReceived += Device_EventReceived;

            }

            var midiDeviceComboBox = GetPropertyControl<ComboBoxControl>();
            midiDeviceComboBox.Title = "Device";
            midiDeviceComboBox.Items = MidiDevices;
            midiDeviceComboBox.Tag = "DeviceSelector";
            var b = new Binding("DeviceCaps");
            b.Source = this;
            b.Mode = BindingMode.TwoWay;
            b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(midiDeviceComboBox, ComboBoxControl.ValueProperty, b);
            //midiDeviceComboBox.Value = Device != null ? InputDevice.GetDeviceCapabilities(Device.DeviceID).name : "";


            var midiMappingCtrl = GetPropertyControl<MidiMappingControl>();
            midiMappingCtrl.LearnFunction = Learn;


            midiMappingCtrl.Midis.Clear();
            foreach (var item in Midis)
            {
                midiMappingCtrl.Midis.Add(item);
            }

            /*var maxInt = GetPropertyControl<IntControl>("IntMidiMax");
            maxInt.Value = MaxMIDIValue;
            maxInt.Title = "Maximum MIDI Value (127/255)"; ;*/


            return base.GetPropertiesControls().Union(new UserControl[] { midiDeviceComboBox, midiMappingCtrl }).ToArray();
        }

        private MidiInterfaceKey Learn()
        {

            var wnd = new MidiLearnWindow(Device = CreateDeviceByName(DeviceCaps));
            var result = wnd.ShowDialog();
            if (result ?? true)
            {
                var k = wnd.Key;
                wnd.Close();
                return k;
            }

            return null;
            //throw new NotImplementedException();
        }

        public override void SetProperties(UserControl[] _controls)
        {
            MidiDeviceName = (string)(_controls.Where(x => (string)x.Tag == "DeviceSelector").First() as ComboBoxControl).Value;
            DeviceCaps = MidiDeviceName;
            var ctrl = _controls.OfType<MidiMappingControl>().First();
            Midis.Clear();
            foreach (var item in ctrl.Midis)
            {
                Midis.Add(item);
            }
            
            base.SetProperties(_controls);
        }

        protected override void Dispose(bool managed)
        {
            if (_disposed) return;

            if (managed)
            {
                if (Device != null)
                {
                    //Device.ChannelMessageReceived -= Device_ChannelMessageReceived;
                    Device.EventReceived -= Device_EventReceived;
                    if (Device.IsListeningForEvents)
                        Device.StopEventsListening();
                    //Device.Reset();
                    //Device.Close();
                    Device.Dispose();
                }
                base.Dispose(managed);
                GC.SuppressFinalize(this);
            }
        }
    }

    public class MidiInterfaceKey : Quadriple<int, int, string, /*Sanford.Multimedia.Midi.ChannelCommand*/Melanchall.DryWetMidi.Core.MidiEventType>
    {

    }
}
