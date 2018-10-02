/*
  Modbus-Arduino Example - Lamp (Modbus IP)
  Copyright by André Sarmento Barbosa
  http://github.com/andresarmento/modbus-arduino
*/
 
#include <SPI.h>
#include <Ethernet.h>
#include <Modbus.h>
#include <ModbusIP.h>
#include <Servo.h>

//Modbus Registers Offsets (0-9999)
const int LAMP1_COIL = 1;  
const int LAMP2_COIL = 2;  
const int LAMP3_COIL = 3;
// Modbus Registers Offsets (0-9999)
const int SERVO_HREG = 5; 
//Used Pins
const int ledPin = 2;
const int ledPin2 = 3;
const int ledPin3 = 4;
const int servoPin = A0;
  
//ModbusIP object
ModbusIP mb;
// Servo object
Servo servo; 

void setup() {
    // The media access control (ethernet hardware) address for the shield
    byte mac[] = { 0xDE, 0xAD, 0xBE, 0xEF, 0xFE, 0xED };  
    // The IP address for the shield
    byte ip[] = { 192, 168, 1, 100 };   
    //Config Modbus IP 
    mb.config(mac, ip);
    //Set ledPin mode
    pinMode(ledPin, OUTPUT);
    pinMode(ledPin2, OUTPUT);
    pinMode(ledPin3, OUTPUT);
    // Add LAMP1_COIL register - Use addCoil() for digital outputs
    mb.addCoil(LAMP1_COIL);
    mb.addCoil(LAMP2_COIL);
    mb.addCoil(LAMP3_COIL);
    // Attaches the servo pin to the servo object
    servo.attach(servoPin); 
    // Add SERVO_HREG register - Use addHreg() for analog outpus or to store values in device 
    mb.addHreg(SERVO_HREG, 127);
}

void loop() {
   //Call once inside loop() - all magic here
   mb.task();
   
   //Attach ledPin to LAMP1_COIL register     
   digitalWrite(ledPin, mb.Coil(LAMP1_COIL));
   //Attach ledPin to LAMP2_COIL register     
   digitalWrite(ledPin2, mb.Coil(LAMP2_COIL));
   //Attach ledPin to LAMP3_COIL register     
   digitalWrite(ledPin3, mb.Coil(LAMP3_COIL));

    //Attach switchPin to SWITCH_ISTS register     
   servo.write(mb.Hreg(SERVO_HREG));
}
