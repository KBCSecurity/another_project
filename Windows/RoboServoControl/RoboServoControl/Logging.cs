using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RoboServoControl
{
    class Logging
    {
        //Pfade und Dateinamen
        private static string path = "C:\\wwwroot\\";
        private static string logfilename = "log.txt";

        //Zuweisung des Arrays
        private enum SERVOS { SERVO_SCHWENK, SERVO_UPDOWN, SERVO_FORWARD, SERVO_GREIF, SERVO_GREIFSCHWENK };
        //Aktueller Status der Servus
        private static int[] ACTUAL_STATUS = {0, 0 ,0, 0, 0};

        public static void writeLog(string name, string value)
        {
            StreamWriter sw = new StreamWriter(path + logfilename, true);
            sw.WriteLine(name + ":" + value);
            sw.Close();
            //Updaten des Statusarrays
            updateValues(name, value);
            //Ablegen der Koordinatenfiles (x, y, z)
            updateCoordinates();
        }

        private static void updateValues(string name, string value)
        {
            switch (name)
            {
            case "SERVO_SCHWENK":
                    ACTUAL_STATUS[(int)SERVOS.SERVO_SCHWENK] = int.Parse(value);
                    break;
            case "SERVO_UPDOWN":
                ACTUAL_STATUS[(int)SERVOS.SERVO_UPDOWN] = int.Parse(value);
                break;
            case "SERVO_FORWARD":
                ACTUAL_STATUS[(int)SERVOS.SERVO_FORWARD] = int.Parse(value);
                break;
            case "SERVO_GREIF":
                ACTUAL_STATUS[(int)SERVOS.SERVO_GREIF] = int.Parse(value);
                break;
            case "SERVO_GREIFSCHWENK":
                ACTUAL_STATUS[(int)SERVOS.SERVO_GREIFSCHWENK] = int.Parse(value);
                break;
            }
        }

        public static int getActualValue(string name)
        {
            switch (name)
            {
                case "SERVO_SCHWENK":
                    return ACTUAL_STATUS[(int)SERVOS.SERVO_SCHWENK];
                case "SERVO_UPDOWN":
                    return ACTUAL_STATUS[(int)SERVOS.SERVO_UPDOWN];
                case "SERVO_FORWARD":
                    return ACTUAL_STATUS[(int)SERVOS.SERVO_FORWARD];
                case "SERVO_GREIF":
                    return ACTUAL_STATUS[(int)SERVOS.SERVO_GREIF];
                case "SERVO_GREIFSCHWENK":
                    return ACTUAL_STATUS[(int)SERVOS.SERVO_GREIFSCHWENK];
                default:
                    return -1;
            }
        }
        
        private static void updateCoordinates()
        {
            StreamWriter sw = new StreamWriter(path + "\\x.txt");
            sw.WriteLine(ACTUAL_STATUS[(int)SERVOS.SERVO_SCHWENK]);
            sw.Close();
            sw = new StreamWriter(path + "\\y.txt");
            sw.WriteLine(ACTUAL_STATUS[(int)SERVOS.SERVO_UPDOWN]);
            sw.Close();
            sw = new StreamWriter(path + "\\z.txt");
            sw.WriteLine(ACTUAL_STATUS[(int)SERVOS.SERVO_FORWARD]);
            sw.Close();
        }


    }
}
