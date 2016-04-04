using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalBeaconLibrary.Beacon;
using Windows.Devices.Bluetooth.Advertisement;
namespace RMeX3
{
    public class Scan
    {
        private  BluetoothLEAdvertisementWatcher _watcher;
        private  BeaconManager _beaconManager;

        public void scan_beacons(List<Beacon> beacons)
        {

            _beaconManager = new BeaconManager();

            // Create & start the Bluetooth LE watcher from the Windows 10 UWP
            _watcher = new BluetoothLEAdvertisementWatcher { ScanningMode = BluetoothLEScanningMode.Active };
            _watcher.Received += WatcherOnReceived;
            _watcher.Start();
            string macAddress = ""; string signal = "";
            foreach (var bluetoothBeacon in _beaconManager.BluetoothBeacons.ToList())
            {
                //  Debug.WriteLine("================================  START OF LOOP ============================");
                //[00128] -->  78:A5:04:5B:35:EA 
                //[00181] --> 78:A5:04:5B:21:3C
                //[0057] --> 78:A5:04:5B:2E:80   
                //[0047] ---> 78:A5:04:5B:35:90  
                Debug.WriteLine("Beacons discovered so far\n-------------------------" + _beaconManager.BluetoothBeacons.Count);
                try
                {
                    if (bluetoothBeacon == null) Debug.WriteLine("================== NULL ================== ");
                }
                catch (NullReferenceException e)
                {

                    break;
                }
                Debug.WriteLine("\nBeacon: " + bluetoothBeacon.BluetoothAddressAsString);
                Debug.WriteLine("Type: " + bluetoothBeacon.BeaconType);
                Debug.WriteLine("Last Update: " + bluetoothBeacon.Timestamp);
                Debug.WriteLine("RSSI: " + bluetoothBeacon.Rssi);
                if (_beaconManager.BluetoothBeacons.Count != 0)
                {
                    macAddress = bluetoothBeacon.BluetoothAddressAsString;
                    signal = bluetoothBeacon.Rssi.ToString();

                }
                Debug.WriteLine("===================");

                // **** Step 2: Begin Converting RSSI to Distance(metres)   ****    //
                double distance;
                AccuracyCalculator cfdc = new CurveFittedDistanceCalculator(0.42093, 6.9476, 0.54992);
                distance = Math.Round(cfdc.calculateAccuracy(-58, bluetoothBeacon.Rssi), 2);
                // **** Step 2: End Converting RSSI to Distance(metres)   ****    //


                for (int i = 0; i < beacons.Count; i++)
                {

                    if (beacons[i].id.Equals(macAddress))
                    {

                        beacons[i].distances[beacons[i].mean_counter] = distance;
                        beacons[i].mean_counter++;

                    }

                }

            }

        }

     

        private async void WatcherOnReceived(BluetoothLEAdvertisementWatcher sender, BluetoothLEAdvertisementReceivedEventArgs eventArgs)
        {
            // await GetBluetoothIsEnabledAsync();


            // Let the library manager handle the advertisement to analyse & store the advertisement
            _beaconManager.ReceivedAdvertisement(eventArgs);
        }

    }
}
