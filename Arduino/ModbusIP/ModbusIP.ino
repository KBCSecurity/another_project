/*
  Arduino ModBus Steuerung
  Copyright by André Sarmento Barbosa & Kapsch
  http://github.com/andresarmento/modbus-arduino
*/
 
#include <SPI.h>
#include <Ethernet.h>
#include <Modbus.h>
#include <ModbusIP.h>
#include <Servo.h>
#include <Wire.h>
#include <Adafruit_PWMServoDriver.h>

// Modbus Registers Offsets (0-9999)
const int SERVO_SCHWENK = 10;
const int SERVO_UPDOWN = 11; 
const int SERVO_FORWARD = 12; 
const int SERVO_GREIF = 13; 
const int SERVO_GREIFSCHWENK = 14;
  
//ModbusIP object
ModbusIP mb;
// Servo Shield
Adafruit_PWMServoDriver pwm = Adafruit_PWMServoDriver();

void setup() {
    // The media access control (ethernet hardware) address for the shield
    byte mac[] = { 0xDE, 0xAD, 0xBE, 0xEF, 0xFE, 0xED };  
    // The IP address for the shield
    byte ip[] = { 10, 99, 13, 37 };   
    //Config Modbus IP 
    mb.config(mac, ip);
    
    // Add SERVO_HREG register - Use addHreg() for analog outpus or to store values in device 
    mb.addHreg(SERVO_SCHWENK, 2048);
    mb.addHreg(SERVO_UPDOWN, 2048);
    mb.addHreg(SERVO_FORWARD, 2048);
    mb.addHreg(SERVO_GREIF, 2048);
    mb.addHreg(SERVO_GREIFSCHWENK, 2048);

    pwm.begin();
    pwm.setPWMFreq(60);  // Analog servos run at ~60 Hz updates
}

void loop() {
   //Call once inside loop() - all magic here
   mb.task();
   
   //Register the servos (degree, minwinkel, maxwinkel, minservero, maxservo)
   pwm.setPWM(0, 0, map(mb.Hreg(SERVO_UPDOWN), 0, 90, 200, 520));
   pwm.setPWM(1, 0, map(mb.Hreg(SERVO_FORWARD), 0, 120, 210, 440));
   pwm.setPWM(2, 0, map(mb.Hreg(SERVO_SCHWENK), 0, 180, 130, 560));
   pwm.setPWM(3, 0, map(mb.Hreg(SERVO_GREIFSCHWENK), 0, 180, 90, 450));
   pwm.setPWM(4, 0, map(mb.Hreg(SERVO_GREIF), 0, 100, 40, 400));   
}
